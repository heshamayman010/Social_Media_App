using System;
using System.Net;
using System.Text.Json;
using API.Exceptions;

namespace API.Middlewares;

public class ExceptionHandlerMiddleware (RequestDelegate next ,ILogger<ExceptionHandlerMiddleware> logger,IHostEnvironment env)
{

public async Task  InvokeAsync(HttpContext context){

try
{
    await next(context);  // this part is used to move on to the next part of the middlewares if there is no erorrs 
}
catch (Exception ex)
{
    
logger.LogError(ex,ex.Message);   // this will log the error message to our terminal to sea it 

// then we will create the resoponse that will be sent using the context 
context.Response.ContentType="application/json";
context.Response.StatusCode=(int)HttpStatusCode.InternalServerError; // here we will maket its status code like the internal server error 
// then we need to check if it is in the developer mode or not 

var response=env.IsDevelopment()?new ApiExceptionHandler(context.Response.StatusCode,ex.Message,ex.StackTrace):

new ApiExceptionHandler(context.Response.StatusCode,ex.Message,"internal server erorr ");

// and to change the output as json we need to use 


var options=new JsonSerializerOptions{
PropertyNamingPolicy= JsonNamingPolicy.CamelCase
};

var output =JsonSerializer.Serialize(response,options);

await context.Response.WriteAsync(output);
}
}




}
