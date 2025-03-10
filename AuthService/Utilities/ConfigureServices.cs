using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Utilities
{
    public static class  ConfigureServices
    {
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters=new TokenValidationParameters
                {
                    ValidateIssuer =true,
                    ValidateAudience =true,
                    ValidateLifetime =true,
                    ValidateIssuerSigningKey =true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key"))),
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"]
    
                };
            });
            
     
       
        
    }


public static void AddCorsPolicy (this IServiceCollection services, ConfigurationManager configuration)
{
      var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        services.AddCors(options=> 
        options.AddPolicy(name: MyAllowSpecificOrigins,
        policy=>{
            policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
           
          
        }));
        }
}
    }
