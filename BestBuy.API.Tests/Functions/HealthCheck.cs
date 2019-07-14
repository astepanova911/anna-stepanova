using BestBuy.API.Tests.Helpers;
using NUnit.Framework;
using System;

namespace test.UI.Tests.API_Extensons
{
    public class HealthCheckFunctions
    {
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