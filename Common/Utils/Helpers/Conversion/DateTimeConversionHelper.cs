namespace Utils.Helpers.Conversion
{
    public static class DateTimeConversionHelper
    {
        public static int ConvertToIntFromDateTime(DateTime dateTime)
        {
            return Convert.ToInt32((dateTime).ToOADate());
        }

    }
}
