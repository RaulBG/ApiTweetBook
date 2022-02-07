using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetBook.Contracts;
using TweetBook.Domain;

namespace TweetBook.Controllers.v1
{
    public class PostController : Controller
    {
        private List<Post> _postCollection;

        public PostController ()
        {
            _postCollection = new List<Post>();
            for (int i = 0; i < 5; i++)
            {
                _postCollection.Add(new Post { Id = Guid.NewGuid().ToString() });
            }
        }

        [HttpGet(ApiRoutes.Post.GetAll)]
        public IActionResult GetAll()
        {
            return Ok(_postCollection);
        }

        [HttpPost(ApiRoutes.Post.Create)]
        public IActionResult Create([FromBody] Post post)
        {
            if(string.IsNullOrEmpty(post.Id))
            {
                post.Id= Guid.NewGuid().ToString();
            }

            _postCollection.Add(post);

            var baseUrl=$"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri= $"{baseUrl}/{ApiRoutes.Post.Get.Replace("{postId}",post.Id)}";

            return Created(locationUri, post);
        }
    }
}
