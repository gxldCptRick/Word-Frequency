using FrequencyAnalysisLib.Analysis;
using FrequencyAnalysisTools.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FrequencyAnalysisTools.Pages
{
    /// <summary>
    /// Interaction logic for MonoalpabeticTool.xaml
    /// </summary>
    public partial class MonoalpabeticTool : Page
    {
        public MonoalpabeticTool()
        {
            InitializeComponent();
            data = new List<LetterContext>();
        }
        private TextStatistics stats;
        private List<LetterContext> data;

        private void ResetClicked(object sender, RoutedEventArgs e)
        {
            ResetState();
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
            PlainText.Document.Blocks.Clear();
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
            if (!string.IsNullOrWhiteSpace(GetStringFromTextBox(CypherText)))
            {
                Frequencies.Children.Clear();
                data.ForEach(e =>
                {
                    var letterUI = new LetterControl
                    {
                        DataContext = e
                    };
                    letterUI.LetterChanged += (letter, mapping) =>
                    {
                        SubstituteLetters();
                    };
                    Frequencies.Children.Add(letterUI);

                });
            }
        }

        private void CypherText_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBoxString = GetStringFromTextBox(CypherText);
            Count.Content = $"Total Letters: {textBoxString.Count((element) => char.IsLetter(element))}";
            GenerateFrequencies();
            UpdateFrequencies();
            this.GenerateAlphabits.IsEnabled = textBoxString.Length > 1;
        }
        private void GenerateAlphabits_Click(object sender, RoutedEventArgs e)
        {
            string encryptedAlphabet;
            string top = "";
            string bottom = "";
            foreach (var superDatto in data.OrderBy(datto => datto.Mapping))
            {
                top += (string.IsNullOrWhiteSpace(superDatto.Mapping) ? "?" : superDatto.Mapping) + " ";
                bottom += superDatto.Letter + " ";
            }
            encryptedAlphabet = $"{top}\n{bottom}";
            Alphabet.Content = encryptedAlphabet;
        }
    }
}
