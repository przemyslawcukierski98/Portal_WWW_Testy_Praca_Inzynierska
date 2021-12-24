using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTesty_Library
{
    public class ValidationObject
    {
        public ValidationObject(bool studentFilterIsNullOrEmpty, bool titleFilterIsNullOrEmpty, 
            bool groupFilterIsNullOrEmpty, bool studentFilterIsFilled, 
            bool titleFilterIsFilled, bool groupFilterIsFilled)
        {
            StudentFilterIsNullOrEmpty = studentFilterIsNullOrEmpty;
            TitleFilterIsNullOrEmpty = titleFilterIsNullOrEmpty;
            GroupFilterIsNullOrEmpty = groupFilterIsNullOrEmpty;
            StudentFilterIsFilled = studentFilterIsFilled;
            TitleFilterIsFilled = titleFilterIsFilled;
            GroupFilterIsFilled = groupFilterIsFilled;
        }

        public bool StudentFilterIsNullOrEmpty { get; set; }
        public bool TitleFilterIsNullOrEmpty { get; set; }
        public bool GroupFilterIsNullOrEmpty { get; set; }
        public bool StudentFilterIsFilled { get; set; }
        public bool TitleFilterIsFilled { get; set; }
        public bool GroupFilterIsFilled { get; set; }

    }
}
