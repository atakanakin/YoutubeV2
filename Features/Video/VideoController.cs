namespace YoutubeV2.Features.Video;

using MediatR;
using Microsoft.AspNetCore.Mvc;
using YoutubeV2.Features.Video.Constants;
using YoutubeV2.Features.Video.GetVideoDetails;
using YoutubeV2.Features.Video.GetAudioDetails;
using YoutubeV2.Features.Video.Models;

[ApiController]
[Route("api/[controller]")]
[Tags("Video")]
public sealed class VideoController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    /// <summary>
    /// Get video details with all available streams (audio and video)
    /// </summary>
    /// <param name="videoIdOrUrl">YouTube video ID or full URL</param>
    /// <returns>Video metadata and all available streams</returns>
    [HttpGet]
    public async Task<ActionResult<VideoDetailsResult>> GetVideoDetails([FromQuery] string videoIdOrUrl)
    {
        if (string.IsNullOrWhiteSpace(videoIdOrUrl))
        {
            return BadRequest(new { error = VideoConstants.ErrorMessages.InvalidVideoId });
        }

        var query = new GetVideoDetailsQuery(videoIdOrUrl);
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFound(new { error = VideoConstants.ErrorMessages.VideoNotFound });
        }

        return Ok(result);
    }

    /// <summary>
    /// Get audio-only details with all available audio streams
    /// </summary>
    /// <param name="videoIdOrUrl">YouTube video ID or full URL</param>
    /// <returns>Video metadata and all available audio streams</returns>
    [HttpGet("audio")]
    public async Task<ActionResult<AudioDetailsResult>> GetAudioDetails([FromQuery] string videoIdOrUrl)
    {
        if (string.IsNullOrWhiteSpace(videoIdOrUrl))
        {
            return BadRequest(new { error = VideoConstants.ErrorMessages.InvalidVideoId });
        }

        var query = new GetAudioDetailsQuery(videoIdOrUrl);
        var result = await _mediator.Send(query);
        
        if (result == null)
        {
            return NotFound(new { error = VideoConstants.ErrorMessages.VideoNotFound });
        }
        
        return Ok(result);
    }
}