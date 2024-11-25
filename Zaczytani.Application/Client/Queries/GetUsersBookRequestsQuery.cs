using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zaczytani.Application.Dtos;
using Zaczytani.Application.Filters;
using Zaczytani.Domain.Repositories;

namespace Zaczytani.Application.Client.Queries;

public class GetUsersBookRequestsQuery : IRequest<IEnumerable<UserBookRequestDto>>, IUserIdAssignable
{
    private Guid UserId { get; set; }

    public void SetUserId(Guid userId)
    {
        UserId = userId;
    }

    private class GetUsersBookRequestsQueryHandler(IBookRequestRepository bookRequestRepository, IMapper mapper) : IRequestHandler<GetUsersBookRequestsQuery, IEnumerable<UserBookRequestDto>>
    {
        private readonly IBookRequestRepository _bookRequestRepository = bookRequestRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<IEnumerable<UserBookRequestDto>> Handle(GetUsersBookRequestsQuery request, CancellationToken cancellationToken)
        {
            var bookRequests = await _bookRequestRepository.GetByUserId(request.UserId).ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<UserBookRequestDto>>(bookRequests);
        }
    }
}
