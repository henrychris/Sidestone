using FluentValidation;
using HenryUtils.Extensions;
using HenryUtils.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Sidestone.Host.Configuration.Settings;
using Sidestone.Host.Data.Entities;
using Sidestone.Host.Data.Enums;
using Sidestone.Host.Features.Auth.SignUp;
using Sidestone.Host.Mappers;
using Sidestone.Host.Utility;

namespace Sidestone.Host.Features.Auth.Login
{
    public class LoginRequest : IRequest<Result<AuthResponse>>
    {
        public string EmailAddress { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class Handler(
        SignInManager<ApplicationUser> signInManager,
        IValidator<LoginRequest> validator,
        ILogger<Handler> logger,
        IOptions<JwtSettings> options
    ) : IRequestHandler<LoginRequest, Result<AuthResponse>>
    {
        private readonly TokenUtility _tokenUtility = new(options.Value);

        public async Task<Result<AuthResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var err = validationResult.ToErrorList();
                logger.LogError("Validation failed for request.\nErrors: {errors}.", err);
                return Result<AuthResponse>.Failure(err);
            }

            // todo: complete sign in.

            //var user = UserMapper.CreateUser(request);
            var signInResponse = await signInManager.PasswordSignInAsync(request.EmailAddress, request.Password, false, false);
            if (!signInResponse.Succeeded)
            {
                var errors = signInResponse.Errors.Select(e => Error.Failure(e.Code, e.Description)).ToList();
                logger.LogError("Failed to create user. Errors: {errors}", errors);
                return Result<AuthResponse>.Failure(errors);
            }

            var token = _tokenUtility.CreateUserJwt(user.Email!, request.Role, user.Id);
            var authResponse = UserMapper.ToAuthResponse(user, request.Role, token);
            return Result<AuthResponse>.Success(authResponse);
        }
    }

    public class Validator : AbstractValidator<LoginRequest>
    {
        public Validator()
        {
            RuleFor(x => x.EmailAddress).EmailAddress().NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
