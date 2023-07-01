using AEBITRestfulAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static AEBITRestfulAPI.Models.AuthModels;

namespace AEBITRestfulAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PostController :Controller
    {
        private IPostService postService { get; set;}
        public PostController(IPostService postService) { 
            this.postService = postService;
        }

        [HttpGet("all"), AllowAnonymous]
        public async Task<object> GetAllPosts()
        {
            try
            {
                var result = await postService.GetAllPosts();
                return result;
            }catch (Exception ex)
            {
                var response = new ExceptionMessage();
                response.message = ex.Message;
                return response;
            }
        }
        [HttpGet("{postId}"), AllowAnonymous]
        public async Task<object> GetPost(int postId)
        {
            try
            {
                var result = await postService.GetPost(postId);
                return result;
            }
            catch (Exception ex)
            {
                var response = new ExceptionMessage();
                response.message = ex.Message;
                return response;
            }
        }
        [HttpPost("create")]
        public async Task<object> CreatePost([FromBody] RequestPost post)
        {
            try
            {
                return await postService.CreatePost(post);
            }
            catch (Exception ex)
            {
                var response = new ExceptionMessage();
                response.message = ex.Message;
                return response;
            }
        }
        [HttpPost("update")] //[HttpPut("update/")]
        public async Task<object> UpdatePost([FromBody] Post post)
        {
            try
            {
                return await postService.UpdatePost(post);
            }catch(Exception ex)
            {
                var response = new ExceptionMessage();
                response.message = ex.Message;
                return response;
            }
        }
        [HttpGet("delete/{postId}")] //[HttpDelete("delete/{postId}")]
        public async Task<object> DeletePost(int postId)
        {
            try
            {
                return await postService.DeletePost(postId);
            }catch(Exception ex)
            {
                var response = new ExceptionMessage();
                response.message = ex.Message;
                return response;
            }
        }
    }
}
