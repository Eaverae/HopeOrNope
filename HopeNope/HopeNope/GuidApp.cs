using Autofac;
using HopeNope.Extensions;
using HopeNope.Handlers;
using HopeNope.Interfaces;
using System;
using Xamarin.Forms;

namespace HopeNope
{
	/// <summary>
	/// Baseclass for the application
	/// </summary>
	/// <seealso cref="Xamarin.Forms.Application" />
	public class GuidApp : Application
	{
		/// <summary>
		/// Gets the dependency container.
		/// </summary>
		public static IContainer Container { get; private set; }

		/// <summary>
		/// The builder that builds the dependency container
		/// </summary>
		protected ContainerBuilder ContainerBuilder { get; private set; }

		/// <summary>
		/// Application developers override this method to perform actions when the application starts.
		/// </summary>
		protected override void OnStart()
		{
			Initialize();

			base.OnStart();
		}

		/// <summary>
		/// Initializes the application.
		/// </summary>
		protected virtual void Initialize()
		{
			ContainerBuilder = new ContainerBuilder();

			RegisterDependencies();
			Container = ContainerBuilder.Build();
		}

		/// <summary>
		/// Registers the dependencies.
		/// </summary>
		/// <param name="ignoreDefaults">if set to <c>true</c> [ignores default dependencies].</param>
		protected virtual void RegisterDependencies(bool ignoreDefaults = false)
		{
			if (!ignoreDefaults)
			{
				if (!Current.Resources.ContainsKey("FontAwesomeSolid"))
					throw new InvalidOperationException("Missing key in application dictionary! Key 'FontAwesomeSolid' was not found.");

				string fontFamily = Current.Resources.PlatformSpecificValue<string>("FontAwesomeSolid");

				ContainerBuilder.RegisterType<LogHandler>().As<ILogHandler>();
				ContainerBuilder.RegisterType<AlertHandler>().As<IAlertHandler>();

				if (!fontFamily.IsNullOrWhiteSpace())
					ContainerBuilder.RegisterType<ToastHandler>().As<IToastHandler>().WithParameter(new NamedParameter("fontFamily", fontFamily));

				//ContainerBuilder.RegisterType<ValidationHandler>().As<IValidationHandler>();

				//ContainerBuilder.RegisterType<NavigationService>().As<INavigationService>();
			}
		}
	}
}
