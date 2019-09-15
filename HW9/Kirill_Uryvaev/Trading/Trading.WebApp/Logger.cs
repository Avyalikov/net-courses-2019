using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.WebApp
{
    class Logger : OwinMiddleware
    {
        public Logger(OwinMiddleware next)
            : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            //handle request logging
            Console.WriteLine($"{DateTime.Now} request: {context.Request.Method} {context.Request.Uri.ToString()}");

            await Next.Invoke(context);

            //handle response logging                       
            Console.WriteLine($"{DateTime.Now} response: {context.Response.StatusCode.ToString()} {context.Response.ReasonPhrase}");
        }
    }
}
