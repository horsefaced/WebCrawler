using System;
using Xunit;
using WebCrawler;
using System.Net;

namespace WebCrawlerTest
{
    public class CrawlerTest
    {
        [Fact]
        public void CanGetBaidu()
        {
            var crawler = new Crawler();
            var res = crawler.Get("www.baidu.com").End();
            Assert.Equal(res.Status, HttpStatusCode.OK);
        }
    }
}
