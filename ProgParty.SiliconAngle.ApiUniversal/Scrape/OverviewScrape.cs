using HtmlAgilityPack;
using ProgParty.SiliconAngle.ApiUniversal.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace ProgParty.SiliconAngle.ApiUniversal.Scrape
{
    internal class OverviewScrape
    {
        private Parameter.OverviewParameter Parameters { get; set; }

        public OverviewScrape(Parameter.OverviewParameter parameters)
        {
            Parameters = parameters;
        }

        public List<OverviewResult> Execute()
        {
            string url = ConstructUrl();

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Host = "www.siliconangle.com";
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/44.0.2403.125 Safari/537.36");
                //client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");

                var response = client.GetAsync(url).Result;
                var result = response.Content.ReadAsStringAsync().Result;
                
                return ConvertToResult(result);
            }
        }

        public string ConstructUrl() => $"http://www.siliconangle.com{GetBaseUrl()}/page/{Parameters.Paging}";

        private string GetBaseUrl()
        {
            switch (Parameters.Category)
            {
                case Parameter.ArticleCategory.All: return string.Empty;
                case Parameter.ArticleCategory.Cloud: return "/cloud";
                case Parameter.ArticleCategory.Mobile: return "/mobile";
                case Parameter.ArticleCategory.Social: return "/social";
                case Parameter.ArticleCategory.BigData: return "/big-data";
                case Parameter.ArticleCategory.BleedingEdge: return "/bleeding-edge";

                default:
                    throw new Exception("Not implemented the parameter: " + Parameters.Category);
            }
        }

        public List<OverviewResult> ConvertToResult(string result)
        {
            List<OverviewResult> overviewResult = new List<OverviewResult>();

            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(result);
            var node = htmlDocument.DocumentNode;

            var sectionNode = node.Descendants("section").SingleOrDefault(c => c.Attributes["class"]?.Value?.Contains("posts") ?? false);
            if (sectionNode == null)
                return overviewResult;

            var posts = sectionNode.Descendants("article").Where(c => c.Attributes["class"]?.Value?.Contains("post") ?? false);

            foreach (var post in posts)
            {
                overviewResult.Add(ConvertSingleResult(post));
            }

            return overviewResult;
        }

        public OverviewResult ConvertSingleResult(HtmlNode node)
        {
            var aDiv = node.Descendants("div").FirstOrDefault(c => c.Attributes["class"]?.Value == "post-cover-container").Descendants("a").FirstOrDefault();

            var o = new OverviewResult()
            {
                Title = (node.Descendants("h2").FirstOrDefault()?.Descendants("a")?.FirstOrDefault()?.InnerText ?? "Title not found").Trim(),
                SubTitle = node.Descendants("p")?.FirstOrDefault(c => c.Attributes["class"]?.Value == "description")?.InnerText ?? "Sub title not found",
                ImageUrl = aDiv?.Descendants("img")?.FirstOrDefault()?.Attributes["src"]?.Value ?? string.Empty,
                Url = aDiv?.Attributes["href"]?.Value ?? string.Empty
            };

            //Uri temp;
            //if(Uri.TryCreate(aDiv?.Attributes["href"]?.Value ?? string.Empty, UriKind.Absolute, out temp))
            //    o.Url = temp;
            //else
            //    o.Url = new Uri("");
            o.Title = System.Net.WebUtility.HtmlDecode(o.Title);
            o.SubTitle = System.Net.WebUtility.HtmlDecode(o.SubTitle);
            return o;
        }
    }
}
