using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Playwright.Services.Authorization.Common
{
    public class ODataResponseModifierMiddleware
    {
        private readonly RequestDelegate _next;
        private Serilog.ILogger _logger;

        public ODataResponseModifierMiddleware(RequestDelegate next, Serilog.ILogger logger)
        {
            _next = next;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _logger.Information($"in ODataResponseModifierMiddleware , request action {context.Request.RouteValues["action"]}");
            if("GetAllAccessTokenV2".Equals(context.Request.RouteValues["action"])){
                var originalBodyStream = context.Response.Body;
            
                var responseBodyStream = new MemoryStream();
                context.Response.Body = responseBodyStream;
            
                await _next(context);
                responseBodyStream.Seek(0, SeekOrigin.Begin);
                var responseJson = await new StreamReader(responseBodyStream).ReadToEndAsync();

                var modifiedResponse = ModifyResponse(responseJson);

                var responseBytes = Encoding.UTF8.GetBytes(modifiedResponse);

                // Update the Content-Length header to match the modified response size
                context.Response.ContentLength = responseBytes.Length;

                // Write the modified response body back to the original response stream
                context.Response.Body = originalBodyStream;
                await context.Response.Body.WriteAsync(responseBytes, 0, responseBytes.Length);
            }else{
                await _next(context);
            }
        }

        private string ModifyResponse(string responseJson)
        {
            Console.WriteLine($"response {responseJson}");
            var jObject = JObject.Parse(responseJson);
            jObject.Remove("@odata.context");
            if (jObject["@odata.nextLink"] != null)
            {
                jObject["nextLink"] = jObject["@odata.nextLink"];
                jObject.Remove("@odata.nextLink");
            }else{
                jObject["nextLink"] = null;
            }
            Console.WriteLine($"modified response {jObject.ToString()}");
            return jObject.ToString();
        }
    }
}