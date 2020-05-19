using System;
using System.Threading.Tasks;

namespace GuidFramework.Interfaces
{
    /// <summary>
    /// ValidationHandler interface
    /// </summary>
    public interface IValidationHandler
    {
        /// <summary>
        /// Validates the given validatable object in the given validationIndex.
        /// </summary>
        /// <param name="validatable">The object to validate.</param>
        /// <param name="validationIndex">[Optional] ValidationIndex to validate groups</param>
        /// <returns>A boolean value</returns>
        /// <exception cref="InvalidOperationException">No validatable properties found on type.</exception>
        Task<bool> ValidateAsync(IValidatable validatable, int? validationIndex = null);

        /// <summary>
        /// Validates the given validatable object in the given validationIndex.
        /// </summary>
        /// <typeparam name="T">The type of enum</typeparam>
        /// <param name="validatable">The validatable view model.</param>
        /// <param name="validationEnum">The validation enum.</param>
        /// <returns>True when valid</returns>
        Task<bool> ValidateAsync<T>(IValidatable validatable, T validationEnum)
            where T : struct, IConvertible;

        /// <summary>
        /// Clears the validationerrors
        /// </summary>
        /// <param name="validatableViewModel">The viewmodel to clear the errors for</param>
        void Clear(IValidatableViewModel validatableViewModel);
    }
}
