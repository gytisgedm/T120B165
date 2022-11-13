using api.Context;
using api.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace api.Features.Assets.Queries;

[Route("/employees")]
public class GetAllEmployees : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllEmployees(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetEmployees() =>
        Ok(await _mediator.Send(new GetAllEmployeesQuery()));

}

public class EmployeeSummaryViewModel
{
    public string Username { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Department { get; set; }
    public bool? IsAdmin { get; set; }
}

public class GetAllEmployeesQuery : IRequest<IEnumerable<EmployeeSummaryViewModel>>
{
    public GetAllEmployeesQuery()
    {

    }
}

public class GetAllEmployeesHandler : IRequestHandler<GetAllEmployeesQuery, IEnumerable<EmployeeSummaryViewModel>>
{
    private readonly FixedAssetsContext _db;

    public GetAllEmployeesHandler(FixedAssetsContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<EmployeeSummaryViewModel>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
    {
        return _db.Employees.Select(e => new EmployeeSummaryViewModel()
        {
            Username = e.Username,
            Name = e.Name,
            Surname = e.Surname,
            Department = e.Department,
            IsAdmin = e.IsAdmin
        });
    }
}
