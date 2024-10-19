using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCRS.Business.Models
{
    public abstract class Entity
    {
        protected Entity() 
        {
            Id = Guid.NewGuid(); 
        }

        [Key]
        public Guid Id { get; set; }
    }
}
