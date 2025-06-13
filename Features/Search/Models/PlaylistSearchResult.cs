using YoutubeV2.Shared.Models;

namespace YoutubeV2.Features.Search.Models;

public sealed record PlaylistSearchResult(
    string Id,
    string Title,
    string Url,
    string Author,
    string ChannelId,
    string ChannelUrl,
    IReadOnlyList<ThumbnailInfo> Thumbnails
) : ISearchResult
{
    public SearchResultType Type => SearchResultType.Playlist;
}