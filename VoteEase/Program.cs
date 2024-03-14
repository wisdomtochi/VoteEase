using VoteEase.Application.Error;
using VoteEase.Application.Votings;
using VoteEase.Infrastructure.Error;
using VoteEase.Infrastructure.Votings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<INominationService, NominationService>();
builder.Services.AddScoped<IVoteService, VoteService>();
builder.Services.AddScoped<IAccreditedMemberService, AccreditedMemberService>();
builder.Services.AddScoped<IErrorService, ErrorService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();