namespace YoutubeV2.Features.Search.SearchChannels;

using MediatR;
using YoutubeV2.Features.Search.Constants;
using YoutubeV2.Features.Search.Models;

public sealed record SearchChannelsQuery(
    string Query,
    int Page = SearchConstants.Pagination.DefaultPage,
    int PageSize = SearchConstants.Pagination.DefaultPageSize
) : IRequest<IReadOnlyList<ChannelSearchResult>>;