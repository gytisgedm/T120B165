using api.Context;
using api.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace api.Features.Assets.Commands;

[Route("/fixed-asset")]
public class AddFixedAsset : ControllerBase
{
    private readonly IMediator _mediator;

    public AddFixedAsset(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Roles = "FAManager")]
    public async Task<IActionResult> Add([FromBody] AddFixedAssetCommand command)
    {
        if (command == null)
            return BadRequest();
        return Ok(await _mediator.Send(command));
    }
}

public class AddFixedAssetCommand : IRequest<bool>
{
    public string Code { get; set; }
    public string? Description { get; set; }
    public string Class { get; set; }
    public string? SerialNumber { get; set; }
    public string? ManagedBy { get; set; }
    public DateTime? BoughtAt { get; set; }
}

public class AddFixedAssetCommandHandler : IRequestHandler<AddFixedAssetCommand, bool>
{
    private readonly FixedAssetsContext _db;

    public AddFixedAssetCommandHandler(FixedAssetsContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(AddFixedAssetCommand request, CancellationToken cancellationToken)
    {
        var asset = new FixedAsset()
        {
            Code = request.Code,
            Description = request.Description,
            Class = request.Class,
            SerialNumber = request.SerialNumber,
            ManagedBy = request.ManagedBy,
            BoughtAt = request.BoughtAt
        };

        _db.FixedAssets.Add(asset);

        await _db.SaveChangesAsync(cancellationToken);

        return true;
    }
}