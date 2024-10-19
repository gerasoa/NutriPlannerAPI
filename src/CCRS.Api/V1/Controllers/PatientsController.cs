using AutoMapper;
using CCRS.Api.Controllers;
using CCRS.Api.ViewModels;
using CCRS.Business.Interfaces;
using CCRS.Business.Models;
using Microsoft.AspNetCore.Mvc;


namespace CCRS.Api.V1.Controllers
{
    //[Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/patient")]
    public class PatientsController : MainController
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IPatientService _patientService;
        private readonly IMapper _mapper;
        private readonly IAddressRepository _addressRepository;        

        public PatientsController(IPatientRepository patientRepository,
                                  IMapper mapper,
                                  IPatientService patientService,
                                  INotifier notifier,
                                  IAddressRepository addressRepository,
                                  IUser user) : base(notifier, user)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
            _patientService = patientService;
            _addressRepository = addressRepository;
        }


        /// <summary>
        /// Registers a new patient in the system with optional image upload.
        /// </summary>
        /// <param name="patientViewModel">The view model containing the details of the patient to be added, including personal information and an optional image upload.</param>
        /// <returns>
        /// Returns a <see cref="PatientViewModel"/> object representing the newly created patient if the operation is successful.
        /// If validation fails, returns a bad request response with the validation errors.
        /// </returns>
        /// <remarks>
        /// This action allows the registration of a new patient, and if an image is provided, it will be uploaded with a unique name.
        /// The request size limit is set to 500,000 bytes (approximately 488 KB) to allow for image upload.
        /// </remarks>
        /// <response code="200">Returns the newly created patient object.</response>
        /// <response code="400">Returns validation errors if the input data is invalid.</response>
        /// <response code="413">Returns if the uploaded file exceeds the size limit.</response>
        [RequestSizeLimit(500000)]
        [HttpPost("new-patient")]
        public async Task<ActionResult<PatientViewModel>> Add(PatientViewModel patientViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var imageName = string.Empty;

            if (!string.IsNullOrEmpty(patientViewModel.ImageUpload))
            {
                imageName = Guid.NewGuid().ToString();
                UploadArquivo(patientViewModel.ImageUpload, imageName);
            }

            patientViewModel.Image = imageName;
            var result = await _patientService.Add(_mapper.Map<Patient>(patientViewModel));

            return CustomResponse(_mapper.Map<PatientViewModel>(result));
        }
               
        /// <summary>
        /// Retrieves a list of patients associated with a specific doctor.
        /// </summary>
        /// <remarks>
        /// This endpoint allows a doctor to fetch all patients linked to their ID. 
        /// It returns a collection of <see cref="PatientViewModel"/> objects containing the details of each patient.
        /// </remarks>
        /// <param name="id">The unique identifier of the doctor whose patients are being retrieved.</param>
        /// <returns>Returns an <see cref="IEnumerable{PatientViewModel}"/> containing the list of patients associated with the doctor.</returns>
        /// <response code="200">Returns the list of patients successfully retrieved.</response>
        /// <response code="404">Returns a not found error if no patients are associated with the provided doctor ID.</response>
        //[ClaimsAuthorize("Role", "Patient")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PatientViewModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id:guid}")]
        public async Task<IEnumerable<PatientViewModel>> GetPatientsByDoctorId(Guid id)
        {
            var patientsViewModel = await GetPatientAddressByDoctorAsync(id);

            if (patientsViewModel == null) { return (IEnumerable<PatientViewModel>) NotFound(); }

            return patientsViewModel;
        }

        /// <summary>
        /// Updates an existing patient's details by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the patient to be updated.</param>
        /// <param name="patientViewModel">The updated details of the patient.</param>
        /// <returns>An <see cref="ActionResult{PatientViewModel}"/> indicating the result of the operation.</returns>
        /// <response code="200">Returns the updated patient details on successful update.</response>
        /// <response code="400">Returns a bad request response if the provided patient ID does not match the ID in the request body or if the model state is invalid.</response>
        /// <response code="404">Returns a not found response if the patient with the specified ID does not exist.</response>
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<PatientViewModel>> Update(Guid id, PatientViewModel patientViewModel)
        {
            if (id != patientViewModel.Id) return BadRequest();

            var patientUpdated = await _patientRepository.GetById(id);
            patientViewModel.Image = patientUpdated.Image;

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var imageName = string.Empty;
            if (patientViewModel.ImageUpload != null)
            {
                imageName = Guid.NewGuid().ToString();
                UploadArquivo(patientViewModel.ImageUpload, imageName);

                RemoveArquivo(patientUpdated.Image);
                patientUpdated.Image = imageName;
            }

            patientUpdated.Name = patientViewModel.Name;
            patientUpdated.DoB = patientViewModel.DoB;

            await _patientService.Update(_mapper.Map<Patient>(patientUpdated));

            return CustomResponse(patientViewModel);
        }

        /// <summary>
        /// Deletes a patient by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the patient to be deleted.</param>
        /// <returns>An <see cref="ActionResult{PatientViewModel}"/> indicating the result of the operation.</returns>
        /// <response code="200">Returns an empty response indicating the patient was successfully deleted.</response>
        /// <response code="404">Returns a not found response if the patient with the specified id does not exist.</response>
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<PatientViewModel>> Delete(Guid id)
        {
            var patientViewModel = await GetPatientAddressAsync(id);

            if (patientViewModel == null) return NotFound();

            await _patientService.DeleteAsync(id);

            return CustomResponse();
        }

        /// <summary>
        /// Adds a new address for an existing patient.
        /// </summary>
        /// <remarks>
        /// This endpoint allows a user to add an address for a specified patient. 
        /// The patient's ID must be provided in the URL and should match the ID present in the address model. 
        /// The request body should contain the address details, such as street, city, state, etc.
        /// </remarks>
        /// <param name="patientId">The unique ID of the patient to whom the address will be added.</param>
        /// <param name="addressViewModel">The address model containing the information of the new address to be added.</param>
        /// <returns>Returns an <see cref="AddressViewModel"/> object with the details of the added address, or an error if validation fails.</returns>
        /// <response code="200">Returns the address successfully added.</response>
        /// <response code="400">Returns a bad request error if the patient ID does not match or if the model state is invalid.</response>
        /// <response code="404">Returns an error if the patient is not found.</response>
        [HttpPost("{patientId:guid}/address")]
        public async Task<ActionResult<AddressViewModel>> AddPatientAddress(Guid patientId, AddressViewModel addressViewModel)
        {
            if (patientId != addressViewModel.PatientId) return BadRequest();

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _patientService.AddAddress(_mapper.Map<Address>(addressViewModel));

            return CustomResponse(_mapper.Map<AddressViewModel>(result));
        }

        /// <summary>
        /// Retrieves the address associated with a specific patient by their unique identifier.
        /// </summary>
        /// <param name="patientId">The unique identifier of the patient.</param>
        /// <returns>
        /// An <see cref="ActionResult{T}"/> containing the <see cref="AddressViewModel"/> with the address information
        /// associated with the patient, or an appropriate HTTP response if the address is not found.
        /// </returns>
        /// <response code="200">Returns the patient's address information.</response>
        /// <response code="404">Returns not found if no address is associated with the specified patient ID.</response>
        /// <response code="400">Returns a bad request if the input data is invalid.</response>
        [HttpGet("{patientId:guid}/Address")]
        public async Task<ActionResult<AddressViewModel>> GetAddress(Guid patientId)
        {
            return CustomResponse(_mapper.Map<AddressViewModel>(await _addressRepository.GetAddressByPatient(patientId)));
        }

        /// <summary>
        /// Updates the address of a patient using the provided patient ID.
        /// </summary>
        /// <param name="Id">The unique identifier (GUID) of the patient whose address is being updated.</param>
        /// <param name="addressViewModel">The <see cref="AddressViewModel"/> containing the updated address details.</param>
        /// <returns>
        /// An <see cref="ActionResult"/> indicating the outcome of the update operation, including the updated address details.
        /// </returns>
        /// <response code="200">Returns if the patient's address was successfully updated.</response>
        /// <response code="400">Returns a bad request if the input data is invalid or the patient ID does not match the address ID.</response>
        /// <response code="404">Returns not found if the patient or address does not exist.</response>
        [HttpPut("{Id:guid}/Address")]
        public async Task<ActionResult> UpdateAddress(Guid Id, AddressViewModel addressViewModel)
        {
            if (Id != addressViewModel.Id) return BadRequest();

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _patientService.UpdateAddress(_mapper.Map<Address>(addressViewModel));

            return CustomResponse(addressViewModel);
        }

        private bool UploadArquivo(string arquivo, string imgNome)
        {
            if (string.IsNullOrEmpty(arquivo))
            {
                NotifyError("Informe um arquivo de imagem");
                return false;
            }

            if (string.IsNullOrWhiteSpace(imgNome))
            {
                NotifyError("Nome da imagem não fornecido.");
                return false;
            }

            const int maxFileSize = 500 * 1024; // 500KB
            if (arquivo.Length > maxFileSize)
            {
                NotifyError("O arquivo excede o tamanho permitido de 500KB.");
                return false;
            }

            byte[] imageDataByteArray;

            try
            {
                imageDataByteArray = Convert.FromBase64String(arquivo);
            }
            catch (FormatException ex)
            {
                NotifyError(ex.Message);
                return false;
            }

            var filePath = BuildFilePath(imgNome);

            if (System.IO.File.Exists(filePath))
            {
                imgNome = Guid.NewGuid().ToString();
                filePath = BuildFilePath(imgNome);
            }

            try
            {
                System.IO.File.WriteAllBytes(filePath, imageDataByteArray);
                return true;
            }
            catch (IOException ex)
            {
                NotifyError(ex.Message);
                return false;
            }
        }

        private bool RemoveArquivo(string imgNome)
        {
            if (string.IsNullOrEmpty(imgNome)) return false;

            string filePath = BuildFilePath(imgNome);

            if (!System.IO.File.Exists(filePath)) return false;

            try
            {
                System.IO.File.Delete(filePath);
                return true;
            }
            catch (Exception ex)
            {
                NotifyError(ex.Message);
                return false;
            }
        }

        private static string BuildFilePath(string imgNome)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/profiles", imgNome + ".jpg");
        }

        private async Task<IEnumerable<PatientViewModel>> GetPatientAddressByDoctorAsync(Guid id)
        {
            return _mapper.Map<IEnumerable<PatientViewModel>>(await _patientRepository.GetAllPatientsByDoctor(id));
        }

        private async Task<PatientViewModel> GetPatientAddressAsync(Guid id)
        {
            return _mapper.Map<PatientViewModel>(await _patientRepository.GetById(id));
        }
    }
}


