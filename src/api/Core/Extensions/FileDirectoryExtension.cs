using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Portal.Apis.Core.BLL;
using Portal.Apis.Core.Helpers;

namespace Portal.Apis.Core.Extensions
{
    public static class FileDirectoryExtension
    {
        public static string DirectoryExist(this string path, IHostingEnvironment env)
        {
            if (!Directory.Exists(Path.Combine(env.ContentRootPath, path)))
            {
                Directory.CreateDirectory(Path.Combine(env.ContentRootPath, path));
            }
            return path;
        }
    }
}

