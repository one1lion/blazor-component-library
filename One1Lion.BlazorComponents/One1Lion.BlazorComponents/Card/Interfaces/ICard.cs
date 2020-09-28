using System;
using System.Collections.Generic;
using System.Text;

namespace One1Lion.BlazorComponents.Card {
  public interface ICard {
    void SetHeader(CardHeader header);
    void SetFooter(CardFooter footer);
    void AddBody(CardBody body);
    void ChildStateChanged();
  }
}
