using BestBuy.API.Tests.Helpers;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gDoc.UI.Tests.API_Extensons
{
    public class StoresFunctions
    {
        public void PostStore(string endpoint, string name, string type, string address, string address2, string city, string state, string zip, string hours)
        {
            StoreDTO p = new StoreDTO();
            p.name = name;
            p.type = type;
            p.address = address;
            p.address2 = address2;
            p.city = city;
            p.state = state;
            p.zip = zip;
            p.hours = hours;
            var resp = Common.ExecuteRequestWithBody<StoreDTO, StoreResponseDTO>(Common.CoreUrl, endpoint, Method.POST, p);
            Assert.AreEqual(resp.name, p.name);
            Assert.IsTrue(resp.id > 1000);
            Assert.IsNotEmpty(resp.updatedAt);
            Assert.IsNotEmpty(resp.createdAt);
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

        public void GetByStateIsSuccess(string endpoint, string name)
        {
            var storesResponse = Common.ExecuteRequest<StoresResponseDTO>(Common.CoreUrl, endpoint, Method.GET);

            int i = 0;
            while (i < storesResponse.data.Length)
            {
                Assert.AreEqual(storesResponse.data[i].state, name);
                i++;
            }
        }
    }
}
