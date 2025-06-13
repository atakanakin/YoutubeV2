using YoutubeV2.Shared.Models;

namespace YoutubeV2.Features.Search.Models;

public sealed record ChannelSearchResult(
    string Id,
    string Title,
    string Url,
    IReadOnlyList<ThumbnailInfo> Thumbnails
) : ISearchResult
{
    public SearchResultType Type => SearchResultType.Channel;
}