using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Алгоритм_Мальгранжа
{
    public partial class Form1 : Form
    {
        Panel panel = new Panel();
        int size = 0;
        string[,] comboboxmatrix;
        ComboBox[,] cb;
        public Form1()
        {
            InitializeComponent();
        }
        private void Begin_Click(object sender, EventArgs e)
        {
            if (Int32.TryParse(Size.Text, out int siz) && siz > 0 && siz <= 20)
            {
                size = siz;
                comboboxmatrix = new string[size, size];
                cb = new ComboBox[size, size];
                Updates();
            }
            else
            {
                MessageBox.Show("Неверный размер матрицы, повторите попытку.", "Сообщение");
                Size.Text = "";
            }
        }
        public async void Updates()
        {
            panel.Location = new Point(12, 153);
            panel.Size = new Size(1100, 700);
            panel.AutoScroll = true;
            this.Controls.Add(panel);
            panel.Controls.Clear();
            int x = 1;
            for (int i = 1; i <= size; i++)
            {
                Label label = new Label();
                label.BorderStyle = BorderStyle.FixedSingle;
                label.Text = $"X{i}";
                label.Location = new Point(i * 40, 0);
                label.Size = new Size(40, 30);
                panel.Controls.Add(label);

                Label label1 = new Label();
                label1.BorderStyle = BorderStyle.FixedSingle;
                label1.Text = $"X{i}";
                label1.Location = new Point(0, i * 30);
                label1.Size = new Size(40, 30);
                panel.Controls.Add(label1);

                x++;
                await Task.Run(() =>
                {
                    for (int j = 1; j <= size; j++)
                    {
                        cb[i - 1, j - 1] = new ComboBox();
                        cb[i-1, j-1].FlatStyle = FlatStyle.Flat;
                        cb[i - 1, j - 1].Items.AddRange(new object[] { "0", "1" });
                        cb[i - 1, j - 1].Size = new Size(40, 30);
                        cb[i - 1, j - 1].Location = new Point(i * 40, j * 30);
                        panel.Invoke((MethodInvoker)(() => panel.Controls.Add(cb[i - 1, j - 1])));
                    }
                });
            }
            Button Start = new Button();
            Start.Text = "Выполнить";
            Start.Location = new Point(x * 40 + 10, 0);
            Start.BackColor = Color.White;
            Start.Size = new Size(100, 45);
            Start.Click += new EventHandler(StartAlgorithm);
            panel.Controls.Add(Start);
        }
        public void StartAlgorithm(object sender, EventArgs e)
        {
            for(int i = 0; i < size; i++)
                for(int j = 0; j < size; j++)
                {
                    comboboxmatrix[i, j] = cb[j, i].Text;
                }
            Algorithm algo = new Algorithm(comboboxmatrix);
            algo.BeginAlgorithm();
            MessageBox.Show(algo.tt);
            MessageBox.Show(algo.ToString(), "Результат разбиения:");
        }
    }
}