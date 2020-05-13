using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mangau.WillNeedUmbrella.Entities
{
    [Table("secuser")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(128)]
        [JsonIgnore]
        public string Password { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public bool Recover { get; set; }

        [Required]
        [MaxLength(128)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(128)]
        public string LastName { get; set; }

        [MaxLength(128)]
        public string Email { get; set; }

        public virtual IList<GroupUser> GroupsUsers { get; set; }

        public virtual IList<SessionToken> SessionTokens { get; set; }

        public virtual IList<UserCity> UsersCities { get; set; }
    }
}
