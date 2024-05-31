namespace Shared.Users.Teams.Biographies;

public abstract class BioResult
{
	public class Index
	{
		public IEnumerable<BioResult.Index>? Biographies { get; set; }
		public int TotalAmount { get; set; }
	}

	public class Detail
	{
		public long Id { get; set; }
		public string? Info { get; set; }
	}
}
