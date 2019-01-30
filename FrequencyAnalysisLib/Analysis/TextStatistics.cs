using System;
using System.Collections.Generic;
using System.Linq;

namespace FrequencyAnalysisLib.Analysis
{
    public class TextStatistics
    {
        public static readonly char[] alphabits;

        static TextStatistics()
        {
            alphabits = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToArray();
        }
        public string OriginalText { get; private set; }

        public IDictionary<char, int> CharacterCount { get; private set; }

        public IDictionary<char, double> CharacterFrequencies { get; private set; }

        public TextStatistics(string textToAnalyize)
        {
            if (textToAnalyize is null) throw new ArgumentNullException(nameof(textToAnalyize));
            //if (string.IsNullOrWhiteSpace(textToAnalyize)) throw new ArgumentException("I cannot analyze an empty string.", nameof(textToAnalyize));
            OriginalText = textToAnalyize;
            AnalyizeText(textToAnalyize);
        }

        private void AnalyizeText(string textToAnalyize)
        {
            var characterCount = CountCharacters(textToAnalyize);
            var frequencies = CalculateFrequencies(characterCount, textToAnalyize);
            CharacterCount = characterCount;
            CharacterFrequencies = frequencies;
        }

        private IDictionary<char, int> CountCharacters(string textToAnalyize)
        {
            textToAnalyize = textToAnalyize.ToUpper();
            var characterCounts = CreateInitialCharacterCounts<int>();
            
            foreach (var character in textToAnalyize)
            {
                if (char.IsLetter(character))
                {
                    characterCounts[character]++;
                }
            }
            return characterCounts;
        }

        private IDictionary<char, double> CalculateFrequencies(IDictionary<char, int> characterCount, string textToAnalyize)
        {
            var frequencies = CreateInitialCharacterCounts<double>();
            var totalCharacters = (double) textToAnalyize.Count(c => char.IsLetter(c));
            foreach (var count in characterCount)
            {
                frequencies[count.Key] = (count.Value / (totalCharacters)) * 100;
            }

            return frequencies;
        }

        private IDictionary<char, TValue> CreateInitialCharacterCounts<TValue>()
        {
            IDictionary<char, TValue> stuffAndThings = new SortedDictionary<char, TValue>();
            foreach (var item in alphabits)
            {
                stuffAndThings[item] = default(TValue);
            }
            return stuffAndThings;
        }
    }
}
