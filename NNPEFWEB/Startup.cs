using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using NNPEFWEB.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NNPEFWEB.Models;
using NNPEFWEB.Service;
using NNPEFWEB.Repository;
using Wkhtmltopdf.NetCore;
using Microsoft.Extensions.Logging;
using System.IO;
using NNPEFWEB.Services;
using System.Net.Mail;

namespace NNPEFWEB
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
            services.AddDataProtection();
            //.PersistKeysToDbContext<DbContext>()

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(40);
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.User.RequireUniqueEmail = false;
                options.SignIn.RequireConfirmedEmail = false;
                options.User.AllowedUserNameCharacters = "";
                
                
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddWkhtmltopdf("wkhtmltopdf");
            services.AddAutoMapper(typeof(Startup));
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddScoped<IGeneratePayslip, GeneratePayslip>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IMenuGroupRepository, MenuGroupRepository>();
            services.AddScoped<IMenuGroupService, MenuGroupService>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IAuthService, AuthenticationService>();
            services.AddScoped<ISectionDashboard, SectionDashboard>();
            services.AddScoped<IUnitOfWorks, UnitOfWorks>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IPersonInfoService, PersonInfoService>();
            services.AddScoped<IDashboard, Dashboard>();
            services.AddScoped<ICommandDashboard, CommandDashboard>();
            services.AddScoped<ISystemsInfoService, SystemsInfoService>();
            services.AddScoped<IShipService, ShipService>();
            services.AddScoped<IContactUsService, ContactUsService>();
            services.AddScoped<IControlService, ControlService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IDapper, Dapperr>();
            services.AddScoped<IPensionService, PensionService>();
            services.AddScoped<IDeathService, DeathService>();
            services.AddSession();
            services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
            services.AddScoped<IEmailSenderService, EmailSenderService>();
            services.AddScoped<MailMessage>();
            services.AddMvc();
            //SeedCustomData.Initialize(Configuration);

            services.AddAuthorizationCore(options =>
            {
                options.AddPolicy("SuperAdmin", policy => policy.RequireRole("SuperAdmin"));
                options.AddPolicy("Commander", policy => policy.RequireRole("Commander"));
                options.AddPolicy("Operator", policy => policy.RequireRole("Operator"));
                options.AddPolicy("Secertariat", policy => policy.RequireRole("Secertariat"));
                options.AddPolicy("Section A", policy => policy.RequireRole("Section A"));
                options.AddPolicy("Section B", policy => policy.RequireRole("Section B"));
                options.AddPolicy("Section C", policy => policy.RequireRole("Section C"));
                options.AddPolicy("Section D", policy => policy.RequireRole("Section D"));
                options.AddPolicy("Section E", policy => policy.RequireRole("Section E"));
                options.AddPolicy("Officers Section", policy => policy.RequireRole("Officers Section"));
            });

            //services.AddTransient<SeedCustomData>();
            //services.AddMailKit(optionBuilder =>
            //{
            //    optionBuilder.UseMailKit(new MailKitOptions()
            //    {
            //        //get options from sercets.json
            //        Server = Configuration["Server"],
            //        Port = Convert.ToInt32(Configuration["Port"]),
            //        SenderName = Configuration["SenderName"],
            //        SenderEmail = Configuration["SenderEmail"],

            //        // can be optional with no authentication 
            //        Account = Configuration["Account"],
            //        Password = Configuration["Password"],
            //        // enable ssl or tls
            //        Security = true
            //    });
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILoggerFactory loggerFactory, UserManager<User> userManager, RoleManager<Role> roleManager,
           IConfiguration config, IUnitOfWorks unitOfWork)
        {
            var path = Directory.GetCurrentDirectory();
            loggerFactory.AddFile($"{path}\\Logs\\Log.txt");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
              //  app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
               // app.UseHsts();
            }
            var IsCentralServer = config.GetValue<bool>("ServerSettings:CentralServer");
            if (!IsCentralServer)
            {
                Seeder.SeedData(userManager, roleManager, config, unitOfWork);
            }


            //SeedCustomData.Initialize(app.ApplicationServices);
            //SeedCustomData.SeedUserRole(app.ApplicationServices);

            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=HomePage}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
