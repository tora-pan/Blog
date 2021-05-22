using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Repository
{
    public class Repository : IRepository
    {

        private AppDbContext _dbContext;

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddPost(Post post)
        {
            _dbContext.Posts.Add(post);
        }

        public List<Post> GetAllPosts(int id)
        {
            return _dbContext.Posts.ToList();
        }

        public Post GetPost(int id)
        {
            // check to see if the post 'p' has an ID == the id passed in
            return _dbContext.Posts.FirstOrDefault(p => p.Id == id);
        }

        public void RemovePost(int id)
        {
            _dbContext.Posts.Remove(GetPost(id));
        }


        public void UpdatePost(Post post)
        {
            _dbContext.Posts.Update(post);
        }

        public async Task<bool> SaveChangesAsync()
        {
            if(await _dbContext.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }
    }
}
