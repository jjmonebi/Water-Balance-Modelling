using LiveCharts;
using LiveCharts.Wpf;
using MWBModels.Model;
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
using System.Windows.Shapes;

namespace MWBModels
{
    /// <summary>
    /// Interaction logic for GraphWindow.xaml
    /// </summary>
    public partial class GraphWindow : Window
    {
        public GraphWindow(List<DataSheet> resultSheet, ObservableCollection model)
        {
            InitializeComponent();

            List<string> times = new List<string>();
            List<double> PREs = new List<double>();
            List<double> PETs = new List<double>();
            List<List<double>> AETss = new List<List<double>>();
            List<List<double>> SoilMoisturess = new List<List<double>>();
            List<List<double>> Runoffss = new List<List<double>>();

            foreach (var sheet in resultSheet)
            {
                List<double> AETs = new List<double>();
                List<double> SoilMoistures = new List<double>();
                List<double> Runoffs = new List<double>();

                for (int i = 0; i < sheet.Data.Count(); i++)
                {
                    foreach (var item in sheet.Data[i].GetType().GetProperties(System.Reflection.BindingFlags.Instance|System.Reflection.BindingFlags.Public))
                    {
                        switch (item.Name)
                        {
                            case "Time":
                                var x = item.GetValue(sheet.Data[i]);
                                if (!times.Contains(x))
                                    times.Add(x.ToString());
                                break;
                            case "PRE":
                                var x1 = double.Parse(item.GetValue(sheet.Data[i]).ToString());
                                if (!PREs.Contains(x1))
                                    PREs.Add(x1);
                                break;
                            case "PET":
                                var x2 = double.Parse(item.GetValue(sheet.Data[i]).ToString());
                                if (!PETs.Contains(x2))
                                    PETs.Add(x2);
                                break;
                            case "AET":
                                if (model.AET)
                                    AETs.Add(double.Parse(item.GetValue(sheet.Data[i]).ToString()));
                                break;
                            case "SoilMoisture":
                                if (model.SMC)
                                    SoilMoistures.Add(double.Parse(item.GetValue(sheet.Data[i]).ToString()));
                                break;
                            case "Runoff":
                                if (model.Runoff)
                                    Runoffs.Add(double.Parse(item.GetValue(sheet.Data[i]).ToString()));
                                break;
                        }
                    }
                }
                
                if (AETs.Count > 0)
                    AETss.Add(AETs);
                if (SoilMoistures.Count > 0)
                    SoilMoisturess.Add(SoilMoistures);
                if (Runoffs.Count > 0)
                    Runoffss.Add(Runoffs);
            }

            if (model.PRE)
            {
                var collection = new SeriesCollection()
                {
                    new LineSeries
                    {
                        Title = "PRE",
                        Values = new ChartValues<double>(PREs),
                        PointGeometry = null
                    }
                };
                GraphControl control = new GraphControl(collection, times.ToArray());
                panel.Children.Add(control);
            }
            if (model.PET)
            {
                var collection = new SeriesCollection()
                {
                    new LineSeries
                    {
                        Title = "PET",
                        Values = new ChartValues<double>(PETs),
                        PointGeometry = null
                    }
                };
                GraphControl control = new GraphControl(collection, times.ToArray());
                panel.Children.Add(control);
            }
            if (model.AET)
            {
                var collection = new SeriesCollection();
                for (int i = 0; i < AETss.Count; i++)
                {
                    collection.Add(new LineSeries
                    {
                        Title = "AET-" + resultSheet[i].Title,
                        Values = new ChartValues<double>(AETss[i]),
                        PointGeometry = null
                    });
                }
                GraphControl control = new GraphControl(collection, times.ToArray());
                panel.Children.Add(control);
            }
            if (model.SMC)
            {
                var collection = new SeriesCollection();
                for (int i = 0; i < SoilMoisturess.Count; i++)
                {
                    collection.Add(new LineSeries
                    {
                        Title = "Soil Moisture-" + resultSheet[i].Title,
                        Values = new ChartValues<double>(SoilMoisturess[i]),
                        PointGeometry = null
                    });
                }
                GraphControl control = new GraphControl(collection, times.ToArray());
                panel.Children.Add(control);
            }
            if (model.Runoff)
            {
                var collection = new SeriesCollection();
                for (int i = 0; i < Runoffss.Count; i++)
                {
                    collection.Add(new LineSeries
                    {
                        Title = "Runoff-" + resultSheet[i].Title,
                        Values = new ChartValues<double>(Runoffss[i]),
                        PointGeometry = null
                    });
                }
                GraphControl control = new GraphControl(collection, times.ToArray());
                panel.Children.Add(control);
            }
        }
    }
}
