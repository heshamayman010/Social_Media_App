using System;
using System.Text.Json;
using API.Helpers;
using Microsoft.AspNetCore.Http.Json;
using Newtonsoft.Json.Serialization;

namespace API.Extensions;

public static class HttpExtensions
{
    public static void AddPaginationHeader<T>(this HttpResponse response,PageList<T> Data )
{   

var PaginationHeader=new PaginationHeader(Data.Currentpage,Data.Pagesize,Data.Count,Data.Totalpages);

// options for converting the data to json 
var jsonoptions=new JsonSerializerOptions(){PropertyNamingPolicy=JsonNamingPolicy.CamelCase};

var jsondata=JsonSerializer.Serialize(PaginationHeader,jsonoptions);
response.Headers.Append("Pagination",jsondata);


// and to make the clien be able to access this header we must add aslo the next header to allow it to be displayed

response.Headers.Append("Access-Control-Expose-Headers","Pagination");


}
}
