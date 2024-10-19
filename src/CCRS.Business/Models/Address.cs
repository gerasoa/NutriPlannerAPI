using System.Text.Json.Serialization;

namespace CCRS.Business.Models
{
    public class Address : Entity
    {
        /// <summary>
        /// The street name or number of the address.
        /// </summary>
        public string Street { get; private set ; }

        /// <summary>
        /// The house or flat number associated with the address.
        /// </summary>
        public string HouseNumber { get; private set ; }

        /// <summary>
        /// Additional information about the address (e.g., flat number, building name).
        /// </summary>
        public string AdditionalInfo { get; private set ; }

        /// <summary>
        /// The neighbourhood or district of the address.
        /// </summary>
        public string District { get; private set ; }

        /// <summary>
        /// The city where the address is located.
        /// </summary>
        public string City { get; private set ; }

        /// <summary>
        /// The region or state of the address.
        /// </summary>
        public string State { get; private set ; }

        /// <summary>
        /// The postal code associated with the address.
        /// </summary>
        public string PostalCode { get; private set ; }

        /// <summary>
        /// The country where the address is located.
        /// </summary>
        public string Country { get; private set ; }

        /// <summary>
        /// The primary phone number associated with the address.
        /// </summary>
        public string PrimaryPhone { get; private set ; }

        /// <summary>
        /// The secondary phone number associated with the address.
        /// </summary>
        public string SecondaryPhone { get; private set ; }

        /// <summary>
        /// The email address associated with the address.
        /// </summary>
        public string Email { get; private set ; }



        public Guid PatientId { get; set; }
        [JsonIgnore]
        public virtual Patient Patient { get; set; }


        // Optional: Override ToString for easy debugging and logging
        public override string ToString()
        {
            return $"{Street ?? "Not specified"}, {HouseNumber ?? "N/A"}, " +
                   $"{AdditionalInfo ?? "No additional info"}, {District ?? "Not specified"}, " +
                   $"{City ?? "Not specified"}, {State ?? "N/A"}, {PostalCode ?? "N/A"}, " +
                   $"{Country ?? "Not specified"}";
        }
    }
}
