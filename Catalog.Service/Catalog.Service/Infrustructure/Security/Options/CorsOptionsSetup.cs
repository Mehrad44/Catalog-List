using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Options;

namespace Catalog.Service.Infrustructure.Security.Options
{
    public class CorsOptionsSetup : IConfigureOptions<CorsOptions>
    {
        private readonly IWebHostEnvironment _environment;

        public CorsOptionsSetup(IWebHostEnvironment environment)
        {
            _environment = environment; 
        }
        public void Configure(CorsOptions options)
        {
            options.AddDefaultPolicy(policy =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    if (_environment.IsDevelopment())
                    {
                        builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .WithExposedHeaders("X-TotalRecordCount");
                    }
                    else
                    {
                        builder.WithOrigins("http://localhost:4200")
                        .WithMethods("GET", "POST", "PUT", "DELETE")
                        .WithHeaders("Header Name1", "Header Name2")
                        .WithExposedHeaders("X-TotalRecordCount"); 

                    }
                    
                });
            });
        }
    }
}