using Common.Application.Exceptions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Payments.Application.Dto;
using Payments.Application.Interfaces;
using Payments.Application.Ports;
using Payments.Domain.Entities;

namespace Payments.Application.Services;

public class PaymentService : IPaymentService
{
    private readonly IApplicationContext _context;
    private readonly ITicketGrpcClient _ticketGrpcClient;

    public PaymentService(IApplicationContext context, ITicketGrpcClient ticketGrpcClient)
    {
        _context = context;
        _ticketGrpcClient = ticketGrpcClient;
    }

    public async ValueTask<PaymentDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Payments.SingleOrDefaultAsync(item => item.Id == id,
                         cancellationToken: cancellationToken) ??
                     throw new EntityNotFoundException("Платёж не найден.");
        var dto = entity.Adapt<PaymentDto>();

        return dto;
    }

    public async ValueTask<IReadOnlyCollection<PaymentDto>> GetByTicketTypeAsync(Guid ticketTypeId,
        CancellationToken cancellationToken = default) => (await _context.Payments
            .Where(entity => entity.TicketTypeId == ticketTypeId)
            .ToArrayAsync(cancellationToken: cancellationToken))
        .Select(entity => entity.Adapt<PaymentDto>())
        .ToArray();

    public async ValueTask<IReadOnlyCollection<PaymentDto>> GetByUserAsync(Guid userId,
        CancellationToken cancellationToken = default) => (await _context.Payments
            .Where(entity => entity.UserId == userId)
            .ToArrayAsync(cancellationToken: cancellationToken))
        .Select(entity => entity.Adapt<PaymentDto>())
        .ToArray();

    public async ValueTask<bool> BuyAsync(Guid ticketTypeId, Guid userId, CancellationToken cancellationToken = default)
    {
        var ticketType = await _ticketGrpcClient.GetTicketTypeAsync(ticketTypeId, cancellationToken: cancellationToken);

        if (!(DateTime.UtcNow >= ticketType.SalesStartDate && DateTime.UtcNow <= ticketType.SalesEndDate))
            return false;

        var isSuccessfullyBooked = await _ticketGrpcClient.BookAsync(ticketTypeId, cancellationToken);

        if (isSuccessfullyBooked)
        {
            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                TicketTypeId = ticketTypeId,
                UserId = userId,
                Price = ticketType.Price,
                PurchaseStatus = PurchaseStatus.Buyed,
                ChangeDate = DateTime.UtcNow
            };

            await _context.Payments.AddAsync(payment, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return isSuccessfullyBooked;
    }
}
