using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class TokenService (IConfiguration config) : ITokenService
{
    public string CreateToken(AppUser user)
    {

// this is the key that will be used in the jwt
var tokenkey=config["TokenKey"]??throw new Exception("cant access the token key");

// and make check for the token key length 

if(tokenkey.Length<64) throw new Exception("the token key must be longer than that");

// and now lets create the token 

//1 creating the key 
var Key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenkey));

// and create the claims that will be stored
var claims=new List<Claim>(){

new(ClaimTypes.NameIdentifier,user.UserName)
};
var credintials=new SigningCredentials(Key,SecurityAlgorithms.HmacSha512Signature);

// then we will create the descriptor for our token
var tokendescriptor=new SecurityTokenDescriptor(){

Subject=new ClaimsIdentity(claims),
Expires=DateTime.UtcNow.AddDays(7),
SigningCredentials=credintials

};

// then we will create the handler of the token 

var tokenhandller=new JwtSecurityTokenHandler();
var token=tokenhandller.CreateToken(tokendescriptor);
// and now we will get back the token to the user 
return tokenhandller.WriteToken(token);



    }
}
