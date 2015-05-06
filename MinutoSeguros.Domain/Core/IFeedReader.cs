using System.Collections.Generic;

namespace MinutoSeguros.Domain
{
    public interface IFeedReader
    {
        IEnumerable<FeedEntry> Read(string feedUri);
    }
}
