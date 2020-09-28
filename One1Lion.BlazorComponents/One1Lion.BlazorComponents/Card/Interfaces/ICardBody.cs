using System;
using System.Collections.Generic;
using System.Text;

namespace One1Lion.BlazorComponents.Card {
  public interface ICardBody {
    void SetTitle(CardTitle title);
    void AddText(CardText text);
    void AddLink(CardLink link);
  }
}
