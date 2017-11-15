using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler
{
    public class Response
    {
        private byte[] responseResult = null;
        private Encoding encoding;

        internal Response(HttpWebResponse httpWebResponse)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    httpWebResponse.GetResponseStream().CopyTo(ms);
                    this.responseResult = ms.ToArray();
                }

                this.Status = httpWebResponse.StatusCode;
                this.StatusDesc = httpWebResponse.StatusDescription;
                this.Headers = httpWebResponse.Headers;

                if (!string.IsNullOrEmpty(httpWebResponse.CharacterSet))
                    this.encoding = Encoding.GetEncoding(httpWebResponse.CharacterSet);
                else
                    this.encoding = Encoding.UTF8;
            }
            finally
            {
                httpWebResponse.Close();
            }
        }

        public HttpStatusCode Status { get; private set; }

        public string StatusDesc { get; private set; }

        public WebHeaderCollection Headers { get; private set; }

        public string Text
        {
            get
            {
                if (this.responseResult == null) return null;
                else return this.encoding.GetString(this.responseResult);
            }
        }

        public Response Chartset(string value)
        {
            this.encoding = Encoding.GetEncoding(value);
            return this;
        }

        public JObject Body
        {
            get
            {
                try
                {
                    return JObject.Parse(this.Text);
                }
                catch (JsonReaderException)
                {
                    return null;
                }
            }
        }

        public string ToFile(string filename)
        {
            using (FileStream fs = File.OpenWrite(filename))
            {
                fs.Write(responseResult, 0, responseResult.Length);
                fs.Flush();
            }
            return filename;
        }

    }
}
