using api.Context;
using api.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Features.Assets.Commands;

[Route("/employee/remove/{username}")]
public class RemoveEmployee : ControllerBase
{
    private readonly IMediator _mediator;

    public RemoveEmployee(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut]
    public async Task<IActionResult> Remove([FromRoute] string username)
    {
        return Ok(await _mediator.Send(new RemoveEmployeeCommand(username)));
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
            throw new ArgumentNullException(nameof(employee));

        employee.ContractTerminated = true; 

        await _db.SaveChangesAsync(cancellationToken);

        return true;
    }
}