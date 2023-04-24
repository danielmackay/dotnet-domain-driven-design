namespace DDD.Domain.Common.Extensions;

internal static class StringExtensions
{
    public static bool IsEmpty(this string str) => string.IsNullOrEmpty(str);
}
