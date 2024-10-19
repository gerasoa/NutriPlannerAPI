using System;
using System.ComponentModel.DataAnnotations;

namespace CCRS.Api.ViewModels
{
    public class PatientViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public Guid DoctorId { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "The field {0} is required.")]
        public DateTime DoB { get; set; }
        public string IdentityDocumenty { get; set; }
        public string ImageUpload { get; set; }
        public string Image { get; set; }


        //public AddressViewModel Address { get; set; }
    }
}
