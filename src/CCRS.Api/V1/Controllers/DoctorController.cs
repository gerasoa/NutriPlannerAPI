using AutoMapper;
using CCRS.Api.Controllers;
using CCRS.Api.ViewModels;
using CCRS.Business.Interfaces;
using CCRS.Business.Models;
using CCRS.Business.Services;
using IdentityModel.OidcClient;
using Microsoft.AspNetCore.Mvc;

namespace CCRS.Api.V1.Controllers
{
    //[Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/doctor")]
    public class DoctorController : MainController
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IDoctorService _doctorService;
        private readonly IMapper _mapper;

        public DoctorController(INotifier notifier,
                                IUser appUser,
                                IDoctorService doctorService,
                                IDoctorRepository doctorRepository,
                                IMapper mapper) : base(notifier, appUser)
        {
            _doctorService = doctorService;
            _doctorRepository = doctorRepository;
            _mapper = mapper;
        }


        /// <summary>
        /// Registers a new doctor in the system.
        /// </summary>
        /// <param name="doctorViewModel">The view model containing the details of the doctor to be added, such as name, specialty, and online consultation availability.</param>
        /// <returns>
        /// Returns a <see cref="DoctorViewModel"/> object representing the newly created doctor if the operation is successful.
        /// If validation fails, returns a bad request response with the validation errors.
        /// If a null reference exception occurs, returns an error message.
        /// </returns>
        /// <response code="200">Returns the newly created doctor object.</response>
        /// <response code="400">Returns validation errors if the input data is invalid.</response>
        /// <response code="500">Returns an internal server error message if a null reference exception occurs.</response>
        [HttpPost("new-doctor")]
        public async Task<ActionResult<DoctorViewModel>> Add(DoctorViewModel doctorViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            try
            {
                var result = await _doctorService.Add(_mapper.Map<Doctor>(doctorViewModel));
                return CustomResponse(_mapper.Map<DoctorViewModel>(result));
            }
            catch (NullReferenceException ex)
            {

                CustomResponse(ex.Message);
            }

            return CustomResponse();                       
        }
    }
}
    