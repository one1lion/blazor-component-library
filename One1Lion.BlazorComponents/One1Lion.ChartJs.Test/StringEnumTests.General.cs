using System;
using Xunit;

namespace One1Lion.ChartJs.Tests {
  public partial class StringEnumTests {
    [Fact]
    public void Construct_NullValue_ThrowsArgumentNullException() {
      Assert.Throws<ArgumentNullException>(() => TestStringEnum.Custom(null));
    }
  }
}
