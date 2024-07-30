using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LMS_SASS.Models
{

    [Table("Activities")]
    [PrimaryKey(nameof(Id))]
    public class ActivityModel
    {
    	public const string TYPE_QUIZ = "quiz";
      public const string  TYPE_ASSIGNMENT = "assignment";
      public const string   TYPE_MEETING = "meeting";
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public required int Id { get; set; } 

      [ForeignKey(nameof(Course))]
      public required int CourseId { get; set; } 
      public required CourseModel Course { get; set; }

      public required string Name { get; set; }

      public required string Type { get; set; }=TYPE_MEETING; // Assuming Type can be either "quiz", "assignment", or "meeting"

      public required int InstanceId { get; set; }

      [DataType(DataType.DateTime)]
      public required DateTime StartDate { get; set; }

      [DataType(DataType.DateTime)]
      public required DateTime EndDate { get; set; }

      [DataType(DataType.DateTime)]
      public required DateTime Created { get; set; }
    }
}