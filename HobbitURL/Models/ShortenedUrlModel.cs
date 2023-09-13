namespace HobbitURL.Models;

public class ShortenedUrlModel
{
    public string Id { get; init; }
    public string LongUrl { get; init; }
    public string ShortUrl { get; init; }

    public ShortenedUrlModel(string id, string longUrl, string shortUrl)
    {
        Id = id;
        LongUrl = longUrl;
        ShortUrl = shortUrl;
    }
}
