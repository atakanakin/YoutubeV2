namespace YoutubeV2.Features.Video.GetCaptionDetails;

using MediatR;
using YoutubeExplode;
using YoutubeExplode.Exceptions;
using YoutubeExplode.Videos;
using YoutubeV2.Features.Video.Models;
using YoutubeV2.Features.Video.Utils;
using Microsoft.Extensions.Logging;

public sealed class GetCaptionDetailsHandler(YoutubeClient youtubeClient, ILogger<GetCaptionDetailsHandler> logger) : IRequestHandler<GetCaptionDetailsQuery, CaptionDetailsResult?>
{
    private readonly YoutubeClient _youtubeClient = youtubeClient;
    private readonly ILogger<GetCaptionDetailsHandler> _logger = logger;

    public async Task<CaptionDetailsResult?> Handle(GetCaptionDetailsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching caption details for: {VideoIdOrUrl}", request.VideoIdOrUrl);
        
        try
        {
            var videoId = VideoId.Parse(request.VideoIdOrUrl);
            
            var captionManifest = await _youtubeClient.Videos.ClosedCaptions.GetManifestAsync(videoId, cancellationToken);

            var result = VideoResultMapper.MapToCaptionDetailsResult(captionManifest);
            
            _logger.LogInformation("Successfully fetched caption details for: {VideoIdOrUrl} - CaptionTracks: {CaptionCount}", 
                request.VideoIdOrUrl, result.CaptionTracks.Count);
            
            return result;
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Invalid video ID or URL provided: {VideoIdOrUrl}", request.VideoIdOrUrl);
            return null;
        }
        catch (VideoUnavailableException ex)
        {
            _logger.LogWarning(ex, "Video not found or unavailable: {VideoIdOrUrl}", request.VideoIdOrUrl);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching caption details for: {VideoIdOrUrl}", request.VideoIdOrUrl);
            return null;
        }
    }
} 