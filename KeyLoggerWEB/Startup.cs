using KeyLoggerWEB.Context;
using KeyLoggerWEB.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KeyLoggerWEB
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
            services.AddScoped<IKeyLoggerRepository, KeyLoggerRepository>();
            var ConnectionString = "server=localhost;userid=root;password=root;database=KeyLogger";
            services.AddDbContext<KeyLoggerContext>(x => x.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString)));

            services.AddMvc(m =>
            {
                m.EnableEndpointRouting = false;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(o =>
            {
                o.AccessDeniedPath = new PathString("/Login/");
                o.LoginPath = new PathString("/Login/");
                o.Cookie.Path = "/";
                o.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                o.Cookie.HttpOnly = true;
                o.LogoutPath = new PathString("/Logout/");
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

            app.UseStaticFiles();

            app.UseRouting();
            app.UseStatusCodePagesWithReExecute("/Login");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCookiePolicy();

            app.UseMvcWithDefaultRoute();
        }
    }
}
