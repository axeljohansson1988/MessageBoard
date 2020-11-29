namespace MessageBoard.API.Extensions
{
    public static class IntExtension
    {
        public static bool IsValidId(this int? id)
        {
            return id != null && id > 0;
        }
    }
}
