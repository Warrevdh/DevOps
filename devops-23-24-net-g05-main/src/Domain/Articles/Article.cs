using Domain.Articles.Fragments;

namespace Domain.Articles;

public class Article : Entity {

    private string title = default!;

    public string Title {
        get => title;
        set => title = Guard.Against.NullOrWhiteSpace(value, nameof(Title));
    }

    private string description = default!;
    public string Description
    {
        get => description;
        set => description = Guard.Against.NullOrWhiteSpace(value, nameof(Description));
    }

    private string content = default!;

    public string Content {
        get => content;
        set => content = Guard.Against.NullOrWhiteSpace(value, nameof(Content));
    }

/*private string author = default!;

    public string Author {
        get => author;
        set => author = Guard.Against.NullOrWhiteSpace(value, nameof(Author));
    }
*/

    private string imageUrl = default!;

    public string ImageUrl
    {
        get => imageUrl;
        set => imageUrl = Guard.Against.NullOrWhiteSpace(value, nameof(ImageUrl));
    }


    //Database Constructor
    private Article() { }

    public Article(string title, string description, string content, string imageUrl)
    {
        Title = title;
        Description = description;
        Content = content;
        ImageUrl = imageUrl;
    }
}
