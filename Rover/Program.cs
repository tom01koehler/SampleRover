using Microsoft.Extensions.DependencyInjection;
using Rover.Configuration;
using RoverLibrary.Services;

namespace Rover
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
            var services = new ServiceCollection()
                .AddLogging()
                .ConfigureInjection()
                .BuildServiceProvider();

            var inputService = services.GetService<IInputService>();

            inputService.StartSimulation();

        }
    }
}
