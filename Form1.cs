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
 
namespace 连接数据库
{
 
	public partial class Form1 : Form
	{
		MySqlConnection conn; //连接数据库对象
		MySqlDataAdapter mda; //适配器变量
		DataSet ds;  //临时数据集
 
		public Form1()
		{
			InitializeComponent();
		}
 
		private void Form1_Load(object sender, EventArgs e)
		{
 
 
		}
 
		private void button1_Click(object sender, EventArgs e)
		{
			string M_str_sqlcon = "server=localhost;user id=root;password=123456;database=sys";                                                                                              //创建数据库连接对象
			conn = new MySqlConnection(M_str_sqlcon);
			try
			{
				//打开数据库连接
				conn.Open();
				MessageBox.Show("数据库已经连接了！");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
 
		}
 
		private void sysDataSet1BindingSource_CurrentChanged(object sender, EventArgs e)
		{
 
		}
 
		private void button2_Click(object sender, EventArgs e)
		{
			string sql = "select * from score";
			mda = new MySqlDataAdapter(sql, conn);
			ds = new DataSet();
			mda.Fill(ds, "score");
			//显示数据
			dataGridView1.DataSource = ds.Tables["score"];
			conn.Close();
 
		}
 
		private void button5_Click(object sender, EventArgs e)
		{
 
			int count = 0;
			try
			{
				
				
				//for循环，dataGridView1.SelectedRows.Count为鼠标选中行的数目，一次for循环删除一行数据
				for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
				{
					//获得i行的编号
					int id = Convert.ToInt32(dataGridView1.SelectedRows[i].Cells[0].Value);
					//编写数据库删除代码，这里还用到了动态变量，用于改变每次id值
					string dataToDo3 = $"delete from student where id = {id}";
					MySqlCommand
										//创建MySqlCommand类用于SQL语句的执行
										cmd = new MySqlCommand(dataToDo3, conn);
					//定义x接收返回值SQL语句返回值，为0则为执行失败
					int x = cmd.ExecuteNonQuery();
					//执行判断
					if (x == 0)
					{
						MessageBox.Show("删除失败");
					}
					//若成功则计数值+1
					count = count + 1;
				}
				//若计数值等于选中行的数目，代表成功完成所有行的删除
				if (count == dataGridView1.SelectedRows.Count)
				{
					MessageBox.Show("删除成功");
				}
 
			}
			catch (Exception ex)
			{
 
				MessageBox.Show(ex.ToString());
			}
			finally
			{
				//这里是刷新界面
				button2.PerformClick();
			
				
			}
 
 
 
		}
 
		private void button4_Click(object sender, EventArgs e)
		{
			if (mda == null || ds == null)
			{
				MessageBox.Show("请先导入数据");
				return;
			}
			try
			{
				string msg = "确实要修改吗？";
				if (1 == (int)MessageBox.Show(msg, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation))
				{
					MySqlCommandBuilder builder = new MySqlCommandBuilder(mda); //命令生成器。
																				//适配器会自动更新用户在表上的操作到数据库中
					mda.Update(ds, "score");
					MessageBox.Show("修改成功", "提示");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "错误信息");
			}
 
		}
 
		private void button3_Click(object sender, EventArgs e)
		{
			if (mda == null || ds == null)
			{
				MessageBox.Show("请先导入数据");
				return;
			}
			try
			{
				string msg = "确实要添加此条数据吗？";
				if (1 == (int)MessageBox.Show(msg, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation))
				{
					MySqlCommandBuilder builder = new MySqlCommandBuilder(mda);
					mda.Update(ds, "score");
					MessageBox.Show("添加成功", "提示");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "错误信息");
			}
 
		}
 
		private void button6_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
	}
}
