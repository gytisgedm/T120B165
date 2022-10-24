using api.Context;
using api.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Features.Assets.Commands;

[Route("/fixed-asset/return")]
public class InitiateReturn : ControllerBase
{
    private readonly IMediator _mediator;

    public InitiateReturn(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Return([FromBody] InitiateReturnCommand command)
    {
        if (command == null)
            return BadRequest();
        return Ok(await _mediator.Send(command));
    }
}

public class InitiateReturnCommand : IRequest<bool>
{
    public string Code { get; set; }
    public string RequestedBy{ get; set; }
}

public class InitiateReturnCommandHandler : IRequestHandler<InitiateReturnCommand, bool>
{
    private readonly FixedAssetsContext _db;

    public InitiateReturnCommandHandler(FixedAssetsContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(InitiateReturnCommand request, CancellationToken cancellationToken)
    {
        var asset = _db.FixedAssets.Where(e => e.Code == request.Code).FirstOrDefault();
        if (asset == null)
            throw new ArgumentNullException(nameof(asset));

        var assetEvent = new ReturnInitiatedByManagerEvent
        {
            CreatedBy = request.RequestedBy,
            FixedAssetCode = request.Code
        };

        _db.FixedAssetEvents.Add(assetEvent);

        await _db.SaveChangesAsync(cancellationToken);

        return true;
    }
}