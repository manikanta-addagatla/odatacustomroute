using Microsoft.AspNetCore.OData.Formatter.Deserialization;
using Microsoft.AspNetCore.OData.Formatter.Wrapper;
using Microsoft.OData.Edm;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Microsoft.Playwright.Services.Authorization.Common
{
    // A custom serializer provider to inject the AnnotatingEntitySerializer.
    public class CustomODataDeserializerProvider : ODataDeserializerProvider
    {
        private CustomODataDeserializer _customODataDeserializer;

        public CustomODataDeserializerProvider(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _customODataDeserializer = new CustomODataDeserializer(this);
        }

        public override IODataEdmTypeDeserializer GetEdmTypeDeserializer(IEdmTypeReference edmType, bool isDelta = false)
        {
            Console.WriteLine($"in GetEdmTypeDeserializer type {edmType.IsEntity()}");
            if (edmType.IsEntity() || edmType.IsComplex())
            {
                return _customODataDeserializer;
            }

            return base.GetEdmTypeDeserializer(edmType, isDelta);
        }

        /*public override ODataSerializer GetODataPayloadSerializer(Type type, HttpRequestMessage request){
            Console.WriteLine($"in GetODataPayloadSerializer type {type}");
            return base.GetODataPayloadSerializer(type, request);
        }*/

        /*public override IODataDeserializer GetODataDeserializer(Type type, HttpRequest request)
        {
            Console.WriteLine($"in GetODataDeserializer type {type}");
            return base.GetODataDeserializer(type, request);
        }

        public override IODataEdmTypeDeserializer GetEdmTypeDeserializer(IEdmTypeReference edmType, bool isDelta = false)
        {
            Console.WriteLine($"in GetEdmTypeDeserializer type {edmType}");
            return base.GetEdmTypeDeserializer(edmType, isDelta);
        }*/
    }


    // A custom entity serializer that adds the score annotation to document entries.
    public class CustomODataDeserializer : ODataResourceDeserializer
    {
        private readonly JsonSerializerSettings _jsonSerializerSettings;
        public CustomODataDeserializer(IODataDeserializerProvider serializerProvider) : base(serializerProvider)
        {
            _jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(), // Example: camelCase
                Formatting = Formatting.None
            };
        }
        public override object ReadResource(ODataResourceWrapper resourceWrapper, IEdmStructuredTypeReference structuredType, ODataDeserializerContext readContext)
        {
            Console.WriteLine($"in ReadResource type {resourceWrapper}");
            object resource = base.ReadResource(resourceWrapper, structuredType, readContext);

            return resource;
        }

        /*public override Task<object> ReadAsync(ODataMessageReader messageReader, Type type, ODataDeserializerContext readContext)
        {
            Console.WriteLine($"in ReadAsync type {type}");
            throw new NotImplementedException();
        }*/
    }
}