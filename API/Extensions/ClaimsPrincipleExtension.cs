using System;
using System.Security.Claims;

namespace API.Extensions;

// this class is mainly used to get the user name from the claims instead of 
//wrirting the same functionality in multiple places 
public static class ClaimsPrincipleExtension
{

public static string GetUsername(this ClaimsPrincipal user)

{

var username=user.FindFirstValue(ClaimTypes.Name);
if(username==null) throw new Exception("the token contains no user name ");

return username;


}

public static int GetUserid(this ClaimsPrincipal user)

{

var id=int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)??throw new Exception("use id is null "));


return id;


}


}
