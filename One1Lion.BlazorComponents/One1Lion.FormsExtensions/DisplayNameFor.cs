using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using One1Lion.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace One1Lion.FormsExtensions {
  /// <summary>
  /// Displays a list of Display Name from the <see cref="System.ComponentModel.DataAnnotations.DisplayAttribute" />
  /// </summary>
  /// <remarks>
  /// This is an additional component that will use the Name property of the DisplayAttribute Data Annotation.
  /// If there is no DisplayAttribute, it will use the property name.
  /// 
  /// Adapted from <see cref="Microsoft.AspNetCore.Components.Forms.ValidationMessage{TValue}"/>
  /// https://github.com/dotnet/aspnetcore/blob/4928eb3de0d80570dad93a143b52a8f5a205dac7/src/Components/Web/src/Forms/ValidationMessage.cs
  /// </remarks>
  public class DisplayNameFor<T> : ComponentBase {
    private Expression<Func<T>> _previousFieldAccessor;
    private string _displayName;

    /// <summary>
    /// Gets or sets a collection of additional attributes that will be applied to the created <c>div</c> element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)] public IReadOnlyDictionary<string, object> AdditionalAttributes { get; set; }

    /// <summary>
    /// Specifies the field to get the Display Name for.
    /// </summary>
    [Parameter] public Expression<Func<T>> Field { get; set; }

    /// <inheritdoc />
    protected override void OnParametersSet() {
      if (Field == null) // Not possible except if you manually specify T
      {
        throw new InvalidOperationException($"{GetType()} requires a value for the {nameof(Field)} parameter.");
      } else if (Field != _previousFieldAccessor || string.IsNullOrWhiteSpace(_displayName) && Field is { }) {
        _displayName = Field.GetDisplayName();

        _previousFieldAccessor = Field;
      }
    }

    /// <inheritdoc />
    protected override void BuildRenderTree(RenderTreeBuilder builder) {
      var curElem = 0;
      if (AdditionalAttributes?.Any() ?? false) {
        builder.OpenElement(curElem++, "span");
        builder.AddMultipleAttributes(curElem++, AdditionalAttributes);
      }
      builder.AddContent(curElem, _displayName);
      if (AdditionalAttributes?.Any() ?? false) {
        builder.CloseElement();
      }
    }

    private void HandleValidationStateChanged(object sender, ValidationStateChangedEventArgs eventArgs) {
      StateHasChanged();
    }
  }
}
