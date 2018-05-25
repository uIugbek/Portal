using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Portal.Apis.Core.Extensions;

namespace Portal.Apis.Models
{
    public class FileModel
    {
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string Value { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(FileName) &&
                   !string.IsNullOrEmpty(FileType) &&
                   !string.IsNullOrEmpty(Value);
        }

        public void Save(string fileName, string path, IHostingEnvironment appEnvironment)
        {
            string fileDirectory = Path.Combine(appEnvironment.ContentRootPath, path);
            if (!Directory.Exists(fileDirectory))
                Directory.CreateDirectory(fileDirectory);

            string fullPath = Path.Combine(appEnvironment.ContentRootPath, fileDirectory, fileName);
            File.WriteAllBytes(fullPath, Convert.FromBase64String(Value));
        }
    }
}