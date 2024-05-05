using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Stripe;
using Stripe.Checkout;
using WebShopBackEnd.Models;

namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
//[ApiExplorerSettings(IgnoreApi = true)]
public class CheckoutController : ControllerBase
{
    private readonly IConfiguration _configuration;

    private static string s_wasmClientURL = string.Empty;

    public CheckoutController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    //[HttpPost]
    //public async Task<ActionResult> CheckoutOrder([FromBody]List<CheckOutProduct> checkOutProducts, [FromServices] IServiceProvider sp)
    //{
    //    var referer = Request.Headers.Referer;
    //    s_wasmClientURL = referer[0];

    //    // Build the URL to which the customer will be redirected after paying.
    //    var server = sp.GetRequiredService<IServer>();

    //    var serverAddressesFeature = server.Features.Get<IServerAddressesFeature>();

    //    string? thisApiUrl = null;

    //    if (serverAddressesFeature is not null)
    //    {
    //        thisApiUrl = serverAddressesFeature.Addresses.FirstOrDefault();
    //    }

    //    if (thisApiUrl is not null)
    //    {
    //        //var sessionId =  CheckOut(checkOutProducts);
    //        var pubKey = _configuration["Stripe:PubKey"];

    //        var checkoutOrderResponse = new CheckOutOrderResponse()
    //        {
    //            //SessionId = sessionId,
    //            PubKey = pubKey
    //        };

    //        return Ok(checkoutOrderResponse);
    //    }
    //    else
    //    {
    //        return StatusCode(500);
    //    }
    //}

    [HttpPost]
    public IActionResult CheckOut(List<CheckOutProduct> checkOutProducts)
    {
        // Create a payment flow from the items in the cart.
        // Gets sent to Stripe API.
        var domain = "http://localhost:3000";
        var options = new SessionCreateOptions
        {
            // Stripe calls the URLs below when certain checkout events happen such as success and failure.
            SuccessUrl = domain + $"aboutUs", // Customer paid.
            CancelUrl = domain + "shop",  // Checkout cancelled.
            PaymentMethodTypes = new List<string> // Only card available in test mode?
            {
                "card"
            },
            LineItems = new List<SessionLineItemOptions>(),

            Mode = "payment" // One-time payment. Stripe supports recurring 'subscription' payments.
        };
        foreach (var product in checkOutProducts)
        {
            var sessionListItem = new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)product.Price, // Price is in USD cents.
                    Currency = "USD",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = product.ProductName,
                        Images = new List<string> { product.ProductImage }
                    },
                },
                Quantity = product.Quantity,
            };
            options.LineItems.Add(sessionListItem);
        }

        var service = new SessionService();
        Session session = service.Create(options);

        Response.Headers.Add("Location", session.Url);
        return new StatusCodeResult(303);
    }

    [HttpGet("success")]
    // Automatic query parameter handling from ASP.NET.
    // Example URL: https://localhost:7051/checkout/success?sessionId=si_123123123123
    public ActionResult CheckoutSuccess(string sessionId)
    {
        var sessionService = new SessionService();
        var session = sessionService.Get(sessionId);

        // Here you can save order and customer details to your database.
        var total = session.AmountTotal.Value;
        var customerEmail = session.CustomerDetails.Email;

        return Redirect(s_wasmClientURL + "success");
    }
}