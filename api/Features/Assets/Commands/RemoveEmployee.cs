using api.Context;
using api.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace api.Features.Assets.Commands;

[Route("/employee/remove/{username}")]
public class RemoveEmployee : ControllerBase
{
    private readonly IMediator _mediator;

    public RemoveEmployee(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Remove([FromRoute] string username)
    {
        var command = new RemoveEmployeeCommand(username);
        if (command == null)
            return BadRequest();
        bool completed = await _mediator.Send(command);
        if (completed)
            return Ok();
        else return NotFound();
    }
}

public class RemoveEmployeeCommand : IRequest<bool>
{
    public string Username { get; }

    public RemoveEmployeeCommand(string username)
    {
        Username = username ?? throw new ArgumentNullException(nameof(username));
    }
}

public class RemoveEmployeeCommandHandler : IRequestHandler<RemoveEmployeeCommand, bool>
{
    private readonly FixedAssetsContext _db;

    public RemoveEmployeeCommandHandler(FixedAssetsContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(RemoveEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = _db.Employees.Where(e => e.Username == request.Username).FirstOrDefault();

        if (employee == null)
            return false;

        _db.Employees.Remove(employee);

        //employee.ContractTerminated = true; 

        await _db.SaveChangesAsync(cancellationToken);

        return true;
    }
}