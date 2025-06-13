namespace YoutubeV2.Features.Video.Models;

public sealed class VideoStreamInfo
{
    public required string Url { get; init; }
    public required string Container { get; init; }
    public required long Size { get; init; }
    public required string Quality { get; init; }
    public required int? Width { get; init; }
    public required int? Height { get; init; }
    public required int? Framerate { get; init; }
    public required string VideoCodec { get; init; }
} 