using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace One1Lion.Samples.SharedLib.Search.SQLExpressions {
  public class SQLExpressionCollection : ICollection<SQLExpressions.ISQLElement> {
    public SQLExpressionCollection() {
      Expressions = new List<SQLExpressions.ISQLElement>();
    }

    public ICollection<SQLExpressions.ISQLElement> Expressions { get; private set; }

    public int Count => Expressions == null ? 0 : Expressions.Count;

    public bool IsReadOnly => Expressions == null ? false : Expressions.IsReadOnly;

    public void Add(SQLExpressions.ISQLElement Item) {
      if (Expressions == null) { Expressions = new List<SQLExpressions.ISQLElement>(); }
      Expressions.Add(Item);
    }

    public void Clear() {
      if (Expressions == null) { return; }
      Expressions.Clear();
    }

    public bool Contains(SQLExpressions.ISQLElement item) {
      if (Expressions == null) { return false; }
      return Expressions.Contains(item);
    }

    public void CopyTo(SQLExpressions.ISQLElement[] array, int arrayIndex) {
      if (Expressions == null) { Expressions = new List<SQLExpressions.ISQLElement>(); }
      Expressions.CopyTo(array, arrayIndex);
    }

    public IEnumerator<SQLExpressions.ISQLElement> GetEnumerator() {
      if (Expressions == null) { Expressions = new List<SQLExpressions.ISQLElement>(); }
      return Expressions.GetEnumerator();
    }

    public bool Remove(SQLExpressions.ISQLElement item) {
      return Expressions.Remove(item);
    }

    IEnumerator IEnumerable.GetEnumerator() {
      return Expressions.AsEnumerable().GetEnumerator();
    }
  }
}
