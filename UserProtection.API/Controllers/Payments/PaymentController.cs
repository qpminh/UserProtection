using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserProtection.Application.Dtos.Payments;
using UserProtection.Application.Interfaces.Payments;

namespace UserProtection.API.Controllers.Payments
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("vnpay")]
        //[Authorize(Roles = "Customer")]
        [Authorize]
        public async Task<IActionResult> CreateVNPayPayment([FromBody] PaymentRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _paymentService.CreatePaymentAsync(request);
            return Ok(result);
        }

        [HttpPost("callback")]
        //[AllowAnonymous]
        [Authorize]
        public async Task<IActionResult> VNPayCallback([FromBody] PaymentCallbackDto callback)
        {
            var result = await _paymentService.HandleCallbackAsync(callback);
            if (result == null)
                return NotFound(new { Message = "Invalid transaction." });

            return Ok(result);
        }

        [HttpPost("manual")]
        //[Authorize(Roles = "Admin,Staff")]
        [Authorize]
        public async Task<IActionResult> CreateManualPayment([FromBody] ManualPaymentRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _paymentService.CreateManualPaymentAsync(request);
            return CreatedAtAction(nameof(GetByTransactionId), new { txnId = result.TransactionId }, result);
        }

        [HttpGet("{txnId}")]
        //[Authorize(Roles = "Admin,Staff,Customer")]
        [Authorize]
        public async Task<IActionResult> GetByTransactionId(string txnId)
        {
            var payment = await _paymentService.GetByTransactionIdAsync(txnId);
            if (payment == null)
                return NotFound(new { Message = "Payment not found." });

            return Ok(new
            {
                payment.PaymentId,
                payment.SubscriptionId,
                payment.Amount,
                payment.Status,
                payment.PaymentMethod,
                payment.TransactionId,
                payment.PaymentDate
            });
        }
    }
}
