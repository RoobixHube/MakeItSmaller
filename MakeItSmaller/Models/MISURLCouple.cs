using System.ComponentModel.DataAnnotations;

namespace MakeItSmaller.Models
{
    public class MISURLCouple
    {
        [Display(Name = "Url")]
        [Required(ErrorMessage = "URL Is required to create a MakeItSmaller URL")]
        [DataType(DataType.Url)]
        public string URL { get; set; }

        [Display(Name = "New Url")]
        public string MISURL { get; set; }
    }
}