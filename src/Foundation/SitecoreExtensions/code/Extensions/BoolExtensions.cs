namespace Foundation.SitecoreExtensions.Extensions
{
    public static class BoolExtensions
    {
        public static string ToSCBoolean(this bool value) => value ? "1" : "0";
    }
}