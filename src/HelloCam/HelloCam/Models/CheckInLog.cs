using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloCam.Models
{
    public class CheckInLog
    {
        [AutoIncrement]
        public int Id { get; set; }

        public string GroupId { get; set; }

        public string PersonName { get; set; }

        public int Action { get; set; }

        public string TimeTag { get; set; }

    }
}
