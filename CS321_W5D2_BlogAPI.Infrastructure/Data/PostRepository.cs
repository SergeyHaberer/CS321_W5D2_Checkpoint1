using System;
using System.Collections.Generic;
using System.Linq;
using CS321_W5D2_BlogAPI.Core.Models;
using CS321_W5D2_BlogAPI.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace CS321_W5D2_BlogAPI.Infrastructure.Data
{
    public class PostRepository : IPostRepository
    {
        private AppDbContext _dbContext;
        public PostRepository(AppDbContext dbContext) 
        {  
            _dbContext=dbContext;
        }

        public Post Get(int id)
        {
            // TODO: Implement Get(id). Include related Blog and Blog.User
             return _dbContext.Posts
                .Include(b => b.Blog)
                .Include(b => b.Blog.User)
                .SingleOrDefault(b => b.Id == id);
        }

        public IEnumerable<Post> GetBlogPosts(int blogId)
        {
            // TODO: Implement GetBlogPosts, return all posts for given blog id
            // TODO: Include related Blog and AppUser
             return _dbContext.Posts
                .Include(b => b.Blog)
                .Include(b => b.Blog.User)
                .Where(b => b.BlogId == blogId)
                .ToList();
        }

        public Post Add(Post Post)
        {
            // TODO: add Post
             _dbContext.Posts.Add(Post);
            _dbContext.SaveChanges();
            return Post;
        }

        public Post Update(Post Post)
        {
            // TODO: update Post
            var existingItem = _dbContext.Posts.Find(Post.Id);
            if (existingItem == null) return null;
            _dbContext.Entry(existingItem)
               .CurrentValues
               .SetValues(Post);
            _dbContext.Posts.Update(existingItem);
            _dbContext.SaveChanges();
            return existingItem;
        }

        public IEnumerable<Post> GetAll()
        {
            // TODO: get all posts
             return _dbContext.Posts
                .Include(P => P.Blog.Name)
                .ToList();
        }

        public void Remove(int id)
        {
            // TODO: remove Post
            Post post = _dbContext.Posts.Find(id);
            if(post != null)
            {
                _dbContext.Remove(id);
                _dbContext.SaveChanges();
            }
            
        }

    }
}
