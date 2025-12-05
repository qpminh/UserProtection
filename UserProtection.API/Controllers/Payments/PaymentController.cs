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

        [HttpPost("manual")]
        [Authorize]
        public async Task<IActionResult> CreateManualPayment([FromBody] ManualPaymentRequestDto request)
        {
            var result = await _paymentService.CreateManualPaymentAsync(request);
            return Ok(result);
        }

        [HttpPut("{paymentId}/status")]
        [Authorize]
        public async Task<IActionResult> UpdatePaymentStatus(int paymentId, [FromBody] UpdatePaymentStatusRequest request)
        {
            var result = await _paymentService.UpdatePaymentStatusAsync(paymentId, request.Status);
            if (result == null)
                return NotFound(new { Message = "Payment not found." });

            return Ok(result);
        }

        [HttpGet("{txnId}")]
        [Authorize]
        public async Task<IActionResult> GetByTransactionId(string txnId)
        {
            var payment = await _paymentService.GetByTransactionIdAsync(txnId);
            if (payment == null)
                return NotFound(new { Message = "Payment not found." });

            return Ok(payment);
        }

        [HttpPut("{paymentId}/amount")]
        [Authorize]
        public async Task<IActionResult> UpdatePaymentAmount(int paymentId, [FromBody] UpdatePaymentAmountRequest request)
        {
            var result = await _paymentService.UpdatePaymentAmountAsync(paymentId, request.Amount);
            if (result == null)
                return NotFound(new { Message = "Payment not found." });

            return Ok(result);
        }
    }
}
