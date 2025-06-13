namespace YoutubeV2.Features.Video.GetVideoDetails;

using MediatR;
using YoutubeExplode;
using YoutubeExplode.Exceptions;
using YoutubeExplode.Videos;
using YoutubeV2.Features.Video.Models;
using YoutubeV2.Features.Video.Utils;
using Microsoft.Extensions.Logging;

public sealed class GetVideoDetailsHandler(YoutubeClient youtubeClient, ILogger<GetVideoDetailsHandler> logger) : IRequestHandler<GetVideoDetailsQuery, VideoDetailsResult?>
{
    private readonly YoutubeClient _youtubeClient = youtubeClient;
    private readonly ILogger<GetVideoDetailsHandler> _logger = logger;

    public async Task<VideoDetailsResult?> Handle(GetVideoDetailsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var videoId = VideoId.Parse(request.VideoIdOrUrl);

            var videoTask = _youtubeClient.Videos.GetAsync(videoId, cancellationToken).AsTask();
            var streamManifestTask = _youtubeClient.Videos.Streams.GetManifestAsync(videoId, cancellationToken).AsTask();

            await Task.WhenAll(videoTask, streamManifestTask);

            var video = await videoTask;
            var streamManifest = await streamManifestTask;

            return VideoResultMapper.MapToVideoDetailsResult(video, streamManifest);
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
            _logger.LogError(ex, "Error fetching video details for: {VideoIdOrUrl}", request.VideoIdOrUrl);
            return null;
        }
    }
}