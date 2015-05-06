using System;
using System.Collections.Generic;
using System.Linq;
namespace MinutoSeguros.Domain
{
    public class BlogFeedStatisct
    {
        readonly static IEnumerable<string> unwantedWords;
        readonly static IEnumerable<string> unwantedPhrases;
        readonly IEnumerable<FeedEntry> feedEntries;
        IDictionary<string, int> wordsFrequencys;

        static BlogFeedStatisct()
        {
            unwantedWords = new[]
            { 
                //Preposicao
                "a", "ante", "após", "até", "com", "contra", "de", "desde", "em", "entre", "para", "per", "perante", "por", "sem", "sob", "sobre", "trás",
                //Artigos Definidos e indefinidos
                "o", "a", "os", "as", "um", "uma", "uns", "umas",
                //Artigos + Preposicao
                "ao", "à", "aos", "às", "de", "do", "da", "dos", "das", "dum", "duma", "duns", "dumas", "em", "no", "na", "nos", "nas", "num", "numa", "nuns", "numas", "por", "pelo", "pela", "pelos", "pelas",
                //Pontuacao
                ".", "!", ",", "?",
                //conjunções coordenativas
                "e", "nem", "mas", "porém", "contudo", "todavia", "entretanto", "ou", "ora", "já", "quer", "pois" , "portanto", "porque", "que",
                //conjunções subordinativas
                "que", "se", "porque", "como", "se", "caso", "que", "como", "conforme", "segundo", "embora",  
                //Advérbios
                "não", "muito", "pouco", "talvez", "também", "bem", "mal", "assim", "mais", "ainda",
                //Pronomes possesivos
                "Meu", "minha", "meus",	"minhas", "Teu","Tua","teus","Tuas", "Seu",	"sua",	"seus",	"Suas", "Nosso",	
                "nossa", "nossos","Nossas", "Vosso", "vossa", "Vossos",	"vossas", "seu", "sua",	"seus",	"Suas",
                //Pronomes
                "eu", "tu", "ele", "ela", "eles", "elas", "nós", "vós", "eles", "você", "se",
                //Verbos comuns
                "é", "foi", "são", "ser", "vão", "iram",
                //Palavras comuns
                "post", "posts"
            }
            .Distinct()
            .OrderBy(k => k);

            unwantedPhrases = new[] 
            { 
                "No related posts.", 
                "appeared first on Blog Minuto", 
                "appeared first on",
                //conjunções coordenativas
                "mas também",
                "quer logo",
                //conjunções subordinativas
                "já que", "visto que", "desde que", "contanto que", "de modo que", "assim como", "se bem que"
            };
        }

        public BlogFeedStatisct(IEnumerable<FeedEntry> feedEntries)
        {
            this.feedEntries = feedEntries;
        }

        public IDictionary<string, int> WordsFrequencys
        {
            get
            {
                if (wordsFrequencys != null) return wordsFrequencys;
                return wordsFrequencys = AnalyseWordsFrequency();
            }
        }

        private IDictionary<string, int> AnalyseWordsFrequency()
        {
            return feedEntries
                .Select(s => RemoveUnwantedPhrases(s.Content))
                .SelectMany(s => s.Split(' '))
                .Where(p => p.Length != 0 && !unwantedWords.Contains(p, StringComparer.InvariantCultureIgnoreCase))
                .GroupBy(k => k, StringComparer.InvariantCultureIgnoreCase)
                .Select(s => new { word = s.Key, frequency = s.Count() })
                .OrderByDescending(k => k.frequency)
                .ToDictionary(k => k.word, e => e.frequency);
        }

        private string RemoveUnwantedPhrases(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            foreach (var phrase in unwantedPhrases)
                input = input.Replace(phrase, "");

            return input;
        }
    }
}