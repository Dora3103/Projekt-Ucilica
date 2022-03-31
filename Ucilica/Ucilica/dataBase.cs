using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ucilica
{
    internal class dataBase
    {

        public bool register(string user, string pass, int year)
        {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\login.accdb");
            try
            {
                con.Open();
                OleDbCommand comm = new OleDbCommand();
                comm.Connection = con;
                comm.CommandText = "insert into login ([Username], [Password], [Razred]) values('" + user + "'," + pass + "," + year + ")";
                comm.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
            
        }

        public int login(string user, string pass)
        {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\login.accdb");
            try
            {
                con.Open();
                OleDbDataAdapter sda = new OleDbDataAdapter("select count(*) from login where Username='" + user + "' and Password=" + pass, con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1")   // ako vrijedi znači da u bazi postoji točno jedna osoba koja zadovoljava tražene uvjete (naveden username i lozinku)
                {

                    if (user == "admin")
                    {
                        con.Close();  // obavezno zatvaramo vezu s bazom
                        return -1;
                    }
                    else
                    {
                        con.Close();  // obavezno zatvaramo vezu s bazom
                        return 1;
                    }
                }

                else
                {
                    con.Close();
                    return 0;
                }
            }
            

            catch (Exception ex)  // ako povezivanje s bazom nije uspjelo - javi error
            {
                Console.WriteLine(ex.Message);
                return -2;

            }
        }

        public int getYearByUser(string user)
        {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\login.accdb");
            int ret;
            try
            {
                con.Open();
                OleDbDataAdapter sd = new OleDbDataAdapter("select Razred from login where Username='" + user+"'", con);
                DataTable dta = new DataTable();
                sd.Fill(dta);
                ret = int.Parse(dta.Rows[0][0].ToString());
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
            return ret;
        }

        public bool checkIfPassExists(string pass)
        {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\login.accdb");
            try
            {
                con.Open();
                OleDbDataAdapter sd = new OleDbDataAdapter("select count(*) from login where Password=" + pass, con);
                DataTable dta = new DataTable();
                sd.Fill(dta);
                if (dta.Rows[0][0].ToString() == "1") //u bazi već postoji ta lozinka
                {
                    return false;

                }
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
           
            return true;
        }

        public bool chackIfUserExists(string user)
        {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\login.accdb");
            try
            {
                con.Open();

                OleDbDataAdapter sda = new OleDbDataAdapter("select count(*) from login where Username='" + user + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows[0][0].ToString() == "1") //u bazi već postoji taj username
                    return false;
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public List<pitanjeKlasa> getQuestionsByYearAndSubject(int year, string subject)
        {
            List<pitanjeKlasa> ret = new List<pitanjeKlasa>();
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\login.accdb");
            try
            {
                con.Open();

                OleDbDataAdapter sda = new OleDbDataAdapter("select * from " + subject +" where razred = " + year.ToString(), con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    ret.Add(new pitanjeKlasa()
                    {
                        id = int.Parse(row[0].ToString()),
                        pitanje = row[1].ToString(),
                        odgovori = new List<string>() { row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString() },
                        točan = row[5].ToString(),
                        razred = year,
                        slika = row[7].ToString(),
                        predmet = subject

                    }); ;
                }
                con.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return ret;
        }

        public void addQuestion(pitanjeKlasa q)
        {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\login.accdb");
            try
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("insert into " + q.predmet + " ([pitanja],[odg_1],[odg_2],[odg_3],[odg_t],[razred],[slika]) values('" +
                   q.pitanje + "','" + q.odgovori[0] + "','" + q.odgovori[1] + "','" + q.odgovori[2] + "','" + q.točan + "'," + q.razred + ",'" + q.slika + "')", con);
                int inserted = cmd.ExecuteNonQuery();
                Console.WriteLine("ubaceno redaka " + inserted);
                con.Close();
               // return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //return false;
            }
        }

        public List<Tuple<string, int, string>> getScoresBySubjectAndYear(string subject, int year)
        {
            List<Tuple<string,int, string>> ret = new List<Tuple<string, int, string>>();

            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\login.accdb");
            try
            {
                con.Open();

                OleDbDataAdapter sda = new OleDbDataAdapter("select top 10 korisnik, bodovi, vrijeme from bodovi where predmet='" + subject +"' and razred=" + year.ToString() + " order by bodovi desc ,vrijeme desc", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    ret.Add(new Tuple<string, int, string>(row[0].ToString(), int.Parse(row[1].ToString()), row[2].ToString()));
                }
                con.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return ret;
        }

        public bool insertNewScore(string subject, int year, string user, int score, string time)
        {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\login.accdb");
            try
            {
                con.Open();
                OleDbCommand cmd = new OleDbCommand("insert into bodovi ([korisnik],[razred],[predmet],[bodovi],[vrijeme]) values('" +
                   user + "'," + year + ",'" + subject + "'," + score + ",'" + time + "')", con);
                int inserted = cmd.ExecuteNonQuery();
                Console.WriteLine("ubaceno redaka " + inserted);
                con.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

        }

        public List<Tuple<int,string,int,string>> getResultsByName(string name)
        {
            List<Tuple<int,string,int,string>> ret = new List<Tuple<int,string,int,string>>();
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\login.accdb");
            try
            {
                con.Open();

                OleDbDataAdapter sda = new OleDbDataAdapter("select bodovi, vrijeme, predmet, razred from bodovi where korisnik='" + name + "'", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    ret.Add(new Tuple<int,string,int, string>(int.Parse(row[3].ToString()), row[2].ToString(), int.Parse(row[0].ToString()),row[1].ToString()));
                }
                con.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return ret;
        }

        public void changeUserName(string oldName, string newName)
        {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\login.accdb");
            try
            {
                con.Open();

                OleDbCommand cmd = new OleDbCommand("update login set Username = '" + newName + "' where Username ='" + oldName+"'", con);
                int updated = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void changePass(string name, int newPass)
        {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\login.accdb");
            try
            {
                con.Open();

                OleDbCommand cmd = new OleDbCommand("update login set Password = " + newPass + " where Username = '" + name + "'", con);
                int updated = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void changeYear(string name, int year)
        {
            OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\login.accdb");
            try
            {
                con.Open();

                OleDbCommand cmd = new OleDbCommand("update login set Razred = " + year + " where Username = '" + name + "'", con);
                int updated = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

    }
}
