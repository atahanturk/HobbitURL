using HobbitURL.Models;

namespace HobbitURL.Repository;

public interface IShortenedUrlRepo
{
    IEnumerable<ShortenedUrlModel> GetAllShortUrls();
    ShortenedUrlModel? GetShortUrlById(string id);
    ShortenedUrlModel? GetLongUrlByShortUrl(string shortUrl);
    ShortenedUrlModel? GetShortUrlByLongUrl(string longUrl);

    bool DeleteUrl(string id);

    bool IsUrlIdExist(string id);

    bool IsShortUrlExist(string shortUrl);

    bool IsLongUrlExist(string longUrl);
}