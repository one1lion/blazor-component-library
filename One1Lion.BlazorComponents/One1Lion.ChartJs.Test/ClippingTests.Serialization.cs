using Newtonsoft.Json;
using One1Lion.ChartJs.Common;
using System.Globalization;
using Xunit;

namespace One1Lion.ChartJs.Tests {
  public partial class ClippingTests {
    [Theory]
    [InlineData(0)]
    [InlineData(-100)]
    [InlineData(100)]
    [InlineData(int.MinValue)]
    [InlineData(int.MaxValue)]
    public void Serialize_AllEdgesEqual_AsRoot(int value) {
      // Arrange
      string Expected = value.ToString(CultureInfo.InvariantCulture);
      Clipping clipping = new Clipping(value);

      // Act
      string serialized = JsonConvert.SerializeObject(clipping);

      // Assert
      Assert.Equal(Expected, serialized);
    }

    [Fact]
    public void Serialize_DifferentEdges_AsRoot() {
      // Arrange
      const string Expected = "{\"Top\":0,\"Right\":false,\"Bottom\":10,\"Left\":-100}";
      Clipping clipping = new Clipping(top: 0, right: null, bottom: 10, left: -100);

      // Act
      string serialized = JsonConvert.SerializeObject(clipping);

      // Assert
      Assert.Equal(Expected, serialized);
    }
  }
}
