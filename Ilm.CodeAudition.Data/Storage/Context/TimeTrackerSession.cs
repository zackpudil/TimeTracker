using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ilm.CodeAudition.Data.Storage.EntityFrameworkCodeFirst;

namespace Ilm.CodeAudition.Data.Storage.Context
{
    public interface ITimeTrackerSession : ISession { }

    public class TimeTrackerSession : EFCodeFirstSession, ITimeTrackerSession
    {
        public TimeTrackerSession() : base(new TimeTrackerContext()) { }
    }
}
