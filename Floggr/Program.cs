using Microsoft.EntityFrameworkCore;
using Floggr.Code;
using Floggr.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<FloggrContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("FloggrContext")));
//builder.Services.AddDatabaseDeveloperPageExcepionFilter();

var app = builder.Build();

//to seed FoundationFoods
using (var scope = app.Services.CreateScope())
{

    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
    /*
   DinnerGeneratorContext context = scope.ServiceProvider.GetRequiredService<DinnerGeneratorContext>();
   SeedData.Initialize(context).Wait();
*/
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
