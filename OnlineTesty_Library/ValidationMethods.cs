using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTesty_Library
{
    public static class ValidationMethods
    {
        public static ValidationObject ValidationForExams(string titleFilter, string studentFilter, string groupFilter)
        {
            bool studentFilterIsNullOrEmpty = studentFilter == null || studentFilter.Equals("");
            bool titleFilterIsNullOrEmpty = titleFilter == null || titleFilter.Equals("");
            bool groupFilterIsNullOrEmpty = groupFilter == null || groupFilter.Equals("");

            bool studentFilterIsFilled = studentFilter != null && !studentFilter.Equals("");
            bool titleFilterIsFilled = titleFilter != null && !titleFilter.Equals("");
            bool groupFilterIsFilled = groupFilter != null && !groupFilter.Equals("");

            ValidationObject validationObject = new ValidationObject(studentFilterIsNullOrEmpty, 
                titleFilterIsNullOrEmpty, groupFilterIsNullOrEmpty, studentFilterIsFilled,
                titleFilterIsFilled, groupFilterIsFilled);

            return validationObject;
        }
    }
}
