using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DwbiETL.Models
{
    [Table("tbVehicle")]
    public class Vehicle
    {
        [Key]
        public Guid? id { get; set; }
        public string? vin { get; set; }
        public string? city { get; set; }
        public string? make { get; set; }
        public string? model { get; set; }
        public int? model_year { get; set; }

        [ConcurrencyCheck]
        public string? After2020 { get; set; }
    }
}
