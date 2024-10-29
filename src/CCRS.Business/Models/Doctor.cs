using System.Text.Json.Serialization;

namespace CCRS.Business.Models
{
    public class Doctor : Entity
    {
        public string Name { get;  set; }
        public string Specialty { get;  set; }   
        public bool OffersOnlineConsultations { get;  set; }
        public IEnumerable<Patient> Patients { get; set; }



        [JsonIgnore]
        public virtual Appointment Appointment { get; set; }

        //public List<AppointmentLocation> AppointmentLocations { get; private set; }

        public Doctor()
        {
        }

        public Doctor(string name, string specialty, bool offersOnlineConsultations)
        {
            Id = Guid.NewGuid();
            Name = name;
            Specialty = specialty;
            OffersOnlineConsultations = offersOnlineConsultations;
            //AppointmentLocations = new List<AppointmentLocation>();
        }
        //public void AddAppointmentLocation(AppointmentLocation location)
        //{
        //    AppointmentLocations.Add(location);
        //}
    }
}
