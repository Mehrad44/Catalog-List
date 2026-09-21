using Carter;
using Catalog.Service.Security.Application.Users.Contracts;
using Catalog.Service.Security.Application.Users.Contracts.Dtos;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Security.Service.Security.Persentation
{
    public class UserEndPoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var Users = app.MapGroup("Users").WithTags("Users");

            Users.MapPost("/register", RegisterAsync);
            Users.MapPost("/login", LoginAsync);



        }


        private async Task<IResult> RegisterAsync(
            [FromBody] UserForRegistrationDtos registrationDto,
            [FromServices] IUserManager UserManager)
        {

            var result = await UserManager.RegisterAsync(registrationDto);

            if (result.IsSuccess)
            {
                return Results.Ok();

            }
            var validationResult = JsonSerializer.Deserialize<ValidationResult>(result.Error);
            if (validationResult is not null)
            {
                return Results.ValidationProblem(validationResult!.ToDictionary());

            }

            return Results.BadRequest(result.Error);




        }




        private async Task<IResult> LoginAsync(
            [FromBody] UsserForLoginDto LoginDto,
            [FromServices] IUserManager UserManager)
        {

            var result = await UserManager.LoginAsync(LoginDto);

            if (result.IsSuccess)
            {
                return Results.Ok(result.Value);

            }
            var validationResult = JsonSerializer.Deserialize<ValidationResult>(result.Error);
            if (validationResult is not null)
            {
                return Results.ValidationProblem(validationResult!.ToDictionary());

            }

            return Results.BadRequest(result.Error);




        }



    }
}
