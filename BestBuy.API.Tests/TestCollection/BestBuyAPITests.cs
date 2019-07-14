using BestBuy.API.Tests.API_Extensons;
using BestBuy.API.Tests.Helpers;
using test.UI.Tests.API_Extensons;
using NUnit.Framework;
using System;

namespace BestBuy.API.Tests.TestCollection
{
    [TestFixture]
    public class _BestBuyAPITests
    {
        /// PRODUCTS GET
        private ProductsFunctions prodFunctions = new ProductsFunctions();

        [TestCase("/products", TestName = "Check all products responce status and content type")]
        public void GetListOfAllProductsAPISuccess(string url)
        {
            prodFunctions.GetMethodAPISuccess(url);
        }

        [TestCase("/products", TestName = "Check all products total, limit and skip are correct")]
        public void GetAssertDataAllProductsAPISuccess(string url)
        {
            prodFunctions.GetAProductsTotalIsSucess(url);
        }
        [TestCase("/products?$sort[price]=-1", TestName = "Check product with highest price")]
        public void GetAllProductsHighestProductIsSucess(string url)
        {
            prodFunctions.GetAllProductsHighestProductIsSucess(url);
        }
        [TestCase("/products?name[$like]=*star+wars*&price[$lt]=30", "star+wars", 30, TestName = "Check with name and within range")]

        public void GetByNameAndPriceIsSucess(string url, string name, double price)
        {
            prodFunctions.GetByNameAndPriceIsSucess(url, name, price);
        }
        [TestCase("/products?category.name", "Coffee Pods", TestName = "Check product with categoty")]
        public void GetAllProductsWithCaterogy(string url, string category)
        {
            prodFunctions.GetAllProductsWithCaterogy(url+"="+category, category);
        }
        [TestCase("/products?shipping[$gt]",10, TestName = "Check product with shipping Price not more than")]
        public void GetAllProductsWithLowShippingPrice(string url, double shipping)
        {
            prodFunctions.GetAllProductsShippingSucess(url, shipping);
        }
        [TestCase("/products", 9132294, TestName = "Check product with id is correct")]
        public void GetAssertDataAllProductsAPISuccessIdIsCorrect(string url, int id)
        {
            prodFunctions.GetProductsByIdIsSucess(url +"/"+id.ToString(), id);
        }

        //-----------------------------
        // PRODUCTS POST
        [TestCase("/products", "test name","test type","test upc",150,"test description","test model",TestName = "Create new product")]

        public void PostNewProduct(string url, string name, string type, string upc, double price, string description, string model)
        {
            prodFunctions.PostProduct(url, name,  type,  upc,  price,  description, model);
        }

        [TestCase("/products", "test name", "test type", "test upc", 150, "test description", TestName = "Create new product with mandatory data missing")]
        public void PostNewProduct(string url, string name, string type, string upc, double price, string description)
        {
            prodFunctions.PostProductFail(url, name, type, upc, price, description);
        }


        [TestCase("/products", "test name", "", "test upc", 150, "test description", "test model", TestName = "Create new product with type less than1 char long")]
        [TestCase("/products", "test name", "11", "", 150, "test description", "test model", TestName = "Create new product with upc less than 1 char long")]
        [TestCase("/products", "test name", "11", "11", 150, "", "test model", TestName = "Create new product with decription less than1 char long")]
        [TestCase("/products", "test name", "11", "11", 150, "11", "", TestName = "Create new product with model less than 1 char long")]
        public void PostNewProductValidations(string url, string name, string type, string upc, double price, string description, string model)
        {
            prodFunctions.PostProductValidationsFail(url, name, type, upc, price, description, model);
        }

        
        //-----------------------------
        // CATEGORIES GET
        private CategoriesFunctions categoriesFunctions = new CategoriesFunctions();

        [TestCase("/categories", TestName = "Check all categories")]
        public void GetListOfAllCategoriesAPISuccess(string url)
        {
            prodFunctions.GetMethodAPISuccess(url);
        }
        [TestCase("/categories?name[$like]=*TV*", "TV", TestName = "Check all categories witn Name containing TV")]
        public void GetListOfAllCategoriesByNameAPISuccess(string url, string name)
        {
            categoriesFunctions.GetByNameIsSucess(url, name);
        }
        //-----------------------------
        //CATEGORIES POST
        [TestCase("/categories", "TEST CATEGORY", TestName = "Create new category")]
        
        public void PostNewCategory(string url, string name)
        {
            string id = Guid.NewGuid().ToString();
            categoriesFunctions.PostCategory(url, id, name);
        }

        [TestCase("/categories", "test1","test1", TestName = "Create duplicate category")]
        public void PostCategoryDuplicateFail(string url, string id, string name)
        {
            categoriesFunctions.PostCategoryDuplicateFail(url, id, name);
        }

        [TestCase("/categories", "test1", TestName = "Create category with missing data")]
        public void PostCategoryFail(string url, string id)
        {
            categoriesFunctions.PostCategoryFail(url, id);
        }
        //-----------------------------
        /// STORES GET
        private StoresFunctions storesFunctions = new StoresFunctions();

        [TestCase("/stores", TestName = "Check all stores")]
        public void GetListOfAllStoresAPISuccess(string url)
        {
            storesFunctions.GetMethodAPISuccess(url);
        }
        [TestCase("/stores?state=", "MN", TestName = "Check all stores in state")]
        public void GetByStateIsSuccess(string url, string name)
        {
            storesFunctions.GetByStateIsSuccess(url+name, name);
        }

        //-----------------------------
        // STORES POST
        [TestCase("/stores", "New Store", "BigBox", "123 Fake St","", "Springfield", "MN", "55123", "Mon: 10-9; Tue: 10-9; Wed: 10-9; Thurs: 10-9; Fri: 10-9; Sat: 10-9; Sun: 10-8", TestName = "Create new store")]
        public void PostNewStore(string url, string name, string type, string address, string address2, string city, string state, string zip, string hours)
        {
            storesFunctions.PostStore(url, name,  type,  address,  address2,  city,  state,  zip, hours);
        }

        //-----------------------------
        // HEALTHCHECK
        private HealthCheckFunctions healthFunctions = new HealthCheckFunctions();

        [TestCase("/healthcheck", TestName = "Health Check")]
        public void GetLHealthcheckAPISuccess(string url)
        {
            storesFunctions.GetMethodAPISuccess(url);
        }
    }
}
