using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NVB5UL_project
{
    class Szines:Label
    {
        public Szines()
        {
            Height = 30;
            Width = Height;
            BackColor = Color.DarkBlue;
            MouseEnter += Szines_MouseEnter;
            MouseLeave += Szines_MouseLeave;
        }

        private void Szines_MouseLeave(object sender, EventArgs e)
        {
            BackColor = Color.DarkBlue;
        }

        private void Szines_MouseEnter(object sender, EventArgs e)
        {
            BackColor = Color.Blue;
        }
    }
}
