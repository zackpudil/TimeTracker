using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ilm.CodeAudition.Service.ViewModels
{
    public class TimesheetViewModel 
    {
        public int Id { get; set; }
        public ProjectViewModel Project { get; set; }

        public int Monday { get; set; }
        public int Tuesday { get; set; }
        public int Wednesday { get; set; }
        public int Thursday { get; set; }
        public int Friday { get; set; }

        public int Total { get; set; }
    }
}