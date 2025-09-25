using CCRS.Business.Interfaces;
using CCRS.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCRS.Business.Services
{
    public class ConsultationConfigService : BaseService, IConsultationConfigService
    {
        private readonly IConsultationConfigRepository _repository;

        public ConsultationConfigService(INotifier notificador, 
                                         IConsultationConfigRepository repository) : base(notificador)
        {
            _repository = repository;
        }

        public async Task AddAsync(ConsultationConfig config)
        {
            await _repository.Add(config);
        }

        public async Task<IEnumerable<ConsultationConfig>> GetAllAsync(DateTime startDate, int daysAhead)
        {
            return await _repository.GetAllAsync(startDate, daysAhead);
        }

        public async Task<ConsultationConfig> GetByIdAsync(Guid id)
        {
            return await _repository.GetById(id);
        }

        public async Task UpdateAsync(ConsultationConfig config)
        {
            await _repository.Update(config);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.Remove(id);
        }
        public void Dispose()
        {
            _repository?.Dispose();
        }
    }
}
