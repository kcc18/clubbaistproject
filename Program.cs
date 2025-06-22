using BAIS3230Project.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BAIS3230Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();


            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddRazorPages();
            builder.Services.AddAuthorization(); // Enables policy-based authorization

            builder.Services.AddAuthorization(options => {
                // admin,clerk,etc
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin")); // Ensure users have an "Admin" role
                options.AddPolicy("Clerk", policy => policy.RequireRole("Clerk"));

                // RENAME these to something shorter maybe? Staff and Committee maybe?
                options.AddPolicy("Staff", policy => policy.RequireRole("Staff"));
                options.AddPolicy("MembershipCommittee", policy => policy.RequireRole("MembershipCommittee"));

                // membership tiers
                options.AddPolicy("Gold", policy => policy.RequireRole("Gold"));
                options.AddPolicy("Silver", policy => policy.RequireRole("Silver"));
                options.AddPolicy("Bronze", policy => policy.RequireRole("Bronze"));
                options.AddPolicy("Copper", policy => policy.RequireRole("Copper"));


                // BULK AUTHORIZATION ON FOLDERS
                options.AddPolicy("TeeTimeAccess", policy =>
                    policy.RequireRole("Clerk", "Gold", "Silver", "Bronze", "Admin"));

                options.AddPolicy("StandingTeeTimeAccess", policy =>
                    policy.RequireRole("Clerk", "Gold","ProShop", "Admin"));

                options.AddPolicy("ApplicationApproval", policy =>
                    policy.RequireRole("MembershipCommittee", "Admin"));

                options.AddPolicy("TeeSheetAccess", policy =>
                    policy.RequireRole("Clerk", "Admin"));

                options.AddPolicy("EventAccess", policy =>
                    policy.RequireRole("Clerk", "Admin"));
                // You can still keep your individual role policies too if needed elsewhere

            });


            // authorization on entire folders (TESTING)
            builder.Services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizeFolder("/TeeTimes", "TeeTimeAccess");

                options.Conventions.AuthorizeFolder("/StandingTeeTimes", "StandingTeeTimeAccess");

                options.Conventions.AuthorizeFolder("/Events", "EventAccess");

                options.Conventions.AuthorizePage("/Applications/ApprovalIndex", "ApplicationApproval");
                options.Conventions.AuthorizePage("/Applications/Index", "ApplicationApproval");

                options.Conventions.AuthorizePage("/TeeSheet", "TeeSheetAccess");


                // optional but kind of weird. keeping here for reference
                //options.Conventions.AllowAnonymousToPage("/Applications/Create");

            });


            // Microsoft Identity code from documentation

            builder.Services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            builder.Services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
