using api.Context;
using api.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace api.Features.Assets.Commands;

[Route("/fixed-asset/reject")]
public class RejectAssignedAsset : ControllerBase
{
    private readonly IMediator _mediator;

    public RejectAssignedAsset(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Roles = "Employee")]
    public async Task<IActionResult> Reject([FromBody] RejectAssignedAssetCommand command)
    {
        if (command == null)
            return BadRequest();
        return Ok(await _mediator.Send(command));
    }
}

public class RejectAssignedAssetCommand : IRequest<bool>
{
    public string Code { get; set; }
    public string RequestedBy{ get; set; }
}

public class RejectAssignedAssetCommandHandler : IRequestHandler<RejectAssignedAssetCommand, bool>
{
    private readonly FixedAssetsContext _db;

    public RejectAssignedAssetCommandHandler(FixedAssetsContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(RejectAssignedAssetCommand request, CancellationToken cancellationToken)
    {
        var asset = _db.FixedAssets.Where(e => e.Code == request.Code).FirstOrDefault();
        if (asset == null)
            throw new ArgumentNullException(nameof(asset));
        asset.AssignedBy = null;
        asset.AssignedTo = null;

        var assetEvent = new RejectedByUserEvent
        {
            CreatedBy = request.RequestedBy,
            FixedAssetCode = request.Code
        };

        _db.FixedAssetEvents.Add(assetEvent);

        await _db.SaveChangesAsync(cancellationToken);

        return true;
    }
}