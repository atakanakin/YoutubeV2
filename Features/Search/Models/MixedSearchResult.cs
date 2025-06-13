namespace YoutubeV2.Features.Search.Models;

public sealed record MixedSearchResult(
    ISearchResult Result
)
{
    public SearchResultType Type => Result.Type;
    public string Id => Result.Id;
    public string Title => Result.Title;
    public string ThumbnailUrl => Result.ThumbnailUrl;
} 