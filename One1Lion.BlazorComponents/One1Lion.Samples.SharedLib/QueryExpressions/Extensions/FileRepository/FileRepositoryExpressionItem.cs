using One1Lion.Samples.SharedLib.Search.QueryExpressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace One1Lion.Samples.SharedLib.Search.FileRepositoryExpressions {
  public class FileRepositoryExpressionItem : FileRepositoryElement, IFileRepositoryExpressionItem {
    public FileRepositoryExpressionItem() : base() { }


    public override string FormattedDisplay() {
      return "File Repository Search Element";
    }
  }
}
