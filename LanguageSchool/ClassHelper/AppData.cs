using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageSchool.ClassHelper
{
    class AppData
    {
        public static EF.LanguageSchoolEntities Context { get; } = new EF.LanguageSchoolEntities();
    }
}
