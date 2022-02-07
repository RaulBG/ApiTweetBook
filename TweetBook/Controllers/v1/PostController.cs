using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetBook.Contracts;
using TweetBook.Controllers.v1.Requests;
using TweetBook.Controllers.v1.Responses;
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
        public IActionResult Create([FromBody] CreatePostRequest postRequest)
        {
            Post post = new Post{Id= postRequest.Id}; // No se deben de mezclar los contratos de las apis con los objetos del dominoo

            if(string.IsNullOrEmpty(post.Id))
                post.Id= Guid.NewGuid().ToString();            

            _postCollection.Add(post);

            var baseUrl=$"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri= $"{baseUrl}/{ApiRoutes.Post.Get.Replace("{postId}",post.Id)}";

            PostResponse response = new PostResponse{Id=post.Id};

            return Created(locationUri, post);
        }
    }
}
