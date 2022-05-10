using Foundation.Dictionary.Models;
using Foundation.Dictionary.Services;
using Sitecore.Services.Core;
using Sitecore.Services.Infrastructure.Web.Http;
using System.Web.Http;

namespace Foundation.Dictionary.Controllers
{
    [ServicesController]
    [RoutePrefix(Constants.RoutePrefixes.Dictionary)]
    public class DictionaryController : ServicesApiController
    {
        private readonly SiteDictionaryService _siteDictionaryService;

        public DictionaryController(SiteDictionaryService siteDictionaryService)
        {
            _siteDictionaryService = siteDictionaryService;
        }

        [HttpGet]
        [Route("")]
        public PhraseDictionary GetSiteDictionary()
        {
            return _siteDictionaryService.GetCurrentSiteDictionary();
        }

    }
}