using HopeNope.iOS.Services;
using Plugin.InAppBilling.Abstractions;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(InAppBillingVerifyService))]
namespace HopeNope.iOS.Services
{
    /// <summary>
    /// InAppBillingVerifyService
    /// </summary>
    /// <seealso cref="Plugin.InAppBilling.Abstractions.IInAppBillingVerifyPurchase" />
    public class InAppBillingVerifyService : IInAppBillingVerifyPurchase
    {
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
            return Task.FromResult(true);
        }
    }
}