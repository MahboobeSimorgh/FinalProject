using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
	/// <summary>
	/// 
	/// </summary>
	public class Startup : object
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="configuration"></param>
		public Startup(Microsoft.Extensions.Configuration.IConfiguration configuration)
		{
			Configuration = configuration;
		}

		/// <summary>
		/// 
		/// </summary>
		public Microsoft.Extensions.Configuration.IConfiguration Configuration { get; }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="services"></param>
		public void ConfigureServices
			(Microsoft.Extensions.DependencyInjection.IServiceCollection services)
		{
			services.AddControllers();

			// (4)
			services.AddSwaggerGen(current =>
			{
				// **************************************************
				// Set the comments path for the Swagger JSON and UI.
				// File Name: Application.xml
				var xmlFile =
					$"{ System.Reflection.Assembly.GetExecutingAssembly().GetName().Name }.xml";

				var xmlPathName =
					System.IO.Path.Combine(System.AppContext.BaseDirectory, xmlFile);

				current.IncludeXmlComments(filePath: xmlPathName);
				// **************************************************

				current.SwaggerDoc
					(name: "v1",
					info: new Microsoft.OpenApi.Models.OpenApiInfo
					{
						Version = "v1",
						Title = "Application",

						Description = "Developed by Mahbube Simorgh",

						TermsOfService =
							new System.Uri("https://DTApp.ir/")
					});
			});
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="app"></param>
		/// <param name="env"></param>
		public void Configure
			(Microsoft.AspNetCore.Builder.IApplicationBuilder app,
			Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();

				// (5)
				app.UseSwagger();

				// (6)
				app.UseSwaggerUI(current =>
				{
					current.SwaggerEndpoint("/swagger/v1/swagger.json", "Application v1");
				});
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
