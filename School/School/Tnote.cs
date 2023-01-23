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
    public partial class Tnote : Form
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
        
        public Tnote(string id )
        {
            
            InitializeComponent();
            this.id = id;
            loadsession(id);
            loadstudent();
        }

        private void loadstudent()
        {

            studentlist.Rows.Clear();

            maconnexion = new MySqlConnection(parametres);
            maconnexion.Open();
            string request = "select *  from student ;";

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


                adapter = new MySqlDataAdapter("SELECT * FROM `note` WHERE `teacher_id` = '" + id + "' AND `student_id` = '" + myArray[0] + "'", connection);
                adapter.Fill(table2);

                if (table2.Rows.Count <= 0)
                {
                    studentlist.Rows.Add(myArray[0], myArray[1], myArray[2],"null","null","null");
                }
                else
                {
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
                    studentlist.Rows.Add(myArray[0], myArray[1], myArray[2], myArray2[1], myArray2[2], myArray2[3]);







                }
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            {
                if (X4.Text == "null")
                {
                    maconnexion = new MySqlConnection(parametres);
                    maconnexion.Open();
                    MySqlCommand cmd2 = maconnexion.CreateCommand();
                    cmd2.CommandText = "INSERT INTO note (id ,controle1, controle2,moy,student_id,teacher_id) VALUES (@id ,@controle1, @controle2,@moy,@student_id,@teacher_id)";
                    cmd2.Parameters.AddWithValue("@id", "null");
                    cmd2.Parameters.AddWithValue("@controle1", guna2TextBox1.Text);
                    cmd2.Parameters.AddWithValue("@controle2", guna2TextBox2.Text);
                    double c1 = Convert.ToDouble(guna2TextBox1.Text);
                    double c2 = Convert.ToDouble(guna2TextBox2.Text);
                    double a = (c1 + c2) / 2;
                    cmd2.Parameters.AddWithValue("@moy", a.ToString());
                    cmd2.Parameters.AddWithValue("@student_id", X1.Text);
                    cmd2.Parameters.AddWithValue("@teacher_id", id);

                    cmd2.ExecuteNonQuery();
                    maconnexion.Close();

                    Tnote r = new Tnote(id);
                    r.Show();
                    this.Hide();

                }
                else
                {
                    maconnexion = new MySqlConnection(parametres);
                    maconnexion.Open();
                    MySqlCommand cmd2 = maconnexion.CreateCommand();
                    cmd2.CommandText = "UPDATE note SET controle1= @controle1,controle2=@controle2,moy=@moy WHERE teacher_id= @teacher_id  AND student_id=@student_id";
                    cmd2.Parameters.AddWithValue("@controle1", guna2TextBox1.Text);
                    cmd2.Parameters.AddWithValue("@controle2", guna2TextBox2.Text);
                    double c1 = Convert.ToDouble(guna2TextBox1.Text);
                    double c2 = Convert.ToDouble(guna2TextBox2.Text);
                    double a = (c1 + c2) / 2;
                    cmd2.Parameters.AddWithValue("@moy", a.ToString());
                    cmd2.Parameters.AddWithValue("@student_id", X1.Text);
                    cmd2.Parameters.AddWithValue("@teacher_id", id);

                    cmd2.ExecuteNonQuery();
                    maconnexion.Close();

                    Tnote r = new Tnote(id);
                    r.Show();
                    this.Hide();

                }
            }
        }

        private void studentlist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.studentlist.Rows[e.RowIndex];
            X1.Text = row.Cells[0].Value.ToString();
            X2.Text = row.Cells[1].Value.ToString();
            X3.Text = row.Cells[2].Value.ToString();
            X4.Text = row.Cells[5].Value.ToString();
            guna2TextBox1.Text = row.Cells[3].Value.ToString();
            guna2TextBox2.Text = row.Cells[4].Value.ToString();
            guna2Button3.Enabled = true;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            TeacherDash a = new TeacherDash(id);
            a.Show();
            this.Hide();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Tnote a =new Tnote(id);
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

        private void Tnote_Load(object sender, EventArgs e)
        {

        }
    }
}
