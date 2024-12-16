using System;
using Microsoft.AspNetCore.OData.Formatter.Deserialization;
using Microsoft.AspNetCore.OData.Formatter.Serialization;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Microsoft.Playwright.Services.Authorization.Common
{
    // A custom serializer provider to inject the AnnotatingEntitySerializer.
    public class CustomODataSerializerProvider : ODataSerializerProvider
    {
        private CustomODataSerializer _customODataSerializer;

        public CustomODataSerializerProvider(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _customODataSerializer = new CustomODataSerializer();
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

        public override IODataEdmTypeSerializer GetEdmTypeSerializer(IEdmTypeReference edmType)
        {
            Console.WriteLine($"in GetEdmTypeSerializer edmType {edmType}");
            return base.GetEdmTypeSerializer(edmType);
        }

        public override IODataSerializer GetODataPayloadSerializer(Type type, HttpRequest request)
        {
            Console.WriteLine($"in GetODataPayloadSerializer type {type}");
            return base.GetODataPayloadSerializer(type, request);
        }
    }


    // A custom entity serializer that adds the score annotation to document entries.
    public class CustomODataSerializer : ODataSerializer
    {
        private readonly JsonSerializerSettings _jsonSerializerSettings;
        public CustomODataSerializer() : base(ODataPayloadKind.Resource)
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

        public override Task<object> WriteObjectAsync(object graph, Type type, ODataMessageWriter messageWriter, ODataSerializerContext writeContext)
        {
            Console.WriteLine($"in WriteObjectAsync type {type}");
            throw new NotImplementedException();
        }
    }
}