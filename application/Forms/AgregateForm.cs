using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arduin
{
    public partial class AgregateForm : Form
    {
        private Form1 form1;
        public AgregateForm(Form1 form)
        {
            form1 = form;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
            form1.OpenHeatMap();
        }

        private void button2_Click(object sender, EventArgs e)
        { 
            Close();
            form1.CreateHeatFromCurrent();
        }
    }
}
