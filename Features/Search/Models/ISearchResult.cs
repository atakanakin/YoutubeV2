using YoutubeV2.Shared.Models;

namespace YoutubeV2.Features.Search.Models;

public interface ISearchResult
{
    string Id { get; }
    string Title { get; }
    string Url { get; }
    IReadOnlyList<ThumbnailInfo> Thumbnails { get; init; }
    SearchResultType Type { get; }
}

public enum SearchResultType
{
    Video,
    Channel,
    Playlist
}