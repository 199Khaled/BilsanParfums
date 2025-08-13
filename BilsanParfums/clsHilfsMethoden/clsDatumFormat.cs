using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsHilfsMethoden
{
    public class clsDatumFormat
    {
        public static string DateToShort(DateTime date)
        {
            return date.ToString("dd/MMM/yyyy");

        }

       
}
}
