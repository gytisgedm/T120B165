﻿using api.Context;
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
        if (command == null)
            return BadRequest();
        bool completed = (await _mediator.Send(new RemoveAssetManagerCommand()));
        if (completed)
            return Ok();
        else return NotFound();
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
        var asset = _db.FixedAssetManagers.Where(e => e.Username == request.Username && e.FACategory == request.FACategory).FirstOrDefault();
        if (asset == null)
            return false;
        _db.FixedAssetManagers.Remove(new FixedAssetManager() 
        {
            Username = request.Username,
            FACategory = request.FACategory,
        });

        await _db.SaveChangesAsync(cancellationToken);

        return true;
    }
}