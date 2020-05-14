using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SwashBuckleWebApiOData.Controllers.OData
{
    [ApiVersion (@"1.0")]
    [Produces (@"application/json")]    
    [ODataRoutePrefix (@"Accounts")]
    public class AccountController : ODataController
    {

        [ODataRoute]
        [Produces ("application/json")]
        [ProducesResponseType (typeof (ODataValue<IQueryable<Models.Account>>), StatusCodes.Status200OK)]        
        [EnableQuery (MaxTop = 100, AllowedQueryOptions = AllowedQueryOptions.All)]
        public Task<IQueryable<Models.Account>> Get()
        {
            return Task.FromResult (new List<Models.Account> () {
                new Models.Account
                {
                    Id = Guid.NewGuid(),
                    Email = new System.Net.Mail.MailAddress (@"qq@qq.qq"),
                    FirstName = @"q",
                    Surname = @"qq",
                    AccountName = @"QQQ\Q"
                },
                new Models.Account
                {
                    Id = Guid.NewGuid(),
                    Email = new System.Net.Mail.MailAddress (@"bb@bb.b"),
                    FirstName = @"b",
                    Surname = @"bb",
                    AccountName = @"BBB\b"
                },
                new Models.Account
                {
                    Id = Guid.NewGuid(),
                    Email = new System.Net.Mail.MailAddress (@"cc@cc.c"),
                    FirstName = @"c",
                    Surname = @"cc",
                    AccountName = @"CCC\c"
                }
            }.AsQueryable ());
        }
    }
}
