using DataAccess.Infraestructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class User : EntityBase
    {

        [Required]
        [Column(TypeName = "VARCHAR(50)")]
        public string UserName { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(50)")]
        public string Name { get; set; }

        [Required, EmailAddress]
        [Column(TypeName = "VARCHAR(50)")]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(20)")]
        public string Password { get; set; }

        [Column(TypeName = "VARCHAR(30)")]
        public string Phone { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(15)")]
        public string Role { get; set; }

    }
}