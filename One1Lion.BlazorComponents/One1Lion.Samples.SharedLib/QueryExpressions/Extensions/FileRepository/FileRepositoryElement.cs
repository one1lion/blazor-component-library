using One1Lion.Samples.SharedLib.Search.QueryExpressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace One1Lion.Samples.SharedLib.Search.FileRepositoryExpressions {
  public class FileRepositoryElement : IFileRepositoryElement {
    public FileRepositoryElement() {
      Id = Guid.NewGuid().ToString();
    }
    public string Id { get; set; }

    public bool AndWithNext { get; set; } = true;
    public IQueryExpressionGroup Parent { get; set; }
    public FileRepositoryExpressionGroup FileRepositoryGroupParent => Parent?.GetType()?.GetInterface(nameof(IFileRepositoryExpressionGroup)) is { } ? (FileRepositoryExpressionGroup)Parent : null;

    public virtual string FormattedDisplay() => "FileRepositoryElement";

  }
}
