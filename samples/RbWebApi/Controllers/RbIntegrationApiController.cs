using Microsoft.AspNetCore.Mvc;

using Rb.Integration.Api.Abstractions;
using Rb.Integration.Api.Data.Authentication;

namespace Rb.Integration.Api.WebApi.Sample.Controllers;
[ApiController]
[Route("[controller]")]
public class RbIntegrationApiController(IRobinhood robinhood) : ControllerBase
{
	[HttpPost]
	public async Task<IActionResult> LoginAsync()
	{
		AuthenticationResponse authResponse = await robinhood.LoginAsync();
		return Ok(authResponse);
	}
}
