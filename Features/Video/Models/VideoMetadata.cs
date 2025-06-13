namespace YoutubeV2.Features.Video.Models;

using YoutubeV2.Shared.Models;

public sealed class VideoMetadata
{
    public required string Id { get; init; }
    public required string Url { get; init; }
    public required string Title { get; init; }
    public required string Author { get; init; }
    public required string ChannelId { get; init; }
    public required TimeSpan Duration { get; init; }
    public required string Description { get; init; }
    public required IReadOnlyList<string> Keywords { get; init; }
    public required IReadOnlyList<ThumbnailInfo> Thumbnails { get; init; }
    public required DateTimeOffset UploadDate { get; init; }
    public required long? ViewCount { get; init; }
    public required long? LikeCount { get; init; }
    public required long? DislikeCount { get; init; }
}