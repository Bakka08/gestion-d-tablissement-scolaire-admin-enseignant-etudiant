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

namespace School
{
    public partial class UserDash : Form
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
        DataTable dataTable4 = new DataTable();
        DataTable dataTable5 = new DataTable();
        string nom;
        string prenom;
        string id;
        string groupe_id;

        public UserDash(string id)
        {
            InitializeComponent();
            this.id= id;
            loadnote();
            loadsession(id);
            loadseance();
            loadabsence(id);
            
        }
        private void loadsession(string d)
        {
            adapter = new MySqlDataAdapter("SELECT * FROM `student` WHERE `id` = '" + d + "'", connection); ;
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
                groupe_id = myArray[5];


                session.Text = "Bonjour " + nom + " " + prenom;
                guna2HtmlLabel2.Text = "Etudient";




            }

        }

        private void studentlist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void loadseance()
        {

            seancelist.Rows.Clear();

            maconnexion = new MySqlConnection(parametres);
            maconnexion.Open();
            string request = "select *  from seance where groupe_id= " + groupe_id + ";";

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



                adapter = new MySqlDataAdapter("SELECT * FROM `teacher` WHERE `id` = '" + myArray[2] + "'", connection); ;
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



                seancelist.Rows.Add(myArray[1], myArray[4], myArray[5], myArray2[1]+" " + myArray2[2], myArray2[6]);

            }
        }

        private void seancelist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void UserDash_Load(object sender, EventArgs e)
        {

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

        private void loadabsence(string id )
        {

            absencelist.Rows.Clear();

            maconnexion = new MySqlConnection(parametres);
            maconnexion.Open();
            string request = "select *  from absence where student_id = " + id + ";";

            MySqlCommand cmd = new MySqlCommand(request, maconnexion);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dataTable2);

            int i;

            String[] myArray = new String[10];
            foreach (DataRow dataRow in dataTable2.Rows)
            {
                i = 0;
                foreach (var item in dataRow.ItemArray)
                {
                    myArray[i] = item.ToString();
                    i++;
                }


                adapter = new MySqlDataAdapter("SELECT * FROM `seance` WHERE `id` = '" + myArray[1] +  "'", connection);
                adapter.Fill(table2);

               
                    int i2;

                    String[] myArray2 = new String[10];
                    foreach (DataRow dataRow2 in table2.Rows)
                    {
                        i2 = 0;
                        foreach (var item in dataRow2.ItemArray)
                        {
                            myArray2[i2] = item.ToString();

                            i2++;
                        }



                    }
                absencelist.Rows.Add(myArray2[1], myArray[3]);







                }






            }
        private void loadnote()
        {

            seancelist.Rows.Clear();

            maconnexion = new MySqlConnection(parametres);
            maconnexion.Open();
            string request = "select *  from note where student_id= " + id + ";";

            MySqlCommand cmd = new MySqlCommand(request, maconnexion);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dataTable4);

            int i;
            String[] myArray = new String[10];
            foreach (DataRow dataRow in dataTable4.Rows)
            {
                i = 0;
                foreach (var item in dataRow.ItemArray)
                {
                    myArray[i] = item.ToString();
                    i++;
                }



                adapter = new MySqlDataAdapter("SELECT * FROM `teacher` WHERE `id` = '" + myArray[5] + "'", connection); ;
                adapter.Fill(dataTable5);

                int i2;
                String[] myArray2 = new String[10];
                foreach (DataRow dataRow2 in dataTable5.Rows)
                {
                    i2 = 0;
                    foreach (var item in dataRow2.ItemArray)
                    {
                        myArray2[i2] = item.ToString();
                        i2++;
                    }



                }



                notelist.Rows.Add(myArray[1], myArray[2], myArray[3],  myArray2[6]);

            }
        }
    }
    }

