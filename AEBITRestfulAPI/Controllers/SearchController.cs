using AEBITRestfulAPI.Services;
using Microsoft.AspNetCore.Mvc;
using static AEBITRestfulAPI.Models.AuthModels;

namespace AEBITRestfulAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : Controller
    {
        private ISearchService searchService { get; set; }
        public SearchController(ISearchService searchService) {
            this.searchService = searchService;
        }
        [HttpPost]
        public async Task<object> SearchAsync([FromBody]SearchText request)
        {
            try
            {
                var result =  await searchService.SearchAsync(request);
                return result;
            }
            catch (Exception ex)
            {
                var response = new ExceptionMessage();
                response.message = ex.Message;
                return response;
            }
        }
    }
}
