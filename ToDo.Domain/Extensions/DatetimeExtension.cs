namespace ToDo.Domain.Extensions
{
    internal static class DatetimeExtension
    {
        internal static string ToSQLDate(this DateTime dateTime) => dateTime.ToString("yyyy-MM-dd HH:mm:ss");
    }
}
