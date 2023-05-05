using Clevyr.Blogs.Api.Data;var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var myOrigins = "myOrigins";
builder.Services.AddCors(options =>
    options.AddPolicy(myOrigins,
        policy => policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader()
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await DataContext.Migrate();
await DataContext.Seed();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors(myOrigins);
app.UseAuthorization();
app.MapControllers();
app.Run();
