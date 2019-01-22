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
            //d.CreateAndSaveDataToFile("test", 10, 20);

            EAAlgorithm alg = new EAAlgorithm(35, 9, 20, 0.35);
            
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
            for (int i = 0; i < 500; i++)
            {
                lab.Add((i + 1).ToString());
            }
            Labels = lab.ToArray();

            DataContext = this;
            
            Iterate(alg, SeriesCollection);
            alg.SaveBest("best");
        }

        public void Iterate(EAAlgorithm alg, SeriesCollection scol)
        {
            
            for (int i = 0; i < 500; ++i)
            {
                alg.Run();
                SeriesCollection[0].Values.Add(alg.BestSolLog[i]);
                SeriesCollection[1].Values.Add(alg.LoadLog[i]);
                SeriesCollection[2].Values.Add(alg.PrefLog[i]);
                Console.WriteLine("InvalidDueToShiftsOverlap: \t" + (alg.InvalidDueToShiftsOverlap).ToString() + " out of " + alg.AllChecked.ToString());
                Console.WriteLine("InvalidDueToLocalization: \t" + (alg.InvalidDueToLocalization).ToString() + " out of " + alg.AllChecked.ToString());
                Console.WriteLine("InvalidDueToWorkingOver6Day: \t" + (alg.InvalidDueToWorkingOver6Day).ToString() + " out of " + alg.AllChecked.ToString());

            }
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

    }
}