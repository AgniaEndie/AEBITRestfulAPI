using AEBITRestfulAPI.Data;
using Microsoft.EntityFrameworkCore;
using static AEBITRestfulAPI.Models.AuthModels;

namespace AEBITRestfulAPI.Services
{
    public interface ISearchService{
        Task<object> SearchAsync(SearchText request);
    }

    public class SearchService : ISearchService
    {
        private WebApiContext webApiContext;

        public SearchService(WebApiContext webApiContext) {
            this.webApiContext = webApiContext;
        }


        public async Task<object> SearchAsync(SearchText request)
        {
            var data = await webApiContext.Post.ToListAsync();
            List<Post> resultList = new List<Post>();

            foreach(var dataItem in data)
            {
                if (dataItem.title.ToLower().Contains(request.text.ToLower()) && !resultList.Contains(dataItem))
                {
                    resultList.Add(dataItem);
                }
                if (dataItem.text.ToLower().Contains(request.text.ToLower()) && !resultList.Contains(dataItem))
                {
                    resultList.Add(dataItem);
                }
            }
            if(resultList.Count > 0)
            {
                return resultList;
            }
            else
            {
                throw new Exception("По вашему запросу ничего не найдено");
            }
        }
    }
}
