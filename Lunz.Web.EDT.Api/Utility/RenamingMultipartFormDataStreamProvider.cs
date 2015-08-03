using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Lunz.Web.EDT.Api.Utility
{
    public class RenamingMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public string Root { get; private set; }
        //public Func<FileUpload.PostedFile, string> OnGetLocalFileName { get; set; }

        public RenamingMultipartFormDataStreamProvider(string root)
            : base(root)
        {
            Root = root;
        }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            string filePath = headers.ContentDisposition.FileName;

            // Multipart requests with the file name seem to always include quotes.
            if (filePath.StartsWith(@"""") && filePath.EndsWith(@""""))
                filePath = filePath.Substring(1, filePath.Length - 2);

            var fileName = Path.GetFileName(filePath);

            return fileName;
        }

    }
}