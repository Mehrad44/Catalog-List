using Catalog.Service.Security.Services.Domain.Users;
using CSharpFunctionalExtensions;
using System.Security.Claims;

namespace Catalog.Service.Security.Application.Contracts
{
    public interface IIdentityService
    {
        Task<Result<User>> FindByNameAsync(string userName);


        Task<Result> RegisterAsync(User user , string password);

        Task<Result> AddClaimsAsync(User user, IEnumerable<Claim> claims);

        Task<Result> CheckPasswordAsync(User uer, string password);

        Task<Result> GGetClaimsAsync(User user , string password);

        Task<Result<IEnumerable<Claim>>> GetResultAsync(User user);


        Task<Result<string>> GenerateIdTokenAsync(IEnumerable<Claim> claims , DateTime expire);
    }
}
 