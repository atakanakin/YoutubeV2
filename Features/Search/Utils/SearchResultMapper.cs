namespace YoutubeV2.Features.Search.Utils;

using YoutubeExplode.Search;
using YoutubeV2.Features.Search.Models;
using YoutubeV2.Features.Search.Constants;

public static class SearchResultMapper
{
    public static Models.VideoSearchResult MapToVideoSearchResult(YoutubeExplode.Search.VideoSearchResult youtubeVideo)
    {
        return new Models.VideoSearchResult(
            Id: youtubeVideo.Id.Value,
            Title: youtubeVideo.Title,
            Author: youtubeVideo.Author.ChannelTitle ?? SearchConstants.VideoDefaults.UnknownAuthor,
            ChannelId: youtubeVideo.Author.ChannelId.Value,
            ChannelUrl: youtubeVideo.Author.ChannelUrl,
            Duration: youtubeVideo.Duration,
            ThumbnailUrl: youtubeVideo.Thumbnails
                .OrderByDescending(t => t.Resolution.Area)
                .FirstOrDefault()?.Url ?? SearchConstants.Thumbnails.DefaultThumbnailUrl
        );
    }

    public static Models.ChannelSearchResult MapToChannelSearchResult(YoutubeExplode.Search.ChannelSearchResult youtubeChannel)
    {
        return new Models.ChannelSearchResult(
            Id: youtubeChannel.Id.Value,
            Title: youtubeChannel.Title ?? SearchConstants.VideoDefaults.UnknownAuthor,
            Url: youtubeChannel.Url,
            ThumbnailUrl: youtubeChannel.Thumbnails
                .OrderByDescending(t => t.Resolution.Area)
                .FirstOrDefault()?.Url ?? SearchConstants.Thumbnails.DefaultChannelThumbnailUrl
        );
    }

    public static Models.PlaylistSearchResult MapToPlaylistSearchResult(YoutubeExplode.Search.PlaylistSearchResult youtubePlaylist)
    {
        return new Models.PlaylistSearchResult(
            Id: youtubePlaylist.Id.Value,
            Title: youtubePlaylist.Title ?? SearchConstants.VideoDefaults.UnknownPlaylist,
            Author: youtubePlaylist.Author?.ChannelTitle ?? SearchConstants.VideoDefaults.UnknownAuthor,
            ChannelId: youtubePlaylist.Author?.ChannelId.Value ?? string.Empty,
            ChannelUrl: youtubePlaylist.Author?.ChannelUrl ?? string.Empty,
            ThumbnailUrl: youtubePlaylist.Thumbnails
                .OrderByDescending(t => t.Resolution.Area)
                .FirstOrDefault()?.Url ?? SearchConstants.Thumbnails.DefaultThumbnailUrl
        );
    }
}