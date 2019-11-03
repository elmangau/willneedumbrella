using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mangau.WillNeedUmbrella.Entities
{
    [Table("secsessiontoken")]
    public class SessionToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public long UserId { get; set; }

        public User User { get; set; }

        [Required]
        public DateTime LoggedAt { get; set; }

        [Required]
        public DateTime Expires { get; set; }
    }
}
