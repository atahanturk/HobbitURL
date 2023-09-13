using HobbitURL.Data;
using HobbitURL.Models;
using HobbitURL.Utils;


namespace HobbitURL.Services
{
    public class ShortenedUrlService : IShortenedUrlService
    {
        private readonly DataContext _context;

        public ShortenedUrlService(DataContext context)
        {
            _context = context;
        }

        public ShortenedUrlModel CreateShortUrl(string longUrl)
        {
            
            var id = GenerateId.Id();
            var shortUrl = id.Substring(8, 8);

            var urlMapping = new ShortenedUrlModel(
            
                id,
                longUrl,
                shortUrl
            );

            _context.ShortenedUrls.Add(urlMapping);
            _context.SaveChanges();
            
            return urlMapping;
        }
        
    }
}