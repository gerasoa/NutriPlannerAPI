using CCRS.Business.Interfaces;
using CCRS.Business.Models;
using CCRS.Business.Models.Validations;

namespace CCRS.Business.Services
{
    public class PatientService : BaseService, IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IAddressRepository _addressRepository;
        public PatientService(INotifier notificador,
                              IPatientRepository patientRepository,
                              IAddressRepository addressRepository) : base(notificador)
        {
            _patientRepository = patientRepository;
            _addressRepository = addressRepository;
        }

        public async Task<Patient> Add(Patient patient)
        {
            if (!ExecutarValidacao(new PatientValidation(), patient)) return null;

            //todo: completar os atributos da classe Patient e adicionar validacoes para patient ja existente
            if (_patientRepository.Get(f => f.IdentityDocumenty == patient.IdentityDocumenty ||
                                                           f.Id == patient.Id).Result.Any())
            {
                Notificar("A patient with this name already exists.");
                return null;
            }

            await _patientRepository.Add(patient);
            return patient;
        }

        public async Task<Address> AddAddress(Address address)
        {
            //if (!ExecutarValidacao(new PatientValidation(), patient)) return null;

            //todo: completar os atributos da classe Patient e adicionar validacoes para patient ja existente
            //if (_patientRepository.Get(f => f.IdentityDocumenty == patient.IdentityDocumenty ||
            //                                               f.Id == patient.Id).Result.Any())
            //{
            //    Notificar("A patient with this name already exists.");
            //    return null;
            //}

            await _addressRepository.Add(address);
            return address;
        }

        public async Task<bool> Update(Patient patient)
        {
            if (!ExecutarValidacao(new PatientValidation(), patient)) return false;           

            if (_patientRepository.Get(f => f.IdentityDocumenty == patient.IdentityDocumenty && f.Id != patient.Id).Result.Any())
            {
                Notificar("A patient with this name already exists.");
                return false;
            }

            await _patientRepository.Update(patient);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var patient = await _patientRepository.GetPatientAddressById(id);

            if (patient == null) return false;

            if (patient.Address != null)
            {
                await _addressRepository.Remove(patient.Address.Id);
            }

            await _patientRepository.Remove(patient.Id);
            return true;
        }

        public async Task<bool> UpdateAddress(Address address)
        {
            if (!ExecutarValidacao(new AddressValidation(), address)) return false;

            await _addressRepository.Update(address);
            return true;
        }

        public void Dispose()
        {
            _patientRepository?.Dispose();
            _addressRepository?.Dispose();
        }        
    }
}
