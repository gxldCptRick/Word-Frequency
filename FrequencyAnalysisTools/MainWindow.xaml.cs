using FrequencyAnalysisLib.Analysis;
using FrequencyAnalysisTools.Data;
using FrequencyAnalysisTools.Pages;
using System;
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
            PagesVisited = new Dictionary<Type, Page>();
        }

        Dictionary<Type, Page> PagesVisited;
        Page currentPage;
        private Page GetPageToVisit(Type type)
        {
            if (type is null) throw new ArgumentNullException(nameof(type));
            if (PagesVisited.ContainsKey(type))
            {
                return PagesVisited[type];
            }
            else
            {
               var ctor = type.GetConstructor(new Type[0]);
               var obj = ctor?.Invoke(new object[0]);
                PagesVisited[type] = obj as Page;
               return PagesVisited[type];
            }
        }

        private void NavigateToType(Type type)
        {
            if (!type.IsSubclassOf(typeof(Page))) throw new ArgumentException("type must be subclass of page", nameof(type));
            if (currentPage != GetPageToVisit(type))
            {
                currentPage = GetPageToVisit(type);
                BestFrame.Navigate(GetPageToVisit(type));
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NavigateToType(typeof(MonoalpabeticTool));
        }

        private void PolyClicked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Wont Do Nothing");
        }

        private void MonoClicked(object sender, RoutedEventArgs e)
        {
            NavigateToType(typeof(MonoalpabeticTool));
        }
    }
}
