using api.Context;
using api.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Features.Assets.Commands;

[Route("/fixed-asset/assign")]
public class AssignAssetToEmployee : ControllerBase
{
    private readonly IMediator _mediator;

    public AssignAssetToEmployee(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AssignAssetToEmployeeCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
}

public class AssignAssetToEmployeeCommand : IRequest<bool>
{
    public string Code { get; set; }
    public string AssignTo { get; set; }
    public string AssignedBy{ get; set; }
}

public class AssignAssetToEmployeeCommandHandler : IRequestHandler<AssignAssetToEmployeeCommand, bool>
{
    private readonly FixedAssetsContext _db;

    public AssignAssetToEmployeeCommandHandler(FixedAssetsContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(AssignAssetToEmployeeCommand request, CancellationToken cancellationToken)
    {
        var asset = _db.FixedAssets.Where(e => e.Code == request.Code).FirstOrDefault();
        if (asset == null)
            throw new ArgumentNullException(nameof(asset));
        asset.AssignedBy = request.AssignedBy;
        asset.AssignedTo = request.AssignTo;

        var assetEvent = new AssignedToUserEvent
        {
            CreatedBy = request.AssignedBy,
            AssignedTo = request.AssignTo,
            FixedAssetCode = request.Code
        };

        _db.FixedAssetEvents.Add(assetEvent);

        await _db.SaveChangesAsync(cancellationToken);

        return true;
    }
}