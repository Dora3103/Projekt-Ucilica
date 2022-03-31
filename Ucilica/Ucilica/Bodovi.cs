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
    public partial class Bodovi : Form
    {
        dataBase db = new dataBase();
        public Bodovi()
        {
            InitializeComponent();
            razredComboBox.Items.Add("5");
            razredComboBox.Items.Add("6");
            razredComboBox.Items.Add("7");
            razredComboBox.Items.Add("8");

            predmetComboBox.Items.Add("hrvatski");
            predmetComboBox.Items.Add("matematika");
            predmetComboBox.Items.Add("engleski");
            predmetComboBox.Items.Add("geografija");
            predmetComboBox.Items.Add("povijest");
        }

        private void razredComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(predmetComboBox.Text != "")
                changeTable();
        }

        private void predmetComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (razredComboBox.Text != "")
                changeTable();
        }

        void changeTable()
        {
            for(int i = tableLayoutPanel1.Controls.Count-1; i > 2; --i)
            {
                tableLayoutPanel1.Controls.RemoveAt(i);
            }
            List<Tuple<string, int, string>> bodovi = db.getScoresBySubjectAndYear(predmetComboBox.Text, int.Parse(razredComboBox.Text));
            foreach(var rez in bodovi)
            {
                Label user = new Label();
                user.Dock = DockStyle.Fill;
                user.Text = rez.Item1;
                user.TextAlign = ContentAlignment.MiddleCenter;
                user.Font = label1.Font;//new Font(user.Font.Name, 9);
                Label score = new Label();
                score.Dock = DockStyle.Fill;
                score.Text = rez.Item2.ToString();
                score.TextAlign = ContentAlignment.MiddleCenter;
                score.Font = label1.Font;//new Font(user.Font.Name, 9);
                Label time = new Label();
                time.Dock = DockStyle.Fill;
                time.Text = rez.Item3;
                time.TextAlign = ContentAlignment.MiddleCenter;
                time.Font = label1.Font;//new Font(user.Font.Name, 9);
                tableLayoutPanel1.Controls.AddRange(new Control[] { user, score, time });
            }
        }
    }
}
