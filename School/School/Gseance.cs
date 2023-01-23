using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace School
{
    public partial class Gseance : Form
    {

        string parametres = "SERVER=127.0.0.1; DATABASE=school; UID=root; PASSWORD=";
        private MySqlConnection maconnexion;

        MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='school';username=root;password=");

        MySqlDataAdapter adapter, adapter2, adapter3;

        DataTable table = new DataTable();
        DataTable table2 = new DataTable();
        DataTable table3 = new DataTable();
        DataTable dataTable = new DataTable();
        DataTable dataTable2 = new DataTable();
        DataTable dataTable3 = new DataTable();
        string nom;
        string prenom;

        string id;
        public Gseance(string id)
        {
            this.id = id;
            InitializeComponent();
            loadsession(id);
            loaddgroupe();
            loadteacher();
            loadseance();
            guna2Button8.Enabled = false;
            guna2Button9.Enabled = false;

        }
            private void loadsession(string d)
            {
                adapter = new MySqlDataAdapter("SELECT * FROM `admin` WHERE `id` = '" + d + "'", connection); ;
                adapter.Fill(table);


                int i;
                String[] myArray = new String[8];
                foreach (DataRow dataRow in table.Rows)
                {
                    i = 0;
                    foreach (var item in dataRow.ItemArray)
                    {
                        myArray[i] = item.ToString();
                        i++;
                    }
                    nom = myArray[1];
                    prenom = myArray[2];


                    session.Text = "Bonjour " + nom + " " + prenom;
                    guna2HtmlLabel2.Text = "admin";




                }

            }
        private void loaddgroupe()
        {
            string query = "select * from groupe ;";
            MySqlConnection conDataBase = new MySqlConnection(parametres);
            MySqlCommand cmdDataBase = new MySqlCommand(query, conDataBase);
            MySqlDataReader myReader;

            try
            {
                conDataBase.Open();
                myReader = cmdDataBase.ExecuteReader();
                while (myReader.Read())
                {

                    string numero = myReader.GetString("id");



                    groupecombo.Items.Add(numero);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void guna2Button7_Click(object sender, EventArgs e)
        {

            if (date.Text == "" || time1txt.Text == "" || time2txt.Text == "" || teachercombo.Text == "" || groupecombo.Text == "")
            {
                MessageBox.Show("remplir tout les case");


            }
            else
            {


            

                maconnexion = new MySqlConnection(parametres);
                maconnexion.Open();
                MySqlCommand cmd2 = maconnexion.CreateCommand();
                cmd2.CommandText = "INSERT INTO seance (id ,date, teacher_id,groupe_id,time1,time2) VALUES (@id,@date , @teacher_id ,@groupe_id, @time1,@time2 )";
                cmd2.Parameters.AddWithValue("@id", "null");
                cmd2.Parameters.AddWithValue("@date", date.Text);
                cmd2.Parameters.AddWithValue("@teacher_id", teachercombo.Text);
                cmd2.Parameters.AddWithValue("@groupe_id", groupecombo.Text);
                cmd2.Parameters.AddWithValue("@time1", time1txt.Text);
                cmd2.Parameters.AddWithValue("@time2", time2txt.Text) ;

                cmd2.ExecuteNonQuery();
                maconnexion.Close();

                Gseance a = new Gseance(id);
                a.Show();
                this.Hide();
            }
        }

        private void studentlist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void loadteacher()
        {
            string query = "select * from teacher ;";
            MySqlConnection conDataBase = new MySqlConnection(parametres);
            MySqlCommand cmdDataBase = new MySqlCommand(query, conDataBase);
            MySqlDataReader myReader;

            try
            {
                conDataBase.Open();
                myReader = cmdDataBase.ExecuteReader();
                while (myReader.Read())
                {

                    string numero = myReader.GetString("id");



                    teachercombo.Items.Add(numero);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void X_Click(object sender, EventArgs e)
        {
            DialogResult dialogClose = MessageBox.Show("Voulez vous vraiment fermer l'application ?", "Quitter le programme", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogClose == DialogResult.OK)
            {
                Application.Exit();
            }
            else if (dialogClose == DialogResult.Cancel)
            {
                //do something else
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            AdminDash a = new AdminDash(id);
            a.Show();
            this.Hide();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Gteacher a = new Gteacher(id);
            a.Show();
            this.Hide();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Ggroupe a = new Ggroupe(id);
            a.Show();
            this.Hide();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            Gseance a = new Gseance(id);
            a.Show();
            this.Hide();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Gabsence a = new Gabsence(id); a.Show(); this.Hide();
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }


        private void loadseance()
        {

            seancelist.Rows.Clear();

            maconnexion = new MySqlConnection(parametres);
            maconnexion.Open();
            string request = "select *  from seance;";

            MySqlCommand cmd = new MySqlCommand(request, maconnexion);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dataTable);

            int i;
            String[] myArray = new String[10];
            foreach (DataRow dataRow in dataTable.Rows)
            {
                i = 0;
                foreach (var item in dataRow.ItemArray)
                {
                    myArray[i] = item.ToString();
                    i++;
                }



                adapter = new MySqlDataAdapter("SELECT * FROM `groupe` WHERE `id` = '" + myArray[3] + "'", connection); ;
                adapter.Fill(dataTable3);

                int i2;
                String[] myArray2 = new String[10];
                foreach (DataRow dataRow2 in dataTable3.Rows)
                {
                    i2 = 0;
                    foreach (var item in dataRow2.ItemArray)
                    {
                        myArray2[i2] = item.ToString();
                        i2++;
                    }



                }
                adapter = new MySqlDataAdapter("SELECT * FROM `teacher` WHERE `id` = '" + myArray[2] + "'", connection); ;
                adapter.Fill(dataTable2);

                int i22;
                String[] myArray22 = new String[10];
                foreach (DataRow dataRow22 in dataTable2.Rows)
                {
                    i22 = 0;
                    foreach (var item in dataRow22.ItemArray)
                    {
                        myArray22[i22] = item.ToString();
                        i22++;
                    }



                }


                seancelist.Rows.Add(myArray[0], myArray22[1], myArray2[1], myArray[1], myArray[4], myArray[5]);

            }
        }
        }
}
