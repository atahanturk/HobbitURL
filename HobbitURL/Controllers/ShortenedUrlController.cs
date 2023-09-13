using HobbitURL.Data;
using HobbitURL.Models;
using HobbitURL.Repository;
using HobbitURL.Services;
using Microsoft.AspNetCore.Mvc;

namespace HobbitURL.Controllers
{
    [Route("")]
    [ApiController]
    public class ShortenedUrlController : Controller
    {
        private readonly IShortenedUrlRepo _urlRepo;
        private readonly IShortenedUrlService _urlService; 

        

        public ShortenedUrlController(IShortenedUrlRepo urlRepo, IShortenedUrlService urlService)
        {
            _urlRepo = urlRepo;
            _urlService = urlService;
        }
        
        [HttpPut("createUrl")]
        [ProducesResponseType(200, Type = typeof(ShortenedUrlModel))]
        [ProducesResponseType(400)]
        public IActionResult CreateShortUrl([FromBody] ShortenedUrlModel inputModel)
        {
            if (string.IsNullOrEmpty(inputModel.LongUrl))
            {
                return BadRequest("Lütfen geçerli bir url adresi giriniz.");
            }

            // Long url adresi zaten dbde varolan bir adres mi diye kontrol ediyoruz.
            if (_urlRepo.IsLongUrlExist(inputModel.LongUrl))
            {
                var existingModel = _urlRepo.GetShortUrlByLongUrl(inputModel.LongUrl);
                
                return Ok(existingModel);
            }
            else
            {
                // Dbde olmayan bir long url girilmişse yeni shorurl generate ediyoruz.
                var createdModel = _urlService.CreateShortUrl(inputModel.LongUrl);
                return Ok(createdModel);
            }
        }
        
        [HttpDelete("deleteUrl")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult DeleteShortUrl(string id)
        {
            if (!_urlRepo.IsUrlIdExist(id))
            {
                return NotFound("Bu Id'ye sahip bir url bulunmamakta.");
            }

            if (_urlRepo.DeleteUrl(id))
            {
                return Ok("URL başarıyla silindi.");
            }

            return BadRequest("Bilinmeyen bir hata meydana geldi.");
        }


        [HttpGet("getAllUrl")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ShortenedUrlModel>))]
        public IActionResult GetAllShortUrls()
        {
            var shortenedUrls = _urlRepo.GetAllShortUrls().ToList();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(shortenedUrls);
        }

        [HttpGet("getUrlById")]
        [ProducesResponseType(200, Type = typeof(ShortenedUrlModel))]
        [ProducesResponseType(400)]
        public IActionResult GetShortUrlById(string id)
        {
            if (!_urlRepo.IsUrlIdExist(id))
            {
                return NotFound("Sorguladığınız Id'ye sahip bir Url bulunamadı.");
            }

            var shortenedUrl = _urlRepo.GetShortUrlById(id);
            return Ok(shortenedUrl);
        }

        [HttpGet("getUrlByShortUrl")]
        [ProducesResponseType(200, Type = typeof(ShortenedUrlModel))]
        [ProducesResponseType(400)]
        public IActionResult GetLongUrlByShortUrl(string shortUrl)
        {
            if (!_urlRepo.IsShortUrlExist(shortUrl))
            {
                return NotFound(("Sorguladığınız Kısa Url kayıtlarda bulunmamaktadır."));
            }

            var urlInfo = _urlRepo.GetLongUrlByShortUrl(shortUrl);
            return Ok(urlInfo);
        }
        
        [HttpGet("getUrlByLongUrl")]
        [ProducesResponseType(200, Type = typeof(ShortenedUrlModel))]
        [ProducesResponseType(400)]
        public IActionResult GetShortUrlByLongUrl(string longUrl)
        {
            if (!_urlRepo.IsLongUrlExist(longUrl))
            {
                return NotFound("Sorguladığınız Url kayıtlarda bulunmamaktadır.");
            }

            var urlInfo = _urlRepo.GetShortUrlByLongUrl(longUrl);
            return Ok(urlInfo);
        }
        
        [HttpGet("{shortUrl}")]
        public IActionResult RedirectToLongUrl(string shortUrl)
        {   
            
            var urlMapping = _urlRepo.GetLongUrlByShortUrl(shortUrl);
            if (urlMapping != null)
            {
                string longUrl = urlMapping.LongUrl;

                if (!longUrl.StartsWith("http://", StringComparison.OrdinalIgnoreCase) &&
                    !longUrl.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                {
                    longUrl = "http://" + longUrl;
                }

                return Redirect(longUrl);
            }
            
            
            return NotFound("Yanlış bir Url adresi girdiniz."); 
            
        }

        
        
    }
}