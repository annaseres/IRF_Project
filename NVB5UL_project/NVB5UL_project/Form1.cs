using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NVB5UL_project
{
    public partial class Form1 : Form
    {
        Database1Entities1 context = new Database1Entities1();

        public Form1()
        {
            InitializeComponent();

            context.Adatoks.Load();

            adatokBindingSource.DataSource = context.Adatoks.Local;

            label1.Text = "\uE721";

            listBox1.DisplayMember = "Futar_ID";
        }

        private void FillDataSource()
        {
            listBox1.DataSource = (from i in context.Adatoks
                                   where i.Futar_ID.ToString().StartsWith(textBox1.Text)
                                   select i).ToList();
        }

        private void adatokBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            adatokBindingSource.EndEdit();

            try
            {
                context.SaveChanges();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

            adatokDataGridView.Refresh();
        }
    }
}
