using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mangau.WillNeedUmbrella.Entities
{
    [Table("secpermission")]
    public class Permission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public long PermissionCategoryId { get; set; }
        public virtual PermissionCategory PermissionCategory { get; set; }

        public virtual IList<GroupPermission> GroupsPermissions { get; set; }
    }
}
