namespace YoutubeV2.Features.Search.Utils;

using YoutubeV2.Features.Search.Constants;
using YoutubeV2.Shared.Utils;

public static class SearchResultMapper
{
    public static Models.VideoSearchResult MapToVideoSearchResult(YoutubeExplode.Search.VideoSearchResult youtubeVideo)
    {
        return new Models.VideoSearchResult(
            Id: youtubeVideo.Id.Value,
            Title: youtubeVideo.Title,
            Url: youtubeVideo.Url,
            Author: youtubeVideo.Author.ChannelTitle ?? SearchConstants.VideoDefaults.UnknownAuthor,
            ChannelId: youtubeVideo.Author.ChannelId.Value,
            ChannelUrl: youtubeVideo.Author.ChannelUrl,
            Duration: youtubeVideo.Duration,
            Thumbnails: ThumbnailMapper.MapToThumbnails(youtubeVideo.Thumbnails)
        );
    }

    public static Models.ChannelSearchResult MapToChannelSearchResult(YoutubeExplode.Search.ChannelSearchResult youtubeChannel)
    {
        return new Models.ChannelSearchResult(
            Id: youtubeChannel.Id.Value,
            Title: youtubeChannel.Title ?? SearchConstants.VideoDefaults.UnknownAuthor,
            Url: youtubeChannel.Url,
            Thumbnails: ThumbnailMapper.MapToThumbnails(youtubeChannel.Thumbnails)
        );
    }

    public static Models.PlaylistSearchResult MapToPlaylistSearchResult(YoutubeExplode.Search.PlaylistSearchResult youtubePlaylist)
    {
        return new Models.PlaylistSearchResult(
            Id: youtubePlaylist.Id.Value,
            Title: youtubePlaylist.Title ?? SearchConstants.VideoDefaults.UnknownPlaylist,
            Url: youtubePlaylist.Url,
            Author: youtubePlaylist.Author?.ChannelTitle ?? SearchConstants.VideoDefaults.UnknownAuthor,
            ChannelId: youtubePlaylist.Author?.ChannelId.Value ?? string.Empty,
            ChannelUrl: youtubePlaylist.Author?.ChannelUrl ?? string.Empty,
            Thumbnails: ThumbnailMapper.MapToThumbnails(youtubePlaylist.Thumbnails)
        );
    }
}