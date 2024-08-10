using HenryUtils.Api.Controllers;
using HenryUtils.Extensions;
using HenryUtils.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sidestone.Host.Features.Auth;
using Sidestone.Host.Features.Auth.SignUp;

namespace Sidestone.Host.Controllers
{
    public class AuthController(IMediator mediator) : BaseController
    {
        [HttpPost("register")]
        [ProducesResponseType(typeof(Result<AuthResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> RegisterAsync(SignUpRequest request)
        {
            var result = await mediator.Send(request);
            return result.Match(_ => Ok(result.ToSuccessfulApiResponse()), ReturnErrorResponse);
        }
    }
}
