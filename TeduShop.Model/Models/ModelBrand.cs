using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using TeduShop.Model.Abstract;

namespace TeduShop.Model.Models
{
    [Table("ModelBrands")]
    public class ModelBrand : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        [Required]
        [MaxLength(256)]
        public string Mode { set; get; }

        [MaxLength(500)]
        public string Description { set; get; }

        [Required]
        public int BrandID { set; get; }

        [ForeignKey("BrandID")]
        public virtual Brand Brand { set; get; }
    }
}