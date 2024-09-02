using System;
using API.Entities;
using API.interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public  static class ApplicationServicesExtensionmethods
{


public static IServiceCollection addApplicationService(this IServiceCollection services,IConfiguration config){
// here we will add the part of the controllers and the cors and dbcontext and also the service regesteriations
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddDbContext<AppDbContext>(options=>{
options.UseSqlite(config.GetConnectionString("Defaultconnectionstring"));
        
});
services.AddCors();
services.AddScoped<ITokenService,TokenService>();



return services;

}


}
