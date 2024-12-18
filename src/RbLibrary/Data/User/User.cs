namespace Rb.Integration.Api.Data.User;

public class User
{
	public string Url { get; set; } //Example: "https://api.robinhood.com/user/"

	public Guid? Id { get; set; }

	public string IdInfo { get; set; }

	public string Username { get; set; }

	public string Email { get; set; }

	public bool? EmailVerified { get; set; }

	public string FirstName { get; set; }

	public string LastName { get; set; }

	public Origin Origin { get; set; }

	public string ProfileName { get; set; }

	public DateTime? CreatedAt { get; set; }
}
