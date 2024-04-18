using Microsoft.AspNetCore.Mvc;
using Payments.Api.Models.Requests;
using Payments.Application.Dto;
using Payments.Application.Interfaces;

namespace Payments.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController
{
    private readonly IPaymentService _paymentService;

    public PaymentsController(IPaymentService paymentService) => _paymentService = paymentService;

    [HttpGet("{id:guid}")]
    public async Task<PaymentDto> GetPayment(Guid id) => await _paymentService.GetByIdAsync(id);

    [HttpGet("byTicketType/{ticketTypeId:guid}")]
    public async Task<IReadOnlyCollection<PaymentDto>> GetTicketTypePayments(Guid ticketTypeId) =>
        await _paymentService.GetByTicketTypeAsync(ticketTypeId);

    [HttpGet("byUser/{userId:guid}")]
    public async Task<IReadOnlyCollection<PaymentDto>> GetUserPayments(Guid userId) =>
        await _paymentService.GetByUserAsync(userId);

    [HttpPost("buy")]
    public async Task<bool> BuyAsync(BuyModel model) =>
        await _paymentService.BuyAsync(model.TicketTypeId, model.UserId);
}
