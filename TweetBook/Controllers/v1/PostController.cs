using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetBook.Contracts;
using TweetBook.Controllers.v1.Requests;
using TweetBook.Controllers.v1.Responses;
using TweetBook.Domain;
using TweetBook.Services;

namespace TweetBook.Controllers.v1
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController (IPostService postService)
        {
            _postService= postService;
        }

        [HttpGet(ApiRoutes.Post.GetAll)]
        public IActionResult GetAll()
        {
            return Ok(_postService.GetPosts());
        }

        [HttpGet(ApiRoutes.Post.Get)]
        public IActionResult Get([FromRoute]Guid postId)
        {
            var result =_postService.GetPostById(postId); 

            if (result == null)
                return NotFound("Item not Found"); 

            return Ok(result);
        }

        [HttpPost(ApiRoutes.Post.Create)]
        public IActionResult Create([FromBody] CreatePostRequest postRequest)
        {
            Post post = new Post{Id= postRequest.Id}; // No se deben de mezclar los contratos de las apis con los objetos del dominoo

            if(post.Id != Guid.Empty)            
                post.Id= Guid.NewGuid();            

            _postService.GetPosts().Add(post);

            var baseUrl=$"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri= $"{baseUrl}/{ApiRoutes.Post.Get.Replace("{postId}",post.Id.ToString())}";

            PostResponse response = new PostResponse{Id=post.Id};

            return Created(locationUri, post);
        }
    }
}
