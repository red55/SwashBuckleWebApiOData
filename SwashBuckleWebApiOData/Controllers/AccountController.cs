using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SwashBuckleWebApiOData.Controllers
{
    [ApiController]
    [ApiVersion (@"1.0")]
    [Route ("api/v{version:apiVersion}/[controller]")]
    [Produces (@"application/json")]
    public class AccountController : ControllerBase
    {
        

        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Models.Account> Get()
        {
            return new List<Models.Account> () {
                new Models.Account
                {
                    Id = Guid.NewGuid(),
                    Email = new System.Net.Mail.MailAddress (@"zz@zz.zz"),
                    FirstName = @"z",
                    Surname = @"@zz",
                },
                new Models.Account
                {
                    Id = Guid.NewGuid(),
                    Email = new System.Net.Mail.MailAddress (@"xx@xx.x"),
                    FirstName = @"xx",
                    Surname = @"xx",
                },
                new Models.Account
                {
                    Id = Guid.NewGuid(),
                    Email = new System.Net.Mail.MailAddress (@"yy@yy.y"),
                    FirstName = @"y",
                    Surname = @"yy",
                }
            }.AsEnumerable ();
        }
    }
}
