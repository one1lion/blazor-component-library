using One1Lion.ChartJs.Common;
using Xunit;

namespace One1Lion.ChartJs.Tests {
  public partial class ClippingTests {
    [Fact]
    public void Equals_SameAll_ReturnsTrue() {
      // Arrange
      const int Value = 10;
      Clipping a = new Clipping(Value);
      Clipping b = new Clipping(Value);

      // Act
      bool equals = a.Equals(b);

      // Assert
      Assert.True(equals);
    }

    [Fact]
    public void Equals_AllAndIndividual_ReturnsTrue() {
      // Arrange
      const int Value = -10;
      Clipping a = new Clipping(Value);
      Clipping b = new Clipping(Value, Value, Value, Value);

      // Act
      bool equals = a.Equals(b);

      // Assert
      Assert.True(equals);
    }

    [Fact]
    public void GetHashCode_AllAndIndividual_Equals() {
      // Arrange
      const int Value = 12345;
      Clipping a = new Clipping(Value);
      Clipping b = new Clipping(Value, Value, Value, Value);

      // Act
      int hashA = a.GetHashCode();
      int hashB = b.GetHashCode();

      // Assert
      Assert.Equal(hashA, hashB);
    }
  }
}
