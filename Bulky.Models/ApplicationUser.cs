using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Bulky.Models.Models;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Bulky.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        public string? StateAddress { get; set; }
        public string? City {  get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set;}

		[ForeignKey("CategoryId")]
        [ValidateNever]
        public Company Company { get; set; }

        
        public int? CompanyId {  get; set; }
    }
}
