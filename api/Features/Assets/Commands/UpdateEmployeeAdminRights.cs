using api.Context;
using api.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace api.Features.Assets.Commands;

[Route("/employee/")]
public class UpdateEmployeeAdminRights : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateEmployeeAdminRights(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update([FromBody] UpdateEmployeeAdminRightsCommand command)
    {
        if (command == null)
            return BadRequest();
        bool completed = (await _mediator.Send(command));
        if (completed)
            return Ok();
        else return NotFound();
    }
}

public class UpdateEmployeeAdminRightsCommand : IRequest<bool>
{
    public string Username { get; set; }
}

public class UpdateEmployeeAdminRightsCommandHandler : IRequestHandler<UpdateEmployeeAdminRightsCommand, bool>
{
    private readonly FixedAssetsContext _db;

    public UpdateEmployeeAdminRightsCommandHandler(FixedAssetsContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(UpdateEmployeeAdminRightsCommand request, CancellationToken cancellationToken)
    {
        var employee = _db.Employees.Where(e => e.Username == request.Username).FirstOrDefault();

        if (employee == null)
            return false;

        employee.IsAdmin = !employee.IsAdmin;

        await _db.SaveChangesAsync(cancellationToken);

        return true;
    }
}