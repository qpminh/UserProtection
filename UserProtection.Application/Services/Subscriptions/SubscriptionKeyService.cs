using UserProtection.Application.Interfaces.Subscriptions;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Subscriptions;

namespace UserProtection.Application.Services.Subscriptions
{
    public class SubscriptionKeyService : ISubscriptionKeyService
    {
        private readonly ISubscriptionKeyRepository _keyRepo;
        private readonly ISubscriptionRepository _subRepo;

        public SubscriptionKeyService(ISubscriptionKeyRepository keyRepo, ISubscriptionRepository subRepo)
        {
            _keyRepo = keyRepo;
            _subRepo = subRepo;
        }

        /// <summary>
        /// Sinh key mới cho một subscription
        /// </summary>
        public async Task<string> GenerateKeyAsync(int subscriptionId, bool deactivateOld = false)
        {
            var subscription = await _subRepo.GetByIdAsync(subscriptionId);
            if (subscription == null) throw new Exception("Subscription not found");

            if (deactivateOld)
            {
                var oldKeys = await _keyRepo.GetBySubscriptionIdAsync(subscriptionId);
                foreach (var old in oldKeys) old.IsActive = false;
            }

            var keyValue = Guid.NewGuid().ToString("N");
            var key = new SubscriptionKey
            {
                SubscriptionId = subscriptionId,
                KeyValue = keyValue,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            await _keyRepo.AddAsync(key);
            await _keyRepo.SaveChangesAsync();

            return keyValue;
        }

        /// <summary>
        /// Xác thực API key và trả về Subscription (nếu hợp lệ)
        /// </summary>
        public async Task<Subscription?> ValidateKeyAsync(string keyValue)
        {
            var key = await _keyRepo.GetByValueAsync(keyValue);
            if (key == null || !key.IsActive) return null;

            // kiểm tra expired
            if (key.ExpiredAt.HasValue && key.ExpiredAt.Value < DateTime.UtcNow)
                return null;

            return key.Subscription;
        }

        public async Task<IEnumerable<SubscriptionKey>> GetKeysAsync(int subscriptionId)
        {
            return await _keyRepo.GetBySubscriptionIdAsync(subscriptionId);
        }
    }
}