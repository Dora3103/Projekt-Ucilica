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
    public partial class Kraj : Form
    {
        public Kraj(int b, string time, string predmet, int razred, string ime)
        {
            InitializeComponent();
            var split = time.Split(' ');
            label4.Text = split[1];
            label2.Text = b + "/90";
            dataBase db = new dataBase();
            db.insertNewScore(predmet, razred, ime, b, split[1]);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
