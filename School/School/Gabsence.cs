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
    public partial class Gabsence : Form
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
        string id;
        string nom;
        string prenom;
        public Gabsence(string id )
        {
            this.id = id;
            InitializeComponent();
            loadsession(id);
            loadabsence();
        }

        private void Gabsence_Load(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

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

        a.Show(); this.Hide();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Gseance a = new Gseance(id);
            a.Show(); this.Hide();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            Gabsence a = new Gabsence(id);
            a.Show(); this.Hide();
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
        private void loadabsence()
        {

            absencetlist.Rows.Clear();

            maconnexion = new MySqlConnection(parametres);
            maconnexion.Open();
            string request = "select *  from absence;";

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



                adapter = new MySqlDataAdapter("SELECT * FROM `seance` WHERE `id` = '" + myArray[1] + "'", connection); ;
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
                adapter = new MySqlDataAdapter("SELECT * FROM `student` WHERE `id` = '" + myArray[2] + "'", connection); ;
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


                absencetlist.Rows.Add(myArray[0], myArray2[0], myArray22[1] + "" + myArray22[2], myArray[3]);

            }
        }
    }


}

