using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventManagement.Concrete;
using EventManagement.Filters;
using EventManagement.Hubs;
using EventManagement.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EventManagement
{
    public class Startup
    {
        /* Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }*/
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddMvc(options =>
            {
                options.Filters.Add(new CustomExceptionFilterAttribute());
                options.ReturnHttpNotAcceptable = false;
                // options.OutputFormatters = xml
            })
            .AddJsonOptions(options =>
            {
                //For Maintaining Json Format 
                options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
            });

            // For FileUpload
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue; // In case of multipart
                x.ValueLengthLimit = int.MaxValue; //not recommended value
                x.MemoryBufferThreshold = Int32.MaxValue;
            });

            // For Setting Session Timeout
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.Name = ".EventCore";
            });

            //Getting Connection String From Database
            var connection = Configuration.GetConnectionString("DatabaseConnection");

            // UseRowNumberForPaging for Using Skip and Take in .Net Core
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connection, b => b.UseRowNumberForPaging()));
            services.AddTransient<IRegistration, RegistrationConcrete>();
            services.AddTransient<ICountry, CountryConcrete>();
            services.AddTransient<IState, StateConcrete>();
            services.AddTransient<ICity, CityConcrete>();
            services.AddTransient<IRoles, RolesConcrete>();
            services.AddTransient<ILogin, LoginConcrete>();
            services.AddTransient<IVenue, VenueConcrete>();
            services.AddTransient<IEquipment, EquipmentConcrete>();
            services.AddTransient<IFood, FoodConcrete>();
            services.AddTransient<ILight, LightConcrete>();
            services.AddTransient<IFlower, FlowerConcrete>();
            services.AddTransient<IDishtypes, DishtypesConcrete>();
            services.AddTransient<IBookingVenue, BookingVenueConcrete>();
            services.AddTransient<IEvent, EventConcrete>();
            services.AddTransient<IBookEquipment, BookEquipmentConcrete>();
            services.AddTransient<IBookFood, BookFoodConcrete>();
            services.AddTransient<IBookingLight, BookingLightConcrete>();
            services.AddTransient<IBookFlower, BookFlowerConcrete>();
            services.AddTransient<ITotalbilling, TotalbillingConcrete>();
            services.AddTransient<IContact, ContactConcrete>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            // Enabling Session
            app.UseSession();
            //app.UseCookiePolicy();
            // Routing
            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/chatHub");
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
