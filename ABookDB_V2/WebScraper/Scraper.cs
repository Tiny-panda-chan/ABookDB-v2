using EpubCore;
using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using Models.Models;
using static WebScraper.ScrapedFileModel;

namespace WebScraper
{
    public class Scraper
    {
        private ScrapedFileModel _scrapedFile { get; set; }
        private HtmlDocument _doc { get; set; }
        private EpubWriter _docBuilder { get; set; }

        private string baseUrl { get; set; }
        public async Task<ScrapedFileModel> GetBookData(string url)
        {
            _scrapedFile = new ScrapedFileModel();
            var web = new HtmlWeb();
            baseUrl = new Uri(url).Host;
            web.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/124.0.0.0 Safari/537.36";
            _doc = web.Load(url);
            _docBuilder = new EpubWriter();


            GetBookInfo();
            ScrapeChapters(web);
            _docBuilder.SetTitle(_scrapedFile.BookData.Name);
            //_book.Book. = _docBuilder.Write();
            using (var memStream = new MemoryStream())
            {
                _docBuilder.Write(memStream);
                _scrapedFile.FileData = new ScrapedFileModel.File
                {
                    Data = memStream.ToArray(),
                    Name = _scrapedFile.BookData.Name + ".epub",
                    FileType = ".epub"
                };
            }
            return _scrapedFile;
        }

        public void GetBookInfo()
        {
            var bdt = new ScrapedFileModel.Book();
            bdt.Name = HtmlEntity.DeEntitize(_doc.DocumentNode.QuerySelector("h1.font-white").InnerHtml);
            var synopsisSelect = _doc.DocumentNode.QuerySelector("div.hidden-content");
            string synopsis = "";
            foreach (var item in synopsisSelect.ChildNodes.Where(cn => cn.Name == "p"))
            {
                if (item.QuerySelector("span") != null)
                {
                    synopsis += HtmlEntity.DeEntitize(item.QuerySelector("span").InnerHtml) + "\n";
                }
                else
                {
                    synopsis += HtmlEntity.DeEntitize(item.InnerHtml) + "\n";
                }
            }
            bdt.Description = synopsis;
            int pRes = 0;
            var chapters = _doc.DocumentNode.QuerySelector("span.label.label-default.pull-right");
            Int32.TryParse(new String(chapters.InnerHtml.Where(char.IsDigit).ToArray()), out pRes);
            bdt.TotalPages = pRes;
            _scrapedFile.BookData = bdt;
        }

        public void ScrapeChapters(HtmlWeb web)
        {
            List<string> chapterList = GetChapterList(new List<string>());
            foreach (var chapter in chapterList)
            {
                GetChapterText(web, chapter);
            }

        }

        public List<string> GetChapterList(List<string> chapters)
        {
            var chaptersOnPage = _doc.DocumentNode.QuerySelectorAll("tr.chapter-row");
            foreach (var chapter in chaptersOnPage)
            {
                chapters.Add("https://" + baseUrl + chapter.QuerySelector("a").Attributes["href"].Value);
            }

            return chapters;
        }

        public void GetChapterText(HtmlWeb web, string chapterUrl)
        {
            string result = "";
            HtmlDocument chapterDoc = web.Load(chapterUrl);
            string title = HtmlEntity.DeEntitize(chapterDoc.QuerySelector("h1.font-white.break-word").InnerHtml);

            var chapterParagraphs = chapterDoc.QuerySelector("div.chapter-inner.chapter-content");
            foreach (var item in chapterParagraphs.ChildNodes.Where(cn => cn.Name == "p"))
            {
                result += item.InnerText + "\n";
            }
            _docBuilder.AddChapter(title, result);
        }
    }
}