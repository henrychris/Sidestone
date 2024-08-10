using FluentValidation;
using HenryUtils.Extensions;
using HenryUtils.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Sidestone.Host.Configuration.Settings;
using Sidestone.Host.Data.Entities;
using Sidestone.Host.Data.Enums;
using Sidestone.Host.Mappers;
using Sidestone.Host.Utility;

namespace Sidestone.Host.Features.Auth.SignUp
{
    public class SignUpRequest : IRequest<Result<AuthResponse>>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
    }

    public class Handler(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IValidator<SignUpRequest> validator,
        ILogger<Handler> logger,
        IOptions<JwtSettings> options
    ) : IRequestHandler<SignUpRequest, Result<AuthResponse>>
    {
        private readonly TokenUtility _tokenUtility = new(options.Value);

        public async Task<Result<AuthResponse>> Handle(SignUpRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var err = validationResult.ToErrorList();
                logger.LogError("Validation failed for request.\nErrors: {errors}.", err);
                return Result<AuthResponse>.Failure(err);
            }

            if (!await roleManager.RoleExistsAsync(request.Role))
            {
                var error = Error.Failure("InvalidRole", $"The role '{request.Role}' does not exist.");
                logger.LogError("Invalid role specified. Error: {error}", error);
                return Result<AuthResponse>.Failure(error);
            }

            var user = UserMapper.CreateUser(request);
            var createResult = await userManager.CreateAsync(user, request.Password);
            if (!createResult.Succeeded)
            {
                var errors = createResult.Errors.Select(e => Error.Failure(e.Code, e.Description)).ToList();
                logger.LogError("Failed to create user. Errors: {errors}", errors);
                return Result<AuthResponse>.Failure(errors);
            }

            var addToRoleResult = await userManager.AddToRoleAsync(user, request.Role);
            if (!addToRoleResult.Succeeded)
            {
                var errors = addToRoleResult.Errors.Select(e => Error.Failure(e.Code, e.Description)).ToList();
                logger.LogError("Failed to add user to role. Errors: {errors}", errors);
                return Result<AuthResponse>.Failure(errors);
            }

            var token = _tokenUtility.CreateUserJwt(user.Email!, request.Role, user.Id);
            var authResponse = UserMapper.ToAuthResponse(user, request.Role, token);
            return Result<AuthResponse>.Success(authResponse);
        }
    }

    public class Validator : AbstractValidator<SignUpRequest>
    {
        public Validator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.EmailAddress).EmailAddress().NotEmpty();
            RuleFor(x => x.Password).NotEmpty();

            RuleFor(x => x.Role)
                .NotEmpty()
                .IsEnumName(typeof(Roles), caseSensitive: false)
                .WithMessage("Role must be one of: " + string.Join(", ", Enum.GetNames(typeof(Roles))));
        }
    }
}
