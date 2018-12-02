using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using MySecondApp.Library.Models;
using MySecondApp.Library.Repositories;

namespace MySecondApp.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = ConfigureServices();
            RunApplication(serviceProvider, args);
        }

        private static void RunApplication(IServiceProvider serviceProvider, string[] args)
        {
            var recipient = "NONE!!!";
            var iterations = 1;
            if (args != null) {
                if (args.Length > 0) recipient = args[0];
                if (args.Length > 1) iterations = Convert.ToInt32(args[1]);
            }

            var repository = serviceProvider.GetService<IWordRepository>();

            var reverse = true;
            foreach(var sentence in repository.SayHello(recipient, iterations)) {
                System.Console.WriteLine(reverse ?
                    repository.ReverseWord(sentence) :
                    sentence);
                reverse = !reverse;
            }
        }

        private static IServiceProvider ConfigureServices()
        {
            var config = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .Build();
            
           return new ServiceCollection()
                .Configure<ApplicationSettings>(config)
                .AddScoped<IWordRepository, WordRepository>()
                .BuildServiceProvider();
        }
    }
}
