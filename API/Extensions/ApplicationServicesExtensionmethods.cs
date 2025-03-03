using System;
using System.Buffers;
using API.Data;
using API.Entities;
using API.Helpers;
using API.interfaces;
using API.Interfaces;
using API.Services;
using API.TheSignalR;
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
services.AddScoped<IMessagesRepository,MessagesRepository>();
services.AddScoped<ILikeRepository,LikeRepository>();
services.AddScoped<IUserRepository,UserRepository>();
services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies() ); // here we give it the place of the 
services.AddScoped<LogUserActivity>();
services.AddSignalR();
services.AddScoped<IUnitOfWork,UnitOfWork>();
services.AddSingleton<PresenceTracker>();
services.AddScoped<IPhotoService,PhotoService>();
// here we use the config with the place where it will get the configuration data 
services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
return services;

}


}
