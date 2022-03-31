using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ucilica
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dodaj n = new Dodaj();  // korisnik se uspješno ulogirao - ulazi na glavnu "stranicu"
            n.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 f = new Form1();
            f.Show(); // ponovno otvori login formu kako bi se drugi radnik mogao ulogirati
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
