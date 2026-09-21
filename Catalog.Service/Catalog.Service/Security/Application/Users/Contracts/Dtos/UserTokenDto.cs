namespace Catalog.Service.Security.Application.Users.Contracts.Dtos
{
    public sealed record UserTokenDto( string IdToken ,string AccessToken , string RefreshToken  ,int ExpiresIn);
}
