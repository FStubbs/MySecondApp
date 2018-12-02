using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Options;
using MySecondApp.Library.Models;

namespace MySecondApp.Library.Repositories
{
    public class WordRepository : IWordRepository
    {
        private readonly IOptions<ApplicationSettings> _settings;

        public WordRepository(
            IOptions<ApplicationSettings> settings
        )
        {
            _settings = settings;
        }
        public string ReverseWord(string word)
        {
            var sb = new StringBuilder();
            foreach (var ch in word)
            {
                sb.Insert(0, ch);
            }
            return sb.ToString();
        }

        public IEnumerable<string> SayHello(string recipient, int iterations)
        {
            for(var i=0; i< iterations; i++) {
                yield return string.Format(_settings.Value.GreetingFormat, recipient, _settings.Value.Greeter);
            }
        }
    }
}