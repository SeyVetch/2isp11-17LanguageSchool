using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchool.ClassHelper
{
    class SortConditions
    {
        public static int sortByFIO(EF.Client c1, EF.Client c2)
        {
            string FIO1 = c1.Fio;
            string FIO2 = c2.Fio;
            return FIO1.CompareTo(FIO2);
        }
        public static int sortByLastVisit(EF.Client c1, EF.Client c2)
        {
            DateTime visit1 = c1.LastVisit == "" ? DateTime.MinValue : DateTime.Parse(c1.LastVisit);
            DateTime visit2 = c2.LastVisit == "" ? DateTime.MinValue : DateTime.Parse(c2.LastVisit);
            return visit2.CompareTo(visit1);
        }
        public static int sortByTotalVisits(EF.Client c1, EF.Client c2)
        {
            int? visits1 = c1.TotalVisits;
            int? visits2 = c2.TotalVisits;
            int v1 = visits1 == null ? 0 : (int)visits1;
            int v2 = visits2 == null ? 0 : (int)visits2;
            return v2.CompareTo(v1);
        }
        public static int sortByID(EF.Client c1, EF.Client c2)
        {
            int id1 = c1.IdClient;
            int id2 = c2.IdClient;
            return id1.CompareTo(id2);
        }
    }
}
