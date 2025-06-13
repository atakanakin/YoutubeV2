namespace YoutubeV2.Features.Search.Models;

public sealed record PlaylistSearchResult(
    string Id,
    string Title,
    string Author,
    string ChannelId,
    string ChannelUrl,
    string ThumbnailUrl
) : ISearchResult
{
    public SearchResultType Type => SearchResultType.Playlist;
}