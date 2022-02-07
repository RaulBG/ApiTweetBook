using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetBook.Domain;

namespace TweetBook.Services
{
    public class PostService : IPostService
    {
          private readonly List<Post> _postCollection;

        public PostService ()
        {
            _postCollection = new List<Post>();
            for (int i = 0; i < 5; i++)
            {
                _postCollection.Add(new Post { Id = Guid.NewGuid()
                , Name= $"Post Name {i}"});
            }
        }

        public Post GetPostById(Guid postId)
        {
            return _postCollection.SingleOrDefault(p=> p.Id==postId);
        }

        public List<Post> GetPosts()
        {
           

            return _postCollection;
        }
    }
}