namespace YoutubeV2.Features.Video.GetVideoDetails;

using MediatR;
using YoutubeV2.Features.Video.Models;

public sealed record GetVideoDetailsQuery(string VideoIdOrUrl) : IRequest<VideoDetailsResult?>; 