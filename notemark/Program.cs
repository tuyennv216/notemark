using notemark.Interface;
using notemark.Service;

var builder = WebApplication.CreateBuilder(args);

// Add custom configuraion for IConfiguration
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
	config.AddJsonFile("customsettings.json", optional: true, reloadOnChange: true);
});

// Add services to the container.
builder.Services.AddSingleton<INotepadService, NotepadService>();
builder.Services.AddSingleton<IDataService, DataService>();
builder.Services.AddMemoryCache();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Build app
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
