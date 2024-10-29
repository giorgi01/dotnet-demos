using Microsoft.EntityFrameworkCore;
using TaskTracker.Application.UserTasks;
using TaskTracker.Domain.Interfaces;
using TaskTracker.Persistence;
using TaskTracker.Persistence.Repositories;

namespace TaskTracker.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
            typeof(CreateUserTaskCommand).Assembly,
            typeof(TaskDbContext).Assembly));

        builder.Services.AddDbContext<TaskDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped<IUserTaskRepository, UserTaskRepository>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
