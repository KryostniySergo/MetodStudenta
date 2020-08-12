using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetodStudenta
{
    public partial class Form1 : Form
    {
        Control[] doRem;
        public Form1()
        {
            InitializeComponent();
            doRem = new Control[] { textBox1, textBox2, textBox3, button1, button2, label1,label2 , label3, label4};
        }

        static int sikoka;
        Control[] AlltextBox = new Control[sikoka];
        Control[] AllLabel = new Control[sikoka];

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                sikoka = Convert.ToInt32(textBox1.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Вы не ввели количество измерений!");
                this.Close();
            }

            Control[] LabelArray = new Control[sikoka];
            Control[] TextBoxArray = new Control[sikoka];

            for (int i = 0; i < sikoka; i++)
            {
                LabelArray[i] = (new Label
                {
                    Name = i.ToString(),
                    Location = new Point(1, i + (i * 20)),
                    Text = (i+1).ToString(),
                    Size = new Size(20,20)
                });
                TextBoxArray[i] = (new TextBox() {
                    Name = Convert.ToString(i),
                    Location = new Point(2, i+(i*20)), 
                    Text = "",
                    Left = 21
                }
                );
                this.Controls.Add(LabelArray[i]);
                this.Controls.Add(TextBoxArray[i]);
            }
            if (sikoka > 5)
            {
                this.Height = sikoka + (sikoka * 20) + 70;
            }
            else
            {
                this.Height = sikoka + (sikoka * 20) + 200;
            }
            AlltextBox = TextBoxArray;
            AllLabel = LabelArray;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<double[]> izm = Izmereniya();

            foreach (var item in doRem)
            {
                this.Controls.Remove(item);
            }
            for (int i = 0; i < AlltextBox.Length; i++)
            {
                this.Controls.Remove(AlltextBox[i]);
                this.Controls.Remove(AllLabel[i]);
            }
            this.Width = 300;

            this.Controls.Add(new Label
            {
                Name = "<X>",
                Text = "<X>",
                Location = new Point(4, 10),
                Size = new Size(30, 20)
            });
            this.Controls.Add(new TextBox
            {
                Name = "<X> T",
                Text = izm[0][0].ToString(),
                Location = new Point(2, 30),
                Size = new Size(35, 20)
            });
            this.Controls.Add(new Label
            {
                Name = "Lambda X",
                Text = "ΔX",
                Location = new Point(70, 10),
                Size = new Size(30, 20)

            });
            for (int i = 0; i < sikoka; i++)
            {
                this.Controls.Add(new Label
                {
                    Name = i.ToString(),
                    Location = new Point(50, 30 + (i * 20)),
                    Text = (i + 1).ToString(),
                    Size = new Size(20, 20)
                });
                this.Controls.Add(new TextBox
                {
                    Name = "Lambda X T",
                    Text = izm[1][i].ToString(),
                    Location = new Point(70, 30 + (i * 20)),
                    Size = new Size(35, 20)
                });
            }
            this.Controls.Add(new Label
            {
                Name = "S <x>",
                Text = "S<x>",
                Location = new Point(110, 10),
                Size = new Size(35, 20)
            });
            this.Controls.Add(new TextBox
            {
                Name = "S<x>T",
                Text = izm[0][1].ToString(),
                Location = new Point(110, 30),
                Size = new Size(35, 20)
            });
            this.Controls.Add(new Label
            {
                Name = "ΔXsl",
                Text = "ΔXsl",
                Location = new Point(150, 10),
                Size = new Size(35, 20)
            });
            this.Controls.Add(new TextBox
            {
                Name = "ΔXslT",
                Text = izm[0][2].ToString(),
                Location = new Point(150, 30),
                Size = new Size(35, 20)
            });
            this.Controls.Add(new Label
            {
                Name = "AbsΔX",
                Text = "AbsΔX",
                Location = new Point(190, 10),
                Size = new Size(35, 20)
            });
            this.Controls.Add(new TextBox
            {
                Name = "AbsΔXT",
                Text = izm[0][3].ToString(),
                Location = new Point(190, 30),
                Size = new Size(35, 20)
            });
            this.Controls.Add(new Label
            {
                Name = "E",
                Text = "E",
                Location = new Point(235, 10),
                Size = new Size(35, 20)
            });
            this.Controls.Add(new TextBox
            {
                Name = "ET",
                Text = izm[0][4].ToString() + "%",
                Location = new Point(230, 30),
                Size = new Size(40, 20)
            });
        }

        List<double[]> Izmereniya()
        {
            double[] data = new double[sikoka];
            for (int i = 0; i < sikoka; i++)
            {
                try
                {
                    data[i] = Convert.ToDouble(AlltextBox[i].Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Вы ввели не цифры, а символы в поля поэтому идёте нахуй");
                    this.Close();
                }
            }


            double Xsred = 0;
            for (int i = 0; i < sikoka; i++)
            {
                Xsred += data[i];
            }
            Xsred = Xsred / sikoka;

            double[] AbsX = new double[sikoka];
            for (int i = 0; i < sikoka; i++)
            {
                AbsX[i] = data[i] - Xsred;
                AbsX[i] = Math.Round(AbsX[i], 2);
            }

            double Sx = 0;
            for (int i = 0; i < sikoka; i++)
            {
                Sx += Math.Pow(AbsX[i], 2);
            }
            Sx = Math.Sqrt(Sx / (sikoka * (sikoka - 1)));

            double Xsl = Convert.ToDouble(textBox2.Text)*Sx;

            double Xpribor = Math.Sqrt(Math.Pow(Xsl, 2) + Math.Pow(Convert.ToDouble(textBox3.Text), 2));

            double E = (Xpribor / Xsred) * 100;

            double[] odin = new double[5];
            odin[0] = Math.Round(Xsred,2);
            odin[1] = Math.Round(Sx, 2);
            odin[2] = Math.Round(Xsl, 2);
            odin[3] = Math.Round(Xpribor, 2);
            odin[4] = Math.Round(E, 2);
            List<double[]> list = new List<double[]>() { odin , AbsX};

            return list;
        }

    }
}
