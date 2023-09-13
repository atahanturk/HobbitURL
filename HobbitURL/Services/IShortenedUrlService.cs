using HobbitURL.Models;

namespace HobbitURL.Services
{
    public interface IShortenedUrlService
    {
        ShortenedUrlModel CreateShortUrl(string longUrl);
        
    }
}