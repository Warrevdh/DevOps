using Shared.EyeConditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.EyeConditions;

public abstract class SymptomResult
{
    public class Index
    {
        public IEnumerable<SymptomDto.Index>? Symptoms { get; set; }
        public int TotalAmount { get; set; }
    }
    public class Create
    {
        public long SymptomId { get; set; }
        public string? Name { get; set; }
    }
}
