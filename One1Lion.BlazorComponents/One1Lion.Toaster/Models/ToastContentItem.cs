﻿using Microsoft.AspNetCore.Components;
using System;

namespace One1Lion.Toaster {
  public class ToastContentItem {
    public RenderFragment Title { get; set; }
    public string GUID { get; set; }
    public DateTime? Time { get; set; }
    public RenderFragment Body { get; set; }
    public bool AutoReset { get; set; } = true;
    public int TimeDisplaying { get; set; } = 4;
    public bool ShowIcon { get; set; }
    public string IconUrl { get; set; } = "_content/One1Lion.Toaster/toasticon.ico";
  }
}
