<p align="center">
<img src=".github\robinhood-csharp.png">
</p>

# Introduction 
C# library to make trades with Unofficial Robinhood API.
<br>
See @Sanko's [Unofficial Documentation](https://github.com/sanko/Robinhood) for more information.

# Getting Started
1. Install tha package
```
```
2. Create Authentication section configuration in your project that call this package
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

3. Inject ``IRobinhood`` Interface and ``Robinhood`` class in ``Program.Cs``
4. Call login method
```
```

