using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Repository
{
    public interface IRepository
    {
        public Post GetPost(int id);
        List<Post> GetAllPosts();
        List<Post> GetAllPosts(string Category);
        void UpdatePost(Post post);
        void RemovePost(int id);
        void AddPost(Post post);

        Task<bool> SaveChangesAsync();
    }
}
