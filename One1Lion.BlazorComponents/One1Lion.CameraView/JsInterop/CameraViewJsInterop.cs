using Microsoft.JSInterop;
using One1Lion.CameraView.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace One1Lion.CameraView.JsInterop {
  public static class CameraViewJsInterop {
    public static ValueTask<List<DeviceInfo>> GetDeviceInfos(IJSObjectReference module, string listElemIdSuffix = null) {
      return module.InvokeAsync<List<DeviceInfo>>(
        "enumerateMediaDevices", listElemIdSuffix);
    }

    public static ValueTask<bool> HasGetUserMedia(IJSObjectReference module) {
      return module.InvokeAsync<bool>("hasGetUserMedia");
    }

    public static ValueTask<bool> StartCapture(IJSObjectReference module, string targetVideoElemId, VideoCaptureConstraint constraint) {
      return module.InvokeAsync<bool>("startCapture", targetVideoElemId, constraint);
    }
    public static ValueTask<bool> StopCapture(IJSObjectReference module, string targetVideoElemId) {
      return module.InvokeAsync<bool>("stopCapture", targetVideoElemId);
    }
  }
}
