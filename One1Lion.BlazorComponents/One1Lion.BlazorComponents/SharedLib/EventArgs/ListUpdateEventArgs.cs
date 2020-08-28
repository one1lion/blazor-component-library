﻿using System;
using System.Collections.Generic;
using System.Text;

namespace One1Lion.BlazorComponents.SharedLib.EventArgs {
  public class ListUpdateEventArgs {
    public UpdateEventType UpdateEventType { get; set; }
    public object AffectedFromList { get; set; }
    public int AffectedIndexInFromList { get; set; }
    public object AffectedToList { get; set; }
    public int AffectedIndexInToList { get; set; }
    public object AffectedItem { get; set; }
    public string AffectedFromAddress { get; set; }
    public string AffectedToAddress { get; set; }
  }
}
