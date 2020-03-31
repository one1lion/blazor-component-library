using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace One1Lion.Samples.SharedLib.Search.DBExpressions {
  public class DBExpressionCollection : ICollection<IDBExpressionItem> {
    public DBExpressionCollection() {
      Expressions = new List<IDBExpressionItem>();
    }

    public ICollection<IDBExpressionItem> Expressions { get; private set; }

    public int Count => Expressions == null ? 0 : Expressions.Count;

    public bool IsReadOnly => Expressions == null ? false : Expressions.IsReadOnly;

    public void Add(IDBExpressionItem Item) {
      if (Expressions == null) { Expressions = new List<IDBExpressionItem>(); }
      Expressions.Add(Item);
    }

    public void Clear() {
      if (Expressions == null) { return; }
      Expressions.Clear();
    }

    public bool Contains(IDBExpressionItem item) {
      if (Expressions == null) { return false; }
      return Expressions.Contains(item);
    }

    public void CopyTo(IDBExpressionItem[] array, int arrayIndex) {
      if (Expressions == null) { Expressions = new List<IDBExpressionItem>(); }
      Expressions.CopyTo(array, arrayIndex);
    }

    public IEnumerator<IDBExpressionItem> GetEnumerator() {
      if (Expressions == null) { Expressions = new List<IDBExpressionItem>(); }
      return Expressions.GetEnumerator();
    }

    public bool Remove(IDBExpressionItem item) {
      return Expressions.Remove(item);
    }

    IEnumerator IEnumerable.GetEnumerator() {
      return Expressions.AsEnumerable().GetEnumerator();
    }
  }
}
