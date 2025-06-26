namespace Utils.Helpers.Conversion
{
    public static class DateTimeConversionHelper
    {
        public static int ConvertToIntFromDateTime(DateTime dateTime)
        {
            return dateTime.Year * 10000 + dateTime.Month * 100 + dateTime.Day;
        }

    }
}
