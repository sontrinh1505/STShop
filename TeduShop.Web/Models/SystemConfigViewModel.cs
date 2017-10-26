using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeduShop.Model.Models
{
    public class SystemConfigViewModel
    {
        public int ID { set; get; }

        public string Code { set; get; }

        public string ValueString { set; get; }

        public int? ValueInt { set; get; }
    }
}