using AutoMapper;
using MediatR;
using Zaczytani.Application.Dtos;
using Zaczytani.Application.Filters;
using Zaczytani.Domain.Repositories;

namespace Zaczytani.Application.Client.Queries;

public class GetChallengeProgressesQuery : IRequest<IEnumerable<ChallengeProgressDto>>, IUserIdAssignable
{
    private Guid UserId { get; set; }
    public void SetUserId(Guid userId) => UserId = userId;

    private class GetChallengeProgressesQueryHandler(IChallengeRepository challengeRepository, IMapper mapper) : IRequestHandler<GetChallengeProgressesQuery, IEnumerable<ChallengeProgressDto>>
    {
        private readonly IChallengeRepository _challengeRepository = challengeRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<IEnumerable<ChallengeProgressDto>> Handle(GetChallengeProgressesQuery request, CancellationToken cancellationToken)
        {
            var progresses = await _challengeRepository.GetChallengesWithProgressByUserId(request.UserId, cancellationToken);

            return _mapper.Map<IEnumerable<ChallengeProgressDto>>(progresses);
        }
    }
}