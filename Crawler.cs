using HtmlAgilityPack;
using System;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using System.IO;

public class Crawler
{
    public void Process()
    {
        var url = @"http://www.rozetka.ua/";

        var imageURLS = collectImageURLS(url);

        processURLS(urls, url);

        Console.WriteLine("done!");
    }

    private IList<string> collectImageURLS(string url)
    {
        var req = (HttpWebRequest)WebRequest.Create(url);

        HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
        HtmlDocument doc = new HtmlDocument();

        var resultStream = resp.GetResponseStream();
        doc.Load(resultStream);

        Console.WriteLine("crawling data");

        var body = doc.DocumentNode.ChildNodes.Single(node => node.Name == "html").ChildNodes.Single(node => node.Name == "body");

        var footer = body.ChildNodes.Single(node => node.Id == "main-footer");

        var containerFooter = footer.ChildNodes.Single(node => node.Name == "div");

        var imgs = containerFooter.ChildNodes["div"].ChildNodes.Where(node => node.Name == "img");

        var urls = new List<string>();

        foreach (var img in imgs)
        {
            urls.Add(img.Attributes["src"].Value);
        }

        return urls;
    }

    private void processURLS(IList<string> urls, string baseUrl)
    {
        foreach (var url in urls)
        {

            byte[] imageBytes;
            HttpWebRequest imageRequest = (HttpWebRequest)WebRequest.Create(baseUrl + url);
            WebResponse imageResponse = imageRequest.GetResponse();

            Console.WriteLine("downloading image");

            Stream responseStream = imageResponse.GetResponseStream();

            using (BinaryReader br = new BinaryReader(responseStream))
            {
                imageBytes = br.ReadBytes((int)imageResponse.ContentLength);
                br.Close();
            }
            responseStream.Close();
            imageResponse.Close();

            FileStream fs = new FileStream("../../images/" + url.Split('/').Last(), FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            try
            {
                bw.Write(imageBytes);
            }
            finally
            {
                fs.Close();
                bw.Close();
            }

        }

    }
}
