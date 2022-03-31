using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ucilica
{
    public partial class Register : Form
    {

        public Register()
        {
            this.MinimumSize = new System.Drawing.Size(440, 220);
            InitializeComponent();
            comboBox1.Items.Add("5");
            comboBox1.Items.Add("6");
            comboBox1.Items.Add("7");
            comboBox1.Items.Add("8");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")  // ako nije unesen username
            {
                MessageBox.Show("Unesite username!");
                return;
            }

            else if (textBox2.Text == "")  // ako nije unesena lozinka
            {

                MessageBox.Show("Unesite lozinku!");
                return;
            }

            else if (textBox2.Text.Length < 4)  // lozinka mora biti duljine barem 4
            {
                MessageBox.Show("Lozinka mora biti duljine barem 4!");
                return;
            }

            else if (!textBox2.Text.Any(char.IsDigit))  // ako neki od znakova nije znamenka
            {
                MessageBox.Show("lozinka mora sadržavati samo brojeve!");
                return;
            }
            dataBase db = new dataBase();
            bool user = db.chackIfUserExists(textBox1.Text);
            if (!user)
            {
                MessageBox.Show("Taj username već postoji!");
                return;
            }

            bool pass = db.checkIfPassExists(textBox2.Text);
            if(!pass)
            {
                MessageBox.Show("Ta lozinka je zauzeta!");
                return;
            }
            int year = 0;
            if(comboBox1.Text != "") year = int.Parse(comboBox1.Text);
            bool register  = db.register(textBox1.Text, textBox2.Text, year);
            if (register)
            {
                MessageBox.Show("Uspješno dodano!");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
