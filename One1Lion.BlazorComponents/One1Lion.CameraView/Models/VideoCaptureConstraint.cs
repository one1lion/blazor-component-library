namespace One1Lion.CameraView.Models {
  public class VideoCaptureConstraint {
    public float? MaxWidth { get; set; }
    public float? MinWidth { get; set; }
    public float? ExactWidth { get; set; }
    public float? MaxHeight { get; set; }
    public float? MinHeight { get; set; }
    public float? ExactHeight { get; set; }
    public string AudioDeviceId { get; set; }
    public string VideoDeviceId { get; set; }
  }
}
