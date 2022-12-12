using api.Context;
using api.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace api.Features.Assets.Commands;

[Route("fixed-asset")]
public class UpdateFixedAsset : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateFixedAsset(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut]
    [Authorize(Roles = "FAManager, Admin")]
    public async Task<IActionResult> Update([FromBody] UpdateFixedAssetCommand command)
    {
        if (command == null)
            return BadRequest();
        bool completed = (await _mediator.Send(command));
        if (completed)
            return Ok();
        else return NotFound();
    }
}

public class UpdateFixedAssetCommand : IRequest<bool>
{
    public string Code { get; set; }
    public string Description { get; set; }
    public string SerialNumber { get; set; }
    public string Class { get; set; }
}

public class UpdateFixedAssetCommandHandler : IRequestHandler<UpdateFixedAssetCommand, bool>
{
    private readonly FixedAssetsContext _db;

    public UpdateFixedAssetCommandHandler(FixedAssetsContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(UpdateFixedAssetCommand request, CancellationToken cancellationToken)
    {
        var fixedAsset = _db.FixedAssets.Where(m => m.Code == request.Code).FirstOrDefault();

        if (fixedAsset == null)
            return false;

        _db.FixedAssets.Remove(fixedAsset);
        await _db.SaveChangesAsync(cancellationToken);

        _db.FixedAssets.Add(new FixedAsset
        {
            Code = request.Code,
            Description = request.Description,
            SerialNumber = request.SerialNumber,
            Class = request.Class
        });

        await _db.SaveChangesAsync(cancellationToken);

        return true;
    }
}