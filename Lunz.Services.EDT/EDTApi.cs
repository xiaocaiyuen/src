using Lunz.Data.EDT;
using Lunz.Web.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lunz.Services.EDT
{
    public class EDTApi
    {
        private const string URL_Test = "basic/District/DistrictByID";

        private static EDTApi _current;
        public static EDTApi Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new EDTApi();
                }
                return _current;
            }
        }

        private string _baseUrl;
        public string BaseUrl
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_baseUrl))
                {
                    _baseUrl = System.Configuration.ConfigurationManager.AppSettings["ApiBaseUrl"];
                }
                return _baseUrl;
            }
            set
            {
                _baseUrl = value;
            }
        }

        private string _appKey;
        public string AppKey
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_appKey))
                {
                    _appKey = System.Configuration.ConfigurationManager.AppSettings["AppKey"];
                }
                return _appKey;
            }
            set
            {
                _appKey = value;
            }
        }

        private JsonWebClient _webClient;
        private JsonWebClient WebClient
        {
            get
            {
                if (_webClient == null)
                {
                    _webClient = new JsonWebClient();
                }
                return _webClient;
            }
        }

        //public WebApiResult<Basic_District> Test()
        //{
        //    return WebClient.Get<WebApiResult<Basic_District>>(GetURL(URL_Test), new { cityId = "e03b545f-04ae-4470-82c8-054b9197ffa9" });
        //}

        protected string GetURL(string url)
        {
            return string.Format("http:{0}api/{1}?appKey={2}", BaseUrl, url, AppKey);
        }
    }
}
