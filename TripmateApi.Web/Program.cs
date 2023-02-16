using TripmateApi.Application;
using TripmateApi.Application.Common.Options;
using TripmateApi.Application.Services.Authentification;
using TripmateApi.Application.Services.Authentification.Interfaces;
using TripmateApi.Common.Authentification;
using TripmateApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.IncludeFields = true;


                });
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyMethod();
                          policy.AllowAnyHeader();
                          policy.AllowAnyOrigin();
                      });
});
builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection("AuthSettings"));
builder.Services.AddInfrastrucure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<JwtMiddleware>();
app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();

app.MapControllers();

app.Run();
