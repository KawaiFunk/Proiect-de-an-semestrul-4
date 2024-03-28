using AizenBankV1.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AizenBankV1.Domain.Entities.User
{
    public class UDataBaseTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Username")]
        //restrictions
        public string Username {  get; set; }

        [Required]
        [Display(Name = "Password")]
        //restrictions
        public string Password { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [StringLength(50)]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime LastLogin { get; set; }
        
        public URoles Level { get; set; }
    }
}
