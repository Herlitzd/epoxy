using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Epoxy.Example.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Epoxy.Example.Web
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

      var epoxyConfig = new EpoxyConfig();

      epoxyConfig.Add(new Epoxy<Customer, int>()
          .WithKey(x => x.CustomerId)
          .AddLocator<ILocator<int, Customer>>()
          .WithRelationship<ShoppingCart, int>(x => x.CustomerId));

      epoxyConfig.Add(new Epoxy<ShoppingCart, Guid>()
          .WithKey(x => x.CartId)
          .AddLocator<ILocator<Guid, ShoppingCart>>()
          .WithRelationship<Customer, int>(x => x.CustomerId));

      epoxyConfig.Register<CustomerCartViewModel>(b =>
      {
        b.With<Customer>((c, viewModel) =>
        {
          viewModel.CustomerId = c.CustomerId;
          viewModel.FirstName = c.FirstName;
          viewModel.LastName = c.LastName;
        })
        .With<IEnumerable<ShoppingCart>>((carts, viewModel) =>
          {
            viewModel.NumberOfCartItems = carts.Select(x=>x.CartItems.Count()).Sum();
          });
      });

      services.AddTransient<ILocator<int, Customer>, CustomerService>();
      services.AddTransient<ILocator<Guid, ShoppingCart>, ShoppingCartService>();

      services.AddTransient<IResolver<CustomerCartViewModel>>(
                  new ResolverProvider(epoxyConfig).ResolverFactory<CustomerCartViewModel>);

      // services.AddSingleton(typeof(IResolver<>), new ResolverProvider(epoxyConfig).ResolverFactory);




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

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
