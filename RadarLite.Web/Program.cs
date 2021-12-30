WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Logging.Services.AddLogging(loggingBuilder =>
    loggingBuilder.AddSeq());

// Add services to the container.
//builder.Services.AddRazorPages();

var app = builder.Build();
builder.WebHost.UseWebRoot(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"));
app.Logger.LogInformation("RadarLite.Web Started.");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//app.MapRazorPages();

app.Run();
