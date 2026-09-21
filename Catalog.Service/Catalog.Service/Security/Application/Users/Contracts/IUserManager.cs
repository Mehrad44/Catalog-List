using Catalog.Service.Security.Application.Users.Contracts.Dtos;
using CSharpFunctionalExtensions;

namespace Catalog.Service.Security.Application.Users.Contracts
{
    public interface IUserManager
    {
         Task<Result> RegisterAsync(UserForRegistrationDtos registrationDto);

         Task<Result<UserTokenDto>> LoginAsync(UsserForLoginDto loginDto);


    }
}