using api.Context;
using api.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Features.Assets.Commands;

[Route("/fixed-asset/manager")]
public class AddFixedAssetManager : ControllerBase
{
    private readonly IMediator _mediator;

    public AddFixedAssetManager(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddFixedAssetManagerCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
}

public class AddFixedAssetManagerCommand : IRequest<bool>
{
    public string Username { get; set; }
    public string FACategory { get; set; }
}

public class AddFixedAssetManagerCommandHandler : IRequestHandler<AddFixedAssetManagerCommand, bool>
{
    private readonly FixedAssetsContext _db;

    public AddFixedAssetManagerCommandHandler(FixedAssetsContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(AddFixedAssetManagerCommand request, CancellationToken cancellationToken)
    {
        var manager = new FixedAssetManager()
        {
            Username = request.Username,
            FACategory = request.FACategory,
        };

        _db.FixedAssetManagers.Add(manager);

        await _db.SaveChangesAsync(cancellationToken);

        return true;
    }
}