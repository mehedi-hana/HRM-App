namespace HanaHRMApi.Providers
{
    public class ClientProvider : IClientProvider
    {
        private const int DefaultClientId = 10001001;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClientProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int ClientId
        {
            get
            {
                var context = _httpContextAccessor.HttpContext;

                if (context == null)
                    //throw new Exception("HttpContext not available");
                    return DefaultClientId;

                if (!context.Request.Headers.TryGetValue("X-Client-Id", out var value))
                    //throw new Exception("X-Client-Id header missing");
                    return DefaultClientId;

                if (!int.TryParse(value, out var clientId))
                    //throw new Exception("Invalid ClientId");
                    return DefaultClientId;

                return clientId;
            }
        }
    }
}
