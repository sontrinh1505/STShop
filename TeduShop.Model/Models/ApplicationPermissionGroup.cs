using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeduShop.Model.Models
{
    [Table("ApplicationPermissionGroups")]
    public class ApplicationPermissionGroup
    {
        [Key]
        [Column(Order = 1)]
        public int GroupId { set; get; }

        [Key]
        [Column(Order = 2)]
        public int PermissionId { set; get; }

        [ForeignKey("GroupId")]
        public virtual ApplicationGroup ApplicationGroup { set; get; }

        [ForeignKey("PermissionId")]
        public virtual ApplicationPermission ApplicationPermission { set; get; }
    }
}
