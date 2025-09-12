using Microsoft.AspNetCore.Mvc;
using UserProtection.Application.DTOs;
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

    // ✅ API khởi tạo thanh toán
    [HttpPost("create")]
    public async Task<IActionResult> CreatePayment([FromBody] PaymentRequestDto request)
    {
        if (request == null || request.Amount <= 0)
            return BadRequest(new { Message = "Invalid request" });

        var response = await _paymentService.CreatePaymentAsync(request);

        return Ok(response); // response sẽ chứa PaymentUrl để client redirect
    }

    // ✅ Callback từ VNPay
    [HttpGet("callback")]
    public async Task<IActionResult> Callback()
    {
        var query = Request.Query;

        if (!_vnPayHelper.ValidateSignature(query))
            return BadRequest(new { Message = "Invalid signature" });

        // lấy SubscriptionId từ OrderInfo
        var orderInfo = query["vnp_OrderInfo"].ToString();
        var subscriptionId = int.Parse(orderInfo.Split(' ').Last());

        var callback = new PaymentCallbackDto
        {
            TransactionId = query["vnp_TxnRef"].ToString(),
            Status = query["vnp_ResponseCode"] == "00" ? "Success" : "Failed",
            SubscriptionId = subscriptionId
        };

        await _paymentService.HandleCallbackAsync(callback);

        return Ok(new
        {
            Message = "Payment processed",
            callback.Status,
            callback.SubscriptionId,
            callback.TransactionId,
            ResponseCode = query["vnp_ResponseCode"].ToString()
        });
    }
}
