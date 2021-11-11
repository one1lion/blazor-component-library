using One1Lion.ChartJs.Common;
using One1Lion.ChartJs.Common.Enums;

namespace One1Lion.ChartJs.BubbleChart {
  /// <summary>
  /// Represents the config for a bubble chart.
  /// </summary>
  public class BubbleConfig : ConfigBase<BubbleOptions> {
    /// <summary>
    /// Creates a new instance of <see cref="BubbleConfig"/>.
    /// </summary>
    public BubbleConfig() : base(ChartType.Bubble) { }
  }
}
