using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace The7class
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //产生所有按钮
            GenerateAllButtons();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //打乱顺序
            Shoffle();
        }
        //打乱按钮的顺序
        private void Shoffle()
        {
            Random rnd = new Random();
            for (int i = 0; i < 100; i++)
            {
                int a = rnd.Next(N);
                int b = rnd.Next(N);
                int c = rnd.Next(N);
                int d = rnd.Next(N);
                Swap(buttons[a, b], buttons[c, d]);
            }
        }
        //交换两个按钮的文本和可见性
        private void Swap(Button button1, Button button2)
        {
            string t = button1.Text;
            button1.Text = button2.Text;
            button2.Text = t;

            bool v = button1.Visible;
            button1.Visible = button2.Visible;
            button2.Visible = v;
        }

        const int N = 4;   //按钮的行，列数
        Button[,] buttons = new Button[N, N];  //按钮的数组
      
        //产生所有按钮
        private void GenerateAllButtons()
        {
            int x0 = 100, y0 = 10,w=45, d = 50;
            for (int r = 0; r < N; r++)
            {
                for (int c = 0; c < N; c++)
                {
                    int num = r * N + c;
                    Button btn = new Button();
                    btn.Text = (num + 1).ToString();
                    btn.Top = y0 + r * d;
                    btn.Left = x0 + c * d;
                    btn.Height = w;
                    btn.Width = w;
                    btn.Visible = true;
                    btn.Tag = r * N + c;//这个数据用来表示它所在行列；

                    //注册事件
                    btn.Click +=new EventHandler( Btn_Click);

                    buttons[r, c] = btn;//放在数组中
                    this.Controls.Add(btn);//加到界面上
                }


            }
            buttons[N - 1, N - 1].Visible = false;//最后一个不可见


        }
        //判断是否进行交换
        private void Btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;//当前点中的按钮
            Button blank = FindHiddenButton();//空白按钮
            //判断是否与空白块相邻，如果是，则交换
            if (IsNeighbor(btn,blank))
            {
                Swap(btn, blank);
                blank.Focus();
            }
            //判断是否完成了
            if (RasultIsOk())
            {
                MessageBox.Show("ok");
            }
        }
        //判断是否完成
        private bool RasultIsOk()
        {
            for (int r = 0; r < N; r++)
            {
                for (int c = 0; c < N; c++)
                {
                    if(buttons[r,c].Text!=(r*N+c+1).ToString())
                    {
                        return false;
                    }
                }

            }
            return true;
        }
        //判断是否相邻
        private bool IsNeighbor(Button btnA, Button btnB)
        {
            int a = (int)btnA.Tag;//Tag中记录是行列位置
            int b = (int)btnB.Tag;
            int r1 = a / N, c1 = a % N;
            int r2 = b / N, c2 = b % N;
            if (r1 == r2 && (c1 == c2 - 1 || c1 == c2 + 1)//左右相邻
                || c1 == c2 && (r1 == r2 - 1 || r1 == r2 + 1))//上下相邻
                return true;
            return false;
        }
        //查找要隐藏的按钮
        private Button FindHiddenButton()
        {
            for (int r = 0; r < N; r++)
            {
                for (int c = 0; c < N; c++)
                {
                    if (!buttons[r, c].Visible)
                    {
                        return buttons[r, c];
                    }
                }
            }
            return null;
        }
    }
}
