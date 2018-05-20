using System;

namespace PetApplication.Utility
{
    public class Audit
    {
        public Guid AuditID { get; set; }
        public string UserName { get; set; }
        public string IPAddress { get; set; }
        public string AreaAccessed { get; set; }
        public DateTime Timestamp { get; set; }

        public string Message { get; set; }
        //Default Constructor
        public Audit() { }
    }
}
