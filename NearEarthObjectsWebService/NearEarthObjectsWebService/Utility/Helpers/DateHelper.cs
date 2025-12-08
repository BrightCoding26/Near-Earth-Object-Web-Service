namespace NearEarthObjectsWebService.Utility.Helpers;

public static class DateHelper
{
    public static bool IsDateRangeExceeded(DateTime dateTime1, DateTime dateTime2)
    {
        const int DateRangeInDays = 7;
        double daysDifference = Math.Abs((dateTime1.Date - dateTime2.Date).TotalDays);

        return daysDifference > DateRangeInDays;
    }

    public static (DateTime StartOfWeek, DateTime EndOfWeek) FindWeekDates(DateTime inputDate)
    {
        DateTime startOfWeek = inputDate.AddDays(-(int)inputDate.DayOfWeek);
        DateTime endOfWeek = startOfWeek.AddDays(6);

        return (startOfWeek, endOfWeek);
    }
}
