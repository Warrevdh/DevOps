namespace Shared.Users.Teams.Biographies;

public abstract class BioRequest
{
	public class Index
	{
		public int Page { get; set; } = 1;
		public int PageSize { get; set; } = 10;
	}
}
