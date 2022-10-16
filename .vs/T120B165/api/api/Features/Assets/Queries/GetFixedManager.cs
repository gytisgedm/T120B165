using api.Context;
using api.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Features.Assets.Queries;

[Route("/fixed-assets/manager/{username}")]
public class GetFixedAssetManager : ControllerBase
{
    private readonly IMediator _mediator;

    public GetFixedAssetManager(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromRoute] string username) =>
       Ok(await _mediator.Send(new GetFixedAssetManagerQuery(username)));

}

public class GetFixedAssetManagerQuery : IRequest<IEnumerable<FixedAssetManagerViewModel>>
{
    public string Username { get; }

    public GetFixedAssetManagerQuery(string username)
    {
        Username = username ?? throw new ArgumentNullException(nameof(username));
    }
}

public class GetFixedAssetManagerHandler : IRequestHandler<GetFixedAssetManagerQuery, IEnumerable<FixedAssetManagerViewModel>>
{
    private readonly FixedAssetsContext _db;

    public GetFixedAssetManagerHandler(FixedAssetsContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<FixedAssetManagerViewModel>> Handle(GetFixedAssetManagerQuery request, CancellationToken cancellationToken)
    {
        return _db.FixedAssetManagers.Where(m => m.Username == request.Username).Select(m => new FixedAssetManagerViewModel()
        {
            Username = m.Username,
            FACategory = m.FACategory,
        });
    }
}
