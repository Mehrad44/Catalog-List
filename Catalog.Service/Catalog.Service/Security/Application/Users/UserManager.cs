using Catalog.Service.Security.Application.Contracts;
using Catalog.Service.Security.Application.Users.Contracts;
using Catalog.Service.Security.Application.Users.Contracts.Dtos;
using Catalog.Service.Security.Services.Domain.Users;
using CSharpFunctionalExtensions;
using System.Security.Claims;

namespace Catalog.Service.Security.Application.Users
{
    public class UserManager(IIdentityService identityService) : IUserManager
    {
        public async Task<Result<UserTokenDto>> LoginAsync(UsserForLoginDto loginDto)

        {
            var userFindResult = await identityService.FindByNameAsync(loginDto.userName);

            if (userFindResult.IsFailure)
            {
                return Result.Failure<UserTokenDto>(userFindResult.Error);
            }

            var checkPasswordResult = await identityService.CheckPasswordAsync(userFindResult.Value, loginDto.Password);

            if(checkPasswordResult.IsFailure)
            {
                return Result.Failure<UserTokenDto>(checkPasswordResult.Error);
            }

            var generateTokenResult = await GenerateToken(userFindResult.Value);






        }


        private async Task<Result<UserTokenDto>> GenerateToken(User user)
        {
            var cliamsReuslt = await identityService.GetResultAsync(user);

            var expire = DateTime.Now.AddSeconds(5);
            var tokenResult = await identityService.GenerateIdTokenAsync(cliamsReuslt,);


        }

        public async Task<Result> RegisterAsync(UserForRegistrationDtos registrationDto)
        {
           var userFindResult = await identityService.FindByNameAsync(registrationDto.UserName);

            if (userFindResult.IsSuccess)
            {
                return Result.Failure($"UserName {registrationDto.UserName} already Registered");
            }

            User user = new() { UserName = registrationDto.UserName, Email = registrationDto.Email};


            var regusterResult = await identityService.RegisterAsync(user,registrationDto.Password);

            if(regusterResult.IsFailure)
                return regusterResult;

            var claims = new List<Claim>
            {
               new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
               new Claim(ClaimTypes.Name , registrationDto.UserName),
               new Claim(ClaimTypes.Email , registrationDto.Email),
               new Claim(ClaimTypes.GivenName , registrationDto.FirstName),
               new Claim(ClaimTypes.Surname , registrationDto.LastName),



            };

            var addClaimsResult = await identityService.AddClaimsAsync(user,claims);

            if(addClaimsResult.IsFailure) {

                return addClaimsResult;

            }
         

                return Result.Success();

        }
    }
}
