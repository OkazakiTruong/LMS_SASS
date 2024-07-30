using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LMS_SASS.Models
{
    [Table("Quiz")]
    [PrimaryKey(nameof(Id))]
    public class QuizModel
    {
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public required int Id { get; set; }

      [Range(0.01, double.MaxValue, ErrorMessage = "Passing grade must be greater than 0")]
      public required double PassingGrade { get; set; }

    }
}