using System.Xml.Linq;

namespace CCRS.Business.Models
{
    public class IdentityDocument
    {
        public DocumentType Type { get; set; }
        public string Number { get; set; }
    }

    public enum DocumentType
    {

    }
}