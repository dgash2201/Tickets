using System.ComponentModel;
using System.Reflection;

namespace Common.Application.Extensions;

public static class EnumExtensions
{
    public static IEnumerable<T> GetValues<T>()
    {
        return Enum.GetValues(typeof(T)).Cast<T>();
    }

    public static string GetDescription(this Enum member)
    {
        if (!member.GetType().GetTypeInfo().IsEnum)
            throw new ArgumentOutOfRangeException(nameof(member), "Переданное значение не enum");

        var fieldInfo = member.GetType().GetTypeInfo().GetField(member.ToString());
        if (fieldInfo is null)
            return string.Empty;

        var attributes = fieldInfo.GetCustomAttributes<DescriptionAttribute>(false).ToArray();

        return attributes.Length > 0 ? attributes[0].Description : member.ToString();
    }
}