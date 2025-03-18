using AutoMapper;
using Data.Boundaries;
using Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Services.Boundaries;
using Services.Interactors;
using Services.Mapping;
using Services.Models;
using Services.Models.Trello;
using shopping_backend.CustomMiddlewareExtensions;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http.Headers;

namespace shopping_backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            ConfigureCors(app);
            app.UseErrorLogging();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
			ConfigureOptions(services);
			ConfigureTrello(services);
			var connectionString = Configuration["Database:ConnectionString"];
            services.AddTransient<IDbConnection>((sp) =>
            {
                return new SqlConnection(connectionString);
            });
            services.AddScoped<IProductRepository, ProductRepository>();
			services.AddScoped<ICategoryRepository, CategoryRepository>();
			services.AddScoped<IAreaRepository, AreaRepository>();
			services.AddScoped<ISuperMarketRepository, SuperMarketRepository>();
			services.AddScoped<IShoppingListRepository, ShoppingListRepository>();
			services.AddScoped<IUomRepository, UomRepository>();
			services.AddScoped<IShoppingListPriceRepository, ShoppingListPriceRepository>();
			services.AddScoped<IRecipeRepository, RecipeRepository>();
			services.AddScoped<IMenuRepository, MenuRepository>();

			services.AddScoped<IProductService, ProductService>();
			services.AddScoped<IAreaService, AreaService>();
			services.AddScoped<ICategoryService, CategoryService>();
			services.AddScoped<ISuperMarketService, SuperMarketService>();
			services.AddScoped<IShoppingListService, ShoppingListService>();
			services.AddScoped<IUomService, UomService>();
			services.AddScoped<IExportProductsService, ExportProductsService>();
			services.AddScoped<IShoppingListPriceService, ShoppingListPriceService>();
			services.AddScoped<IRecipeService, RecipeService>();
			services.AddScoped<IMenuService, MenuService>();

			services.AddScoped<IListReconciliationService, ListReconciliationService>();
			services.AddScoped<ITrelloApiService, TrelloApiService>();
            services.AddScoped<ITrelloQueryService, TrelloQueryService>();
            services.AddScoped<ITrelloCommandService, TrelloCommandService>();
			services.AddScoped<IExcelFile, ExcelFile>();
			services.AddScoped<IExcelService, ExcelService>();
			ConfigureMapper(services);
        }

        private void ConfigureTrello(IServiceCollection services)
        {
            var apiKey = Configuration.GetValue<string>("ThirdPartyApi:Trello:ApiKey");
			var token = Configuration.GetValue<string>("ThirdPartyApi:Trello:Token");
			services.AddHttpClient("trello", options =>
			{
				options.BaseAddress = new Uri("https://api.trello.com/1/");
				options.DefaultRequestHeaders.Authorization =
				new AuthenticationHeaderValue(
					"OAuth",
					$"oauth_consumer_key=\"{apiKey}\", oauth_token=\"{token}\"");
			});
		}

		private void ConfigureMapper(IServiceCollection services)
        {
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        private void ConfigureOptions(IServiceCollection services)
        {
            services.AddOptions();
            var trelloSection = Configuration.GetSection("ThirdPartyApi:Trello");
            services.Configure<TrelloSection>(trelloSection);
			var uploadsSection = Configuration.GetSection("Uploads");
			services.Configure<Uploads>(uploadsSection);
		}

		private void ConfigureCors(IApplicationBuilder app)
        {
            var cors = Configuration.GetSection("Cors").Get<CorsSection>();
            var allowedOrigins = cors.AllowedOrigins.Split(';');

            app.UseCors(builder => 
            {
                builder.AllowAnyOrigin();
                //builder.WithOrigins(allowedOrigins);
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
            });
        }
    }
}
