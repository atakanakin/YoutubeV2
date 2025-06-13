namespace YoutubeV2.Shared.Models;

public sealed class ThumbnailInfo
{
    public required string Url { get; init; }
    public required int Width { get; init; }
    public required int Height { get; init; }
    public required int Resolution { get; init; }
} 