using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LMS_SASS.Models
{

    [Table("Meeting")]
    [PrimaryKey(nameof(Id))]
    public class MeetingModel
    {
      [Key]
      [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
      public required int Id { get; set; }

      [DataType(DataType.Text)]
      public required string url { get; set; }

    }
}