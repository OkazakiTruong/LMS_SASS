using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LMS_SASS.Models
{
    [Table("QuizQuestions")]
    [PrimaryKey(nameof(Id))]
    public class QuizQuestionModel
    {
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public required int Id { get; set; }  
      [ForeignKey(nameof(Quiz))]
      public required int QuizId { get; set; }
      public required QuizModel Quiz { get; set; }
      public required string Question { get; set; }
      public required string Answer1 { get; set; }
      public required string Answer2 { get; set; }
      public required string Answer3 { get; set; }
      public required string Answer4 { get; set; }

      [Range(1, 4, ErrorMessage = "Answer must be between 1 and 4")]
      public int Answer { get; set; }
        
    }
}