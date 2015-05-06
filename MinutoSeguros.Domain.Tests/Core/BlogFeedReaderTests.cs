using MinutoSeguros.Domain;
using Moq;
using NUnit.Framework;
using System;
namespace MinutoSeguros.Tests.Core
{
    [TestFixture]    
    public class BlogFeedReaderTests
    {
        private Mock<IFeedReader> CreateFeedReader(params FeedEntry[] feedEntries)
        {
            var feedReader = new Mock<IFeedReader>();
            feedReader
                .Setup(e => e.Read(It.IsAny<string>()))
                .Returns(() => feedEntries);

            return feedReader;
        }

        [Test]
        public void When_Call_Initialize_Should_Invoke_Read()
        {
            var mock = CreateFeedReader(new FeedEntry(new Uri("http://test.org/"), "", "", ""));

            var reader = new BlogFeedReader(mock.Object);
            reader.Initialize("");

            mock.VerifyAll();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void When_Call_Analyze_On_Uninitialized_Reader_Should_Throw_InvalidOperationException()
        {
            var stub = CreateFeedReader(
                new FeedEntry(new Uri("http://test.org/"), "", "", "Casas 1"),
                new FeedEntry(new Uri("http://test.org/"), "", "", "Casas 2")
            );
            var reader = new BlogFeedReader(stub.Object);

            var statisct = reader.Analyze();
        }
        
        [Test]
        public void When_Call_Analyze_Should_Pass_FeedEntry()
        {
            var stub = CreateFeedReader(
                new FeedEntry(new Uri("http://test.org/"), "", "", "Casas 1"),
                new FeedEntry(new Uri("http://test.org/"), "", "", "Casas 2")
            );
            var reader = new BlogFeedReader(stub.Object);

            reader.Initialize("");
            var statisct = reader.Analyze();

            Assert.AreEqual(2, statisct.WordsFrequencys["Casas"]);
        }
    }
}
