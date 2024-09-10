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
[Required]
public  string? gender { get; set; }=string.Empty;
[Required]
public  string? knownAs { get; set; }=string.Empty;
[Required]
public  string? dateOfBirth { get; set; }=string.Empty;
[Required]
public  string? city { get; set; }=string.Empty;
[Required]
public  string? country { get; set; }=string.Empty;



}


// {
//     "gender": "male",
//     "username": "damam",
//     "knownAs": "hesham",
//     "dateOfBirth": "1985-01-23",
//     "city": "bel",
//     "country": "UK",
//     "password": "Password"
// }
// // city
// : 
// "asdf"
// comparepassword
// : 
// "asdf"
// country
// : 
// "asdf"
// dateofbirth
// : 
// "2024-09-30"
// gender
// : 
// "male"
// knownas
// : 
// "asdf"
// password
// : 
// "asdf"
// username
// : 
// "asdf"
