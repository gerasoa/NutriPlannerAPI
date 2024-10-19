using CCRS.Business.Interfaces;
using CCRS.Business.Models;
using CCRS.Business.Models.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CCRS.Business.Services
{
    public class DoctorService : BaseService, IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(INotifier notificador, 
                             IDoctorRepository doctorRepository) : base(notificador)
        {
            _doctorRepository = doctorRepository;
        }
                
        public async Task<Doctor> Add(Doctor doctor)
        {
            if (!ExecutarValidacao(new DoctorValidation(), doctor)) return null;

            //todo: completar os atributos da classe Patient e adicionar validacoes para patient ja existente
            if (_doctorRepository.Get(f => f.Name == doctor.Name || 
                                             f.Id == doctor.Id).Result.Any())
            {
                Notificar("A patient with this name already exists.");
                return null;
            }

            await _doctorRepository.Add(doctor);
            return doctor;
        }

        public void Dispose()
        {
            _doctorRepository?.Dispose();
            _doctorRepository?.Dispose();
        }
    }
}
