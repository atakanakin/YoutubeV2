namespace YoutubeV2.Shared.Utils;

using YoutubeExplode.Common;
using YoutubeV2.Shared.Models;
using YoutubeV2.Shared.Constants;

public static class ThumbnailMapper
{
    public static IReadOnlyList<ThumbnailInfo> MapToThumbnails(
        IReadOnlyList<Thumbnail> thumbnails)
    {
        return [.. thumbnails.Select(thumbnail => new ThumbnailInfo
        {
            Url = thumbnail.Url,
            Width = thumbnail.Resolution.Width,
            Height = thumbnail.Resolution.Height,
            Resolution = thumbnail.Resolution.Area
        })];
    }

    public static string GetHighestQualityThumbnailUrl(
        IReadOnlyList<Thumbnail> thumbnails, 
        string defaultUrl)
    {
        return thumbnails
            .OrderByDescending(t => t.Resolution.Area)
            .FirstOrDefault()?.Url ?? defaultUrl;
    }
} 