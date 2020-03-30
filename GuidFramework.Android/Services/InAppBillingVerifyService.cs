using GuidFramework.Droid.Services;
using Plugin.InAppBilling.Abstractions;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(InAppBillingVerifyService))]
namespace GuidFramework.Droid.Services
{
    /// <summary>
    /// InAppBillingVerifyService
    /// </summary>
    /// <seealso cref="Plugin.InAppBilling.Abstractions.IInAppBillingVerifyPurchase" />
    public class InAppBillingVerifyService : IInAppBillingVerifyPurchase
    {
        const string key1 = @"XOR_key1";
        const string key2 = @"XOR_key2";
        const string key3 = @"XOR_key3";

        /// <summary>
        /// Verifies the purchase.
        /// </summary>
        /// <param name="signedData">The signed data.</param>
        /// <param name="signature">The signature.</param>
        /// <param name="productId">The product identifier.</param>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <returns></returns>
        public Task<bool> VerifyPurchase(string signedData, string signature, string productId = null, string transactionId = null)
        {
            var key1Transform = Plugin.InAppBilling.InAppBillingImplementation.InAppBillingSecurity.TransformString(key1, 1);
            var key2Transform = Plugin.InAppBilling.InAppBillingImplementation.InAppBillingSecurity.TransformString(key2, 2);
            var key3Transform = Plugin.InAppBilling.InAppBillingImplementation.InAppBillingSecurity.TransformString(key3, 3);

            return Task.FromResult(Plugin.InAppBilling.InAppBillingImplementation.InAppBillingSecurity.VerifyPurchase(key1Transform + key2Transform + key3Transform, signedData, signature));
        }
    }
}