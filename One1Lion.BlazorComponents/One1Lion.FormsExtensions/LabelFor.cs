using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using One1Lion.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace One1Lion.FormsExtensions {
  /// <summary>
  /// Displays a list of Display Name from the <see cref="System.ComponentModel.DataAnnotations.DisplayAttribute" />
  /// for a specified field within a cascaded <see cref="EditContext"/>.
  /// </summary>
  /// <remarks>
  /// This is an additional component that behaves similarly to Html.LabelFor(...) or the &lt;label asp-for ...&gt;
  /// syntax in Html or Tag helpers.
  /// 
  /// Adapted from <see cref="Microsoft.AspNetCore.Components.Forms.ValidationMessage{TValue}"/>
  /// https://github.com/dotnet/aspnetcore/blob/4928eb3de0d80570dad93a143b52a8f5a205dac7/src/Components/Web/src/Forms/ValidationMessage.cs
  /// </remarks>
  public class LabelFor<T> : ComponentBase {
    private EditContext _previousEditContext;
    private Expression<Func<T>> _previousFieldAccessor;
    private string _displayName;

    /// <summary>
    /// Gets or sets a collection of additional attributes that will be applied to the created <c>div</c> element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)] public IReadOnlyDictionary<string, object> AdditionalAttributes { get; set; }

    [CascadingParameter] EditContext CurrentEditContext { get; set; }

    /// <summary>
    /// Specifies the field to generate a label for.
    /// </summary>
    [Parameter] public Expression<Func<T>> Field { get; set; }

    /// <inheritdoc />
    protected override void OnParametersSet() {
      if (CurrentEditContext == null) {
        throw new InvalidOperationException($"{GetType()} requires a cascading parameter " +
            $"of type {nameof(EditContext)}. For example, you can use {GetType()} inside " +
            $"an {nameof(EditForm)}.");
      }

      if (Field == null) // Not possible except if you manually specify T
      {
        throw new InvalidOperationException($"{GetType()} requires a value for the {nameof(Field)} parameter.");
      } else if (Field != _previousFieldAccessor || string.IsNullOrWhiteSpace(_displayName) && Field is { }) {
        _displayName = Field.GetDisplayName();

        _previousFieldAccessor = Field;
      }

      if (CurrentEditContext != _previousEditContext) {
        _previousEditContext = CurrentEditContext;
      }
    }

    /// <inheritdoc />
    protected override void BuildRenderTree(RenderTreeBuilder builder) {
      // TODO: Find out how to get the ElementReference for the Input element for the same property
      builder.OpenElement(0, "label");
      builder.AddMultipleAttributes(1, AdditionalAttributes);
      builder.AddContent(2, _displayName);
      builder.CloseElement();
    }

    private void HandleValidationStateChanged(object sender, ValidationStateChangedEventArgs eventArgs) {
      StateHasChanged();
    }
  }
}
