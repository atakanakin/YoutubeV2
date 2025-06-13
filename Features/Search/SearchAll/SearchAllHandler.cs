namespace YoutubeV2.Features.Search.SearchAll;

using MediatR;
using YoutubeExplode;
using YoutubeV2.Features.Search.Utils;

public sealed class SearchAllHandler : IRequestHandler<SearchAllQuery, IReadOnlyList<object>>
{
    private readonly YoutubeClient _youtubeClient;
    private readonly ILogger<SearchAllHandler> _logger;

    public SearchAllHandler(YoutubeClient youtubeClient, ILogger<SearchAllHandler> logger)
    {
        _youtubeClient = youtubeClient;
        _logger = logger;
    }

    public async Task<IReadOnlyList<object>> Handle(SearchAllQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Searching all content for: {Query}, Page: {Page}, PageSize: {PageSize}",
            request.Query, request.Page, request.PageSize);

        var searchResults = _youtubeClient.Search.GetResultsAsync(request.Query, cancellationToken);
        var results = new List<object>();

        var skipCount = (request.Page - 1) * request.PageSize;
        var currentIndex = 0;
        var addedCount = 0;

        await foreach (var result in searchResults.WithCancellation(cancellationToken))
        {
            if (currentIndex < skipCount)
            {
                currentIndex++;
                continue;
            }

            if (addedCount >= request.PageSize)
                break;

            var mappedResult = MapSearchResult(result);
            if (mappedResult != null)
            {
                results.Add(mappedResult);
                addedCount++;
            }

            currentIndex++;
        }

        _logger.LogInformation("Found {Count} mixed results for query: {Query}", results.Count, request.Query);
        return results;
    }

    private static object? MapSearchResult(YoutubeExplode.Search.ISearchResult result)
    {
        return result switch
        {
            YoutubeExplode.Search.VideoSearchResult video => SearchResultMapper.MapToVideoSearchResult(video),
            YoutubeExplode.Search.ChannelSearchResult channel => SearchResultMapper.MapToChannelSearchResult(channel),
            YoutubeExplode.Search.PlaylistSearchResult playlist => SearchResultMapper.MapToPlaylistSearchResult(playlist),
            _ => null
        };
    }
}