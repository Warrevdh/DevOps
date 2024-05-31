using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Articles
{
    public abstract class ArticleRequest
    {
        public class Index
        {
            public string? Searchterm { get; set; }
            public int Page { get; set; } = 1;
            public int PageSize { get; set; } = 25;
            public int? TagId { get; set; }

        }
    }
}
