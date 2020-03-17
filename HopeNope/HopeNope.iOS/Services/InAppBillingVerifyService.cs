using HopeNope.iOS.Services;
using Plugin.InAppBilling.Abstractions;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(InAppBillingVerifyService))]
namespace HopeNope.iOS.Services
{
    public class InAppBillingVerifyService : IInAppBillingVerifyPurchase
    {
        const string key1 = @"XOR_key1";
        const string key2 = @"XOR_key2";
        const string key3 = @"XOR_key3";

        public Task<bool> VerifyPurchase(string signedData, string signature, string productId = null, string transactionId = null)
        {
            return Task.FromResult(true);
        }
    }
}