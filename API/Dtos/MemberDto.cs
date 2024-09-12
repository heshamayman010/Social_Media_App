using System;

namespace API.Dtos;

public class MemberDto
{


public int id { get; set; }    
public  string? UserName { get; set; }

public int Age {set;get;}
public  string?  PhotoUrl { get; set; }

public  string?  KnownAs { get; set; }
public DateTime Created{set;get;}
public DateTime LAstActiv {set;get;}
public  string? Gender { get; set; }

public string? Introduction { get; set; }
public string? Interstes { get; set; }
public string? LookingFor { get; set; }

public  string? City { get; set; }
public  string? Country { get; set; }

public List<PhotoDto>? Photos {set;get;}
}
