using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetBook.Contracts
{
    public static class ApiRoutes
    {
        private const string Version = "v1";
        private const string Root = "api";
        public const string Base = Root + "/" + Version;

        public static class Post
        {
            public const string GetAll = Base + "/post";
            public const string Create = Base + "/post";
            public const string Get = Base + "/post/{postId}";
            public const string Update = Base + "/post/{postId}";
            public const string Delete = Base + "/post/{postId}";
        }
    }
}
