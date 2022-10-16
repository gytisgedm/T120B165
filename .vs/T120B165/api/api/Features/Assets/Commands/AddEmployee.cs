using api.Context;
using api.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Features.Assets.Commands;

[Route("/employee")]
public class AddEmployee : ControllerBase
{
    private readonly IMediator _mediator;

    public AddEmployee(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddEmployeeCommand command)
    {
        return Ok(await _mediator.Send(command));
    }
}

public class AddEmployeeCommand : IRequest<bool>
{
    public string Username { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Department { get; set; }
    public bool? IsAdmin { get; set; }
}

public class AddEmployeeCommandHandler : IRequestHandler<AddEmployeeCommand, bool>
{
    private readonly FixedAssetsContext _db;

    public AddEmployeeCommandHandler(FixedAssetsContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = new Employee()
        {
            Username = request.Username,
            Name = request.Name,
            Surname = request.Surname,
            Department = request.Department,
            IsAdmin = (bool)request.IsAdmin
        };

        _db.Employees.Add(employee);

        await _db.SaveChangesAsync(cancellationToken);

        return true;
    }
}