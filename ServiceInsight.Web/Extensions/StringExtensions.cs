namespace ServiceInsight.Web.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// https://stackoverflow.com/a/4405876
    /// </summary>
    public static string FirstCharToUpper(this string input) =>
        input switch
        {
            null => throw new ArgumentNullException(nameof(input)),
            "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
            _ => string.Concat(input[0].ToString().ToUpper(), input.AsSpan(1))
        };
}