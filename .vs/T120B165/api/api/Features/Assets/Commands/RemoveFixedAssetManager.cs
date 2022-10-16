using api.Context;
using api.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Features.Assets.Commands;

[Route("/fixed-asset/manager/remove/")]
public class RemoveFixedAssetManager : ControllerBase
{
    private readonly IMediator _mediator;

    public RemoveFixedAssetManager(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpDelete]
    public async Task<IActionResult> Remove([FromBody] RemoveAssetManagerCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
}

public class RemoveAssetManagerCommand : IRequest<bool>
{
    public string Username { get; set; }
    public string FACategory { get; set; }
}

public class RemoveFixedAssetManagerCommandHandler : IRequestHandler<RemoveAssetManagerCommand, bool>
{
    private readonly FixedAssetsContext _db;

    public RemoveFixedAssetManagerCommandHandler(FixedAssetsContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(RemoveAssetManagerCommand request, CancellationToken cancellationToken)
    {
        _db.FixedAssetManagers.Remove(new FixedAssetManager() 
        {
            Username = request.Username,
            FACategory = request.FACategory,
        });

        await _db.SaveChangesAsync(cancellationToken);

        return true;
    }
}