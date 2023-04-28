using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace VehicleApi.Models
{
    [Table("Vehicles")]
    public class Vehicle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string VIN { get; set; }
        public int Year { get; set; }
        
        [StringLength(100)]
        public string Model { get; set; }
        public bool Pass { get; set; }

        [DataType(DataType.Date)]
        public DateTime InspectionDate { get; set; }

        [StringLength(100)]
        public string InspectorName { get; set; }

        [StringLength(100)]
        public string InspectionLocation { get; set; }

        [StringLength(32768)]
        public string Notes { get; set; }

        public int MakerId { get; set; }
        public VehicleMaker Maker { get; set; }
    }
}
