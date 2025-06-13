namespace YoutubeV2.Features.Search.SearchVideos;

using MediatR;
using YoutubeExplode;
using YoutubeV2.Features.Search.Models;
using YoutubeV2.Features.Search.Constants;
using YoutubeV2.Features.Search.Utils;

public sealed class SearchVideosHandler : IRequestHandler<SearchVideosQuery, IReadOnlyList<VideoSearchResult>>
{
    private readonly YoutubeClient _youtubeClient;
    private readonly ILogger<SearchVideosHandler> _logger;

    public SearchVideosHandler(YoutubeClient youtubeClient, ILogger<SearchVideosHandler> logger)
    {
        _youtubeClient = youtubeClient;
        _logger = logger;
    }

    public async Task<IReadOnlyList<VideoSearchResult>> Handle(SearchVideosQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Searching videos for: {Query}, Page: {Page}, PageSize: {PageSize}",
            request.Query, request.Page, request.PageSize);

        var searchResults = _youtubeClient.Search.GetVideosAsync(request.Query, cancellationToken);
        var results = new List<VideoSearchResult>();

        var skipCount = (request.Page - 1) * request.PageSize;
        var currentIndex = 0;
        var addedCount = 0;

        await foreach (var video in searchResults.WithCancellation(cancellationToken))
        {
            if (currentIndex < skipCount)
            {
                currentIndex++;
                continue;
            }

            if (addedCount >= request.PageSize)
                break;

            results.Add(SearchResultMapper.MapToVideoSearchResult(video));

            addedCount++;
            currentIndex++;
        }

        _logger.LogInformation("Found {Count} videos for query: {Query}", results.Count, request.Query);
        return results;
    }
}
