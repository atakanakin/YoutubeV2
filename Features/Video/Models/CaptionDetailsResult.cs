namespace YoutubeV2.Features.Video.Models;

public sealed class CaptionDetailsResult
{
    public required IReadOnlyList<CaptionTrackInfo> CaptionTracks { get; init; }
} 