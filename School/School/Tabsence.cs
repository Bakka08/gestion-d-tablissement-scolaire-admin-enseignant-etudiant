using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheArtOfDevHtmlRenderer.Adapters;

namespace School
{
    public partial class Tabsence : Form
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
        string  sid, gid;
        string id;
        string currentindex;
        public Tabsence(string id , string sid ,string gid)
        {
            
            
           

            InitializeComponent();
            this.id = id;
            this.sid = sid;
                this.gid = gid;
            loadstudent();
            
        }

       

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            if (X5.Text == "non etat")
            {
                maconnexion = new MySqlConnection(parametres);
                maconnexion.Open();
                MySqlCommand cmd2 = maconnexion.CreateCommand();
                cmd2.CommandText = "INSERT INTO absence (id ,seance_id, student_id,etat) VALUES (@id,@seance_id , @student_id , @etat)";
                cmd2.Parameters.AddWithValue("@id", "null");
                cmd2.Parameters.AddWithValue("@seance_id", sid);
                cmd2.Parameters.AddWithValue("@student_id", X1.Text);
                cmd2.Parameters.AddWithValue("@etat", "P");


                cmd2.ExecuteNonQuery();
                maconnexion.Close();

                Tabsence a = new Tabsence(id, sid, gid);
                a.Show();
                this.Hide();

            }
            else
            {
                maconnexion = new MySqlConnection(parametres);
                maconnexion.Open();
                MySqlCommand cmd2 = maconnexion.CreateCommand();
                cmd2.CommandText = "UPDATE absence SET etat= @etat WHERE seance_id= @seance_id  AND student_id=@student_id";
                cmd2.Parameters.AddWithValue("@seance_id", sid);
                cmd2.Parameters.AddWithValue("@student_id", X1.Text);
                cmd2.Parameters.AddWithValue("@etat", "P");

                cmd2.ExecuteNonQuery();
                maconnexion.Close();
                Tabsence a = new Tabsence(id, sid, gid);
                a.Show();
                this.Hide();

            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

            if (X5.Text=="non etat")
            {
                maconnexion = new MySqlConnection(parametres);
                maconnexion.Open();
                MySqlCommand cmd2 = maconnexion.CreateCommand();
                cmd2.CommandText = "INSERT INTO absence (id ,seance_id, student_id,etat) VALUES (@id,@seance_id , @student_id , @etat)";
                cmd2.Parameters.AddWithValue("@id", "null");
                cmd2.Parameters.AddWithValue("@seance_id", sid);
                cmd2.Parameters.AddWithValue("@student_id", X1.Text);
                cmd2.Parameters.AddWithValue("@etat", "A");


                cmd2.ExecuteNonQuery();
                maconnexion.Close();

                Tabsence a = new Tabsence(id, sid, gid);
                a.Show();
                this.Hide();
            
        }else
            {
                maconnexion = new MySqlConnection(parametres);
                maconnexion.Open();
                MySqlCommand cmd2 = maconnexion.CreateCommand();
                cmd2.CommandText = "UPDATE absence SET etat= @etat WHERE seance_id= @seance_id  AND student_id=@student_id";
                cmd2.Parameters.AddWithValue("@seance_id", sid);
                cmd2.Parameters.AddWithValue("@student_id", X1.Text);
                cmd2.Parameters.AddWithValue("@etat", "A");

                cmd2.ExecuteNonQuery();
                maconnexion.Close();
                Tabsence a = new Tabsence(id, sid, gid);
                a.Show();
                this.Hide();

            }




            
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            TeacherDash a = new TeacherDash(id);
            a.Show();
            this.Hide();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Tnote a =new Tnote(id); a.Show(); this.Hide();
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

        private void loadstudent()
        {

            studentlist.Rows.Clear();

            maconnexion = new MySqlConnection(parametres);
            maconnexion.Open();
            string request = "select *  from student where groupe_id = " + gid + ";";

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


                adapter = new MySqlDataAdapter("SELECT * FROM `absence` WHERE `seance_id` = '" + sid + "' AND `student_id` = '" + myArray[0] + "'", connection);
                adapter.Fill(table);

                if (table.Rows.Count <= 0)
                {
                    studentlist.Rows.Add(myArray[0], myArray[1], myArray[2], myArray[3], "non etat");
                }
                else
                {
                    int i2;

                    String[] myArray2 = new String[10];
                    foreach (DataRow dataRow2 in table.Rows)
                    {
                        i2 = 0;
                        foreach (var item in dataRow2.ItemArray)
                        {
                            myArray2[i2] = item.ToString();

                            i2++;
                        }



                    }
                    studentlist.Rows.Add(myArray[0], myArray[1], myArray[2], myArray[3], myArray2[3]) ;







                }




                

            }
        }

        private void Tabsence_Load(object sender, EventArgs e)
        {

        }
        private void studentlist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.studentlist.Rows[e.RowIndex];
            X1.Text = row.Cells[0].Value.ToString();
            X2.Text = row.Cells[1].Value.ToString();
            X3.Text = row.Cells[2].Value.ToString();
            X4.Text = row.Cells[3].Value.ToString();
            X5.Text = row.Cells[4].Value.ToString();





        }
    }
}
