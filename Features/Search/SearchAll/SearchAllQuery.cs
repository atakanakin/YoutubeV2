namespace YoutubeV2.Features.Search.SearchAll;

using MediatR;
using YoutubeV2.Features.Search.Constants;

public sealed record SearchAllQuery(
    string Query,
    int Page = SearchConstants.Pagination.DefaultPage,
    int PageSize = SearchConstants.Pagination.DefaultPageSize
) : IRequest<IReadOnlyList<object>>;