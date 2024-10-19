namespace CCRS.Business.Models
{
    public class AppointmentLocation : Entity
    {
        public string Logradouro { get; private set; } 
        public string Numero { get; private set; }     
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }     
        public string Cidade { get; private set; }     
        public string Estado { get; private set; }
        public string Cep { get; private set; }
        public string Pais { get; private set; }        
        public string Telefone { get; private set; }
        public double? Latitude { get; private set; }
        public double? Longitude { get; private set; }
        public string TipoDeLocal { get; private set; }
        public string Email { get; private set; }
        public string Site { get; private set; }


        public override string ToString()
        {
            return $"{Logradouro ?? "Não especificado"}, " +
                   $"{Numero ?? "N/A"}, " +
                   $"{Complemento ?? "Sem complemento"}, " +
                   $"{Bairro ?? "Não especificado"}, " +
                   $"{Cidade ?? "Não especificada"} - " +
                   $"{Estado ?? "N/A"}, " +
                   $"{Cep ?? "N/A"}, " +
                   $"{Pais ?? "Não especificado"}";
        }
    }
}