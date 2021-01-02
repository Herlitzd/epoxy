using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Epoxy.Example.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Epoxy.Example.Web.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class BasicController : ControllerBase
  {
    private readonly ILogger<BasicController> _logger;
    public IResolver<CustomerCartViewModel> _CustomerCartViewModelProvider { get; }

    public BasicController(ILogger<BasicController> logger,
                                     IResolver<CustomerCartViewModel> customerCartViewModelProvider
    )
    {
      _logger = logger;
      _CustomerCartViewModelProvider = customerCartViewModelProvider;
    }

    [HttpGet("{id}")]
    public CustomerCartViewModel Get(int id)
    {
      return _CustomerCartViewModelProvider.OneFrom<CustomerCartViewModel, int>(id);
    }

  }
}
