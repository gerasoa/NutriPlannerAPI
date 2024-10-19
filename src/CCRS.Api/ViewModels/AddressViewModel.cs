using System.ComponentModel.DataAnnotations;

namespace CCRS.Api.ViewModels
{
    public class AddressViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public Guid PatientId { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        public string Street { get; set; }

        public string HouseNumber { get; set; }

        public string AdditionalInfo { get; set; }

        public string District { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public string PrimaryPhone { get; set; }

        public string SecondaryPhone { get; set; }

        public string Email { get; set; }
    }
}
