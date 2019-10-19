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

namespace WpfDemos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DemoLayout();
        }

        public void DemoLayout()
        {
            interactivePlot1.figure.Clear();

            var plotA = interactivePlot1.figure.Subplot(2, 1, 1);
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(20), QuickPlot.Generate.Sin(20));
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(20), QuickPlot.Generate.Cos(20));
            plotA.title.text = "primary plot";

            var plotB = interactivePlot1.figure.Subplot(2, 1, 2);
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(50), QuickPlot.Generate.Sin(50));
            interactivePlot1.figure.plot.Scatter(QuickPlot.Generate.Consecutative(50), QuickPlot.Generate.Cos(50));
            plotB.title.text = "secondary plot";
        }
    }
}
