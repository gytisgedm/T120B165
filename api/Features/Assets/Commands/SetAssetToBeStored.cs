using api.Context;
using api.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace api.Features.Assets.Commands;

[Route("/fixed-asset/store")]
public class SetAssetToBeStored : ControllerBase
{
    private readonly IMediator _mediator;

    public SetAssetToBeStored(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Roles = "FAManager")]
    public async Task<IActionResult> Store([FromBody] SetAssetToBeStoredCommand command)
    {
        if (command == null)
            return BadRequest();
        return Ok(await _mediator.Send(command));
    }
}

public class SetAssetToBeStoredCommand : IRequest<bool>
{
    public string Code { get; }

    public SetAssetToBeStoredCommand(string code)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
    }
}

public class SetAssetToBeStoredCommandHandler : IRequestHandler<SetAssetToBeStoredCommand, bool>
{
    private readonly FixedAssetsContext _db;

    public SetAssetToBeStoredCommandHandler(FixedAssetsContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(SetAssetToBeStoredCommand request, CancellationToken cancellationToken)
    {
        var asset = _db.FixedAssets.Where(e => e.Code == request.Code).FirstOrDefault();
        if (asset == null)
            throw new ArgumentNullException(nameof(asset));
        asset.AssignedTo = null;
        asset.AssignedBy = asset.ManagedBy;

        var assetEvent = new StoredByManagerEvent
        {
            CreatedBy = asset.ManagedBy,
            FixedAssetCode = request.Code
        };

        _db.FixedAssetEvents.Add(assetEvent);

        await _db.SaveChangesAsync(cancellationToken);

        return true;
    }
}