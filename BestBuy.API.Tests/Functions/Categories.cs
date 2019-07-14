using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestBuy.API.Tests.Helpers
{
    class CategoriesFunctions
    {
        public void PostCategory(string endpoint, string id, string name)
        {
            CategoryDTO p = new CategoryDTO();
            p.name = name;
            p.id = id;

            var resp = Common.ExecuteRequestWithBody<CategoryDTO, CategoryResponseDTO>(Common.CoreUrl, endpoint, Method.POST, p);
            Assert.AreEqual(resp.name, p.name);
            Assert.AreEqual(resp.id, p.id);
            Assert.IsNotEmpty(resp.updatedAt);
            Assert.IsNotEmpty(resp.createdAt);
        }

        public void PostCategoryFail(string endpoint, string id)
        {
            CategoryDTO p = new CategoryDTO();
            p.id = id;

            var resp = Common.ExecuteRequestWithBody<CategoryDTO, CategoryResponseDTO>(Common.CoreUrl, endpoint, Method.POST, p);
            Assert.AreEqual(resp.name, "BadRequest");
        }

        public void PostCategoryDuplicateFail(string endpoint, string id, string name)
        {
            CategoryDTO p = new CategoryDTO();
            p.name = name;
            p.id = id;

            var resp = Common.ExecuteRequestWithBody<CategoryDTO, CategoryResponseDTO>(Common.CoreUrl, endpoint, Method.POST, p);
            Assert.AreEqual(resp.name, "BadRequest");
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

        public void GetByNameIsSucess(string endpoint, string name)
        {
            var categoriesResponse = Common.ExecuteRequest<CategoriesResponseDTO>(Common.CoreUrl, endpoint, Method.GET);

            int i = 0;
            while (i < categoriesResponse.data.Length)
            {
                Assert.IsTrue(categoriesResponse.data[i].name.Contains(name));
                i++;
            }
        }
    }
}
