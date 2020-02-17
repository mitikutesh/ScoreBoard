using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ScoreBoard.API.HubConfig;
using ScoreBoard.API.Persistence;
using ScoreBoard.API.Services;
using ScoreBoard.API.TimerService;

namespace ScoreBoard.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContext<ScoreContext>(opt => opt.UseSqlite("Data Source=ScoreBoard.db"));
            services.AddTransient<IScoreBoardService, ScoreBoardService>();
            services.AddCors(options => options.AddPolicy("CorsPolicy",
          builder =>
          {
              builder.AllowAnyMethod()
                       .AllowAnyHeader()
                     .WithOrigins("http://localhost:4200")
                     .AllowCredentials();
          }));
            services.AddHostedService<ScoreUpdater>();
            services.AddScoped<IScopedScoreUpdater, ScopedScoreUpdater>();
            services.AddSignalR();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseCors("CorsPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ScoreBoardHub>("/signalHub");
            });
        }

    }
}
