using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace WindowsFormsAppMysql
{
    public partial class Form1 : Form
    {
        private MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
        private MySqlConnection connection;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            builder.UserID = "root";
            builder.Password = "123456";
            builder.Server = "localhost";
            builder.Database = "test3";
            connection = new MySqlConnection(builder.ConnectionString);
            connection.Open();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql = "select * from " + textBox5.Text;
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            MySqlDataReader reader = cmd.ExecuteReader();

            //设置dataGridView的列
            dataGridView1.ColumnCount = 3;
            dataGridView1.ColumnHeadersVisible = true;

            dataGridView1.Columns[0].Name = "Cno";
            dataGridView1.Columns[1].Name = "Cname";
            dataGridView1.Columns[2].Name = "Avg";

            //根据查询结果像DataGridView中添加数据行
            while (reader.Read())
            {
                int index = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[index].Cells[0].Value = reader.GetString("Cno");
                this.dataGridView1.Rows[index].Cells[1].Value = reader.GetString("Cname");
                this.dataGridView1.Rows[index].Cells[2].Value = reader.GetString("Avg");
            }
            //保证每次操作后，关闭连接。
            connection.Close();     
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string sql = "insert into " + textBox5.Text + " values (" + "'" + textBox1.Text + "','" + textBox2.Text + "'" + ",'" + textBox3.Text + "')";
            textBox4.Text = sql;
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.ExecuteNonQuery();
            connection.Close();     //关闭连接
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //字段为字符窜时要加上单引号 ''
            string sql = "delete from " + textBox5.Text + " where Cno=" + "'" + textBox1.Text + "'";
            textBox4.Text = sql;
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.ExecuteNonQuery();
            connection.Close();     //关闭连接
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string sql = "update " + textBox5.Text + " set Avg=" + "'" + textBox3.Text + "'" + " where Cno=" + "'"+textBox1.Text+"'";
            textBox4.Text = sql;
            MySqlCommand cmd = new MySqlCommand(sql, connection);
            cmd.ExecuteNonQuery();
            connection.Close();     //关闭连接
        }
    }
}
