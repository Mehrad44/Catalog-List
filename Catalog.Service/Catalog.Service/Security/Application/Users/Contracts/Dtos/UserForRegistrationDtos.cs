namespace Catalog.Service.Security.Application.Users.Contracts.Dtos
{
    public sealed record UserForRegistrationDtos(
        string UserName  , 
        string Password, 
        string Email ,
        string FirstName,
        string LastName );
}
