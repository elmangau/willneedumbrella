using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mangau.WillNeedUmbrella.Entities
{
    [Table("secgroupuser")]
    public class GroupUser
    {
        public long GroupId { get; set; }

        public Group Group { get; set; }

        public long UserId { get; set; }

        public User User { get; set; }
    }
}
