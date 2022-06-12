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
}