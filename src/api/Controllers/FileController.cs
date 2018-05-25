using System.Linq;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using Portal.Apis.Core.Enums;
using Portal.Apis.Core.Helpers;
using AutoMapper;
using System.Collections.Generic;
using Portal.Apis.Models;
using Portal.Apis.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Portal.Apis.Core.BLL;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Collections;
using Newtonsoft.Json;
using Portal.Apis.Core.Configuration;

namespace Portal.Apis.Controllers
{
    [Produces("application/json")]
    [Route("api/File")]
    public class FileController : Controller
    {
        private readonly IHostingEnvironment _appEnvironment;

        public FileController(IHostingEnvironment appEnvironment)
        {
            this._appEnvironment = appEnvironment;
        }

        [HttpPost("Upload")]
        public IActionResult UploadFile(string fileName, string pathName, IFormFile file)
        {
            try
            {
                string _pathName = string.Empty;
                switch (pathName)
                {
                    case "Kml": _pathName = "Storage:KmlFiles"; break;
                    case "Audio": _pathName = "Storage:AudioFiles"; break;
                    case "Photo": _pathName = "Storage:PhotoFiles:PhotoFiles"; break;
                    case "PhotoPreView": _pathName = "Storage:PhotoFiles:PhotoPreViewFiles"; break;
                    default: return BadRequest("In parameters pathName is Empty!!!");
                }

                var g_filename = string.Format("{0}{1}", Guid.NewGuid().ToString("N"), Path.GetExtension(file.FileName));
                var mediafilePath = Startup.Configuration[_pathName].DirectoryExist(_appEnvironment);
                var savePath = Path.Combine(mediafilePath, g_filename);

                using (var fileStream = new FileStream(savePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                var result = g_filename.GetFullPath(_pathName);

                return Ok(new { FileName = result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
