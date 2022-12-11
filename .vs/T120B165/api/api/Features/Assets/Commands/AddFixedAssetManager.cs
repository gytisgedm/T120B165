using api.Context;
using api.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Features.Assets.Commands;

[Route("manager")]
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
        if (command == null)
            return BadRequest();
        bool completed = await _mediator.Send(command);
        if (completed)
            return Ok();
        else return BadRequest();
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
        var categories = _db.FixedAssets.Select(a => a.Class);
        if (!categories.Contains(request.FACategory))
        {
            return false; // Not quite right
        }

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