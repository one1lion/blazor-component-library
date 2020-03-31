using Microsoft.AspNetCore.Components;

namespace One1Lion.BlazorComponents.SharedLib {
  public abstract class FlexibleContainerChildElement<TParent> : ContainerChildElement<TParent> {
    [Parameter] public virtual ElementType ElementType { get; set; } = ElementType.Div;

    protected override ElementType ContainerElementType => ElementType;
  }
}
