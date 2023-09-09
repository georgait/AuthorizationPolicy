using LocalPolicy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.AuthorizarionPolicies;
using WebApi.Infrastructure.Data;

namespace WebApi.Controllers;

[Route("api/listing")]
[ApiController]
public class ListingController : ControllerBase
{
    private readonly ILocalPolicyManager _manager;
    private readonly ISubscriptionPolicyService _subscriptionPolicy;

    public ListingController(ILocalPolicyManager manager, ISubscriptionPolicyService subscriptionPolicy)
    {
        _manager = manager;
        _subscriptionPolicy = subscriptionPolicy;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserInfo()
    {
        var result = await _manager.EvaluateAsync(User);        
        return Ok(result);
    }

    [HttpGet("services/{id:guid}")]
    [Authorize("canBuySubscriptions")]
    public async Task<IActionResult> GetPaidService(Guid id)
    {
        var result = await _subscriptionPolicy.AuthorizeAsync(User, id);

        if (!result.Succeeded)
        {
            return BadRequest("Payment Required");
        }
            
        return Ok(result);
    }
}
