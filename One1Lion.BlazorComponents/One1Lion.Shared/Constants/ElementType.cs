namespace One1Lion.Shared {
  public enum ElementType {
    Div,
    A,
    Button,
    H1,
    H2,
    H3,
    H4,
    H5,
    H6,
    InputCheckbox,
    InputDate,
    InputNumber,
    InputPassword, 
    InputRadio,
    InputSubmit,
    InputText,
    Label,
    LI,
    P,
    Span,
    SubmitButton,
    Text,
    UL
  }

  public enum WrapperElementType {
    Div = ElementType.Div,
    H1 = ElementType.H1,
    H2 = ElementType.H2,
    H3 = ElementType.H3,
    H4 = ElementType.H4,
    H5 = ElementType.H5,
    H6 = ElementType.H6,
    Label = ElementType.Label,
    P = ElementType.P,
    Span = ElementType.Span,
    Text = ElementType.Text
  }
}
