using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodBlogApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using FoodBlogApi.Models;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using FoodBlogApi.PhotoManagement;

namespace FoodBlogApi
{
    public class Startup
    {
        readonly string corsPolicy = "corsPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
           
            services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            // for dtos
            services.AddAutoMapper(typeof(Startup));            
            
            services.AddControllers()
                .AddNewtonsoftJson();

            // services added and injected
            services.AddScoped<IForumPostRepository, EFForumPostRepository>()
                    .AddScoped<IArticleRepository, EFArticleRepository>()
                    .AddScoped<IAuthService, AuthService>()
                    .AddScoped<IUserRepository, UserRepository>()
                    .AddScoped<IPhotoAccessor, PhotoAccessor>()
                    .AddScoped<IPhotoRepository, EFPhotoRepository>();

            // photo management, storage of api key etc
            services.Configure<CloudinarySettings>(Configuration.GetSection("Cloudinary"));

            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<IdentityDbContext>();

            // för chat och notifieringar
            // services.AddSignalR();

            var appSettingsSections = Configuration.GetSection("AppSettings");     

            services.Configure<AppSettings>(appSettingsSections);

            var appSettings = appSettingsSections.Get<AppSettings>();
            var key = Encoding.UTF8.GetBytes(appSettings.Key);

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt => {
                jwt.RequireHttpsMetadata = false;
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // enables cors for react client
            services.AddCors(opt =>
            {
                opt.AddPolicy(corsPolicy,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000")
                            .AllowAnyHeader()                            
                            .AllowAnyMethod();
                    });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(corsPolicy);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                // endpoints.MapHub<ChatHub>("/chat"); // signalR, chathub är dataklassen
            });
        }
    }
}
