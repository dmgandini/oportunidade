using MinutoSeguros.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
namespace MinutoSeguros.Tests.Core
{
    [TestFixture]
    public class BlogFeedStatisctTests
    {
        private IEnumerable<FeedEntry> CreateFeed(params string[] contents)
        {
            foreach (var content in contents)
                yield return new FeedEntry(new Uri("http://test.org/xpto"), "teste", "summary", content);
        }

        [Test]
        public void With_Prepositions_Should_Ignore()
        {
            var statisct = new BlogFeedStatisct(CreateFeed(string.Join(" ", "a", "ante", "após", "até", "com", "contra", "de")));
            Assert.AreEqual(0, statisct.WordsFrequencys.Count);
        }

        [Test]
        public void With_Articles_Should_Ignore()
        {
            var statisct = new BlogFeedStatisct(CreateFeed(string.Join(" ", "o", "a", "os", "as", "um", "uma", "uns", "umas")));
            Assert.AreEqual(0, statisct.WordsFrequencys.Count);
        }

        [Test]
        public void With_Commum_Worlds_Should_Ignore()
        {
            var statisct = new BlogFeedStatisct(CreateFeed(string.Join(" ", "é", "vão", "e", "você", "como", "post", "que")));
            Assert.AreEqual(0, statisct.WordsFrequencys.Count);
        }

        [Test]
        public void With_Standard_Phrase_Should_Ignore()
        {
            var statisct = new BlogFeedStatisct(CreateFeed(string.Join(" ", "No related posts.", "appeared first on Blog Minuto", "appeared first on")));
            Assert.AreEqual(0, statisct.WordsFrequencys.Count);
        }

        [Test]
        public void With_Two_Entries_Should_Count_Words()
        {
            var statisct = new BlogFeedStatisct(CreateFeed(string.Join(" ", "Casa e cozinha", "Pia com duas cubas")));

            Assert.AreEqual(1, statisct.WordsFrequencys["Casa"]);
            Assert.AreEqual(1, statisct.WordsFrequencys["cozinha"]);
            Assert.AreEqual(1, statisct.WordsFrequencys["Pia"]);
            Assert.AreEqual(1, statisct.WordsFrequencys["duas"]);
            Assert.AreEqual(1, statisct.WordsFrequencys["cubas"]);
            Assert.AreEqual(5, statisct.WordsFrequencys.Count());
        }

        [Test]
        public void With_Two_Entries_Should_Count_Words_And_Ignore_Case()
        {
            var statisct = new BlogFeedStatisct(CreateFeed(string.Join(" ", "Casa e cozinha", "casa com duas cubas")));
            Assert.AreEqual(2, statisct.WordsFrequencys["Casa"]);
        }
    }
}
