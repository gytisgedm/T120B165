using api.Context;
using api.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Features.Assets.Queries;

[Route("/fixed-assets/get/{code}")]
public class GetFixedAsset : ControllerBase
{
    private readonly IMediator _mediator;

    public GetFixedAsset(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAssignedAssets([FromRoute] string code) =>
        Ok(await _mediator.Send(new GetFixedAssetQuery(code)));

}

public class GetFixedAssetQuery : IRequest<IEnumerable<AssignedAssetSummaryViewModel>>
{
    public string Code { get; }

    public GetFixedAssetQuery(string code)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
    }
}

public class GetFixedAssetHandler : IRequestHandler<GetFixedAssetQuery, IEnumerable<AssignedAssetSummaryViewModel>>
{
    private readonly FixedAssetsContext _db;

    public GetFixedAssetHandler(FixedAssetsContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<AssignedAssetSummaryViewModel>> Handle(GetFixedAssetQuery request, CancellationToken cancellationToken)
    {
        return _db.FixedAssets.Where(a => a.Code == request.Code).Select(a => new AssignedAssetSummaryViewModel()
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
