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

            CreateLabel();

            context.Adatoks.Load();

            adatokBindingSource.DataSource = context.Adatoks.Local;

            label1.Text = "\uE721";

            button1.Text = "\uE792";

            button2.Text = "\uE74D";

            listBox1.DisplayMember = "Futar_ID";

            

        }

        private void FillDataSource()
        {
            listBox1.DataSource = (from i in context.Adatoks
                                   where i.Futar_ID.ToString().Contains(textBox1.Text)
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

            listBox2.DisplayMember = "Csomag_ID";

            listBox2.DataSource = futarok.ToList();

        }

        public void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var Csomag_ID = ((Adatok)listBox2.SelectedItem).Csomag_ID;
            var csomagok = from y in context.Adatoks
                           where y.Csomag_ID == Csomag_ID
                           select new
                           {
                               Név=y.UgyfelNev,
                               Cím=y.Utca+" "+y.Hazszam+".",
                               Emelet=y.Emelet,
                               Csengő=y.Csengo,
                               Összeg=y.Osszeg,
                               Fizetés=y.FizMod,
                               Kifizetve=y.Fizetve,
                           };
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
                    sw.Write(p.Csomag_ID.ToString());
                    sw.Write(";");
                    sw.Write(p.Futar_ID.ToString());
                    sw.Write(";");
                    sw.Write(p.Ugyfel_ID.ToString());
                    sw.Write(";");
                    sw.Write(p.UgyfelNev);
                    sw.Write(";");
                    sw.Write(p.Utca);
                    sw.Write(";");
                    sw.Write(p.Hazszam.ToString());
                    sw.Write(";");
                    sw.Write(p.Emelet.ToString());
                    sw.Write(";");
                    sw.Write(p.Csengo.ToString());
                    sw.Write(";");
                    sw.Write(p.Osszeg.ToString());
                    sw.Write(";");
                    sw.Write(p.FizMod);
                    sw.Write(";");
                    sw.Write(p.Fizetve.ToString());
                    sw.WriteLine();
                }
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string tor = ((Adatok)listBox2.SelectedItem).Csomag_ID.ToString();

            List<Adatok> adatok;

            adatok = (from v in context.Adatoks
                      where v.Csomag_ID.ToString() == tor
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

        private void CreateLabel()
        {
            int lineWidth = 5;

            for (int row = 0; row < 2; row++)
            {
                for (int col = 0; col < 38; col++)
                {
                    Szines sz = new Szines();
                    sz.Left = col * sz.Width + lineWidth;
                    sz.Top =20+row * sz.Height + lineWidth;
                    Controls.Add(sz);
                }
            }
        }


        private void button1_MouseEnter(object sender, EventArgs e)
        {
            button1.Text = "Mentés fájlba";
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.Text = "\uE792";
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            button2.Text = "Törlés";
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.Text = "\uE74D";
        }
    }
}
