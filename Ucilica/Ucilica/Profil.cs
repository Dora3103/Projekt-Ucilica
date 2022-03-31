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
    public partial class Profil : Form
    {
        string _name;
        int _razred;
        public Profil(string name, int razred)
        {
            InitializeComponent();
            _name = name;
            _razred = razred;
            label1.Text = name;
            if (razred == 0) label2.Text = "Nije odabran razred";
            else label2.Text = razred.ToString() + ". razred";
            dataBase db = new dataBase();
            List<Tuple<int, string, int, string>> rez = db.getResultsByName(name);
            RowStyle temp = resultTable.RowStyles[0];
            foreach(var r in rez)
            {
                resultTable.RowCount++;
                resultTable.RowStyles.Add(new RowStyle(temp.SizeType, temp.Height));
                Label raz = new Label();
                raz.Text = r.Item1.ToString();
                raz.Font = new Font(label7.Font.Name, 10);
                raz.Dock = DockStyle.Fill;
                raz.TextAlign = ContentAlignment.MiddleCenter;
                Label predmet = new Label();
                predmet.Text = r.Item2.ToString();
                predmet.Font = new Font(label7.Font.Name, 10);
                predmet.Dock = DockStyle.Fill;
                predmet.TextAlign = ContentAlignment.MiddleCenter;
                Label bodovi = new Label();
                bodovi.Text = r.Item3.ToString();
                bodovi.Font = new Font(label7.Font.Name, 10);
                bodovi.Dock = DockStyle.Fill;
                bodovi.TextAlign = ContentAlignment.MiddleCenter;
                Label vrijeme = new Label();
                vrijeme.Text = r.Item4.ToString();
                vrijeme.Font = new Font(label7.Font.Name, 10);
                vrijeme.Dock = DockStyle.Fill;
                vrijeme.TextAlign = ContentAlignment.MiddleCenter;
                resultTable.Controls.Add(predmet);
                resultTable.Controls.Add(raz);
                resultTable.Controls.Add(bodovi);
                resultTable.Controls.Add(vrijeme);
            }
           
        }

        private void nameButton_Click(object sender, EventArgs e)
        {
            Form f = new Form();
            f.Width = 400;
            f.Height = 200;
            f.BackgroundImage = Properties.Resources.back1;
            Label nameLabel = new Label();
            nameLabel.AutoSize = true;
            nameLabel.Text = "Novo ime";
            nameLabel.Font = new Font(label1.Font.Name, 12);
            nameLabel.BackColor = Color.Transparent;
            nameLabel.Location = new Point(f.Width/2-nameLabel.Width-5, f.Height/2-50);
            f.Controls.Add(nameLabel);
            TextBox nameTextBox = new TextBox();
            nameTextBox.Font = new Font(label1.Font.Name, 10);
            nameTextBox.Location = new Point(f.Width/2, f.Height/2-50);
            f.Controls.Add(nameTextBox);
            Button changeButton = new Button();
            changeButton.AutoSize = true;
            changeButton.Text = "Promijeni!";
            changeButton.Font = new Font(label1.Font.Name, 12);
            changeButton.Location = new Point(f.Width/2-changeButton.Width/2, f.Height/2);
            changeButton.Click += (_sender, _e) =>
            {
                if (nameTextBox.Text != "")
                {
                    dataBase db = new dataBase();
                    db.changeUserName(_name, nameTextBox.Text);
                    _name = nameTextBox.Text;
                    label1.Text = _name;
                    Form1.name = _name;
                }
                f.Hide();
            };
            f.Controls.Add(changeButton);
            f.Show();
            Refresh();
        }

        private void passButton_Click(object sender, EventArgs e)
        {
            Form f = new Form();
            f.Width = 400;
            f.Height = 200;
            f.BackgroundImage = Properties.Resources.back1;
            Label passLabel = new Label();
            passLabel.AutoSize = true;
            passLabel.Text = "Nova lozinka";
            passLabel.Font = new Font(label1.Font.Name, 12);
            passLabel.BackColor = Color.Transparent;
            passLabel.Location = new Point(f.Width / 2 - passLabel.Width - 5, f.Height / 2 - 50);
            f.Controls.Add(passLabel);
            TextBox passTextBox = new TextBox();
            passTextBox.Font = new Font(label1.Font.Name, 10);
            passTextBox.Location = new Point(f.Width / 2, f.Height / 2 - 50);
            f.Controls.Add(passTextBox);
            Button changeButton = new Button();
            changeButton.AutoSize = true;
            changeButton.Text = "Promijeni!";
            changeButton.Font = new Font(label1.Font.Name, 12);
            changeButton.Location = new Point(f.Width / 2 - changeButton.Width / 2, f.Height / 2);
            changeButton.Click += (_sender, _e) =>
            {
                if (passTextBox.Text != "")
                {
                    dataBase db = new dataBase();
                    db.changePass(_name, int.Parse(passTextBox.Text));
                }
                f.Hide();
            };
            f.Controls.Add(changeButton);
            f.Show();
            Refresh();
        }

        private void yearButton_Click(object sender, EventArgs e)
        {
            Form f = new Form();
            f.Width = 400;
            f.Height = 200;
            f.BackgroundImage = Properties.Resources.back1;
            Label yearLabel = new Label();
            yearLabel.AutoSize = true;
            yearLabel.Text = "Promijeni razred";
            yearLabel.Font = new Font(label1.Font.Name, 12);
            yearLabel.BackColor = Color.Transparent;
            yearLabel.Location = new Point(f.Width / 2 - yearLabel.Width - 25, f.Height / 2 - 50);
            f.Controls.Add(yearLabel);
            ComboBox yearComboBox = new ComboBox();
            yearComboBox.Items.Add("5");
            yearComboBox.Items.Add("6");
            yearComboBox.Items.Add("7");
            yearComboBox.Items.Add("8");
            yearComboBox.Font = new Font(label1.Font.Name, 10);
            yearComboBox.Location = new Point(f.Width / 2, f.Height / 2 - 50);
            yearComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            f.Controls.Add(yearComboBox);
            Button changeButton = new Button();
            changeButton.AutoSize = true;
            changeButton.Text = "Promijeni!";
            changeButton.Font = new Font(label1.Font.Name, 12);
            changeButton.Location = new Point(f.Width / 2 - changeButton.Width / 2, f.Height / 2);
            changeButton.Click += (_sender, _e) =>
            {
                if (yearComboBox.Text != "")
                {
                    dataBase db = new dataBase();
                    db.changeYear(_name, int.Parse(yearComboBox.Text));
                    _razred = int.Parse(yearComboBox.Text);
                    label2.Text = _razred + ". razred";
                    Form1.razred = _razred;
                }
                f.Hide();
            };
            f.Controls.Add(changeButton);
            f.Show();
        }

    }
}
