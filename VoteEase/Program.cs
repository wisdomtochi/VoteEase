using VoteEase.IoC.Dependencies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

var configuration = builder.Configuration;

//builder.Services.AddMvc().AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
//builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.RegisterServices(configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var serviceProvider = scope.ServiceProvider;
//    var recurringJobManager = serviceProvider.GetRequiredService<IRecurringJobManager>();
//    recurringJobManager.AddOrUpdate("log-cleanup-job", () => serviceProvider.GetRequiredService<IErrorService>().CleanupLogs(), Cron.Monthly);
//}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseRouting();

app.UseSwagger();

app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();