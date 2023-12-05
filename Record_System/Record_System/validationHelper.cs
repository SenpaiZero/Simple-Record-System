using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Record_System
{
    internal class validationHelper
    {
        public static bool isEmptyTB(TextBox tb, string message)
        {
            if (string.IsNullOrEmpty(tb.Text))
            {
                MessageBox.Show($"Please fill out the {message}.");
                return true;
            }
            return false;

        }
    }
}
