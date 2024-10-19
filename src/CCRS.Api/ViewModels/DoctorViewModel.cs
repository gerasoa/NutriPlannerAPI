using System.ComponentModel.DataAnnotations;

namespace CCRS.Api.ViewModels
{
    public class DoctorViewModel
    {
        [Key]
        public Guid Id { get; set; }
             
        public string Name { get; set; }
        public string Specialty { get; set; }
        public bool OffersOnlineConsultations { get; set; }
    }
}
