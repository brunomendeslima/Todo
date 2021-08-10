using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Todo.Domain.Handlers;
using Todo.Domain.Infra.Contexts;
using Todo.Domain.Infra.Repositories;
using Todo.Domain.Repositories;



namespace Todo.Domain.Api
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

            services.AddControllers();

            //services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Database"));
            services.AddDbContext<DataContext>(opt => 
                opt.UseSqlServer(Configuration
                        .GetConnectionString("sqlserver")
                    )
                );



            services.AddHealthChecks()
                .AddMongoDb(
                    mongodbConnectionString: Configuration.GetConnectionString("mongodb"),
                    mongoDatabaseName: Configuration.GetConnectionString("database"),
                    name: "MongoDb"
                );

            services.AddTransient<MongoDataContext>();


            services.AddTransient<ITodoRepository, TodoRepository>();
            services.AddTransient<ITodoMongoRespository, TodoMongoRepository>();
            services.AddTransient<TodoHandler, TodoHandler>();

            // Your web app's Firebase configuration
            // var firebaseConfig = {
            //     apiKey: "AIzaSyCmSPYG2g6te5rIORKeJkiDImsUbBi87do",
            //     authDomain: "todo-web-e7fe1.firebaseapp.com",
            //     projectId: "todo-web-e7fe1",
            //     storageBucket: "todo-web-e7fe1.appspot.com",
            //     messagingSenderId: "287856238964",
            //     appId: "1:287856238964:web:62c57dda4abf58aa820c21"
            // };    
            services
            
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => 
            {
                options.Authority = "https://securetoken.google.com/todo-web-e7fe1";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "https://securetoken.google.com/todo-web-e7fe1",
                    ValidateAudience = true,
                    ValidAudience = "todo-web-e7fe1",
                    ValidateLifetime = true
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Todo.Domain.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo.Domain.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
