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
    public partial class Razred : Form
    {
        public static int ukljucen = 1;
        public Razred()
        {
            InitializeComponent();
            predmetiComboBox.Items.Add("hrvatski");
            predmetiComboBox.Items.Add("matematika");
            predmetiComboBox.Items.Add("engleski");
            predmetiComboBox.Items.Add("geografija");
            predmetiComboBox.Items.Add("povijest");


            razredComboBox.Items.Add("5");
            razredComboBox.Items.Add("6");
            razredComboBox.Items.Add("7");
            razredComboBox.Items.Add("8");
            if(Form1.razred != 0) razredComboBox.SelectedIndex = Form1.razred - razredComboBox.Items.Count-1;


            label4.Text = Form1.name + ", dobrodošao!";
            //button5.BackgroundImage = 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string predmet = predmetiComboBox.Text;
            if(predmet == "" || razredComboBox.Text == "")
            {
                MessageBox.Show("Odaberi i predmet i razred!!");
            }
            else
            {
                Pitanje pitanje = new Pitanje(int.Parse(razredComboBox.Text), predmet, Form1.name);
                pitanje.Show();
            }
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 f = new Form1();
            f.Show(); // ponovno otvori login formu kako bi se drugi radnik mogao ulogirati
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Bodovi bodovi = new Bodovi();
            bodovi.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Profil profil = new Profil(Form1.name, Form1.razred);
            profil.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (ukljucen == 1)
            {
                ukljucen = 0;
                button5.BackColor = Color.Blue;
            }
            else
            {
                ukljucen = 1;
                button5.BackColor = Color.Transparent;
            }
        }
    }
}
