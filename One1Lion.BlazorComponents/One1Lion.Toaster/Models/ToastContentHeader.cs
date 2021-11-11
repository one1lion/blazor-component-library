using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using One1Lion.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace One1Lion.Toaster {
  public class ToastContentHeader : ComponentBase {
    public string HeaderText { get; set; }
    public WrapperElementType ElementType { get; set;  }

    public RenderFragment RenderContent => CreateRenderContent;
    
    void CreateRenderContent(RenderTreeBuilder builder) {
      var curElemNum = 0;
      builder.OpenElement(curElemNum++, ElementType.ToString().ToLower());
      builder.AddContent(curElemNum++, HeaderText);
      builder.CloseElement();
    }
  }
}
