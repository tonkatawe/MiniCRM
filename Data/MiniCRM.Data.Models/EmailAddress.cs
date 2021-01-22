namespace MiniCRM.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using MiniCRM.Data.Common.Models;

    public class EmailAddress : BaseModel<int>
    {
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
