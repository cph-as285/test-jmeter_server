using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using jmeter_server.Models;
using Microsoft.Ajax.Utilities;
using Swashbuckle.Swagger.Annotations;

namespace jmeter_server.Controllers
{
    [RoutePrefix("api/bank")]
    public class BankController : ApiController
    {
        [HttpGet]
        [Route("interest")]
        public HttpResponseMessage Interest()
        {
            return Request.CreateResponse("Hallo");
        }

        [HttpGet]
        [Route("discount")]
        [SwaggerResponse(HttpStatusCode.OK, null, typeof(DiscountVM))]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        public HttpResponseMessage Discount(string customerInfo = "")
        {
            var cts = GetCustomerType(customerInfo);
            if (cts == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    "Please enter 'nc' (New customer), 'lc' (Loyalty card) or 'c' (Coupon) as comma separated list without space");

            var discount = CalculateDiscount(cts);
            if (discount == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                    "Can't both be new customer and have a loyalty card");


            var discountVM = new DiscountVM {Discount = discount.Value};
            return Request.CreateResponse(discountVM);
        }

        private List<CustomerType> GetCustomerType(string info)
        {
            var list = new List<CustomerType>();
            if (info.IsNullOrWhiteSpace())
                return list;
            var custInfos = info.Split(',');
            foreach (var custInfo in custInfos)
                switch (custInfo.ToLower())
                {
                    case "nc":
                        list.Add(CustomerType.NewCustomer);
                        break;
                    case "lc":
                        list.Add(CustomerType.LoyaltyCard);
                        break;
                    case "c":
                        list.Add(CustomerType.Coupon);
                        break;
                    default:
                        return null;
                }

            return list;
        }

        private int? CalculateDiscount(List<CustomerType> cts)
        {
            if (cts.Contains(CustomerType.LoyaltyCard) && cts.Contains(CustomerType.NewCustomer))
                return null;

            if (cts.Contains(CustomerType.Coupon) && !cts.Contains(CustomerType.LoyaltyCard))
                return 20;

            if (cts.Contains(CustomerType.Coupon) && cts.Contains(CustomerType.LoyaltyCard))
                return 30;

            if (cts.Contains(CustomerType.NewCustomer))
                return 15;

            if (cts.Contains(CustomerType.Coupon))
                return 20;

            if (cts.Contains(CustomerType.LoyaltyCard))
                return 10;
            
            return 0;
        }
    }
}