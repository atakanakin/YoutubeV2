namespace YoutubeV2.Features.Video.Utils;

using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;
using YoutubeV2.Features.Video.Models;

public static class VideoResultMapper
{
    public static VideoDetailsResult MapToVideoDetailsResult(
        Video video,
        StreamManifest streamManifest)
    {
        var metadata = MapToVideoMetadata(video);
        var videoStreams = MapToVideoStreams(streamManifest.GetVideoOnlyStreams());
        var audioStreams = MapToAudioStreams(streamManifest.GetAudioOnlyStreams());

        return new VideoDetailsResult
        {
            Metadata = metadata,
            VideoStreams = videoStreams,
            AudioStreams = audioStreams
        };
    }

    public static AudioDetailsResult MapToAudioDetailsResult(
        Video video,
        StreamManifest streamManifest)
    {
        var metadata = MapToVideoMetadata(video);
        var audioStreams = MapToAudioStreams(streamManifest.GetAudioOnlyStreams());

        return new AudioDetailsResult
        {
            Metadata = metadata,
            AudioStreams = audioStreams
        };
    }

    private static VideoMetadata MapToVideoMetadata(Video video)
    {
        return new VideoMetadata
        {
            Id = video.Id.Value,
            Title = video.Title,
            Url = video.Url,
            Author = video.Author.ChannelTitle,
            ChannelId = video.Author.ChannelId.Value,
            Duration = video.Duration ?? TimeSpan.Zero,
            Description = video.Description,
            Keywords = [.. video.Keywords],
            ThumbnailUrl = video.Thumbnails.
                OrderByDescending(t => t.Resolution.Area)
                .FirstOrDefault()?.Url ?? string.Empty,
            UploadDate = video.UploadDate,
            ViewCount = video.Engagement?.ViewCount,
            LikeCount = video.Engagement?.LikeCount,
            DislikeCount = video.Engagement?.DislikeCount
        };
    }

    private static IReadOnlyList<VideoStreamInfo> MapToVideoStreams(
        IEnumerable<IVideoStreamInfo> streams)
    {
        return [.. streams.Select(stream => new VideoStreamInfo
        {
            Url = stream.Url,
            Container = stream.Container.Name,
            Size = stream.Size.Bytes,
            Quality = stream.VideoQuality.Label,
            Width = stream.VideoResolution.Width,
            Height = stream.VideoResolution.Height,
            Framerate = stream.VideoQuality.Framerate,
            VideoCodec = stream.VideoCodec
        })];
    }

    private static IReadOnlyList<AudioStreamInfo> MapToAudioStreams(
        IEnumerable<IAudioStreamInfo> streams)
    {
        return [.. streams.Select(stream => new AudioStreamInfo
        {
            Url = stream.Url,
            Container = stream.Container.Name,
            Size = stream.Size.Bytes,
            Quality = stream.AudioCodec,
            Bitrate = stream.Bitrate.BitsPerSecond,
            AudioCodec = stream.AudioCodec
        })];
    }
}