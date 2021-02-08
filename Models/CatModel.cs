using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KattHem.Models
{
    public class CatModel
    {
        [Required(ErrorMessage = "You have to assign an ID.")]
        [Display(Name = "Cat ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "You have to enter a name.")]
        [MaxLength(30)]
        [Display(Name = "Cat name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The cat probably has a gender, it needs to be entered.")]
        [MaxLength(4)]
        [Display(Name = "Gender")]
        public string Sex { get; set; }

        public CatModel()
        {

        }

    }

}
