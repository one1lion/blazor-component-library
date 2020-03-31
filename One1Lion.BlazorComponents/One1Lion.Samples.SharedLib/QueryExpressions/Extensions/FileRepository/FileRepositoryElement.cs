using One1Lion.Samples.SharedLib.Search.QueryExpressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace One1Lion.Samples.SharedLib.Search.FileRepositoryExpressions {
  public class FileRepositoryElement : IFileRepositoryElement {
    public FileRepositoryElement() {
      Id = Guid.NewGuid().ToString();
    }
    public string Id { get; private protected set; }

    public bool AndWithNext { get; set; } = true;
    public IQueryExpressionGroup Parent { get; set; }
    public FileRepositoryExpressionGroup FileRepositoryGroupParent => (FileRepositoryExpressionGroup)Parent;

    public virtual string FormattedDisplay() => "FileRepositoryElement";

  }
}
