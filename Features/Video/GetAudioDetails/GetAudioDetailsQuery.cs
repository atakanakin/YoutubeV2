namespace YoutubeV2.Features.Video.GetAudioDetails;

using MediatR;
using YoutubeV2.Features.Video.Models;

public sealed record GetAudioDetailsQuery(string VideoIdOrUrl) : IRequest<AudioDetailsResult?>; 