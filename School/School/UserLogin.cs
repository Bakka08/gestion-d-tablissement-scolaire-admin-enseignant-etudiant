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
using TheArtOfDevHtmlRenderer.Adapters;

namespace School
{
    public partial class UserLogin : Form
    {
        MySqlConnection connection = new MySqlConnection("datasource=localhost;port=3306;Initial Catalog='school';username=root;password=");

        MySqlDataAdapter adapter;
        MySqlDataAdapter adapter2;

        DataTable table = new DataTable();
        DataTable table2 = new DataTable();
        DataTable dataTable = new DataTable();
        int currRowIndex;
        String id;
        String id2;
        String id3;
        String id4;

        public UserLogin()
        {
            InitializeComponent();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Login_Click(object sender, EventArgs e)
        {
            adapter = new MySqlDataAdapter("SELECT id FROM `admin` WHERE `email` = '" + emailtxt.Text + "' AND `mdp` = '" + mdptxt.Text + "'", connection);
            adapter.Fill(table);

            if (table.Rows.Count <= 0)
            {
                adapter = new MySqlDataAdapter("SELECT id FROM `teacher` WHERE `email` = '" + emailtxt.Text + "' AND `mdp` = '" + mdptxt.Text + "'", connection);
                adapter.Fill(table);

                if (table.Rows.Count <= 0)
                {
                    adapter = new MySqlDataAdapter("SELECT id FROM `student` WHERE `email` = '" + emailtxt.Text + "' AND `mdp` = '" + mdptxt.Text + "'", connection);
                    adapter.Fill(table);

                    if (table.Rows.Count <= 0)
                    {
                        MessageBox.Show("error");
                        emailtxt.Clear();
                        mdptxt.Clear();
                    }
                    else
                    {
                        int i;
                        String[] myArray = new String[1];
                        foreach (DataRow dataRow in table.Rows)
                        {
                            i = 0;
                            foreach (var item in dataRow.ItemArray)
                            {
                                myArray[i] = item.ToString();
                                i++;
                            }
                            id = myArray[0];
                        }
                        
                        UserDash a = new UserDash(id);
                        a.Show();
                        this.Hide();

                    }
                }
                else
                {
                    int i;
                    String[] myArray = new String[1];
                    foreach (DataRow dataRow in table.Rows)
                    {
                        i = 0;
                        foreach (var item in dataRow.ItemArray)
                        {
                            myArray[i] = item.ToString();
                            i++;
                        }
                        id = myArray[0];
                    }

                    TeacherDash a = new TeacherDash(id);
                    a.Show();
                    this.Hide();

                }
            }
            else
            {
                int i;
                String[] myArray = new String[1];
                foreach (DataRow dataRow in table.Rows)
                {
                    i = 0;
                    foreach (var item in dataRow.ItemArray)
                    {
                        myArray[i] = item.ToString();
                        i++;
                    }
                    id = myArray[0];
                }

                AdminDash a = new AdminDash(id);
                a.Show();
                this.Hide();












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
    }
}
