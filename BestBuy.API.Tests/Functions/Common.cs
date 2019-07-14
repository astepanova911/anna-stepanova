using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace BestBuy.API.Tests.Helpers
{
    public class Healthcheck
    {
        [JsonProperty(PropertyName = "uptime")]
        public int uptime { get; set; }
        [JsonProperty(PropertyName = "documents")]
        public Totals[] data;
    }

    public class Totals
    {
        [JsonProperty(PropertyName = "products")]
        public int products { get; set; }
        [JsonProperty(PropertyName = "stores")]
        public int stores { get; set; }
        [JsonProperty(PropertyName = "categories")]
        public int categories { get; set; }
    }
    public class StoreDTO
    {
        [JsonProperty(PropertyName = "name")]
        public String name { get; set; }
        [JsonProperty(PropertyName = "type")]
        public String type { get; set; }
        [JsonProperty(PropertyName = "address")]
        public String address { get; set; }
        [JsonProperty(PropertyName = "state")]
        public String state { get; set; }
        [JsonProperty(PropertyName = "address2")]
        public String address2 { get; set; }
        [JsonProperty(PropertyName = "city")]
        public String city { get; set; }
        [JsonProperty(PropertyName = "zip")]
        public String zip { get; set; }
        [JsonProperty(PropertyName = "hours")]
        public String hours { get; set; }
    }
    public class StoresResponseDTO
    {
        [JsonProperty(PropertyName = "total")]
        public int total { get; set; }
        [JsonProperty(PropertyName = "limit")]
        public int limit { get; set; }
        [JsonProperty(PropertyName = "skip")]
        public int skip { get; set; }
        [JsonProperty(PropertyName = "data")]
        public StoreDTO[] data;
    }

    public class StoreResponseDTO
    {
        [JsonProperty(PropertyName = "id")]
        public int id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public String name { get; set; }
        [JsonProperty(PropertyName = "type")]
        public String storeType { get; set; }
        [JsonProperty(PropertyName = "address")]
        public String address { get; set; }
        [JsonProperty(PropertyName = "state")]
        public String state { get; set; }
        [JsonProperty(PropertyName = "address2")]
        public String address2 { get; set; }
        [JsonProperty(PropertyName = "city")]
        public String city { get; set; }
        [JsonProperty(PropertyName = "zip")]
        public String zip { get; set; }
        [JsonProperty(PropertyName = "hours")]
        public String hours { get; set; }
        [JsonProperty(PropertyName = "createdAt")]
        public string createdAt { get; set; }
        [JsonProperty(PropertyName = "updatedAt")]
        public string updatedAt { get; set; }
    }

    public class CategoryDTO
    {
        [JsonProperty(PropertyName = "id")]
        public String id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public String name { get; set; }
    }

    public class CategoryResponseDTO
    {
        [JsonProperty(PropertyName = "id")]
        public String id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public String name { get; set; }
        [JsonProperty(PropertyName = "createdAt")]
        public string createdAt { get; set; }
        [JsonProperty(PropertyName = "updatedAt")]
        public string updatedAt { get; set; }
    }

    public class CategoriesResponseDTO
    {
        [JsonProperty(PropertyName = "total")]
        public int total { get; set; }
        [JsonProperty(PropertyName = "limit")]
        public int limit { get; set; }
        [JsonProperty(PropertyName = "skip")]
        public int skip { get; set; }
        [JsonProperty(PropertyName = "data")]
        public CategoryResponseDTO[] data;
    }
    public class ProductDTO
    {
        public string name { get; set; }
        [JsonProperty(PropertyName = "type")]
        public string type { get; set; }
        [JsonProperty(PropertyName = "upc")]
        public string upc { get; set; }
        [JsonProperty(PropertyName = "price")]
        public double price { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string description { get; set; }
        [JsonProperty(PropertyName = "model")]
        public string model { get; set; }
    }

    public class ProductResponseDTO
    {
        [JsonProperty(PropertyName = "id")]
        public int id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }
        [JsonProperty(PropertyName = "type")]
        public string type { get; set; }
        [JsonProperty(PropertyName = "upc")]
        public string upc { get; set; }
        [JsonProperty(PropertyName = "price")]
        public double price { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string description { get; set; }
        [JsonProperty(PropertyName = "model")]
        public string model { get; set; }
        [JsonProperty(PropertyName = "updatedAt")]
        public string updatedAt { get; set; }
        [JsonProperty(PropertyName = "createdAt")]
        public string createdAt { get; set; }

        [JsonProperty(PropertyName = "categories")]
        public CategoryDTO[] Categories { get; set; }
        
        [JsonProperty(PropertyName = "shipping")]
        public double shipping { get; set; }
    }

    public class ProductsResponseDTO
    {
        [JsonProperty(PropertyName = "total")]
        public int total { get; set; }
        [JsonProperty(PropertyName = "limit")]
        public int limit { get; set; }
        [JsonProperty(PropertyName = "skip")]
        public int skip { get; set; }
        [JsonProperty(PropertyName = "data")]
        public ProductResponseDTO[] data;
    }

   

    class Common
    {
        public static String CoreUrl = System.Configuration.ConfigurationManager.AppSettings["Core:Host"];
        public static T ExecuteRequest<T>(string url, string endpoint, Method method)
        {
            var client = new RestClient(url);
            var request = new RestRequest(endpoint, method);
            var response = client.Execute(request);
            return JsonConvert.DeserializeObject<T>(response.Content);
        }
        public static U ExecuteRequestWithBody<T, U>(string url, string endpoint, Method method, T body)
        {
            var client = new RestClient(url);
            var request = new RestRequest(endpoint, method, DataFormat.Json);
            request.AddJsonBody(body);

            var response = client.Execute(request);
            return JsonConvert.DeserializeObject<U>(response.Content);
        }

        public static void GetAPI(string url)
        {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"{Common.CoreUrl}{url}");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                var httpResponse = (HttpWebResponse)request.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    Assert.IsTrue(result != null && result.Length > 0);
                }
                Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
                Assert.IsTrue(response.ContentType.Contains("application/json"));
        }
    }    
}