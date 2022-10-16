using api.Context;
using api.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Features.Assets.Queries;

[Route("/fixed-assets/managed/{username}")]
public class GetMyManagedAssets : ControllerBase
{
    private readonly IMediator _mediator;

    public GetMyManagedAssets(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetManagedAssets([FromRoute] string username) =>
        Ok(await _mediator.Send(new GetMyManagedAssetsQuery(username)));

}

public class ManagedAssetSummaryViewModel
{
    public string Code { get; set; }
    public string? Description { get; set; }
    public string? AssignedTo { get; set; }
    public string? SerialNumber { get; set; }
    public DateTime? BoughtAt { get; set; }
    public FixedAssetEventType? EventType { get; set; }
}

public class GetMyManagedAssetsQuery : IRequest<IEnumerable<ManagedAssetSummaryViewModel>>
{
    public string Username { get; }

    public GetMyManagedAssetsQuery(string username)
    {
        Username = username ?? throw new ArgumentNullException(nameof(username));
    }
}

public class GetMyManagedAssetsHandler : IRequestHandler<GetMyManagedAssetsQuery, IEnumerable<ManagedAssetSummaryViewModel>>
{
    private readonly FixedAssetsContext _db;

    public GetMyManagedAssetsHandler(FixedAssetsContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<ManagedAssetSummaryViewModel>> Handle(GetMyManagedAssetsQuery request, CancellationToken cancellationToken)
    {
        var assets = await _db.FixedAssets
            .Include(x => x.Events)
            .Where(a => a.ManagedBy == request.Username && a.IsSold == false)
            .ToListAsync(cancellationToken: cancellationToken);

        return assets.Select(a => new ManagedAssetSummaryViewModel()
        {
            Code = a.Code,
            Description = a.Description,
            AssignedTo = a.AssignedTo,
            SerialNumber = a.SerialNumber,
            BoughtAt = a.BoughtAt,
            EventType = a.Events.OrderByDescending(x => x.CreatedAt).Select(x => x.Type).FirstOrDefault()
        });
    }
}
