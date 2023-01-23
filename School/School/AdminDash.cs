using Guna.UI2.WinForms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using TheArtOfDevHtmlRenderer.Adapters;
using Microsoft.Win32;

namespace School
{
    public partial class AdminDash : Form
    {
        string parametres = "SERVER=127.0.0.1; DATABASE=school; UID=root; PASSWORD=";
        private MySqlConnection maconnexion;

        MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='school';username=root;password=");

        MySqlDataAdapter adapter, adapter2,adapter3;

        DataTable table = new DataTable();
        DataTable table2 = new DataTable();
        DataTable table3 = new DataTable();
        DataTable dataTable = new DataTable();
        DataTable dataTable2 = new DataTable();
        DataTable dataTable3 = new DataTable();
        string nom;
        string prenom;
       
        string id;
        string groupe_id;
        string Euser_id;
        string Egroupe_id;
        string currentindex;
        string currentemail;
        string currentgroupe;
        public AdminDash(string id )
        {
            this.id= id;
            InitializeComponent();
            loadsession(id);
            loaddgroupe();
            loadstudent();
            guna2Button8.Enabled= false;
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
                    
                    string numero = myReader.GetString("nom");
                    


                   groupecombo.Items.Add( numero);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void loadstudent()
        {

            studentlist.Rows.Clear();

            maconnexion = new MySqlConnection(parametres);
            maconnexion.Open();
            string request = "select *  from student;";

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



                adapter = new MySqlDataAdapter("SELECT * FROM `groupe` WHERE `id` = '" + myArray[5] + "'", connection); ;
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

               
                studentlist.Rows.Add(myArray2[1], myArray[0], myArray[1], myArray[2], myArray[3]);

            }






        }

        private void AdminDash_Load(object sender, EventArgs e)
        {
            
        }

        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel1_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void studentlist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.studentlist.Rows[e.RowIndex];

            groupecombo.Text = row.Cells[0].Value.ToString();
            currentindex = row.Cells[1].Value.ToString();
            nomtxt.Text = row.Cells[2].Value.ToString();
            prenomtxt.Text = row.Cells[3].Value.ToString();
            emailtxt.Text = row.Cells[4].Value.ToString();
            currentemail= row.Cells[4].Value.ToString();
            currentgroupe = row.Cells[0].Value.ToString();
            guna2Button8.Enabled = true;
            guna2Button9.Enabled = true;




        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {

            DialogResult dialogUpdate = MessageBox.Show("voulez-vous vraiment modifier les informations  ", "Modifier une appartement", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogUpdate == DialogResult.OK)
            {

                if (emailtxt.Text == "" || nomtxt.Text == "" || prenomtxt.Text == "" || mdptxt.Text == "" || groupecombo.Text == "")
                {
                    DialogResult dialogClose = MessageBox.Show("Veuillez renseigner tous les champs", "Champs requis", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                else
                {
                    adapter3 = new MySqlDataAdapter("SELECT * FROM `groupe` WHERE `nom` = '" + groupecombo.Text + "'", connection); ;
                    adapter3.Fill(table3);


                    int i3;
                    String[] myArray3 = new String[8];
                    foreach (DataRow dataRow3 in table3.Rows)
                    {
                        i3 = 0;
                        foreach (var item in dataRow3.ItemArray)
                        {
                            myArray3[i3] = item.ToString();
                            i3++;
                        }

                        Egroupe_id = myArray3[0];





                    }

                    maconnexion = new MySqlConnection(parametres);
                    maconnexion.Open();
                    MySqlCommand cmd2 = maconnexion.CreateCommand();
                    cmd2.CommandText = "UPDATE student SET nom = @nom,prenom = @prenom , email = @email ,mdp = @mdp ,groupe_id = @groupe_id WHERE id=" + currentindex; 
                    cmd2.Parameters.AddWithValue("@nom", nomtxt.Text);
                    cmd2.Parameters.AddWithValue("@prenom", prenomtxt.Text);
                    cmd2.Parameters.AddWithValue("@email", emailtxt.Text);
                    cmd2.Parameters.AddWithValue("@mdp", mdptxt.Text);
                    cmd2.Parameters.AddWithValue("@groupe_id", Egroupe_id);

                    cmd2.ExecuteNonQuery();
                    maconnexion.Close();

                    AdminDash a = new AdminDash(id);
                    a.Show();
                    this.Hide();

                }



            }
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Gteacher a = new Gteacher(id);
            a.Show();
            this.Hide();    
        }

        private void guna2Button9_Click_1(object sender, EventArgs e)
        {
            

            DialogResult dialogDelete = MessageBox.Show("voulez-vous vraiment supprimer ce etudient ", "Supprimer une zone", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (dialogDelete == DialogResult.OK)
            {
                
                guna2Button9.Enabled = false;
                guna2Button8.Enabled = false;

                maconnexion = new MySqlConnection(parametres);
                maconnexion.Open();
                MySqlCommand cmd = maconnexion.CreateCommand();
                cmd.CommandText = "DELETE FROM student WHERE id=" + currentindex;
                cmd.ExecuteNonQuery();
                maconnexion.Close();

            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Ggroupe a = new Ggroupe(id);
            a.Show();
            this.Hide();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Gseance a = new Gseance(id);
            a.Show();
            this.Hide();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            Gabsence a = new Gabsence(id);
            a.Show();
            this.Hide();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            AdminDash a = new AdminDash(id);
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

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            
            
            if (nomtxt.Text == "" || prenomtxt.Text == "" || emailtxt.Text == "" || mdptxt.Text == "" || groupecombo.Text == "" )
            {
                MessageBox.Show("remplir tout les case");
                

            }
            else
            {
                
                
                adapter3 = new MySqlDataAdapter("SELECT * FROM `groupe` WHERE `nom` = '" + groupecombo.Text + "'", connection); ;
                adapter3.Fill(table3);


                int i3;
                String[] myArray3 = new String[8];
                foreach (DataRow dataRow3 in table3.Rows)
                {
                    i3 = 0;
                    foreach (var item in dataRow3.ItemArray)
                    {
                        myArray3[i3] = item.ToString();
                        i3++;
                    }

                    Egroupe_id = myArray3[0];
                    




                }
                
                maconnexion = new MySqlConnection(parametres);
                maconnexion.Open();
                MySqlCommand cmd2 = maconnexion.CreateCommand();
                cmd2.CommandText = "INSERT INTO student (id ,nom , prenom,email,mdp,groupe_id) VALUES (@id,@nom , @prenom , @email,@mdp , @groupe_id)";
                cmd2.Parameters.AddWithValue("@id", "null");
                cmd2.Parameters.AddWithValue("@nom", nomtxt.Text);
                cmd2.Parameters.AddWithValue("@prenom", prenomtxt.Text);
                cmd2.Parameters.AddWithValue("@email", emailtxt.Text);
                cmd2.Parameters.AddWithValue("@mdp", mdptxt.Text);
                cmd2.Parameters.AddWithValue("@groupe_id", Egroupe_id);

                cmd2.ExecuteNonQuery();
                maconnexion.Close();

                AdminDash a = new AdminDash(id);
                a.Show();
                this.Hide();
            }





        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
