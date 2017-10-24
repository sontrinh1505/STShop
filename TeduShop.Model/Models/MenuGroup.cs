using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeduShop.Model.Abstract;

namespace TeduShop.Model.Models
{
    [Table("MenuGroups")]
    public class MenuGroup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(50)]
        public string Name { set; get; }

        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public virtual MenuGroup MenuGroups { get; set; }  

        public virtual ICollection<Menu> Menus { set; get; }

        public virtual ICollection<MenuGroup> ChildrenGroupMenus { set; get; }
    }
}