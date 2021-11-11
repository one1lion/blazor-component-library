export function enumerateMediaDevices(listIdSuffix) {
  var deviceInfos = navigator.mediaDevices
    .enumerateDevices();

  console.log(deviceInfos);
  // TODO: May need to change this type
  return deviceInfos;
}
export function getElement(elemOrId) {
  let elem = (typeof elemOrId === 'string') ? document.getElementById(elemOrId) : elemOrId;

  if (!elem.getAttribute && elem.id) {
    // It is possible the passed in value is an ElementRef from c#
    elem = document.getElementById(elem.id);
  }
  return elem;
}
export function hasGetUserMedia() {
  return !!(navigator.mediaDevices && navigator.mediaDevices.getUserMedia);
}
export function startCapture(targetVideoElem, constraints) {
  /* 
   constraints - The C# VideoCaptureConstraint object
   {
     MaxWidth: float?,
     MinWidth: float?,
     ExactWidth: float?,
     MaxHeight: float?,
     MinHeight: float?,
     ExactHeight: float?,
     AudioDeviceId: int?,
     VideoDeviceId: int?
   }
  */
  if (hasGetUserMedia()) {
    // Good to go!
  } else {
    alert("getUserMedia() is not supported by your browser");
    return false;
  }

  // TODO: Validate the Constraints

  targetVideoElem = getElement(targetVideoElem);
  if (targetVideoElem) {
    if (window[targetVideoElem.id + "stream"]) {
      window[targetVideoElem.id + "stream"].getTracks().forEach(function (track) {
        track.stop();
      });
    }

    var mediaConstraints = {};

    if (constraints.audioDeviceId) {
      mediaConstraints.audio = {
        deviceId: { exact: constraints.audioDeviceId }
      }
    }

    if (constraints.videoDeviceId) {
      mediaConstraints.video = {
        deviceId: { exact: constraints.videoDeviceId }
      }
    } else {
      mediaConstraints.video = {
        width: {},
        height: {}
      };
      if (constraints.exactWidth) {
        mediaConstraints.video.width.exactWidth = constraints.exactWidth;
      }
      if (constraints.maxWidth) {
        mediaConstraints.video.width.max = constraints.maxWidth;
      }
      if (constraints.minWidth) {
        mediaConstraints.video.width.min = constraints.minWidth;
      }
      if (constraints.exactHeight) {
        mediaConstraints.video.height.exactHeight = constraints.exactHeight;
      }
      if (constraints.maxHeight) {
        mediaConstraints.video.height.max = constraints.maxHeight;
      }
      if (constraints.minHeight) {
        mediaConstraints.video.height.min = constraints.minHeight;
      }
    }

    navigator.mediaDevices
      .getUserMedia(mediaConstraints)
      .then((stream) => gotStream(targetVideoElem, stream))
      .catch((error) => handleError(targetVideoElem, error))
    return true;
  } else {
    console.log("The target video element does not exist.", targetVideoElem);
    return false;
  }
}
export function stopStream(targetVideoElem) {
  targetVideoElem = getElement(targetVideoElem);
  if (targetVideoElem && window[targetVideoElem.id + "stream"]) {
    window[targetVideoElem.id + "stream"].getTracks().forEach(function (track) {
      track.stop();
    });
  }
}
export function gotStream(targetVideoElem, stream) {
  window[targetVideoElem + "stream"] = stream; // make stream available to console
  targetVideoElem.srcObject = stream;
}
export function handleError(targetVideoElem, error) {
  console.error("Error in video element: " + targetVideoElem.id, error);
}
