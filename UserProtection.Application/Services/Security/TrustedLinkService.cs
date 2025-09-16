using AutoMapper;
using UserProtection.Application.Dtos.Security;
using UserProtection.Application.Interfaces.Security;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces.Core;
using UserProtection.Infrastructure.Interfaces.Security;

namespace UserProtection.Application.Services.Security
{
    public class TrustedLinkService : ITrustedLinkService
    {
        private readonly ITrustedLinkRepository _repo;
        private readonly IMapper _mapper;
        private readonly IAuditLogRepository _auditRepo;
        public TrustedLinkService(ITrustedLinkRepository repo, IMapper mapper, IAuditLogRepository auditRepo)
        {
            _repo = repo; _mapper = mapper; _auditRepo = auditRepo;
        }

        public async Task<TrustedLinkDto> Create(TrustedLinkDto dto, string? currentUserId)
        {
            var entity = _mapper.Map<TrustedLink>(dto);
            entity.UserId = currentUserId;
            entity.CreatedAt = DateTime.UtcNow;
            var r = await _repo.Add(entity);
            await _auditRepo.AddLog(new AuditLog { UserId = currentUserId, Action = "TrustedLink.Create", Metadata = $"LinkId={r.LinkId},Domain={r.Domain}" });
            return _mapper.Map<TrustedLinkDto>(r);
        }

        public async Task<bool> Delete(int id, string? currentUserId)
        {
            var existing = await _repo.GetById(id);
            if (existing == null) return false;
            await _repo.Delete(id);
            await _auditRepo.AddLog(new AuditLog { UserId = currentUserId, Action = "TrustedLink.Delete", Metadata = $"LinkId={id}" });
            return true;
        }

        public async Task<IEnumerable<TrustedLinkDto>> GetAll(int? tenantId = null)
        {
            var list = await _repo.GetAll(tenantId);
            return _mapper.Map<IEnumerable<TrustedLinkDto>>(list);
        }

        public async Task<TrustedLinkDto?> GetById(int id)
        {
            var e = await _repo.GetById(id);
            return e == null ? null : _mapper.Map<TrustedLinkDto>(e);
        }

        public async Task<TrustedLinkDto?> Update(int id, TrustedLinkDto dto, string? currentUserId)
        {
            var existing = await _repo.GetById(id);
            if (existing == null) return null;
            existing.Domain = dto.Domain;
            existing.Url = dto.Url;
            existing.Category = dto.Category;
            existing.Source = dto.Source;
            existing.Status = dto.Status;
            existing.UpdatedAt = DateTime.UtcNow;
            await _repo.Update(existing);
            await _auditRepo.AddLog(new AuditLog { UserId = currentUserId, Action = "TrustedLink.Update", Metadata = $"LinkId={id}" });
            return _mapper.Map<TrustedLinkDto>(existing);
        }

        public Task<bool> IsTrusted(string url)
        {
            // naive check: compare domain
            var uri = new Uri(url);
            var domain = uri.Host.Replace("www.", "");
            var found = _repo.GetByDomain(domain).Result.Any();
            return Task.FromResult(found);
        }
    }
}
