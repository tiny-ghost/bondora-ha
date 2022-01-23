using Rental.API.Core; 

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .Addservice()
    .AddDatabase(builder.Configuration.GetSection("Database"))
    .AddControllers();
    ;

var app = builder.Build();

var scope = app.Services.CreateScope();

await scope.ServiceProvider.GetRequiredService<DBInitialization>().InitializeAsync();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}

app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
