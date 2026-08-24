using Azure;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using SFA.DAS.ApprenticeAccounts.DTOs.MyApprenticeship;
using SFA.DAS.ApprenticeAccounts.Infrastructure.Mediator;
using System;

namespace SFA.DAS.ApprenticeAccounts.Application.Commands.PatchMyApprenticeshipCommand;

public class PatchMyApprenticeshipCommand : IRequest<bool>, IUnitOfWorkCommand
{
    public PatchMyApprenticeshipCommand(Guid apprenticeId, JsonPatchDocument<MyApprenticeshipDto> patchData)
    {
        ApprenticeId = apprenticeId;
        PatchData = patchData;
    }
    public Guid ApprenticeId { get; set; }
    public JsonPatchDocument<MyApprenticeshipDto> PatchData { get; set; }
}
