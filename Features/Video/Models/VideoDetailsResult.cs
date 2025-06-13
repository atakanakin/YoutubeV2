namespace YoutubeV2.Features.Video.Models;

public sealed class VideoDetailsResult
{
    public required VideoMetadata Metadata { get; init; }
    public required IReadOnlyList<VideoStreamInfo> VideoStreams { get; init; }
    public required IReadOnlyList<AudioStreamInfo> AudioStreams { get; init; }
} 