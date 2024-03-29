﻿using One1Lion.ChartJs.Common;

namespace One1Lion.ChartJs.BarChart {
  /// <summary>
  /// The options-subconfig of a <see cref="BarConfig"/>.
  /// </summary>
  public class BarOptions : BaseConfigOptions {
    /// <summary>
    /// Gets or sets the scales for this chart.
    /// </summary>
    public BarScales Scales { get; set; }
  }
}
