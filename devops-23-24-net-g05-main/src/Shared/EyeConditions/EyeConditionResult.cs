namespace Shared.EyeConditions;

public abstract class EyeConditionResult
{
	public class Index
	{
		public IEnumerable<EyeConditionDto.Index>? EyeConditions { get; set; }
		public int TotalAmount { get; set; }
	}

	public class Create
	{
		public long EyeConditionId { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public string? Body { get; set; }
		public string UploadUri { get; set; } = default!;
		public string? BrochureUrl { get; set; }
    }
	
}
