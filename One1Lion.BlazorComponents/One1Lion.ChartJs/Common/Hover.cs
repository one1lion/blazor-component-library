using One1Lion.ChartJs.Common.Enums;

namespace One1Lion.ChartJs.Common {
  /// <summary>
  /// The hover configuration contains general settings regarding hovering over a chart.
  /// </summary>
  public class Hover {
    /// <summary>
    /// Gets or sets which elements appear in the tooltip.
    /// </summary>
    public InteractionMode Mode { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether or not the hover mode only applies
    /// when the mouse position intersects an item on the chart.
    /// </summary>
    public bool? Intersect { get; set; }

    /// <summary>
    /// Gets or sets which directions are used in calculating distances.
    /// Defaults to <see cref="AxisDirection.X"/> for <see cref="Mode"/> == <see cref="InteractionMode.Index"/>
    /// and <see cref="AxisDirection.XY"/> for <see cref="Mode"/> == <see cref="InteractionMode.Dataset"/> or <see cref="InteractionMode.Nearest"/>.
    /// </summary>
    public AxisDirection Axis { get; set; }

    /// <summary>
    /// Gets or sets the duration in milliseconds it takes to animate hover style changes.
    /// </summary>
    public long? AnimationDuration { get; set; }
  }
}
