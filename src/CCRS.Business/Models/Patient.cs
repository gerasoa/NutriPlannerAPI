using System.Text.Json.Serialization;

namespace CCRS.Business.Models
{
    public class Patient : Entity
    {
        public string Name { get;  set; }
        public string Gender { get;  set; }
        public DateTime DoB { get;  set; }
        public Address Address{ get;  set; }
        public string IdentityDocumenty { get; set; }
        public string Image { get; set; }
        public Guid DoctorId { get; set; }

        [JsonIgnore]
        public virtual Doctor Doctor { get; set; }



        //public DateOnly RegistrationDate { get; set; }
        //public IdentityDocument MyProperty { get; set; }
        //public Collection<Appointment> Consulta { get; set; }
    }
}
