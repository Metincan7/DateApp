using System.ComponentModel.DataAnnotations;
using Api.Entites;

namespace Api.Interfaces
{
    public interface ITokenService
    {
       string CreateToken (AppUser user);
    }
}