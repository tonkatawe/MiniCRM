namespace MiniCRM.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using MiniCRM.Data.Common.Models;

    public class Industry : BaseDeletableModel<int>
    {
        public Industry()
        {
            this.Companies = new HashSet<Company>();
        }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
    }
}
