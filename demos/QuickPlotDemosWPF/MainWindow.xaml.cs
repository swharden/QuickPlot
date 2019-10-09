using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace QuickPlotDemosWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetupMultiLayout();
        }

        private void SetupMultiLayout()
        {
            interactivePlot1.figure.Clear();

            interactivePlot1.figure.Subplot(3, 2, 1);
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(20), QuickPlot.Generate.Sin(20));
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(20), QuickPlot.Generate.Cos(20));

            interactivePlot1.figure.Subplot(3, 2, 2);
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(50), QuickPlot.Generate.Sin(50));
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(50), QuickPlot.Generate.Cos(50));

            interactivePlot1.figure.Subplot(3, 2, 3, colSpan: 2);
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(50), QuickPlot.Generate.Random(50, seed: 0));
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(50), QuickPlot.Generate.Random(50, seed: 1));

            interactivePlot1.figure.Subplot(3, 2, 5);
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(20), QuickPlot.Generate.Sin(20));

            interactivePlot1.figure.Subplot(3, 2, 6);
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(20), QuickPlot.Generate.Cos(20));
        }

        private void WpfDoEvents()
        {
            Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Background, new Action(delegate { }));
        }

        private void btnRender_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            interactivePlot1.Render();
            //WpfDoEvents();

            double elapsedSec = (double)stopwatch.ElapsedTicks / Stopwatch.Frequency;
            Title = string.Format("Single render in {0:0.00} ms ({1:0.00} Hz)", elapsedSec * 1000.0, 1 / elapsedSec);
        }

        private void btnBenchmark_Click(object sender, RoutedEventArgs e)
        {
            btnRender.IsEnabled = false;
            btnBenchmark.IsEnabled = false;

            Stopwatch stopwatch = Stopwatch.StartNew();

            int renderCount = 20;
            for (int i = 0; i < renderCount; i++)
            {
                interactivePlot1.Render();
                //WpfDoEvents();
            }

            double elapsedSec = (double)stopwatch.ElapsedTicks / Stopwatch.Frequency;
            elapsedSec /= renderCount;
            Title = string.Format("Mean render time: {0:0.00} ms ({1:0.00} Hz)", elapsedSec * 1000.0, 1 / elapsedSec);

            btnRender.IsEnabled = true;
            btnBenchmark.IsEnabled = true;
        }
    }
}
