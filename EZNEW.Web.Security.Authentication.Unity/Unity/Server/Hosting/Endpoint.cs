using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace EZNEW.Web.Security.Authentication.Unity.Server.Hosting
{
    public class Endpoint
    {
        public Endpoint(string name, string path, Type handlerType)
        {
            Name = name;
            Path = path.StartsWith("/") ? path : "/" + path;
            Handler = handlerType;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        public PathString Path { get; set; }

        /// <summary>
        /// Gets or sets the handler.
        /// </summary>
        /// <value>
        /// The handler.
        /// </value>
        public Type Handler { get; set; }
    }
}
