namespace YoutubeV2.Features.Search.Models;

public interface ISearchResult
{
    string Id { get; }
    string Title { get; }
    string Url { get; }
    string ThumbnailUrl { get; }
    SearchResultType Type { get; }
}

public enum SearchResultType
{
    Video,
    Channel,
    Playlist
}