using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EventManagement.Models
{
    public class Feedback
    {
        [Key]
        public int FeedbackID { get; set; }

        [Required(ErrorMessage = "Feedback Required")]
        public string Experience { get; set; }

        [Required(ErrorMessage = "Comment Required")]
        public string Comment { get; set; }

        public string FeedFilename { get; set; }
        public string FeedFilePath { get; set; }
        public string Profession { get; set; }
        public string Createdby { get; set; }

    }

    [NotMapped]
    public class ViewFeedback
    {
        public int FeedbackID { get; set; }

        public string Experience { get; set; }

        public string Createdby { get; set; }

        public string Comment { get; set; }

        public string Profession { get; set; }
    }
}
