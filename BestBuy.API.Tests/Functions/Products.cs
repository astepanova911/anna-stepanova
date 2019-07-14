using NUnit.Framework;
using RestSharp;
using System;
using System.IO;
using System.Net;

namespace BestBuy.API.Tests.Helpers
{
    public class ProductsFunctions
    {
    public void PostProduct(string endpoint, string name, string type, string upc, double price, string description, string model)
        {
            ProductDTO p = new ProductDTO();
            p.name = name;
            p.price = price;
            p.type = type;
            p.upc = upc;
            p.description =description;
            p.model = model;
            var resp = Common.ExecuteRequestWithBody<ProductDTO, ProductResponseDTO>(Common.CoreUrl, endpoint, Method.POST, p);
            Assert.AreEqual(resp.name, p.name);
            Assert.AreEqual(resp.price, p.price);
            Assert.IsTrue(resp.id > 1000);
            Assert.IsNotEmpty(resp.updatedAt);
            Assert.IsNotEmpty(resp.createdAt);
            Assert.AreEqual(resp.type, p.type);
            Assert.AreEqual(resp.upc, p.upc);
            Assert.AreEqual(resp.description,p.description);
            Assert.AreEqual(resp.model, p.model);
        }
        public void PostProductFail(string endpoint, string name, string type, string upc, double price, string description)
        {
            ProductDTO p = new ProductDTO();
            p.name = name;
            p.price = price;
            p.type = type;
            p.upc = upc;
            p.description = description;

            var resp = Common.ExecuteRequestWithBody<ProductDTO, ProductResponseDTO>(Common.CoreUrl, endpoint, Method.POST, p);
            Assert.AreEqual(resp.name, "BadRequest");
        }

        public void PostProductValidationsFail(string endpoint, string name, string type, string upc, double price, string description, string model)
        {
            ProductDTO p = new ProductDTO();
            p.name = name;
            p.price = price;
            p.type = type;
            p.upc = upc;
            p.description = description;
            p.model = model;

            var resp = Common.ExecuteRequestWithBody<ProductDTO, ProductResponseDTO>(Common.CoreUrl, endpoint, Method.POST, p);
            Assert.AreEqual(resp.name, "BadRequest");
        }


        public void GetAProductsTotalIsSucess(string endpoint)
        {
            var productsResponse = Common.ExecuteRequest<ProductsResponseDTO>(Common.CoreUrl, endpoint, Method.GET);
            Assert.IsTrue(productsResponse.total > 51957);
            Assert.IsTrue(productsResponse.data.Length > 0);
            Assert.IsTrue(productsResponse.data[0].Categories.Length > 0);
        }

        public void GetByNameAndPriceIsSucess(string endpoint, string name, double price)
        {
            var productsResponse = Common.ExecuteRequest<ProductsResponseDTO>(Common.CoreUrl, endpoint, Method.GET);

            int i = 0;
            while (i < productsResponse.data.Length)
            {
                Assert.IsTrue(productsResponse.data[i].name.ToLower().Contains(name.Replace("+"," ").ToLower()));
                Assert.IsTrue(productsResponse.data[i].price < price);
                i++;
            }
        }


        public void GetAllProductsWithCaterogy(string endpoint, string category)
        {
            var productsResponse = Common.ExecuteRequest<ProductsResponseDTO>(Common.CoreUrl, endpoint, Method.GET);

            int products = 0;
            int categories = 0;
            int counter = 0;
            while (products < productsResponse.data.Length)
            {
                while (categories < productsResponse.data[products].Categories.Length)
                {
                    if (productsResponse.data[products].Categories[categories].name.Contains(category))
                    {
                        counter++;
                    }
                    categories++;
                }
                Assert.IsTrue(counter > 0);
                products++;
            }
        }
        

        public void GetProductsByIdIsSucess(string endpoint, int id)
        {
            var productsResponse = Common.ExecuteRequest<ProductResponseDTO>(Common.CoreUrl, endpoint, Method.GET);
            Assert.AreEqual(productsResponse.id, id);
        }



        public void GetAllProductsHighestProductIsSucess(string endpoint)
        {
            var productsResponse = Common.ExecuteRequest<ProductsResponseDTO>(Common.CoreUrl, endpoint, Method.GET);
            Assert.AreEqual(productsResponse.data[0].id, 3041141);
        }

        public void GetAllProductsShippingSucess(string endpoint, double shipping)
        {
            var productsResponse = Common.ExecuteRequest<ProductsResponseDTO>(Common.CoreUrl, endpoint, Method.GET);
            int i=0;
            while (i < productsResponse.data.Length)
            {
                Assert.IsTrue(productsResponse.data[i].shipping>shipping);
                i++;
            }

        }

        public void GetMethodAPISuccess(string url)
        {
            try
            {
                Common.GetAPI(url);
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexpected exception" + ex);
            }
        }
    }
}
