<p align="center">
<img src=".github\robinhood-csharp.png">
</p>

# Introduction 
C# library to make trades with Unofficial Robinhood API.
<br>
See @Sanko's [Unofficial Documentation](https://github.com/sanko/Robinhood) for more information.

# Getting Started
1. Install the package from GitHub packages or NuGet packages
```
dotnet add package robinhood-csharp
```
2. Create Authentication section configuration in your project that call this package
```
"Authentication": {
    //email
    "UserName": "**********", 
    "Password": "**********",
    "ClientId": "**********",
    "ExpirationTime": 734000,
    "Timeout": 5,
    "ChallengeType": "sms"
  }
```

3. Add the configuration needed for the package in ``Program.cs``
	1. REST API
		```
		builder.Services.ConfigureRb(builder.Configuration)
		```
	1. Console APP
		```
		private static IConfiguration _configuration;
		private static IServiceProvider _serviceProvider;

		private static IHostBuilder CreateHostBuilder(string[] args)
		{
			return Host.CreateDefaultBuilder(args)
				.ConfigureHostConfiguration(AddConfiguration)
				.ConfigureServices(services =>
				{
					services.ConfigureRb(_configuration);
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
		
		```

4. Inject ``IRobinhood`` Interface

5. Call login method and manage responses types
```
AuthenticationResponse authResponse = await _robinhood.LoginAsync();
```
6. Manage challenge case
```
if (authResponse.IsChallenge)
{
	do
	{
		Challenge challenge = authResponse.Challenge;

		Console.WriteLine($"Input challenge code from {challenge.Type} ({challenge.RemainingAttempts}/{challenge.RemainingRetries}):");
		string code = Console.ReadLine();

		authResponse = await _robinhood.ChallengeOauth2Async(challenge.Id, code);
	} while (authResponse.IsChallenge && authResponse.Challenge.CanRetry);
}
```

7. Manage Mfa case
```
if (authResponse.MfaRequired)
{
	int attempts = 3;
	(HttpStatusCode statusCode, AuthenticationResponse mfaAuth) mfaResponse;
	do
	{
		Console.WriteLine($"Input the MFA code:");
		string code = Console.ReadLine();

		mfaResponse = await _robinhood.MfaOath2Async(code);
		attempts--;
	} while (attempts > 0 && mfaResponse.statusCode != HttpStatusCode.OK);

	authResponse = mfaResponse.mfaAuth;
}

```
8. Configure the token expiration date, refresh token and Authorization header by calling ConfigureManager method : 
```
_robinhood.ConfigureManager(authResponse);
```

# Samples and tests
- Check user information example : 
```
User user = await _robinhood.GetUserAsync();
```
- Find tests examples for all routes under **samples/RbConsoleApp** project.
- Find one test example for REST API under **samples/RbWebApi** project.
