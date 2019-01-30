using FrequencyAnalysisLib.Analysis;
using FrequencyAnalysisTools.Data;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace FrequencyAnalysisTools
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            data = new List<LetterContext>();
        }

        private TextStatistics stats;
        private List<LetterContext> data;

        private void SubstituteLettersClicked(object sender, RoutedEventArgs e)
        {
            SubstituteLetters();
        }

        private void ResetClicked(object sender, RoutedEventArgs e)
        {
            ResetState();
        }

        private void GenerateFrequenciesClicked(object sender, RoutedEventArgs e)
        {
            GenerateFrequencies();
            UpdateFrequencies();
        }

        private void SubstituteLetters()
        {
            var text = GetStringFromTextBox(CypherText);
            text = text.ToUpper();
            var newText = "";
            var something = data.ToDictionary((d) => d.Letter);
            foreach (var character in text)
            {
                var stringCharacter = character.ToString();
                if (something.ContainsKey(stringCharacter) && something[stringCharacter].Mapping != "")
                {
                    newText += something[stringCharacter].Mapping;
                }
                else
                {
                    newText += character;
                }

            }

            AddTextToTextBox(PlainText, newText);
        }

        private void AddTextToTextBox(RichTextBox box, string text)
        {
            PlainText.Document.Blocks.Clear();
            PlainText.Document.Blocks.Add(new Paragraph(new Run(text)));
        }


        private string GetStringFromTextBox(RichTextBox text)
        {
            var range = new TextRange(text.Document.ContentStart, text.Document.ContentEnd);
            return range.Text;
        }

        private void ResetState()
        {
            CypherText.Document.Blocks.Clear();
            PlainText.Document.Blocks.Clear();
            data.Clear();
            stats = null;
            Frequencies.Children.Clear();
        }

        private void GenerateFrequencies()
        {
            var lennyKravitz = GetStringFromTextBox(CypherText);
            if (stats is null || stats.OriginalText != lennyKravitz)
            {
                stats = new TextStatistics(lennyKravitz);
                data.Clear();
                foreach (var count in stats.CharacterCount)
                {
                    var letterDatto = new LetterContext
                    {
                        Count = count.Value,
                        Letter = count.Key.ToString()
                    };
                    data.Add(letterDatto);
                }
                data = data.OrderByDescending(c => c.Count).ToList();
            }
            
        }


        private void UpdateFrequencies()
        {
            Frequencies.Children.Clear();
            data.ForEach(e =>
            {
                var letterUI = new LetterControl
                {
                    DataContext = e
                };
                Frequencies.Children.Add(letterUI);

            });
        }

        private void CypherText_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBoxString = GetStringFromTextBox(CypherText);

            Count.Content = $"Total Letters: {textBoxString.Count((element) => char.IsLetter(element))}";
        }
    }
}
