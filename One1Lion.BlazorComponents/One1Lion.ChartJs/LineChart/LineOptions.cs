﻿using One1Lion.ChartJs.Common;
using One1Lion.ChartJs.Common.Axes;

namespace One1Lion.ChartJs.LineChart {
  /// <summary>
  /// The options-subconfig of a <see cref="LineConfig"/>
  /// </summary>
  public class LineOptions : BaseConfigOptions {
    /// <summary>
    /// The scales for this chart. You can use any <see cref="CartesianAxis"/> for x and y.
    /// </summary>
    public Scales Scales { get; set; }

    /// <summary>
    /// If false, the lines between points are not drawn.
    /// </summary>
    public bool? ShowLines { get; set; }

    /// <summary>
    /// If false, NaN data causes a break in the line.
    /// </summary>
    public bool? SpanGaps { get; set; }
  }
}
