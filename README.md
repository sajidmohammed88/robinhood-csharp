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
dotnet add package robinhood-csharp --version 1.0.6
```
3. Create Authentication section configuration in your project that call this package
```
"Authentication": {
    "UserName": "**********",
    "Password": "**********",
    "ClientId": "**********",
    "ExpirationTime": 734000,
    "Timeout": 5,
    "ChallengeType": "sms"
  }
```

3. Inject ``IRobinhood`` Interface and ``Robinhood`` class in ``Program.Cs``, example for Console App :

```
IRobinhood _robinhood = _serviceProvider.GetRequiredService<IRobinhood>(); 
```
4. Call login method and manage responses types
```
AuthenticationResponse authResponse = await _robinhood.LoginAsync();

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

if (!authResponse.IsOauthValid)
{
	string message;
	if (!string.IsNullOrEmpty(authResponse.Error))
	{
		message = authResponse.Error;
	}
	else
	{
		message = !string.IsNullOrEmpty(authResponse.Detail) ? authResponse.Detail : "Unknown login error";
	}

	Console.WriteLine(message);
	throw new AuthenticationException(message);
}
```
5. Configure the token expiration date, refresh token and Authorization header by calling ConfigureManager method : 
```
_robinhood.ConfigureManager(authResponse);
```

# Samples and tests
- Check user information example : 
```
User user = await _robinhood.GetUserAsync();
```
- Find tests examples for all routes under **tests/RbConsoleApp** project.
