using AgentTracker.API.Interfaces;
 using AgentTracker.Infrastructure.Models.data;
using AgentTracker.Infrastructure.Repository;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
 

namespace AgentTracker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<AaNeel_AgentPortalContext>(opts => 
            //opts.UseSqlServer(Configuration.GetConnectionString("ConnectionString")));

            //services.AddScoped<IAgent, AgentRepository>();
            //services.AddControllers();
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AgentTracker", Version = "v1" });
            //});


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AgentTracker", Version = "v1" });
            });

            services.AddDbContext<AaNeel_AgentPortalContext>(options =>                                 // To Register Data dbContext for other Tables
             options.UseSqlServer(
                 Configuration.GetConnectionString("ConnectionStrings")));

            services.AddScoped<IAgent, AgentRepository>();

            services.AddControllersWithViews();
            services.AddSwaggerGen();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
           .AddIdentityServerAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme, options =>
           {
               options.Authority = Configuration.GetSection("Keys").GetSection("AuthorityUrl").Value.Trim();
               options.SupportedTokens = SupportedTokens.Jwt;
               options.ApiName = Configuration.GetSection("Keys").GetSection("IdentityApiName").Value.Trim();
           });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory logger)
        { 
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AgentTracker v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            }); 
            logger.AddFile(Configuration.GetValue<string>("Logging:FilePath"));
        }
    }
}
