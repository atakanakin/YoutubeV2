namespace YoutubeV2.Features.Video.GetCaptionDetails;

using MediatR;
using YoutubeV2.Features.Video.Models;

public sealed record GetCaptionDetailsQuery(string VideoIdOrUrl) : IRequest<CaptionDetailsResult?>; 