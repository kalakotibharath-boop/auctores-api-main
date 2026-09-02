using Microsoft.AspNetCore.Mvc;
using AuctoresOnline.API.Models.Common;

namespace AuctoresOnline.API.Controllers.User;

[ApiController]
[Route("api/user")]
public abstract class UserBaseController : ControllerBase
{
    protected ActionResult ToResult(ServiceResult result) => result.StatusCode switch
    {
        404 => NotFound(ApiResponse.Fail(result.Message!)),
        409 => Conflict(ApiResponse.Fail(result.Message!)),
        _ when !result.Success => BadRequest(ApiResponse.Fail(result.Message!)),
        _ => Ok(ApiResponse.Ok(result.Message))
    };

    protected ActionResult ToResult<T>(ServiceResult<T> result) => result.StatusCode switch
    {
        404 => NotFound(ApiResponse<T>.Fail(result.Message!)),
        409 => Conflict(ApiResponse<T>.Fail(result.Message!)),
        _ when !result.Success => BadRequest(ApiResponse<T>.Fail(result.Message!)),
        _ => Ok(ApiResponse<T>.Ok(result.Data!, result.Message))
    };
}
