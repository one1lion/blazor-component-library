using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace One1Lion.Shared {
  public abstract class FlexibleContainerChildElement<TParent> : ContainerChildElement<TParent> {
    [Parameter] public virtual ElementType ElementType { get; set; } = ElementType.Div;

    protected override ElementType ContainerElementType => ElementType;
  }
}
