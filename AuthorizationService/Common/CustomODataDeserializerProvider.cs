using System;
using Microsoft.AspNetCore.OData.Formatter.Deserialization;
using Microsoft.OData;
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
            _customODataDeserializer = new CustomODataDeserializer();
        }

        /*public override ODataEdmTypeSerializer GetEdmTypeSerializer(IEdmTypeReference edmType)
        {
            Console.WriteLine($"in GetEdmTypeDeserializer type {edmType.IsEntity()}");
            if (edmType.IsEntity())
            {
                return _customODataSerializer;
            }

            return base.GetEdmTypeSerializer(edmType);
        }

        public override ODataSerializer GetODataPayloadSerializer(Type type, HttpRequestMessage request){
            Console.WriteLine($"in GetODataPayloadSerializer type {type}");
            return base.GetODataPayloadSerializer(type, request);
        }*/

        public override IODataDeserializer GetODataDeserializer(Type type, HttpRequest request)
        {
            Console.WriteLine($"in GetODataDeserializer type {type}");
            return base.GetODataDeserializer(type, request);
        }

        public override IODataEdmTypeDeserializer GetEdmTypeDeserializer(IEdmTypeReference edmType, bool isDelta = false)
        {
            Console.WriteLine($"in GetEdmTypeDeserializer type {edmType}");
            return base.GetEdmTypeDeserializer(edmType, isDelta);
        }
    }


    // A custom entity serializer that adds the score annotation to document entries.
    public class CustomODataDeserializer : ODataDeserializer
    {
        private readonly JsonSerializerSettings _jsonSerializerSettings;
        public CustomODataDeserializer() : base(ODataPayloadKind.Resource)
        {
            _jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(), // Example: camelCase
                Formatting = Formatting.None
            };
        }
        /*public override ODataResource CreateResource(SelectExpandNode selectExpandNode, ResourceContext resourceContext)
        {
            ODataResource resource = base.CreateResource(selectExpandNode, resourceContext);
            var json = JsonConvert.SerializeObject(resource, _jsonSerializerSettings);
            Console.WriteLine("im here");
            var newResource = JsonConvert.DeserializeObject<ODataResource>(json, _jsonSerializerSettings);

            return newResource;
        }*/

        public override Task<object> ReadAsync(ODataMessageReader messageReader, Type type, ODataDeserializerContext readContext)
        {
            Console.WriteLine($"in ReadAsync type {type}");
            throw new NotImplementedException();
        }
    }
}