using AddressBookRestApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Net;

namespace AddressBookApiTest
{
    [TestClass]
    public class TestRestOperations
    {

        RestClient client;

        [TestInitialize]
        public void Setup()
        {
            client = new RestClient("http://localhost:4000");
        }

        /// <summary>
        /// UC1
        /// Tests the retrieving data function using get operation.
        /// </summary>
        [TestMethod]
        public void GivenContactOfAddressBook_UsedGetMethod_ShouldReturnTheTotalContacts()
        {
            RestRequest request = new RestRequest("/contacts", Method.GET);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            List<Contact> dataResponse = JsonConvert.DeserializeObject<List<Contact>>(response.Content);
            Assert.AreEqual(3, dataResponse.Count);
        }
    }
}
