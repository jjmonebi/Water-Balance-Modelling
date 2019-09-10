using MWBModels.Model;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace MWBModels
{
    /// <summary>
    /// Interaction logic for ResultWindow.xaml
    /// </summary>
    public partial class ResultWindow : Window
    {
        public ResultWindow()
        {
            InitializeComponent();
        }

        public ResultWindow(List<DataSheet> resultSheet)
        {
            InitializeComponent();
            foreach (var sheet in resultSheet)
            {
                GroupBox group = new GroupBox() { Header = sheet.Title, Margin = new Thickness(10) };
                DataGrid grid = new DataGrid() { Margin = new Thickness(5) };
                grid.AutoGenerateColumns = false;
                for (int i = 0; i < sheet.Data[0].GetType().GetProperties().Count(); i++)
                {
                    var col = new DataGridTextColumn();
                    var ps = sheet.Data[0].GetType().GetProperties();
                    //Here i bind to the various indices.
                    col.Header = ps[i].Name;
                    var binding = new Binding(ps[i].Name);
                    col.Binding = binding;
                    grid.Columns.Add(col);
                }
                grid.ItemsSource = sheet.Data;
                group.Content = grid;
                panel.Children.Add(group);
            }
        }
    }
}
