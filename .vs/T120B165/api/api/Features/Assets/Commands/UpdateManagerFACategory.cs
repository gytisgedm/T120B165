using api.Context;
using api.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Features.Assets.Commands;

[Route("/fixed-asset/manager/update")]
public class UpdateManagerFACategory : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateManagerFACategory(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateManagerFACategoryCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
}

public class UpdateManagerFACategoryCommand : IRequest<bool>
{
    public string Username { get; set; }
    public string CurrentCategory { get; set; }
    public string NewCategory { get; set; }
}

public class UpdateManagerFACategoryCommandHandler : IRequestHandler<UpdateManagerFACategoryCommand, bool>
{
    private readonly FixedAssetsContext _db;

    public UpdateManagerFACategoryCommandHandler(FixedAssetsContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(UpdateManagerFACategoryCommand request, CancellationToken cancellationToken)
    {
        var manager = _db.FixedAssetManagers.Where(m => m.Username == request.Username && m.FACategory == request.CurrentCategory).FirstOrDefault();

        if (manager == null)
            throw new ArgumentNullException(nameof(manager));

        _db.FixedAssetManagers.Remove(manager);
        await _db.SaveChangesAsync(cancellationToken);

        _db.FixedAssetManagers.Add(new FixedAssetManager
        {
            Username = request.Username,
            FACategory = request.NewCategory,
        });

        await _db.SaveChangesAsync(cancellationToken);

        return true;
    }
}