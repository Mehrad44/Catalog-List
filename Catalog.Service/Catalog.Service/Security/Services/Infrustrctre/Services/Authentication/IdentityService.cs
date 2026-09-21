using Catalog.Service.Security.Application.Contracts;
using Catalog.Service.Security.Services.Domain.Users;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Catalog.Service.Security.Services.Infrustrctre.Services.Authentication
{
    public class IdentityService(UserManager<User> userManager , IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory) : IIdentityService
    {
        public async Task<Result> AddClaimsAsync(User user, IEnumerable<Claim> claims)
        {
            var addClaimsResult = await userManager.AddClaimsAsync(user, claims);

            if (addClaimsResult.Succeeded)
            {
                return Result.Success();
            }
            else
            {
                return Result.Failure(string.Join(",", addClaimsResult.Errors.Select(e => e.Description)));
            }

        }

        public async Task<Result> CheckPasswordAsync(User user, string password)
        {
            var isValidPassword = await userManager.CheckPasswordAsync(user, password);

            if (isValidPassword)
                return Result.Success();
            else
                return Result.Failure
                    (
                    "User name or password is incorrect"
                    );
        }

        public async Task<Result<User>> FindByNameAsync(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            
            if(user is not null)
            {
                return user;
            }
            else
            {
                return Result.Failure<User>("User Name not found ");
            }
        }

        public Task<Result<string>> GenerateIdTokenAsync(IEnumerable<Claim> claims, DateTime expire)
        {
          return  Task.FromResult(Result.Success("fasdasdasdasdasdasdasdasdasdasdasdasdasdasdasdasda"));
        }

        public Task<Result<IEnumerable<Claim>>> GetResultAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> GGetClaimsAsync(User user, string password)
        {
           var principal = await userClaimsPrincipalFactory.CreateAsync(user);

            return Result.Success(principal.Claims);
        }

        public async Task<Result> RegisterAsync(User user, string password)
        {
            var RegisterResult = await userManager.CreateAsync(user, password);

            if (RegisterResult.Succeeded)
            {
                return Result.Success();
            }
            else
            {
               return Result.Failure(string.Join(",", RegisterResult.Errors.Select(e => e.Description)));
            }
        }
    }
}
