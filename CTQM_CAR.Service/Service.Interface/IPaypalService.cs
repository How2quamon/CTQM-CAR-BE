
using PayPal.Api;
using System;

namespace CTQM_CAR.Service.Service.Interface
{
    public interface IPaypalService
    {
        APIContext GetAPIContext(string clientId, string clientSecret, string mode);
        Dictionary<string, string> GetConfig(string _mode);
        string GetAccessToken(string _ClientId, string _ClientSecret, string _mode);
    }
}
