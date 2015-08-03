using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lunz.Data.EDT;
using System.Net;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Json;

namespace Lunz.Services.EDT
{
    public interface IResourceItemService
    {
        WebApiResult<string> AddResourceitem(Basic_ResourceItem ResourceItem);
        WebApiResult<Basic_ResourceItem> GetResourceItemById(Guid Id);
    }
    public class ResourceItemService : ServiceBase, IResourceItemService
    {
        public WebApiResult<string> AddResourceitem(Basic_ResourceItem ResourceItem)
        {
            WebApiResult<string> result = new WebApiResult<string>();

            var resourceItem = DataContext.Basic_ResourceItem.FirstOrDefault(x => x.Deleted == false && (x.Filename == ResourceItem.Filename || x.Url == ResourceItem.Url));

            if (resourceItem != null)
            {
                resourceItem.Url = ResourceItem.Url;
                result.Data = resourceItem.Id.ToString();
                DataContext.SaveChanges();
            }
            else
            {
                DataContext.Basic_ResourceItem.Add(ResourceItem);
                DataContext.SaveChanges();
                result.Data = ResourceItem.Id.ToString();
            }

            return result;
        }


        public WebApiResult<Basic_ResourceItem> GetResourceItemById(Guid Id)
        {
            WebApiResult<Basic_ResourceItem> result = new WebApiResult<Basic_ResourceItem>();
            if (Id != null)
            {
                result.Data = DataContext.Basic_ResourceItem.FirstOrDefault(x => x.Id == Id);
            }
            return result;
        }
    }
}
