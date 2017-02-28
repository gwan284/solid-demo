using HtmlAgilityPack;

namespace Images.Crawler
{
    public interface IPageRetriever
    {
        byte[] GetImage(string url);

        HtmlDocument GetPage(string baseUrl);
    }
}