using AizenBankV1.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AizenBankV1.Domain.Entities.User
{
    public class UDbTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "UserName")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "UserName cannot be longer than 30 characters")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Password cannot be shorter than 8 characters")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [StringLength(30)]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime LastLogin { get; set; }

        [StringLength(30)]
        public string LasIp { get; set; }

        public URole Level { get; set; }

    }
}
