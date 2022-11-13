using api.Context;
using api.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace api.Features.Assets.Queries;

[Route("assigned/{username}/fixed-assets")]
public class GetMyAssignedAssets : ControllerBase
{
    private readonly IMediator _mediator;

    public GetMyAssignedAssets(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Roles = "Employee")]
    public async Task<IActionResult> GetAssignedAssets([FromRoute] string username) 
    {
        var query = new GetMyAssignedAssetsQuery(username); ;
        if (query == null)
            return BadRequest();
        return Ok(await _mediator.Send(query));
    }
}

public class AssignedAssetSummaryViewModel
{
    public string Code { get; set; }
    public string? Description { get; set; }
    public string? AssignedBy { get; set; }
    public string? SerialNumber { get; set; }
    public DateTime? BoughtAt { get; set; }
    public FixedAssetEventType? EventType { get; set; }
}

public class GetMyAssignedAssetsQuery : IRequest<IEnumerable<AssignedAssetSummaryViewModel>>
{
    public string Username { get; }

    public GetMyAssignedAssetsQuery(string username)
    {
        Username = username; //?? throw new ArgumentNullException(nameof(username));
    }
}

public class GetMyAssignedAssetsHandler : IRequestHandler<GetMyAssignedAssetsQuery, IEnumerable<AssignedAssetSummaryViewModel>>
{
    private readonly FixedAssetsContext _db;

    public GetMyAssignedAssetsHandler(FixedAssetsContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<AssignedAssetSummaryViewModel>> Handle(GetMyAssignedAssetsQuery request, CancellationToken cancellationToken)
    {
        var assets = await _db.FixedAssets
            .Include(x => x.Events)
            .Where(a => a.AssignedTo == request.Username)
            .ToListAsync(cancellationToken: cancellationToken);

        return assets.Select(a => new AssignedAssetSummaryViewModel()
        {
            Code = a.Code,
            Description = a.Description,
            AssignedBy = a.AssignedBy,
            SerialNumber = a.SerialNumber,
            BoughtAt = a.BoughtAt,
            EventType = a.Events.OrderByDescending(x => x.CreatedAt).Select(x => x.Type).FirstOrDefault()
        });
    }
}
