using System;
using System.Threading.Tasks;
using NUnit.Framework;
using RestSharp;
using FluentAssertions;
using NUnitFramework.Utilities;


namespace NUnitFramework.APITests
{
    public class APITest1 : BaseSetUp
    {
        bool result = false;

        [Test]
        public async Task Test1GetOp()
        {
            var restClientOptions = new RestClientOptions
            {
                 BaseUrl = new Uri("https://petstore.swagger.io/"),
                 RemoteCertificateValidationCallback = (sender,certificate,chain,errors) => true
            };

            var client = new RestClient(restClientOptions);
            var request = new RestRequest("v2/pet/findByStatus?status=available");
            var response = await client.GetAsync(request);
            int statusCode = (int)response.StatusCode;
            
            if (statusCode == 200)
            {
                result = true;
            }
            Assert.IsTrue(result);
            response.Should().NotBeNull();
        }
    }
}
