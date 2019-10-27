using System.ComponentModel.DataAnnotations.Schema;

namespace Mangau.WillNeedUmbrella.Entities
{
    [Table("secgrouppermission")]
    public class GroupPermission
    {
        public long GroupId { get; set; }

        public Group Group { get; set; }

        public long PermissionId { get; set; }

        public Permission Permission { get; set; }
    }
}
