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
    public partial class Dodaj : Form
    {
        public List<string> predmeti = new List<string>();
        public Dodaj()
        {
            InitializeComponent();
            predmetComboBox.Items.Add("hrvatski");
            predmetComboBox.Items.Add("matematika");
            predmetComboBox.Items.Add("engleski");
            predmetComboBox.Items.Add("geografija");
            predmetComboBox.Items.Add("povijest");

            razredComboBox.Items.Add("5");
            razredComboBox.Items.Add("6");
            razredComboBox.Items.Add("7");
            razredComboBox.Items.Add("8");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataBase db = new dataBase();
            pitanjeKlasa pitanje = new pitanjeKlasa()
            {
                pitanje = pitanjeTextBox.Text,
                razred = int.Parse(razredComboBox.Text),
                predmet = predmetComboBox.Text,
                točan = odgtTtextBox.Text,
                odgovori = new List<string>() { odg1TextBox.Text, odg2TtextBox.Text, odg3TextBox.Text, odgtTtextBox.Text}
            };
            db.addQuestion(pitanje);
            MessageBox.Show("Uspješno dodano!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
