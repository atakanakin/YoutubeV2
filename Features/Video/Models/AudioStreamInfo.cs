namespace YoutubeV2.Features.Video.Models;

public sealed class AudioStreamInfo
{
    public required string Url { get; init; }
    public required string Container { get; init; }
    public required long Size { get; init; }
    public required string Quality { get; init; }
    public required long Bitrate { get; init; }
    public required string AudioCodec { get; init; }
}