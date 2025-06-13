namespace YoutubeV2.Features.Search.Constants;

public static class SearchConstants
{
    public static class Thumbnails
    {
        // TODO: Add default URL's
        public const string DefaultThumbnailUrl = "https://img.youtube.com/vi/default/default.jpg";
        public const string DefaultChannelThumbnailUrl = "https://yt3.ggpht.com/default_channel_avatar.png";
    }

    public static class Pagination
    {
        public const int DefaultPageSize = 20;
        public const int MaxPageSize = 50;
        public const int MinPageSize = 1;
        public const int DefaultPage = 1;
    }

    public static class VideoDefaults
    {
        public const string UnknownAuthor = "Unknown Channel";
        public const string UnknownPlaylist = "Unknown Playlist";
    }
}