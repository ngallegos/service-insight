using System.Text;

namespace ServiceInsight.Web.Extensions;

public static class TimeSpanExtensions
{
    public static string Pretty(this TimeSpan span) {

        if (span == TimeSpan.Zero) return "0 minutes";

        var sb = new StringBuilder();
        if (Math.Floor(span.TotalMinutes) > 0)
        {
            if (span.Days > 0)
                sb.AppendFormat("{0} day{1} ", span.Days, span.Days > 1 ? "s" : String.Empty);
            if (span.Hours > 0)
                sb.AppendFormat("{0} hour{1} ", span.Hours, span.Hours > 1 ? "s" : String.Empty);
            if (span.Minutes > 0)
                sb.AppendFormat("{0} minute{1} ", span.Minutes, span.Minutes > 1 ? "s" : String.Empty);
        }
        else
        {
            if (span.Seconds > 0)
                sb.AppendFormat("{0} second{1} ", span.Seconds, span.Seconds > 1 ? "s" : String.Empty);
            else
                sb.AppendFormat("{0} millisecond{1}", span.Milliseconds, span.Milliseconds > 1 ? "s" : string.Empty);
        }

        return sb.ToString();

    }
}