namespace YoutubeV2.Features.Search.Models;

public sealed record VideoSearchResult(
    string Id,
    string Title,
    string Author,
    string ChannelId,
    string ChannelUrl,
    TimeSpan? Duration,
    string ThumbnailUrl
);