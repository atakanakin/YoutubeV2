namespace YoutubeV2.Features.Search.SearchChannels;

using MediatR;
using YoutubeExplode;
using YoutubeV2.Features.Search.Models;
using YoutubeV2.Features.Search.Utils;

public sealed class SearchChannelsHandler : IRequestHandler<SearchChannelsQuery, IReadOnlyList<ChannelSearchResult>>
{
    private readonly YoutubeClient _youtubeClient;
    private readonly ILogger<SearchChannelsHandler> _logger;

    public SearchChannelsHandler(YoutubeClient youtubeClient, ILogger<SearchChannelsHandler> logger)
    {
        _youtubeClient = youtubeClient;
        _logger = logger;
    }

    public async Task<IReadOnlyList<ChannelSearchResult>> Handle(SearchChannelsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Searching channels for: {Query}, Page: {Page}, PageSize: {PageSize}",
            request.Query, request.Page, request.PageSize);

        var searchResults = _youtubeClient.Search.GetChannelsAsync(request.Query, cancellationToken);
        var results = new List<ChannelSearchResult>();

        var skipCount = (request.Page - 1) * request.PageSize;
        var currentIndex = 0;
        var addedCount = 0;

        await foreach (var channel in searchResults.WithCancellation(cancellationToken))
        {
            if (currentIndex < skipCount)
            {
                currentIndex++;
                continue;
            }

            if (addedCount >= request.PageSize)
                break;

            results.Add(SearchResultMapper.MapToChannelSearchResult(channel));

            addedCount++;
            currentIndex++;
        }

        _logger.LogInformation("Found {Count} channels for query: {Query}", results.Count, request.Query);
        return results;
    }
}