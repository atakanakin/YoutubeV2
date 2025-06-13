namespace YoutubeV2.Features.Video.Constants;

public static class VideoConstants
{
    public static class ErrorMessages
    {
        public const string VideoNotFound = "Video not found or unavailable";
        public const string InvalidVideoId = "Invalid video ID or URL";
        public const string StreamsNotAvailable = "Video streams are not available";
        public const string VideoPrivateOrRestricted = "Video is private or restricted";
    }

    public static class Defaults
    {
        public const int MaxRetries = 3;
        public const int TimeoutSeconds = 30;
    }
}