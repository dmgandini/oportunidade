using MinutoSeguros.Domain.Infra;
using NUnit.Framework;
using System.Linq;
namespace MinutoSeguros.Tests.Infra
{
    [TestFixture]
    public class FeedReaderTests
    {
        [Test]
        [Category("Integration")]
        public void With_Valid_Uri_Should_Download_The_Feed()
        {
            var feedReader = new FeedReader();
            var entries = feedReader.Read("http://www.minutoseguros.com.br/blog/feed/");
            Assert.IsTrue(entries.Count() > 0);
        }
    }
}