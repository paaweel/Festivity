using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using Festivity;
using LiveCharts;
using LiveCharts.Wpf;

namespace Wpf.CartesianChart.PointShapeLine
{
    public partial class PointShapeLineExample : UserControl
    {
        public PointShapeLineExample()
        {
            InitializeComponent();

            //DataCreator d = new DataCreator();
            //d.CreateAndSaveDataToFile("test", 5, 15);

            EAAlgorithm alg = new EAAlgorithm(20, 2, 10);

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Best solution",
                    Values = new ChartValues<double> {},
                    LineSmoothness = 0.3, //0: straight lines, 1: really smooth lines
                    PointGeometry = null//Geometry.Parse("m 25 70.36218 20 -28 -20 22 -8 -6 z"),
                    //PointGeometrySize = 50,
                    //PointForeground = Brushes.Gray
                },
                new LineSeries
                {
                    Title = "Fitness due to load",
                    Values = new ChartValues<double> {},
                    LineSmoothness = 0.3, //0: straight lines, 1: really smooth lines
                    PointGeometry = null//Geometry.Parse("m 25 70.36218 20 -28 -20 22 -8 -6 z"),
                    //PointGeometrySize = 50,
                    //PointForeground = Brushes.Gray
                },
                new LineSeries
                {
                    Title = "Fitness due to availabilty",
                    Values = new ChartValues<double> {},
                    LineSmoothness = 0.3, //0: straight lines, 1: really smooth lines
                    PointGeometry = null//Geometry.Parse("m 25 70.36218 20 -28 -20 22 -8 -6 z"),
                    //PointGeometrySize = 50,
                    //PointForeground = Brushes.Gray
                }
            };
            var lab = new List<string>(1000);
            for (int i = 0; i < 1000; i++)
            {
                lab.Add((i + 1).ToString());
            }
            Labels = lab.ToArray();

            DataContext = this;

            iterate(alg, SeriesCollection);
            alg.SaveBest("best");
        }

        public void iterate(EAAlgorithm alg, SeriesCollection scol)
        {
            
            for (int i = 0; i < 100; ++i)
            {
                alg.Run();
                SeriesCollection[0].Values.Add(alg.BestSolLog[i]);
                SeriesCollection[1].Values.Add(alg.LoadLog[i]);
                SeriesCollection[2].Values.Add(alg.PrefLog[i]);
            }
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

    }
}