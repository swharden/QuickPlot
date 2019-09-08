using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickPlot
{
    /// <summary>
    /// The Plot class holds data, scale, and styling information for a single subplot of a Figure.
    /// </summary>
    public class Plot
    {
        public Settings.Axes axes;
        public Settings.Advanced advancedSettings;
        public Settings.Subplot subplotSettings;
        public List<Plottables.Plottable> plottables;

        /// <summary>
        /// A Plot contains a data area, scales, and labels.
        /// A plot does not render itself (it is GDI-free)
        /// </summary>
        public Plot()
        {
            axes = new Settings.Axes();
            advancedSettings = new Settings.Advanced();
            subplotSettings = new Settings.Subplot(1, 1);
            plottables = new List<Plottables.Plottable>();
        }

        /// <summary>
        /// Clear all data from this plot
        /// </summary>
        public void Clear()
        {
            plottables.Clear();
        }

        /// <summary>
        /// Add a scatter plot
        /// </summary>
        public void Scatter(double[] xs, double[] ys, Plottables.LineStyle ls = null, Plottables.MarkerStyle ms = null)
        {
            Color color = Settings.Colors.GetColor10(plottables.Count);
            ls = ls ?? new Plottables.LineStyle(color: color);
            ms = ms ?? new Plottables.MarkerStyle(color: color);
            plottables.Add(new Plottables.Scatter(xs, ys, ls, ms));
        }
    }
}
