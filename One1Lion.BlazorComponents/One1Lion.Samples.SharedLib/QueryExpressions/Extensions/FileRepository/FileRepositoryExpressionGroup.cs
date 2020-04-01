using One1Lion.Samples.SharedLib.Search.QueryExpressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace One1Lion.Samples.SharedLib.Search.FileRepositoryExpressions {
  public class FileRepositoryExpressionGroup : FileRepositoryElement, IFileRepositoryExpressionGroup {
    public FileRepositoryExpressionGroup() : base() {
      Children = new List<IQueryElement>();
    }

    public string Name { get; set; }
    public bool NotGroup { get; set; }

    public List<IQueryElement> Children { get; set; }

    public override string FormattedDisplay() {
      if (Children is null || Children.Count == 0) { return string.Empty; }

      var sb = new StringBuilder();
      for (var i = 0; i < Children.Count; i++) {
        var child = Children[i];
        sb.Append(child.FormattedDisplay());
        if (i < Children.Count - 1) { sb.Append(child.AndWithNext ? " AND " : " OR "); }
      }
      sb.Insert(0, $"{(NotGroup ? "NOT " : "")}(");
      sb.Append(")");
      return sb.ToString();
    }

    public void AddChild(FileRepositoryElement toAdd, int atIndex = -1) {
      if (Children is null) { Children = new List<IQueryElement>(); }
      if (atIndex < 0) { atIndex = Children.Count; }
      atIndex = Math.Min(Children.Count, Math.Max(0, atIndex));
      Children.Insert(atIndex, toAdd);
      toAdd.Parent = this;
    }

    public void AddChild(IQueryElement toAdd, int atIndex = -1) {
      if (Children is null) { Children = new List<IQueryElement>(); }
      if (atIndex < 0) { atIndex = Children.Count; }
      atIndex = Math.Min(Children.Count, Math.Max(0, atIndex));
      Children.Insert(atIndex, (FileRepositoryElement)toAdd);
      toAdd.Parent = this;
    }

    public override string ToString() {
      return FormattedDisplay();
    }

    public IQueryElement NewItem() {
      return new FileRepositoryExpressionItem();
    }

    public static IQueryElement NewItem<TParent>(TParent parent) where TParent : IQueryElement {
      return IQueryExpressionGroup.NewItem(parent as IQueryExpressionGroup);
    }

    public static IQueryElement NewGroup() {
      return new FileRepositoryExpressionGroup();
    }
  }
}
