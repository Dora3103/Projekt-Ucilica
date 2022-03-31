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
    public partial class Form1 : Form
    {

        public static string name = "";   // u njoj ćemo pamtiti username osobe koja će se ulogirati
        public static int razred = 0;  // pamtimo razred
        public Form1()
        {
            this.MinimumSize = new System.Drawing.Size(400, 230);
            InitializeComponent();
            textBox2.UseSystemPasswordChar = true;
        }

        private void Login_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")   // ako korisnik nije unio username
            {
                MessageBox.Show("Unesite username!");
                return;
            }

            else if (textBox2.Text == "")  // ako korisnik nije unio lozinku
            {
                MessageBox.Show("Unesite lozinku!");
                return;
            }

            else if (!textBox2.Text.Any(char.IsDigit)) // lozinka smije sadržavati samo brojeve
            {
                MessageBox.Show("lozinka mora sadržavati samo brojeve!");
                return;
            }

            dataBase db = new dataBase();
            int login = db.login(textBox1.Text, textBox2.Text);
            name = textBox1.Text;
            if (login == 0) // krivi podaci
            {
                MessageBox.Show("Unesite ispravne podatke!");
            }
            else if(login == 1) //uspješan login korisnika
            {
                razred = db.getYearByUser(name);
                this.Hide();
                Razred m = new Razred();  // korisnik se uspješno ulogirao - ulazi na glavnu "stranicu"
                m.Show();
            }
            else if(login == -1) //login admina
            {
                this.Hide();
                Admin a = new Admin();  // admin se uspješno ulogirao - ulazi na admin stranicu
                a.Show();
            }
            else
            {
                MessageBox.Show("Error:");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();   // ako korisnik odluči izaći iz aplikacije
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Register n = new Register();  //idi na registraciju
            n.Show();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals(Convert.ToChar(13)))
            {
                Login_Click(sender, e);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals(Convert.ToChar(13)))
            {
                Login_Click(sender, e);
            }
        }
    }
}
