namespace UserProtection.Application.Dtos.Payments
{
    public class PaymentRequestDto
    {
        public int SubscriptionId { get; set; }
        public string ReturnUrl { get; set; } = string.Empty;
    }

    public class PaymentResponseDto
    {
        public string PaymentUrl { get; set; } = string.Empty;
    }

    public class PaymentCallbackDto
    {
        public string TransactionId { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }

    public class PaymentResultDto
    {
        public string TransactionId { get; set; } = string.Empty;
        public int SubscriptionId { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? ApiKey { get; set; }
    }

    public class ManualPaymentRequestDto
    {
        public int SubscriptionId { get; set; }
        public decimal Amount { get; set; }
        //public string PaymentMethod { get; set; } = "Bank";
        public string Status { get; set; } = "Succeeded";
        public string? TransactionId { get; set; }
    }

    public class ManualPaymentResponseDto
    {
        public int PaymentId { get; set; }
        public int SubscriptionId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime PaymentDate { get; set; }
        public string TransactionId { get; set; } = string.Empty;
    }

    public class UpdatePaymentStatusRequest
    {
        public string Status { get; set; } = string.Empty;
    }

    public class UpdatePaymentAmountRequest
    {
        public decimal Amount { get; set; }
    }
}
