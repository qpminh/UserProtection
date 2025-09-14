using Microsoft.AspNetCore.Http;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace UserProtection.Infrastructure.Helpers;

public class VnPayHelper
{
    private readonly string _tmnCode;
    private readonly string _hashSecret;
    private readonly string _vnpUrl;
    private readonly string _callbackUrl; 

    public string CallbackUrl => _callbackUrl;

    public VnPayHelper(string tmnCode, string hashSecret, string vnpUrl, string callbackUrl)
    {
        _tmnCode = tmnCode;
        _hashSecret = hashSecret;
        _vnpUrl = vnpUrl;
        _callbackUrl = callbackUrl;
    }

    public string CreatePaymentUrl(string transactionId, int subscriptionId, decimal amount)
    {
        var vnpParams = new SortedList<string, string>
        {
            ["vnp_Version"] = "2.1.0",
            ["vnp_Command"] = "pay",
            ["vnp_TmnCode"] = _tmnCode,
            ["vnp_Amount"] = ((int)(amount * 100)).ToString(),
            ["vnp_CreateDate"] = DateTime.UtcNow.ToString("yyyyMMddHHmmss"),
            ["vnp_CurrCode"] = "VND",
            ["vnp_IpAddr"] = "127.0.0.1",
            ["vnp_Locale"] = "vn",
            ["vnp_OrderInfo"] = $"SubId={subscriptionId}", 
            ["vnp_OrderType"] = "other",
            ["vnp_ReturnUrl"] = _callbackUrl, 
            ["vnp_TxnRef"] = transactionId
        };

        var rawData = string.Join("&", vnpParams.Select(kvp => $"{kvp.Key}={WebUtility.UrlEncode(kvp.Value)}"));
        var secureHash = HmacSHA512(_hashSecret, rawData);

        return $"{_vnpUrl}?{rawData}&vnp_SecureHash={secureHash}";
    }

    public bool ValidateSignature(IQueryCollection query)
    {
        var sorted = query
            .Where(kvp => kvp.Key != "vnp_SecureHash" && kvp.Key != "vnp_SecureHashType")
            .OrderBy(kvp => kvp.Key)
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());

        var rawData = string.Join("&", sorted.Select(kvp => $"{kvp.Key}={WebUtility.UrlEncode(kvp.Value)}"));
        var checkHash = HmacSHA512(_hashSecret, rawData);
        var receivedHash = query["vnp_SecureHash"].ToString();
        return checkHash.Equals(receivedHash, StringComparison.OrdinalIgnoreCase);
    }

    private string HmacSHA512(string key, string inputData)
    {
        using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key));
        return BitConverter.ToString(hmac.ComputeHash(Encoding.UTF8.GetBytes(inputData)))
            .Replace("-", "").ToLower();
    }
}
