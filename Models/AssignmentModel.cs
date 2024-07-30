using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LMS_SASS.Models
{

    [Table("Assignment")]
    [PrimaryKey(nameof(Id))]
    public class AssignmentModel
    {
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required int Id { get; set; }           // Primary key
        public required string Description { get; set; }
        public required double PassingGrade { get; set; }
    }
}