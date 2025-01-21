using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Zaczytani.Domain.Entities;
using Zaczytani.Domain.Enums;
using Zaczytani.Domain.Exceptions;
using Zaczytani.Domain.Repositories;

namespace Zaczytani.Application.Client.Commands;

public record UserBlockedCommand(Guid ReportId):IRequest
{
    private class UserBlockedCommandHandler(IEmailInfoRepository emailInfoRepository, IReportRepository reportRepository, IConfiguration configuration) : IRequestHandler<UserBlockedCommand>
    {
        private readonly IReportRepository _reportRepository = reportRepository;
        private readonly IEmailInfoRepository _emailInfoRepository = emailInfoRepository;
        private readonly string _frontendUrl = configuration.GetSection("FrontendUrl").Value
            ?? throw new InvalidOperationException("Frontend URL is not configured. Please set 'FrontendUrl' in appsettings.json.");

        public async Task Handle(UserBlockedCommand request, CancellationToken cancellationToken)
        {
            var report = await _reportRepository.GetReportById(request.ReportId)
                .Include(r => r.Review)
                .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new NotFoundException("Report with given ID not found");

            if (report.User.Email is null)
                return;

            var emailInfo = new EmailInfo()
            {
                EmailTo = report.User.Email,
                EmailContent = [report.Category.ToString()],//, string.Format("{0}/review/{1}", _frontendUrl, re.Id)],
                EmailTemplate = EmailTemplate.UserBlocked,
            };

            await _emailInfoRepository.AddAsync(emailInfo, cancellationToken);
            await _emailInfoRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
