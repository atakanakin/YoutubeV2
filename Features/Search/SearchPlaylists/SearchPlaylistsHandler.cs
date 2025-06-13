namespace YoutubeV2.Features.Search.SearchPlaylists;

using MediatR;
using YoutubeExplode;
using YoutubeV2.Features.Search.Models;
using YoutubeV2.Features.Search.Utils;

public sealed class SearchPlaylistsHandler : IRequestHandler<SearchPlaylistsQuery, IReadOnlyList<PlaylistSearchResult>>
{
    private readonly YoutubeClient _youtubeClient;
    private readonly ILogger<SearchPlaylistsHandler> _logger;

    public SearchPlaylistsHandler(YoutubeClient youtubeClient, ILogger<SearchPlaylistsHandler> logger)
    {
        _youtubeClient = youtubeClient;
        _logger = logger;
    }

    public async Task<IReadOnlyList<PlaylistSearchResult>> Handle(SearchPlaylistsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Searching playlists for: {Query}, Page: {Page}, PageSize: {PageSize}",
            request.Query, request.Page, request.PageSize);

        var searchResults = _youtubeClient.Search.GetPlaylistsAsync(request.Query, cancellationToken);
        var results = new List<PlaylistSearchResult>();

        var skipCount = (request.Page - 1) * request.PageSize;
        var currentIndex = 0;
        var addedCount = 0;

        await foreach (var playlist in searchResults.WithCancellation(cancellationToken))
        {
            if (currentIndex < skipCount)
            {
                currentIndex++;
                continue;
            }

            if (addedCount >= request.PageSize)
                break;

            results.Add(SearchResultMapper.MapToPlaylistSearchResult(playlist));

            addedCount++;
            currentIndex++;
        }

        _logger.LogInformation("Found {Count} playlists for query: {Query}", results.Count, request.Query);
        return results;
    }
}