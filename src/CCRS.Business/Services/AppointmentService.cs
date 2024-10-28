using CCRS.Business.Interfaces;
using CCRS.Business.Models;
using CCRS.Business.Models.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CCRS.Business.Services
{
    public class AppointmentService : BaseService, IAppointmentService
    {
        private readonly IAppointmentRepository _AppointmentRepository;

        public AppointmentService(INotifier notificador, 
                             IAppointmentRepository AppointmentRepository) : base(notificador)
        {
            _AppointmentRepository = AppointmentRepository;
        }
                
        public async Task<Appointment> Add(Appointment Appointment)
        {
            if (!ExecutarValidacao(new AppointmentValidation(), Appointment)) return null;

            //todo: completar os atributos da classe Patient e adicionar validacoes para patient ja existente
            //if (_AppointmentRepository.Get(f => f.Name == Appointment.Name || 
            //                                 f.Id == Appointment.Id).Result.Any())
            //{
            //    Notificar("A patient with this name already exists.");
            //    return null;
            //}

            await _AppointmentRepository.Add(Appointment);
            return Appointment;
        }

        public void Dispose()
        {
            _AppointmentRepository?.Dispose();
            _AppointmentRepository?.Dispose();
        }
    }
}
