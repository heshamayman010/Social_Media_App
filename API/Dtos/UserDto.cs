using System;

namespace API.Dtos;

public class UserDto
{

public string? knownas { get; set; }
public required string UserName{set;get;}

public required string Token{get;set;}
public string? photpUrl{set;get;}


}
