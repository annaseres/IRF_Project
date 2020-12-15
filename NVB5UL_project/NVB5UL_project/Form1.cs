using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
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

            FillDataSource();

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

        public void textBox1_TextChanged(object sender, EventArgs e)
        {
            FillDataSource();
        }

        public void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var Futar_ID = ((Adatok)listBox1.SelectedItem).Futar_ID;

            var futarok = from x in context.Adatoks where x.Futar_ID == Futar_ID select x;

            listBox2.DisplayMember = "Futar_ID";

            listBox2.DataSource = futarok.ToList();

        }

        public void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var Csomag_ID = ((Adatok)listBox2.SelectedItem).Csomag_ID;
            var csomagok = from y in context.Adatoks
                           where y.Csomag_ID == Csomag_ID
                           select y;
            dataGridView1.DataSource = csomagok.ToList();
            

        }

        public void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "Comma Seperated Values (*.csv)|*.csv";
            sfd.DefaultExt = "csv";
            sfd.AddExtension = true;

            if (sfd.ShowDialog() != DialogResult.OK) return;

            List<Adatok> kiir;

            var ir = from l in context.Adatoks
                     where l.Futar_ID.ToString().Contains(listBox1.SelectedItem.ToString())
                     select l;

            kiir = ir.ToList();

            using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
            {
                foreach (var p in kiir)
                {
                    sw.Write(p.Csomag_ID);
                    sw.Write(";");
                    sw.Write(p.UgyfelNev);
                    sw.Write(";");
                    sw.Write(p.Utca);
                    sw.Write(";");
                    sw.Write(p.Hazszam);
                    sw.Write(";");
                    sw.Write(p.Emelet);
                    sw.Write(";");
                    sw.Write(p.Csengo);
                    sw.Write(";");
                    sw.Write(p.FizMod);
                    sw.Write(";");
                    sw.Write(p.Fizetve);
                    sw.WriteLine();
                }
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var tor = ((Adatok)listBox2.SelectedItem).Csomag_ID;

            List<Adatok> adatok;

            adatok = (from v in context.Adatoks
                      where v.Csomag_ID == tor
                      select v).ToList();

            foreach (var item in context.Adatoks)
            {
                if (adatok.Contains(item))
                {
                    context.Adatoks.Remove(item);
                }
            }

            context.SaveChanges();

        }
    }
}
