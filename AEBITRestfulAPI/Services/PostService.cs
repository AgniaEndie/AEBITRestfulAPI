using AEBITRestfulAPI.Data;
using Microsoft.EntityFrameworkCore;
using static AEBITRestfulAPI.Models.AuthModels;

namespace AEBITRestfulAPI.Services
{
    public interface IPostService
    {
        Task<object> GetAllPosts();
        Task<object> GetPost(int postId);
        Task<object> CreatePost(RequestPost post);
        Task<object> UpdatePost(Post post);
        Task<object> DeletePost(int postId);
    }

    public class PostService : IPostService
    {
        private WebApiContext webApiContext { get; set; }
        public PostService(WebApiContext webApiContext)
        {
            this.webApiContext = webApiContext;
        }

        public async Task<object> GetAllPosts()
        {
            try
            {
                var posts = await webApiContext.Post.ToListAsync();
                return posts;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<object> GetPost(int postId)
        {
            try
            {
                var post = await webApiContext.Post.FirstOrDefaultAsync(p => p.id == postId);
                if(post != null)
                {
                    return post;
                }
                else
                {
                    throw new Exception("Такого поста не существует");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<object> CreatePost(RequestPost post)
        {
            try
            {

                if (post != null)
                {
                    var postToSave = new Post();
                    postToSave.title = post.title;
                    postToSave.text = post.text;
                    postToSave.created_at = DateTime.UtcNow;
                    await webApiContext.Post.AddAsync(postToSave);
                    await webApiContext.SaveChangesAsync();
                    return postToSave;
                }
                else
                {
                    throw new Exception("Произошла неизвестная ошибка");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<object> UpdatePost(Post post)
        {
            try
            {
                if (post.id > 0)
                {
                    var postFromBase = await webApiContext.Post.Where(p => p.id == post.id).FirstAsync();
                    if (postFromBase != null)
                    {
                        postFromBase.title = post.title;
                        postFromBase.text = post.text;
                        webApiContext.Update(postFromBase);
                        await webApiContext.SaveChangesAsync();
                        return postFromBase;
                    }
                    else
                    {
                        throw new Exception("Такого поста не существует");
                    }
                }
                else
                {
                    throw new Exception("Такого поста не существует");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<object> DeletePost(int postId)
        {
            try
            {
                if(postId > 0)
                {
                    var post = await webApiContext.Post.Where(p => p.id == postId).FirstAsync();
                    if(post != null)
                    {
                        webApiContext.Remove(post);
                        await webApiContext.SaveChangesAsync();
                        return "Успешно удален пост: " + post.id;
                    }
                    else
                    {
                        throw new Exception("Такого поста не существует");
                    }
                }
                else
                {
                    throw new Exception("Такого поста не существует");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
