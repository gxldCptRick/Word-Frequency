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

namespace FrequencyAnalysisTools
{
    /// <summary>
    /// Interaction logic for LetterControl.xaml
    /// </summary>
    public partial class LetterControl : UserControl
    {

        public event Action<string, string> LetterChanged;

        public LetterControl()
        {
            InitializeComponent();
        }

        string lastText = null;
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (lastText is null && sender is TextBox box)
            {
                lastText = box.Text;
                if (DataContext is LetterContext c)
                {
                    c.Mapping = lastText;
                    LetterChanged?.Invoke(c.Letter, lastText);
                }
            }
            else
            {
                if (sender is TextBox b)
                {
                    if (b.Text.Length == 0)
                    {
                        lastText = null;
                    }
                    else
                    {
                        b.Text = lastText;
                    }
                }
            }
        }
    }
}
