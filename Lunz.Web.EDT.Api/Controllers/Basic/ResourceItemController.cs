using Lunz.Data.EDT;
using Lunz.Services;
using Lunz.Services.EDT;
using Lunz.Web.Configuration;
using Lunz.Web.Drawing;
using Lunz.Web.EDT.Api.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Lunz.Web.EDT.Api.Controllers
{
    public class ResourceItemController : BaseApiController
    {
        private readonly IResourceItemService _resourceItemService;

        public ResourceItemController(IResourceItemService resourceItemService)
        {
            _resourceItemService = resourceItemService;
        }

        [ActionName("Add")]
        [HttpPost]
        public async Task<WebApiResult<string>> AddResourceItem()
        {
            WebApiResult<string> result = new WebApiResult<string>();
            UploadedFilesResizer resizer = new UploadedFilesResizer();
            string originalPath = resizer.configPath;
            //string originalPath = System.Configuration.ConfigurationManager.AppSettings["ResourceItemPath"];
            string path = originalPath;

            // 检查是否是 multipart/form-data
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            HttpResponseMessage response = null;

            try
            {
                path = HttpContext.Current.Server.MapPath(path);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                // 设置上传目录
                var provider = new RenamingMultipartFormDataStreamProvider(path);

                // 接收数据，并保存文件
                var bodyparts = await Request.Content.ReadAsMultipartAsync(provider);
                response = Request.CreateResponse(HttpStatusCode.Accepted);
                FileInfo fi = new FileInfo(provider.FileData[0].LocalFileName);
                if (fi.Exists)
                {
                    try
                    {
                        Basic_ResourceItem resourceItem = new Basic_ResourceItem
                        {
                            Filename = fi.Name,
                            FileType = fi.Extension,
                            FileLength = fi.Length,
                            Url = string.Format("{0}{1}", originalPath, fi.Name),
                            CreatedAt = DateTime.Now,
                            Deleted = false
                        };
                        result = _resourceItemService.AddResourceitem(resourceItem);
                        resizer.CreateSizedImages(provider.FileData[0].LocalFileName);
                        fi = null;
                    }
                    catch (Exception ex)
                    {
                        result.AddError(ex.Message);
                    }
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            return result;
        }
    }
}
