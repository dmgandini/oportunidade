using MinutoSeguros.Domain;
using MinutoSeguros.Domain.Infra;
using System;
using System.Linq;
namespace MinutoSegurosConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var reader = new BlogFeedReader(new FeedReader());

            foreach (var x in reader
                                    .Initialize("http://www.minutoseguros.com.br/blog/feed/")
                                    .Analyze()
                                    .WordsFrequencys
                                    .Take(10))
            {
                Console.WriteLine("Palavra: {0} Frequência: {1}", x.Key, x.Value);
            }

            Console.Read();

        }
    }
}
