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
