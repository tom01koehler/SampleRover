using Microsoft.Extensions.DependencyInjection;
using Rover.Library.Helpers;
using Rover.Library.Services;
using Rover.Services;
using Rover.Wrappers;
using RoverLibrary.Services;

namespace Rover.Configuration
{
    public static class InjectionConfig
    {
        /// <summary>
        /// Defines Interface implementation
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureInjection(this IServiceCollection services)
        {
            // Services
            services.AddSingleton<IInputService, InputService>();
            services.AddSingleton<INavigationService, NavigationService>();

            // Wrappers
            services.AddSingleton<IConsoleWrapper, ConsoleWrapper>();

            // Helpers
            services.AddSingleton<IInputValidationHelper, InputValidationHelper>();

            return services;
        }
    }
}
