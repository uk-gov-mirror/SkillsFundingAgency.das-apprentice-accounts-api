using MediatR;
using Microsoft.Extensions.Logging;
using SFA.DAS.ApprenticeAccounts.Data;
using SFA.DAS.ApprenticeAccounts.DTOs.MyApprenticeship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeAccounts.Application.Commands.PatchMyApprenticeshipCommand;

public class PatchMyApprenticeshipCommandHandler : IRequestHandler<PatchMyApprenticeshipCommand, bool>
{
    private readonly IMyApprenticeshipContext _myApprenticeships;
    private readonly ILogger<PatchMyApprenticeshipCommandHandler> _logger;

    public PatchMyApprenticeshipCommandHandler(IMyApprenticeshipContext myApprenticeships, ILogger<PatchMyApprenticeshipCommandHandler> logger)
    {
        _myApprenticeships = myApprenticeships;
        _logger = logger;
    }

    public async Task<bool> Handle(PatchMyApprenticeshipCommand request, CancellationToken cancellationToken)
    {
        var myApprenticeship = await _myApprenticeships.FindByApprenticeId(request.ApprenticeId);

        if (myApprenticeship == null)
        {
            _logger.LogInformation("Apprenticeship not found for apprenticeId {ApprenticeId}", request.ApprenticeId);
            return false;
        }

        var dto = new MyApprenticeshipDto
        {
            Uln = myApprenticeship.Uln,
            ApprenticeshipId = myApprenticeship.ApprenticeshipId,
            EmployerName = myApprenticeship.EmployerName,
            StartDate = myApprenticeship.StartDate,
            EndDate = myApprenticeship.EndDate,
            StandardUId = myApprenticeship.StandardUId,
            TrainingCode = myApprenticeship.TrainingCode,
            TrainingProviderId = myApprenticeship.TrainingProviderId,
            TrainingProviderName = myApprenticeship.TrainingProviderName
        };

        request.PatchData.ApplyTo(dto);

        myApprenticeship.Uln = dto.Uln;
        myApprenticeship.ApprenticeshipId = dto.ApprenticeshipId;
        myApprenticeship.EmployerName = dto.EmployerName;
        myApprenticeship.StartDate = dto.StartDate;
        myApprenticeship.EndDate = dto.EndDate;
        myApprenticeship.StandardUId = dto.StandardUId;
        myApprenticeship.TrainingCode = dto.TrainingCode;
        myApprenticeship.TrainingProviderId = dto.TrainingProviderId;
        myApprenticeship.TrainingProviderName = dto.TrainingProviderName;

        _myApprenticeships.Update(myApprenticeship);

        return true;
    }
}
