namespace EventCachingDemo.Shared.Helpers;

public static class DateTimeExtensions
{
    public static DateTime GetFirstMondayOfYear(this DateTime dateTime)
    {
        var dt = new DateTime(dateTime.Year, 1, 1);

        while (dt.DayOfWeek != DayOfWeek.Monday)
        {
            dt = dt.AddDays(1);
        }

        return dt;
    }
    
    public static int GetFirstMondayOfYear(this DateTimeOffset dateTime)
    {
        var dt = new DateTime(dateTime.Year, 1, 1);

        while (dt.DayOfWeek != DayOfWeek.Monday)
        {
            dt = dt.AddDays(1);
        }

        return dt.DayOfYear;
    }

    public static int GetWeek(this DateTimeOffset dateTime)
    {
        var firstMondayOfYear = dateTime.DateTime.GetFirstMondayOfYear().DayOfYear;
        var week = (int)Math.Floor((double)(dateTime.DayOfYear - firstMondayOfYear) / 7) + 2;
        return week;
    }
}