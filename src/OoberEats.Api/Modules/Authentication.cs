using OoberEats.Api.Interfaces;
using OoberEats.Contracts.Authentication;
using OoberEats.Application.Services;
using Microsoft.AspNetCore.Mvc;
using OoberEats.Application;

namespace OoberEats.Api.Modules
{
    public class Authentication : IMinimalApiModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var authenticationGroup = app.MapGroup("/auth").WithTags("Authentication");

            // register a new user
            authenticationGroup.MapPost("/register", async (IAuthService service, RegisterRequest request) =>
            {
                if (request.Email == string.Empty)
                {
                    return Results.BadRequest();
                }
                else
                {
                    var result = await service.RegisterAsync(request.FirstName, request.LastName, request.Email, request.Password);
                    return Results.Ok(result);
                }
            })
           .WithName("Register a new user")
           .WithOpenApi()
           .Produces<AuthenticationResult>(StatusCodes.Status201Created)
           .Produces(StatusCodes.Status400BadRequest);


            // Login with email and password
            authenticationGroup.MapPost("/login", async (IAuthService service, LoginRequest request) =>
            {
                if (request.Email == string.Empty || request.Password == string.Empty)
                {
                    return Results.Unauthorized();
                }
                else
                {
                    var result = await service.LoginAsync(request.Email, request.Password);
                    return Results.Ok(result);
                }
            })
            .WithName("Login with email and password")
            .WithOpenApi()
            .Produces<AuthenticationResult>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);
        }
    }
}
