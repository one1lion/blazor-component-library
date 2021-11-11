using System.Collections.Generic;

namespace One1Lion.Loader.Variables {
  public class ThreeBoxLoaderParameters {
    public bool ShowLoadingText { get; set; }
    public string LoadingText { get; set; } = "Loading";
    public bool Vertical { get; set; }
    public string AddContainerCssClass { get; set; }
    public string AddBoxesContainerCssClass { get; set; }
    public string AddBoxCssClass { get; set; }
    public IDictionary<string, object> AdditionalAttributes { get; set; }

    public ThreeBoxLoaderParameters GetDefaultValues() => new ThreeBoxLoaderParameters() {
      ShowLoadingText = ShowLoadingText,
      LoadingText = LoadingText,
      Vertical = Vertical,
      AddContainerCssClass = AddContainerCssClass,
      AddBoxesContainerCssClass = AddBoxesContainerCssClass,
      AddBoxCssClass = AddBoxCssClass,
      AdditionalAttributes = AdditionalAttributes
    };
  }
}
