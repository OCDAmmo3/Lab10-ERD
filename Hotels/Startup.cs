using System;
using Hotels.Data;
using Hotels.Models;
using Hotels.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Hotels
{
    public class Startup
    {
        // 1. Property to hold our configuration
        public IConfiguration Configuration { get; }

        // 2. Add a constructor to receive our configuration (via magic)
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                // No infinite reference looping here.
                .AddNewtonsoftJson(OptionsBuilderConfigurationExtensions =>
                {
                    OptionsBuilderConfigurationExtensions.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });

            // 3. Register our DbContext with the app
            services.AddDbContext<HotelDbContext>(options =>
            {
                // Equivalent to DATABASE_URL
                string connectionString = Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 3;
                options.Password.RequireUppercase = true;
            })
                .AddEntityFrameworkStores<HotelDbContext>();

            services.AddTransient<IUserService, IdentityUserService>();
            services.AddScoped<JwtTokenService>();

            services.AddTransient<IRoomRepository, DatabaseRoomRepository>();
            services.AddTransient<IHotelRepository, DatabaseHotelRepository>();
            services.AddTransient<IAmenityRepository, DatabaseAmenityRepository>();
            services.AddTransient<IHotelRoomRepository, DatabaseHotelRoomRepository>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Hotel Explorer", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(options =>
            {
                options.RouteTemplate = "/api/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/api/v1/swagger.json", "Hotel Explorer!");
                options.RoutePrefix = "";
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });

                endpoints.MapGet("/boom", context =>
                {
                    throw new InvalidOperationException("boom");
                });
            });
        }
    }
}
