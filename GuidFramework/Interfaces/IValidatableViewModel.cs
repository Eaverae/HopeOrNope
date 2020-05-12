using System.Threading.Tasks;

namespace GuidFramework.Interfaces
{
    /// <summary>
    /// IValidatableViewModel
    /// </summary>
    public interface IValidatableViewModel : IValidatable
    {
        /// <summary>
        /// Adds the validation rules.
        /// </summary>
        void AddValidationRules();

        /// <summary>
        /// Validates this instance.
        /// </summary>
        /// <returns>A boolean value</returns>
        Task<bool> ValidateAsync();
    }
}
