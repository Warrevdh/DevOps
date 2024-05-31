namespace Shared.Users.Teams.Biographies;

public interface IBioService
{
	Task<BioDto.Index> GetDetailAsync(long bioId);
	Task<long> CreateAsync(BioDto.Mutate model);
	Task EditAsync(long bioId, BioDto.Mutate model);
	Task DeleteAsync(long bioId);
}
