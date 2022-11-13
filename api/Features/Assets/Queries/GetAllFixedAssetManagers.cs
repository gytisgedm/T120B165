using api.Context;
using api.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Features.Assets.Queries;

[Route("/fixed-assets/managers")]
public class GetAllFixedAssetManagers : ControllerBase
{
    private readonly IMediator _mediator;

    public GetAllFixedAssetManagers(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetManagers() =>
        Ok(await _mediator.Send(new GetAllFixedAssetManagersQuery()));

}

public class FixedAssetManagerViewModel
{
    public string Username { get; set; }
    public string FACategory { get; set; }
}

public class GetAllFixedAssetManagersQuery : IRequest<IEnumerable<FixedAssetManagerViewModel>>
{
    public GetAllFixedAssetManagersQuery()
    {

    }
}

public class GetAllFixedAssetManagersHandler : IRequestHandler<GetAllFixedAssetManagersQuery, IEnumerable<FixedAssetManagerViewModel>>
{
    private readonly FixedAssetsContext _db;

    public GetAllFixedAssetManagersHandler(FixedAssetsContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<FixedAssetManagerViewModel>> Handle(GetAllFixedAssetManagersQuery request, CancellationToken cancellationToken)
    {
        return _db.FixedAssetManagers.Select(m => new FixedAssetManagerViewModel()
        {
            Username = m.Username,
            FACategory = m.FACategory,
        });
    }
}
