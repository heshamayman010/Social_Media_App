using System;

namespace API.Extensions;

public  static class AgeExtension
{
public static int CalculateAge(this DateOnly dateofbirth){

var today=DateOnly.FromDateTime(DateTime.Now);

var age=today.Year-dateofbirth.Year;
return age;
}

}
