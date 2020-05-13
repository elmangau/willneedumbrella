using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mangau.WillNeedUmbrella.Entities
{
    public class CityCoords
    {
        [Required]
        public float Lon { get; set; }

        [Required]
        public float Lat { get; set; }
    }

    [Table("wnucity")]
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [MaxLength(2)]
        public string Country { get; set; }

        [Required]
        [MaxLength(64)]
        public string State { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        [Required]
        public CityCoords Coord { get; set; }
    }
}
