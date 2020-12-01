using AddressBookRestApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        /// <summary>
        /// UC23
        /// Tests the add multiple entries using post operation.
        /// </summary>
        [TestMethod]
        public void GivenMultipleContactEntries_UsingPostOperation_ShouldReturnAddedContacts()
        {
            //adding multiple employees to table
            List<Contact> contactList = new List<Contact>();
            contactList.Add(new Contact { id = 4, name = "ABD", address = "RSA", phoneNumber = "999-888-999-888", email = "Ab@devilers.com", contactType = "Keeper-Batsman" });
            contactList.Add(new Contact { id = 5, name = "Stokes", address = "England", phoneNumber = "989-888-999-888", email = "ben@stokes.com", contactType = "All-Rounder" });
            foreach (Contact contact in contactList)
            {
                RestRequest request = new RestRequest("/contacts", Method.POST);
                JObject jObject = new JObject();
                jObject.Add("id", contact.id);
                jObject.Add("name", contact.name);
                jObject.Add("address", contact.address);
                jObject.Add("phoneNumber", contact.phoneNumber);
                jObject.Add("email", contact.email);
                jObject.Add("contactType", contact.contactType);
                request.AddParameter("application/json", jObject, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                //Assert
                Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
                //derserializing object for assert and checking test case
                Contact dataResponse = JsonConvert.DeserializeObject<Contact>(response.Content);
                Assert.AreEqual(contact.name, dataResponse.name);
            }
        }
        /// <summary>
        /// UC24
        /// Tests the update data using put operation.
        /// </summary>
        [TestMethod]
        public void TestUpdateDataUsingPutOperation()
        {
            RestRequest request = new RestRequest("contacts/5", Method.PUT);
            JObject jobject = new JObject();
            jobject.Add("name", "Madhavan");
            jobject.Add("contactType", "Fast-Bowler");
            request.AddParameter("application/json", jobject, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Contact dataResponse = JsonConvert.DeserializeObject<Contact>(response.Content);
            Assert.AreEqual(dataResponse.name, "Madhavan");
            Assert.AreEqual(dataResponse.contactType, "Fast-Bowler");
        }
        /// <summary>
        /// UC25
        /// Tests the delete data using delete operation.
        /// </summary>
        [TestMethod]
        public void TestDeleteDataUsingDeleteOperation()
        {
            //Arrange
            RestRequest request = new RestRequest("contacts/5", Method.DELETE);
            //Act
            IRestResponse response = client.Execute(request);
            //Assert
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
        }
    }
}
