namespace Shared.Users.Teams.Groups;

public abstract class GroupResult
{
	public class Index
	{
		public IEnumerable<GroupDto.Index>? Groups { get; set; }
		public int TotalAmount { get; set; }
	}
}
