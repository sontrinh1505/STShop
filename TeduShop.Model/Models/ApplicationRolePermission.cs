using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeduShop.Model.Models
{
    [Table("ApplicationRolePermissions")]
    public class ApplicationRolePermission
    {
        [Key]
        [Column(Order = 1)]
        public int PermissonId { set; get; }

        [StringLength(128)]
        [Key]
        [Column(Order = 2)]
        public string RoleId { set; get; }

        [ForeignKey("PermissonId")]
        public virtual ApplicationPermission ApplicationPermission { set; get; }

        [ForeignKey("RoleId")]
        public virtual ApplicationRole ApplicationRole { set; get; }
    }
}
