using BestBuy.API.Tests.API_Extensons;
using System;

namespace BestBuy.API.Tests.Core
{

    public class CoreUrlBuilder
    {
        public static String CreateUrl(string relativeUrl)
        {
            return String.Concat(BestBuyConstants.HOST, "/", relativeUrl);
        }
    
    }
}