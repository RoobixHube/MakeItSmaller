using System.ComponentModel.DataAnnotations;

namespace MakeItSmaller.Models
{
    public class URL
    {
        [Display(Name = "Url")]
        [Required(ErrorMessage = "Url Is required to create a MakeItSmaller Url")]
        [DataType(DataType.Url)]
        public string URLstring { get; set; }
    }
}