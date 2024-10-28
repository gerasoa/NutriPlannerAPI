using AutoMapper;
using CCRS.Api.Controllers;
using CCRS.Api.ViewModels;
using CCRS.Business.Interfaces;
using CCRS.Business.Models;
using CCRS.Business.Services;
using CCRS.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CCRS.Api.V1.Controllers
{
    //[Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Appointment")]
    public class AppointmentController : MainController
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IAppointmentService _appointmentService;
        private readonly IMapper _mapper;

        public AppointmentController(IAppointmentService appointmentService,
                                    IAppointmentRepository appointmentRepository,
                                    IMapper mapper,
                                    INotifier notifier,
                                    IUser appUser) : base(notifier, appUser)
        {
            _appointmentRepository = appointmentRepository;
            _appointmentService = appointmentService;
            _mapper = mapper;
        }

        [HttpPost("new")]
        public async Task<ActionResult<Appointment>> Add(AppointmentViewModel appointmentViewmodel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            try
            {
                var result = await _appointmentService.Add(_mapper.Map<Appointment>(appointmentViewmodel));
                return CustomResponse(_mapper.Map<AppointmentViewModel>(result));
            }
            catch (NullReferenceException ex)
            {

                CustomResponse(ex.Message);
            }

            return CustomResponse();
        }

        [HttpGet("{id:guid}")]
        public async Task<IEnumerable<AppointmentViewModel>> Get(Guid id )
        {
            var appointmentViewModel = _mapper.Map<IEnumerable<AppointmentViewModel>>(await _appointmentRepository.Get(x => x.Id == id)).ToList();

            if (appointmentViewModel == null) { return (IEnumerable<AppointmentViewModel>)NotFound(); }

            return appointmentViewModel;
        }
    }
}

