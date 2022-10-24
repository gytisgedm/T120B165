using api.Context;
using api.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Features.Assets.Commands;

[Route("/fixed-asset/accept")]
public class AcceptAssignedAsset : ControllerBase
{
    private readonly IMediator _mediator;

    public AcceptAssignedAsset(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AcceptAssignedAssetCommand command)
    {
        if (command == null)
            return BadRequest();
        return Ok(await _mediator.Send(command));
    }
}

public class AcceptAssignedAssetCommand : IRequest<bool>
{
    public string Code { get; set; }
    public string RequestedBy{ get; set; }
}

public class AcceptAssignedAssetCommandHandler : IRequestHandler<AcceptAssignedAssetCommand, bool>
{
    private readonly FixedAssetsContext _db;

    public AcceptAssignedAssetCommandHandler(FixedAssetsContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(AcceptAssignedAssetCommand request, CancellationToken cancellationToken)
    {
        var asset = _db.FixedAssets.Where(e => e.Code == request.Code).FirstOrDefault();
        if (asset == null)
            throw new ArgumentNullException(nameof(asset));

        var assetEvent = new AcceptedByUserEvent
        {
            CreatedBy = request.RequestedBy,
            FixedAssetCode = request.Code
        };

        _db.FixedAssetEvents.Add(assetEvent);

        await _db.SaveChangesAsync(cancellationToken);

        return true;
    }
}