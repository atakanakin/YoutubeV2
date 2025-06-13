namespace YoutubeV2.Features.Search;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using YoutubeV2.Features.Search.Models;
using YoutubeV2.Features.Search.SearchVideos;
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
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = SearchConstants.Pagination.DefaultPageSize,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(query))
            return BadRequest("Search query cannot be empty");

        if (pageSize > SearchConstants.Pagination.MaxPageSize)
            return BadRequest($"Page size cannot exceed {SearchConstants.Pagination.MaxPageSize}");

        if (pageSize < SearchConstants.Pagination.MinPageSize)
            return BadRequest($"Page size must be at least {SearchConstants.Pagination.MinPageSize}");

        if (page < 1)
            return BadRequest("Page number must be at least 1");

        var searchQuery = new SearchVideosQuery(query, page, pageSize);
        var results = await _mediator.Send(searchQuery, cancellationToken);

        return Ok(results);
    }
}