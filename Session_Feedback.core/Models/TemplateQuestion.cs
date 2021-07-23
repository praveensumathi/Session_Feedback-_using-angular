using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class TemplateQuestion
    {
        public int Id { get; set; }
        public string Question { get; set; }

        public int TemplateId { get; set; }
    }
}
