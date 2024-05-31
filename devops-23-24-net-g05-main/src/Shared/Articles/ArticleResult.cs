

namespace Shared.Articles
{
    public abstract class ArticleResult
    {
        public class Index
        {
            public IEnumerable<ArticleDto.Index>? Articles { get; set; }
            public int TotalAmount { get; set; }
        }

        public class Create
        {
            public long Id { get; set; }
            public string? Title { get; set; }
            public string? Description { get; set; }
            public string? Content { get; set; }
            public string? UploadUri { get; set; }
        }

        public class Detail 
        {
            public long Id { get; set; }
            public string? Title { get; set; }
            public string? Description { get; set; }
            public string? Content { get; set; }
            public string? UploadUri { get; set;}
            public DateTime? Created { get; set; }
            public string? Author { get; set; }
        }
    }
}
