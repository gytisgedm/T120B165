using api.Context;
using api.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Features.Assets.Queries;

[Route("/employee/{username}")]
public class GetEmployee : ControllerBase
{
    private readonly IMediator _mediator;

    public GetEmployee(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromRoute] string username) 
    {
        var query = new GetEmployeeQuery(username);
        if (query == null)
            return BadRequest();
        return Ok(await _mediator.Send(query));
    }
       
}

public class GetEmployeeQuery : IRequest<IEnumerable<EmployeeSummaryViewModel>>
{
    public string Username { get; }

    public GetEmployeeQuery(string username)
    {
        Username = username; //?? throw new ArgumentNullException(nameof(username));
    }
}

public class GetEmployeeHandler : IRequestHandler<GetEmployeeQuery, IEnumerable<EmployeeSummaryViewModel>>
{
    private readonly FixedAssetsContext _db;

    public GetEmployeeHandler(FixedAssetsContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<EmployeeSummaryViewModel>> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
    {
        return _db.Employees.Where(e => e.Username == request.Username).Select(e => new EmployeeSummaryViewModel()
        {
            Username = e.Username,
            Name = e.Name,
            Surname = e.Surname,
            Department = e.Department,
            IsAdmin = e.IsAdmin
        });
    }
}
