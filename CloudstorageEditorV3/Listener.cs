using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CloudstorageEditorV3
{
    class Http
    {
        HttpListenerContext context;

        public Http(HttpListenerContext context)
        {
            this.context = context;
        }

        public void Send(string Data)
        {
            context.Response.ContentLength64 = Encoding.UTF8.GetBytes(Data).Length;
            context.Response.OutputStream.Write(Encoding.UTF8.GetBytes(Data), 0, Encoding.UTF8.GetBytes(Data).Length);
        }
    }

    class Listener
    {
        HttpListener Server;
        public async Task Start()
        {
            Server = new HttpListener();
            Server.Prefixes.Add("http://127.0.0.1:5595/");
            Server.Start();

            while (true)
            {
                var context = await Server.GetContextAsync();
                var http = new Http(context);
                
                if (context.Request.Url.PathAndQuery == "/fortnite/api/cloudstorage/system")
                {
                    var Data = JsonConvert.SerializeObject(new
                    {
                        uniqueFilename = "3460cbe1c57d4a838ace32951a4d7171",
                        filename = "DefaultGame.ini",
                        hash = "603E6907398C7E74E25C0AE8EC3A03FFAC7C9BB4",
                        hash256 = "973124FFC4A03E66D6A4458E587D5D6146F71FC57F359C8D516E0B12A50AB0D9",
                        length = File.ReadAllText("DefaultGame.ini").Length,
                        contentType = "application/octet-stream",
                        uploaded = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fff'Z'"),
                        storageType = "S3",
                        doNotCache = false
                    });

                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = 200;
                    http.Send(Data);
                }

                if (context.Request.Url.PathAndQuery == "/fortnite/api/cloudstorage/system/3460cbe1c57d4a838ace32951a4d7171")
                {
                    var defaultGame = File.ReadAllText("DefaultGame.ini");

                    context.Response.ContentType = "application/octet-stream";
                    context.Response.StatusCode = 200;
                    http.Send(defaultGame);
                }

                if (context.Request.Url.PathAndQuery == "/fortnite/api/cloudstorage/system/config")
                {
                    var data = JsonConvert.SerializeObject(new
                    {
                    });

                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = 200;
                    http.Send(data);
                }

                if (context.Request.Url.PathAndQuery.Contains("/fortnite/api/cloudstorage/user"))
                {
                    var data = JsonConvert.SerializeObject(new
                    {
                    });

                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = 200;
                    http.Send(data);
                }
                
                http.Dispose();

            }
        }
    }
}
