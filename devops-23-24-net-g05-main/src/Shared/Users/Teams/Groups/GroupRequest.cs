namespace Shared.Users.Teams.Groups;

public abstract class GroupRequest
{
	public class Index
	{
		public string? Name { get; set; }
		public int Page { get; set; } = 1;
		public int PageSize { get; set; } = 25;
	}
}
