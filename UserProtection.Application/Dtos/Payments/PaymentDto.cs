namespace UserProtection.Application.Dtos.Payments;

public class PaymentRequestDto
{
    public int SubscriptionId { get; set; }
    public string ReturnUrl { get; set; } = null!;
}

public class PaymentResponseDto
{
    public string PaymentUrl { get; set; } = null!;
}

public class PaymentCallbackDto
{
    public string TransactionId { get; set; } = null!;
    public string Status { get; set; } = null!; // "Success" | "Failed"
    public int SubscriptionId { get; set; }
}

public class PaymentResultDto
{
    public string TransactionId { get; set; } = null!;
    public int SubscriptionId { get; set; }
    public string Status { get; set; } = null!;
    public string? ApiKey { get; set; }
}

