using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;
namespace MinutoSeguros.Domain.Infra
{
    public class FeedReader : IFeedReader
    {
        public IEnumerable<FeedEntry> Read(string feedUri)
        {
            SyndicationFeed feed = null;
            using (var reader = XmlReader.Create(feedUri))
            {
                feed = SyndicationFeed.Load(reader);
                reader.Close();
            }

            foreach (var item in feed.Items.OrderBy(k => k.PublishDate))
            {
                yield return new FeedEntry(
                    item.Links.Select(s => s.Uri).FirstOrDefault(),
                    item.Title.Text,
                    item.Summary.Text,
                    item.ElementExtensions.Where(p => p.OuterName == "encoded").Select(s => s.GetObject<string>()).FirstOrDefault());
            }
        }
    }
}
