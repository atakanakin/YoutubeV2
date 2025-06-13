namespace YoutubeV2.Features.Video.Models;

public sealed class AudioDetailsResult
{
    public required VideoMetadata Metadata { get; init; }
    public required IReadOnlyList<AudioStreamInfo> AudioStreams { get; init; }
} 