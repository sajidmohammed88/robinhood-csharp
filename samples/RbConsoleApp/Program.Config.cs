using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Rb.Integration.Api.Extensions;

using System;
using System.IO;

namespace RobinhoodConsoleApp;

/// <summary>
/// Configure the console App to use DI.
/// </summary>
public static partial class Program
{
	private static IConfiguration _configuration;
	private static IServiceProvider _serviceProvider;

	private static IHostBuilder CreateHostBuilder(string[] args)
	{
		return Host.CreateDefaultBuilder(args)
			.ConfigureHostConfiguration(AddConfiguration)
			.ConfigureServices(services =>
			{
				services.ConfigueRb(_configuration);
				_serviceProvider = services.BuildServiceProvider();
			});
	}

	private static void AddConfiguration(IConfigurationBuilder builder)
	{
		IConfigurationBuilder configurationBuilder =
			builder
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", false, true);

		_configuration = configurationBuilder.Build();
	}
}
