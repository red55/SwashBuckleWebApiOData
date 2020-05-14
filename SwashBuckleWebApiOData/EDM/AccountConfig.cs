using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;

namespace SwashBuckleWebApiOData.EDM
{
    public class AccountConfig : IModelConfiguration
    {
        public void Apply(ODataModelBuilder builder, ApiVersion apiVersion)
        {
            builder.EntitySet<Models.Account> ($"{nameof (Models.Account)}s");
            builder.EntityType<System.Net.Mail.MailAddress> ();
        }
    }
}
