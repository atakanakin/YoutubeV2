namespace YoutubeV2.Features.Search;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using YoutubeV2.Features.Search.Models;
using YoutubeV2.Features.Search.SearchVideos;
using YoutubeV2.Features.Search.SearchChannels;
using YoutubeV2.Features.Search.SearchPlaylists;
using YoutubeV2.Features.Search.SearchAll;
using YoutubeV2.Features.Search.Constants;

[ApiController]
[Route("api/[controller]")]
public sealed class SearchController : ControllerBase
{
    private readonly IMediator _mediator;

    public SearchController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Search YouTube videos with all available metadata
    /// </summary>
    [HttpGet("videos")]
    public async Task<ActionResult<IReadOnlyList<VideoSearchResult>>> SearchVideos(
        [FromQuery] string query,
        [FromQuery] int page = SearchConstants.Pagination.DefaultPage,
        [FromQuery] int pageSize = SearchConstants.Pagination.DefaultPageSize,
        CancellationToken cancellationToken = default)
    {
        var validationResult = ValidateSearchRequest(query, page, pageSize);
        if (validationResult != null) return validationResult;

        var searchQuery = new SearchVideosQuery(query, page, pageSize);
        var results = await _mediator.Send(searchQuery, cancellationToken);

        return Ok(results);
    }

    /// <summary>
    /// Search YouTube channels
    /// </summary>
    [HttpGet("channels")]
    public async Task<ActionResult<IReadOnlyList<ChannelSearchResult>>> SearchChannels(
        [FromQuery] string query,
        [FromQuery] int page = SearchConstants.Pagination.DefaultPage,
        [FromQuery] int pageSize = SearchConstants.Pagination.DefaultPageSize,
        CancellationToken cancellationToken = default)
    {
        var validationResult = ValidateSearchRequest(query, page, pageSize);
        if (validationResult != null) return validationResult;

        var searchQuery = new SearchChannelsQuery(query, page, pageSize);
        var results = await _mediator.Send(searchQuery, cancellationToken);

        return Ok(results);
    }

    /// <summary>
    /// Search YouTube playlists
    /// </summary>
    [HttpGet("playlists")]
    public async Task<ActionResult<IReadOnlyList<PlaylistSearchResult>>> SearchPlaylists(
        [FromQuery] string query,
        [FromQuery] int page = SearchConstants.Pagination.DefaultPage,
        [FromQuery] int pageSize = SearchConstants.Pagination.DefaultPageSize,
        CancellationToken cancellationToken = default)
    {
        var validationResult = ValidateSearchRequest(query, page, pageSize);
        if (validationResult != null) return validationResult;

        var searchQuery = new SearchPlaylistsQuery(query, page, pageSize);
        var results = await _mediator.Send(searchQuery, cancellationToken);

        return Ok(results);
    }

    /// <summary>
    /// Search all YouTube content (videos, channels, playlists) mixed together
    /// </summary>
    [HttpGet("all")]
    public async Task<ActionResult<IReadOnlyList<object>>> SearchAll(
        [FromQuery] string query,
        [FromQuery] int page = SearchConstants.Pagination.DefaultPage,
        [FromQuery] int pageSize = SearchConstants.Pagination.DefaultPageSize,
        CancellationToken cancellationToken = default)
    {
        var validationResult = ValidateSearchRequest(query, page, pageSize);
        if (validationResult != null) return validationResult;

        var searchQuery = new SearchAllQuery(query, page, pageSize);
        var results = await _mediator.Send(searchQuery, cancellationToken);

        return Ok(results);
    }

    private BadRequestObjectResult? ValidateSearchRequest(string query, int page, int pageSize)
    {
        if (string.IsNullOrWhiteSpace(query))
            return BadRequest("Search query cannot be empty");

        if (pageSize > SearchConstants.Pagination.MaxPageSize)
            return BadRequest($"Page size cannot exceed {SearchConstants.Pagination.MaxPageSize}");

        if (pageSize < SearchConstants.Pagination.MinPageSize)
            return BadRequest($"Page size must be at least {SearchConstants.Pagination.MinPageSize}");

        if (page < 1)
            return BadRequest("Page number must be at least 1");

        return null;
    }
}