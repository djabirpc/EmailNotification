using EmailNotification.Helper;
using Microsoft.AspNetCore.Identity.UI.Services;

using Microsoft.Extensions.DependencyInjection;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Extensions.Configuration;
using EmailNotification.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<MailJetSettings>(builder.Configuration.GetSection("MailJetSettings"));
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IContractService, ContractService>();
//builder.Services.AddHangfire((sp, config) =>
//{
//    var connectionString = sp.GetRequiredService<IConfiguration>().GetConnectionString("DbConnection");
//    config.UseSqlServerStorage(connectionString);
//});
//builder.Services.AddHangfireServer();

builder.Services.AddHangfire(configuration => configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSqlServerStorage(builder.Configuration.GetConnectionString("HangfireConnection")));

builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHangfireDashboard();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

RecurringJob.AddOrUpdate<IContractService>("DailyTasl", x => x.CheckAndSendContractNotifications(), Cron.Minutely());

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
