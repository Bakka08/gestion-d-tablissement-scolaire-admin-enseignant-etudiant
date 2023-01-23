using Guna.UI2.WinForms;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls.Crypto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace School
{

    

    public partial class TeacherDash : Form
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
        string sid;
        string gid;
        string numero;


        public TeacherDash(string id)
        {
            this.id = id;
            InitializeComponent();
            loadseance(id);
            loadsession(id);
            guna2Button7.Enabled = false;

        }
        private void loadsession(string d)
        {
            adapter = new MySqlDataAdapter("SELECT * FROM `teacher` WHERE `id` = '" + d + "'", connection); ;
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
                guna2HtmlLabel2.Text = "Enseingnant";




            }

        }

        private void seancelist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.seancelist.Rows[e.RowIndex];
            X1.Text = row.Cells[0].Value.ToString();
            sid= row.Cells[0].Value.ToString();
            X2.Text = row.Cells[1].Value.ToString();
           
            X3.Text = row.Cells[2].Value.ToString();

            X4.Text = row.Cells[3].Value.ToString();

            X5.Text = row.Cells[4].Value.ToString();
            guna2Button7.Enabled = true;

            string query = "select * from seance where id=" + X1.Text + ";";
            MySqlConnection conDataBase = new MySqlConnection(parametres);
            MySqlCommand cmdDataBase = new MySqlCommand(query, conDataBase);
            MySqlDataReader myReader;

            try
            {
                conDataBase.Open();
                myReader = cmdDataBase.ExecuteReader();
                while (myReader.Read())
                {

                    numero = myReader.GetString("groupe_id");
                    Labelgid.Text = numero;


                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }




        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {

            string query = "select * from groupe where nom=" + gid + ";";
                MySqlConnection conDataBase = new MySqlConnection(parametres);
                MySqlCommand cmdDataBase = new MySqlCommand(query, conDataBase);
                MySqlDataReader myReader;

                try
                {
                    conDataBase.Open();
                    myReader = cmdDataBase.ExecuteReader();
                    while (myReader.Read())
                    {

                         numero = myReader.GetString("id");
                        MessageBox.Show(numero);


                    }

                }
                catch (Exception ex)
                {
                    
                }
            
            Tabsence a = new Tabsence(id,sid, Labelgid.Text) ;
            a.Show();
            this.Hide();
        }

        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Tnote a = new Tnote(id);
            a.Show();
            this.Hide();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            TeacherDash a =new TeacherDash(id);
            a.Show();
            this.Hide();
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

        private void loadseance(string id )
        {

            seancelist.Rows.Clear();

            maconnexion = new MySqlConnection(parametres);
            maconnexion.Open();
            string request = "select *  from seance where teacher_id = "+id+";";

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
                


                seancelist.Rows.Add(myArray[0], myArray2[1], myArray[1], myArray[4], myArray[5]);

            }
        }
        private void TeacherDash_Load(object sender, EventArgs e)
        {

        }
    }
}
