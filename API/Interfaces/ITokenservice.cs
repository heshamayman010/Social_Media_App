using API.Entities;
using Microsoft.AspNetCore.Identity;

namespace API.interfaces;

public interface ITokenService {

 Task<string> CreateToken(AppUser user);


}