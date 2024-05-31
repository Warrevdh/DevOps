using Shared.EyeConditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.EyeConditions;

public interface ISymptomService
{
    Task<SymptomResult.Index> GetIndexAsync(SymptomRequest.Index request);
    Task<SymptomResult.Create> CreateAsync(SymptomDto.Mutate model);
    Task EditAsync(long symptomId, SymptomDto.Mutate model);
    Task DeleteAsync(long id);
}
