namespace ProductCatalogWeb
{
    public class StaticData
    {
        public static string ProductCatalogAPIBase { get; set; }
        public const string RoleAdmin = "admin";
        public const string RoleManager = "manager";
        public const string RoleUser = "user";
        public const string TokenCookie = "JWTToken";
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
