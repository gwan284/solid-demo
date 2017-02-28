using HtmlAgilityPack;
using System.IO;
using System.Net;

namespace Images.Crawler
{
    class PageRetriever : IPageRetriever
    {
        private ILogger Logger;
        public ILogger MyProperty
        {
            get
            {
                if (this.Logger == null)
                {
                    this.Logger = ServiceLocator.Find<ILogger>();
                }
                return Logger;
            }
            set { Logger = value; }
        }

        public HtmlDocument GetPage(string url)
        {
            var req = (HttpWebRequest)WebRequest.Create(url);

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            HtmlDocument doc = new HtmlDocument();

            var resultStream = resp.GetResponseStream();
            doc.Load(resultStream);

            return doc;
        }

        public byte[] GetImage(string url)
        {
            byte[] imageBytes;
            HttpWebRequest imageRequest = (HttpWebRequest)WebRequest.Create(url);
            WebResponse imageResponse = imageRequest.GetResponse();

            Logger.Log("downloading image: " + url);

            var responseStream = imageResponse.GetResponseStream();

            using (var binaryReader = new BinaryReader(responseStream))
            {
                imageBytes = binaryReader.ReadBytes((int)imageResponse.ContentLength);
                binaryReader.Close();
            }

            responseStream.Close();
            imageResponse.Close();

            return imageBytes;
        }
    }
}
