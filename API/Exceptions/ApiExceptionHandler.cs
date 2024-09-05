using System;

namespace API.Exceptions;

public class ApiExceptionHandler(int Statuscode,string Message,string? Options )
{



public int statuscode { get; set; }=Statuscode;
public string message { get; set; }=Message;
public string? options { get; set; }=Options;



}
