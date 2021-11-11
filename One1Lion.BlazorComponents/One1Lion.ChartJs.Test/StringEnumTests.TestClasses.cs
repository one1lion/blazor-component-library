using One1Lion.ChartJs.Common.Enums;

namespace One1Lion.ChartJs.Tests {
  public partial class StringEnumTests {
    private class TestStringEnum : StringEnum {
      public static TestStringEnum Auto => new TestStringEnum("auto");
      public static TestStringEnum Custom(string value) => new TestStringEnum(value);

      private TestStringEnum(string stringRep) : base(stringRep) { }
    }
  }
}
