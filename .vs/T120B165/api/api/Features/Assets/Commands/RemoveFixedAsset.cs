using api.Context;
using api.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Features.Assets.Commands;

[Route("/fixed-asset/remove/{code}")]
public class RemoveFixedAsset : ControllerBase
{
    private readonly IMediator _mediator;

    public RemoveFixedAsset(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut]
    public async Task<IActionResult> Assign([FromRoute] string code)
    {
        return Ok(await _mediator.Send(new RemoveFixedAssetCommand(code)));
    }
}

public class RemoveFixedAssetCommand : IRequest<bool>
{
    public string Code { get; set; }

    public RemoveFixedAssetCommand(string code)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
    }
}

public class RemoveFixedAssetCommandHandler : IRequestHandler<RemoveFixedAssetCommand, bool>
{
    private readonly FixedAssetsContext _db;

    public RemoveFixedAssetCommandHandler(FixedAssetsContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(RemoveFixedAssetCommand request, CancellationToken cancellationToken)
    {
        var asset = _db.FixedAssets.Where(e => e.Code == request.Code).FirstOrDefault();
        if (asset == null)
            throw new ArgumentNullException(nameof(asset));
        asset.IsSold = true; 

        await _db.SaveChangesAsync(cancellationToken);

        return true;
    }
}