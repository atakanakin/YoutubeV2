namespace YoutubeV2.Features.Search.Models;

public sealed record ChannelSearchResult(
    string Id,
    string Title,
    string Url,
    string ThumbnailUrl
) : ISearchResult
{
    public SearchResultType Type => SearchResultType.Channel;
}