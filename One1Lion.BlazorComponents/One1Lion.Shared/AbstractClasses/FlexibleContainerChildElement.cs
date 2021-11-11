using Microsoft.AspNetCore.Components;

namespace One1Lion.Shared {
  public abstract class FlexibleContainerChildElement<TParent> : ContainerChildElement<TParent> {
    [Parameter] public virtual ElementType ElementType { get; set; } = ElementType.Div;

    protected override ElementType ContainerElementType => ElementType;
  }
}
