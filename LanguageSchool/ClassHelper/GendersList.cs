using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchool.ClassHelper
{
    class GendersList
    {
        public static List<string> Genders()
        {
            List<EF.Gender> genders1 = AppData.Context.Gender.ToList();
            List<string> res = new List<string> { "Нет огран." };
            foreach (EF.Gender g in genders1)
            {
                res.Add(g.GenderName);
            }
            return res;
        }
    }
}
