using Microsoft.AspNetCore.Mvc;
using UserProtection.Application.Dtos.Payment;
using UserProtection.Application.Services.Payment;
using UserProtection.Infrastructure.Helpers;

namespace UserProtection.API.Controllers.Payment;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly PaymentService _paymentService;
    private readonly VnPayHelper _vnPayHelper;

    public PaymentController(PaymentService paymentService, VnPayHelper vnPayHelper)
    {
        _paymentService = paymentService;
        _vnPayHelper = vnPayHelper;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreatePayment([FromBody] PaymentRequestDto request)
    {
        if (request == null || request.SubscriptionId <= 0)
            return BadRequest(new { Message = "Invalid request" });

        var response = await _paymentService.CreatePaymentAsync(request);
        return Ok(response); 
    }

    [HttpGet("callback")]
    public async Task<IActionResult> Callback()
    {
        var query = Request.Query;
        if (!_vnPayHelper.ValidateSignature(query))
            return BadRequest(new { Message = "Invalid signature" });

        var orderInfo = query["vnp_OrderInfo"].ToString();
        if (!orderInfo.StartsWith("SubId="))
            return BadRequest(new { Message = "Invalid OrderInfo format" });

        var subscriptionId = int.Parse(orderInfo.Replace("SubId=", ""));
        var callback = new PaymentCallbackDto
        {
            TransactionId = query["vnp_TxnRef"].ToString(),
            Status = query["vnp_ResponseCode"] == "00" ? "Success" : "Failed",
            SubscriptionId = subscriptionId
        };

        var result = await _paymentService.HandleCallbackAsync(callback);
        if (result == null)
            return NotFound(new { Message = "Payment or subscription not found" });

        var payment = await _paymentService.GetByTransactionIdAsync(callback.TransactionId);
        var feUrl = payment?.FrontendReturnUrl ?? "https://myfrontend.com/payment/result";

        return Redirect($"{feUrl}?status={result.Status}&subscriptionId={result.SubscriptionId}&txnId={result.TransactionId}&apiKey={result.ApiKey}");
    }
}
