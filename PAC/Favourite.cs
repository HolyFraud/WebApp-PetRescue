using System;
using System.Web;

namespace PAC
{
    public class Favourite
    {

        public Favourite(string memid, string anid, string stat)
        {
            MemberListID = memid;
            AnimalListID = anid;
            RecordStatus = stat;
        }

        public Favourite() { }
        public string MemberListID { get; set; }
        public string AnimalListID { get; set; }
        public string RecordStatus { get; set; }

    }
}
