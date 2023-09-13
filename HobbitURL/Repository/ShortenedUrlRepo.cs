using HobbitURL.Data;
using HobbitURL.Models;

namespace HobbitURL.Repository;

public class ShortenedUrlRepo : IShortenedUrlRepo
{
    private readonly DataContext _context;

    public ShortenedUrlRepo(DataContext context)
    {
        _context = context;
    }

    public IEnumerable<ShortenedUrlModel> GetAllShortUrls()
    {
        
        return _context.ShortenedUrls.OrderBy(p => p.Id);
    }

    public ShortenedUrlModel? GetShortUrlById(string id)
    {
        return _context.ShortenedUrls.FirstOrDefault(p => p.Id.Equals(id));
    }

    public ShortenedUrlModel? GetLongUrlByShortUrl(string shortUrl)
    {
        return _context.ShortenedUrls.FirstOrDefault(p => p.ShortUrl == shortUrl);
    }

    public ShortenedUrlModel? GetShortUrlByLongUrl(string longUrl)
    {
        return _context.ShortenedUrls.FirstOrDefault(p => p.LongUrl == longUrl);
    }
    
    public bool DeleteUrl(string id)
    {
        var urlToDelete = _context.ShortenedUrls.FirstOrDefault(p => p.Id.Equals(id));

        if (urlToDelete != null)
        {
            _context.ShortenedUrls.Remove(urlToDelete);
            _context.SaveChanges();
            return true;
        }

        return false;
    }

    public bool IsUrlIdExist(string id)
    {
        return _context.ShortenedUrls.Any(p => p.Id.Equals(id));
    }

    public bool IsShortUrlExist(string shortUrl)
    {
        return _context.ShortenedUrls.Any(p => p.ShortUrl == shortUrl);
    }
    
    public bool IsLongUrlExist(string longUrl)
    {
        return _context.ShortenedUrls.Any(p => p.LongUrl == longUrl);
    }
}