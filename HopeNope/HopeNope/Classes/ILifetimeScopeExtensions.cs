using Autofac;
using GuidFramework.Interfaces;
using HopeNope.Properties;
using System;

namespace HopeNope.Extensions
{
	/// <summary>
	/// ILifetimeScopeExtensions
	/// </summary>
	public static class ILifetimeScopeExtensions
	{
		/// <summary>
		/// Resolves the validation handler with parameters.
		/// </summary>
		/// <param name="scope">The scope.</param>
		/// <returns></returns>
		public static IValidationHandler ResolveValidationHandlerWithParameters(this ILifetimeScope scope)
		{
			if (scope == null)
				throw new ArgumentNullException(nameof(scope));

			return scope.Resolve<IValidationHandler>(new NamedParameter("validationMessageMultipleFieldsAreInvalid", Resources.ValildationMultipleFieldsAreInvalid));
		}
	}
}
