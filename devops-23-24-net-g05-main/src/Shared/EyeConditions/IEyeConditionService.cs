namespace Shared.EyeConditions;
public interface IEyeConditionService
{
    Task<EyeConditionResult.Index> GetIndexAsync(EyeConditionRequest.Index request);
    Task<EyeConditionResult.Create> CreateAsync(EyeConditionDto.Mutate model);
    Task<EyeConditionDto.Mutate> GetMutateAsync(long productId);
    Task<EyeConditionDto.Detail> GetDetailAsync(long productId);
    // Task<int> CreateAsync(ProductDto.Mutate model);
    Task EditAsync(long id, EyeConditionDto.Mutate edit);
    Task DeleteAsync(long id);
    // Task AddTagAsync(int productId, int tagId);
}