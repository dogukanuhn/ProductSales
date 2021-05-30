namespace ProductSales.Infrastructure.Config
{
    public class IyzicoSettings
    {
        public string ApiKey;
        public string SecretKey;
        public string BaseUrl;


        //Configuration için kullanılacak
        #region Const Values

        public const string ApiKeyValue = nameof(ApiKey);
        public const string SecretKeyValue = nameof(SecretKey);
        public const string BaseUrlValue = nameof(BaseUrl);
        #endregion
    }
}
