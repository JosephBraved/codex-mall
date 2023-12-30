using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BulkywebRazor_temp.Models
{
    public class Category
    {
            [Key]
            public int Id { get; set; }
            [Required]
            [DisplayName("Category Name")]
            [MaxLength(30)]
            public string Name { get; set; }
            [DisplayName("Display Order")]
            [Range(1, 100, ErrorMessage = "This is not allowed")]
            public int DisplayOrder { get; set; }
        
    }
}
