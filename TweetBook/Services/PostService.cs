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

        public bool DeletePost(Guid postId)
        {
            var item = this.GetPostById(postId) ;

            if(item==null)
                return false;

             _postCollection.Remove(item);

            return true;
        }

        public Post GetPostById(Guid postId)
        {
            return _postCollection.SingleOrDefault(p=> p.Id==postId);
        }

        public List<Post> GetPosts()
        {
            return _postCollection;
        }

        public bool UpdatePost(Post postToUpdate)
        {
            var exists = this.GetPostById(postToUpdate.Id) != null;

            if(!exists)
                return false;

            var index =  _postCollection.FindIndex(x => x.Id== postToUpdate.Id);
            _postCollection[index] = postToUpdate;

            return true;
        }
    }
}