using System;
using System.Text.RegularExpressions;
namespace MinutoSeguros.Domain
{
    public class FeedEntry
    {
        readonly Uri link;
        readonly string title;
        readonly string summary;
        readonly string content;

        public FeedEntry(Uri link, string title, string summary, string content)
        {
            this.link = link;
            this.title = SanitizeHtml(title);
            this.summary = SanitizeHtml(summary);
            this.content = SanitizeHtml(content);
        }

        public Uri Link { get { return link; } }

        public string Title { get { return title; } }

        public string Summary { get { return summary; } }

        public string Content { get { return content; } }

        private string SanitizeHtml(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            input = Regex
                .Replace(input, @"<[^>]+>|&nbsp;|#8230", "")
                .Trim();

            return Regex.Replace(input, @"\s{2,}", " ");
        }
    }
}
