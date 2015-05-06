using MinutoSeguros.Domain;
using NUnit.Framework;
namespace MinutoSeguros.Tests.Core
{
    [TestFixture]
    public class FeedEntryTest
    {
        [Test]
        public void With_Null_Attributes_Should_Accept_As_It_Is()
        {
            var feed = new FeedEntry(null, null, null, null);

            Assert.AreEqual(null, feed.Content);
            Assert.AreEqual(null, feed.Link);
            Assert.AreEqual(null, feed.Summary);
            Assert.AreEqual(null, feed.Title);
        }

        [Test]
        public void With_NoHtml_Content_Attributes_Should_Accept_As_It_Is()
        {
            var defaultTestWord = "Test default";
            var feed = new FeedEntry(null, null, null, defaultTestWord);
            Assert.AreEqual(defaultTestWord, feed.Content);
        }

        [Test]
        public void With_TwoSpace_Summary_Should_Return_Only_One()
        {
            var feed = new FeedEntry(null, null, "Test  default", null);
            Assert.AreEqual("Test default", feed.Summary);
        }

        [Test]
        public void With_HTML_Title_Should_Remove_Html()
        {
            var feed = new FeedEntry(null, "<html>XPTO</br ></html>", null, null);
            Assert.AreEqual("XPTO", feed.Title);
        }

        [Test]
        public void With_Only_Html_Content_Should_Return_Empty_String()
        {
            var feed = new FeedEntry(null, null, null, "<div class=\"content\" itemprop=\"articleBody\">");
            Assert.AreEqual(string.Empty, feed.Content);
        }
    }
}