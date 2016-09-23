using System;
using System.Linq;
using System.Net;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Dosyaİndirme
{
    public class Program
    {
        static void Main(string[] args)
        {
            WebRequest request = WebRequest.Create("https://www.googleapis.com/customsearch/v1?q=filetype:ppt+las+vegas&key=AIzaSyCtC2bM46MOmG7oXtWrIf7UN_qsK_6o59M&cref&exactTerms");

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream dataStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(dataStream);

            string responseFromServer = reader.ReadToEnd();

            WebClient myWebClient = new WebClient();

            var json = JObject.Parse(responseFromServer);

            foreach (var sampleItem in json["items"].Take(3))
            {
                string sampleItemValue = sampleItem["link"].ToString();
                var title = sampleItem["title"].ToString().Substring(0, 18);
                myWebClient.DownloadFile((sampleItemValue), @"C:\Users\RAMACO\Desktop" + title + ".ppt");

                Console.WriteLine(sampleItemValue);
            }

            reader.Close();
            dataStream.Close();
            response.Close();
        }
    }
}
