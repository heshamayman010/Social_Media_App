using System;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos;

public class RegisterDto
{

[Required]
public  string username { get; set; }=string.Empty;

[Required]
[StringLength(20,MinimumLength =8)]
public  string password { get; set; }=string.Empty;


}
