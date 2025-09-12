using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProtection.Application.Dtos.Security;
using UserProtection.Application.Interfaces;
using UserProtection.Domain.Entities;
using UserProtection.Infrastructure.Interfaces;

namespace UserProtection.Application.Services.Security
{
    public class SuspiciousLinkService : ISuspiciousLinkService
    {
        private readonly ISuspiciousLinkRepository _repo;
        private readonly IMapper _mapper;
        private readonly IAuditLogRepository _auditRepo;
        public SuspiciousLinkService(ISuspiciousLinkRepository repo, IMapper mapper, IAuditLogRepository auditRepo)
        {
            _repo = repo; _mapper = mapper; _auditRepo = auditRepo;
        }

        public async Task<SuspiciousLinkDto> Report(SuspiciousLinkDto dto, string? currentUserId)
        {
            var entity = _mapper.Map<SuspiciousLink>(dto);
            entity.UserId = currentUserId;
            entity.DetectedAt = DateTime.UtcNow;
            var r = await _repo.Add(entity);
            await _auditRepo.AddLog(new AuditLog { UserId = currentUserId, Action = "SuspiciousLink.Report", Metadata = $"Url={r.Url}" });
            return _mapper.Map<SuspiciousLinkDto>(r);
        }

        public async Task<IEnumerable<SuspiciousLinkDto>> GetByUser(string userId)
        {
            var list = await _repo.GetByUser(userId);
            return _mapper.Map<IEnumerable<SuspiciousLinkDto>>(list);
        }

        public async Task<IEnumerable<SuspiciousLinkDto>> GetRecent(int limit = 100)
        {
            var list = await _repo.GetRecent(limit);
            return _mapper.Map<IEnumerable<SuspiciousLinkDto>>(list);
        }
    }
}
