using api.Context;
using api.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Features.Assets.Commands;

[Route("/employee/admin")]
public class UpdateEmployeeAdminRights : ControllerBase
{
    private readonly IMediator _mediator;

    public UpdateEmployeeAdminRights(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateEmployeeAdminRightsCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
}

public class UpdateEmployeeAdminRightsCommand : IRequest<bool>
{
    public string Username { get; set; }
    public bool IsAdmin { get; set; }
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
            throw new ArgumentNullException(nameof(employee));

        employee.IsAdmin = request.IsAdmin;

        await _db.SaveChangesAsync(cancellationToken);

        return true;
    }
}