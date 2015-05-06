using System;
using System.Collections.Generic;

namespace MinutoSeguros.Domain
{
    public class BlogFeedReader
    {
        readonly IFeedReader feedreader;
        IEnumerable<FeedEntry> feedEntries;

        public BlogFeedReader(IFeedReader feedreader)
        {
            this.feedreader = feedreader;
        }

        public BlogFeedReader Initialize(string feedUri)
        {
            feedEntries = feedreader.Read(feedUri);
            return this;
        }

        public BlogFeedStatisct Analyze()
        {
            if (feedEntries == null)
                throw new InvalidOperationException("Call initialize before perform any operation over this type");

            return new BlogFeedStatisct(feedEntries);
        }
    }
}
