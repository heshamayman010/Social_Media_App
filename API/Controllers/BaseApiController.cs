using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using API.Entities;
using API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ServiceFilter(typeof(LogUserActivity))]
 [Route("api/[controller]")]  // this will be called localhost:port/api/User
 [ApiController]
public class BaseApiController:ControllerBase
{

   

}

