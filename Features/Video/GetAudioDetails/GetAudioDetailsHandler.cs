namespace YoutubeV2.Features.Video.GetAudioDetails;

using MediatR;
using YoutubeExplode;
using YoutubeExplode.Exceptions;
using YoutubeExplode.Videos;
using YoutubeV2.Features.Video.Models;
using YoutubeV2.Features.Video.Utils;
using Microsoft.Extensions.Logging;

public sealed class GetAudioDetailsHandler(YoutubeClient youtubeClient, ILogger<GetAudioDetailsHandler> logger) : IRequestHandler<GetAudioDetailsQuery, AudioDetailsResult?>
{
    private readonly YoutubeClient _youtubeClient = youtubeClient;
    private readonly ILogger<GetAudioDetailsHandler> _logger = logger;

    public async Task<AudioDetailsResult?> Handle(GetAudioDetailsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Fetching audio details for: {VideoIdOrUrl}", request.VideoIdOrUrl);
        
        try
        {
            var videoId = VideoId.Parse(request.VideoIdOrUrl);
            
            var videoTask = _youtubeClient.Videos.GetAsync(videoId, cancellationToken).AsTask();
            var streamManifestTask = _youtubeClient.Videos.Streams.GetManifestAsync(videoId, cancellationToken).AsTask();

            await Task.WhenAll(videoTask, streamManifestTask);

            var video = await videoTask;
            var streamManifest = await streamManifestTask;

            var result = VideoResultMapper.MapToAudioDetailsResult(video, streamManifest);
            
            _logger.LogInformation("Successfully fetched audio details for: {VideoIdOrUrl} - Title: {Title}, AudioStreams: {AudioCount}", 
                request.VideoIdOrUrl, result.Metadata.Title, result.AudioStreams.Count);
            
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
            _logger.LogError(ex, "Error fetching audio details for: {VideoIdOrUrl}", request.VideoIdOrUrl);
            return null;
        }
    }
} 