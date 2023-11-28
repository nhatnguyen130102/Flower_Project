using Flower_ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Security.Cryptography;
using System.Text;

namespace FlowerShop_Web.Controllers
{
    public class MomoController : Controller
    {
        private readonly string appID = "your_app_id";
        private readonly string key1 = "your_key1";
        private readonly string key2 = "your_key2";
        private readonly string endpoint = "https://sandbox.zalopay.com.vn/v001/tpe/createorder";


        public IActionResult InitiatePayment()
        {
            // Logic to generate orderId, orderInfo, amount, etc.
            string orderId = "your_order_id";
            string orderInfo = "your_order_info";
            string amount = "10000"; // Replace with the actual amount

            // Create ZaloPay order data
            string data = CreateOrderData(orderId, amount, orderInfo);

            // Calculate signature
            string signature = CalculateSignature(data);

            // Make a request to ZaloPay API
            var response = MakeApiRequest(data, signature);

            // Handle the response and redirect the user to ZaloPay payment page
            if (response != null && response["returncode"].ToString() == "1")
            {
                string orderUrl = response["orderurl"].ToString();
                return Redirect(orderUrl);
            }
            else
            {
                // Handle error
                return View("Error");
            }
        }

        private string CreateOrderData(string orderId, string amount, string orderInfo)
        {
            var dataObject = new
            {
                appid = appID,
                apptransid = orderId,
                appuser = "your_user_id",
                apptime = DateTimeOffset.Now.ToUnixTimeSeconds().ToString(),
                appfee = "0",
                apptoken = "",
                item = orderInfo,
                embeddata = "",
                itemdescription = "Description",
                itemurl = "https://yourdomain.com/item",
                payurl = "",
                redirecturl = "https://yourdomain.com/zalopay/return",
                serverurl = "https://yourdomain.com/zalopay/notify",
                amount = amount,
                description = "Description",
                bankcode = "zalopayapp",
            };

            return Newtonsoft.Json.JsonConvert.SerializeObject(dataObject);
        }

        private string CalculateSignature(string data)
        {
            using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key1)))
            {
                byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        private JObject MakeApiRequest(string data, string signature)
        {
            var client = new RestClient(endpoint);
            var request = new RestRequest(endpoint,Method.Post);

            request.AddParameter("application/json", data, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("x-partner-code", key1);
            request.AddHeader("x-ts", DateTimeOffset.Now.ToUnixTimeSeconds().ToString());
            request.AddHeader("x-sig", signature);

            RestResponse response = client.Execute(request);

            if (response.IsSuccessful)
            {
                return JObject.Parse(response.Content);
            }
            else
            {
                // Handle request failure
                return null;
            }
        }

        public IActionResult Return()
        {
            // Handle the return from ZaloPay after payment completion
            // Update your database or perform any necessary actions

            return View();
        }

        public IActionResult Notify()
        {
            // Handle the notification from ZaloPay
            // Update your database or perform any necessary actions

            return Ok();
        }
    }
}