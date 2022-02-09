using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace game2048
{
	public partial class Form1 : Form
	{
        public int[,] map = new int[4, 4];
        public Label[,] labels = new Label[4, 4];
        public PictureBox[,] pics = new PictureBox[4, 4];

        private int score = 0;

        public Form1()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(OnKeyboardPressed);

            map[0, 0] = 1;
            map[0, 1] = 1;

            CreateMap();
            CreatePics();
            GenerateNewPic();
        }

        private void CreateMap()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    PictureBox pic = new PictureBox();

                    pic.Location = new Point(10 + 106 * j, 63 + 106 * i);
                    pic.Size = new Size(100, 100);
                    pic.BackColor = Color.Gray;

                    this.Controls.Add(pic);
                }
            }
        }

        private void CreatePics()
        {
            pics[0, 0] = new PictureBox();

            labels[0, 0] = new Label();
            labels[0, 0].Text = "2";
            labels[0, 0].Size = new Size(100, 100);
            labels[0, 0].TextAlign = ContentAlignment.MiddleCenter;
            labels[0, 0].Font = new Font(new FontFamily("Microsoft Sans Serif"), 15);

            pics[0, 0].Controls.Add(labels[0, 0]);
            pics[0, 0].Location = new Point(10, 63);
            pics[0, 0].Size = new Size(100, 100);
            pics[0, 0].BackColor = Color.Pink;

            this.Controls.Add(pics[0, 0]);
            pics[0, 0].BringToFront();


            pics[0, 1] = new PictureBox();

            labels[0, 1] = new Label();
            labels[0, 1].Text = "2";
            labels[0, 1].Size = new Size(100, 100);
            labels[0, 1].TextAlign = ContentAlignment.MiddleCenter;
            labels[0, 1].Font = new Font(new FontFamily("Microsoft Sans Serif"), 15);

            pics[0, 1].Controls.Add(labels[0, 1]);
            pics[0, 1].Location = new Point(116, 63);
            pics[0, 1].Size = new Size(100, 100);
            pics[0, 1].BackColor = Color.Pink;

            this.Controls.Add(pics[0, 1]);
            pics[0, 1].BringToFront();
        }

        private void GenerateNewPic()
        {
            Random rnd = new Random();

            int a = rnd.Next(0, 4);
            int b = rnd.Next(0, 4);

            while (pics[a, b] != null)
            {
                a = rnd.Next(0, 4);
                b = rnd.Next(0, 4);
            }

            map[a, b] = 1;

            pics[a, b] = new PictureBox();

            labels[a, b] = new Label();
            labels[a, b].Text = "2";
            labels[a, b].Size = new Size(100, 100);
            labels[a, b].TextAlign = ContentAlignment.MiddleCenter;
            labels[a, b].Font = new Font(new FontFamily("Microsoft Sans Serif"), 15);

            pics[a, b].Controls.Add(labels[a, b]);
            pics[a, b].Location = new Point(10 + b * 106, 63 + 106 * a);
            pics[a, b].Size = new Size(100, 100);
            pics[a, b].BackColor = Color.Pink;

            this.Controls.Add(pics[a, b]);
            pics[a, b].BringToFront();
        }

        private void ChangeColor(int sum, int k, int j)
        {
            if (sum % 2048 == 0) pics[k, j].BackColor = Color.Gold;
            else if (sum % 1024 == 0) pics[k, j].BackColor = Color.Green;
            else if (sum % 512 == 0) pics[k, j].BackColor = Color.Red;
            else if (sum % 256 == 0) pics[k, j].BackColor = Color.Blue;
            else if (sum % 128 == 0) pics[k, j].BackColor = Color.Honeydew;
            else if (sum % 64 == 0) pics[k, j].BackColor = Color.LightCoral;
            else if (sum % 32 == 0) pics[k, j].BackColor = Color.Indigo;
            else if (sum % 16 == 0) pics[k, j].BackColor = Color.LightSalmon;
            else if (sum % 8 == 0) pics[k, j].BackColor = Color.GreenYellow;
            else pics[k, j].BackColor = Color.LightBlue;
        }

        private void OnKeyboardPressed(object sender, KeyEventArgs e)
        {
            bool ifPicWasMoved = false;

            switch (e.KeyCode.ToString())
            {
                case "Right":
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 2; j >= 0; j--)
                        {
                            if (map[i, j] != 0)
                            {
                                for (int n = j + 1; n < 4; n++)
                                {
                                    if (map[i, n] == 0)
                                    {
                                        ifPicWasMoved = true;

                                        map[i, n - 1] = 0;
                                        map[i, n] = 1;

                                        labels[i, n] = labels[i, n - 1];
                                        labels[i, n - 1] = null;

                                        pics[i, n] = pics[i, n - 1];
                                        pics[i, n - 1] = null;
                                        pics[i, n].Location = new Point(pics[i, n].Location.X + 106, pics[i, n].Location.Y);
                                    }
                                    else
                                    {
                                        int a = int.Parse(labels[i, n].Text);
                                        int b = int.Parse(labels[i, n - 1].Text);

                                        if (a == b)
                                        {
                                            ifPicWasMoved = true;

                                            labels[i, n].Text = (a + b).ToString();

                                            score += (a + b);
                                            label1.Text = "Total score: " + score;

                                            ChangeColor(a + b, i, n);

                                            map[i, n - 1] = 0;
                                            this.Controls.Remove(pics[i, n - 1]);
                                            this.Controls.Remove(labels[i, n - 1]);
                                            pics[i, n - 1] = null;
                                            labels[i, n - 1] = null;
                                        }
                                    }
                                }
                            }
                        }
                    }
                break;
                case "Left":
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 1; j < 4; j++)
                        {
                            if (map[i, j] == 1)
                            {
                                for (int n = j - 1; n >= 0; n--)
                                {
                                    if (map[i, n] == 0)
                                    {
                                        ifPicWasMoved = true;

                                        map[i, n + 1] = 0;
                                        map[i, n] = 1;

                                        labels[i, n] = labels[i, n + 1];
                                        labels[i, n + 1] = null;

                                        pics[i, n] = pics[i, n + 1];
                                        pics[i, n + 1] = null;
                                        pics[i, n].Location = new Point(pics[i, n].Location.X - 106, pics[i, n].Location.Y);
                                    }
                                    else
                                    {
                                        int a = int.Parse(labels[i, n].Text);
                                        int b = int.Parse(labels[i, n + 1].Text);

                                        if (a == b)
                                        {
                                            ifPicWasMoved = true;

                                            labels[i, n].Text = (a + b).ToString();

                                            score += (a + b);
                                            label1.Text = "Total score: " + score;

                                            ChangeColor(a + b, i, n);

                                            map[i, n + 1] = 0;
                                            this.Controls.Remove(pics[i, n + 1]);
                                            this.Controls.Remove(labels[i, n + 1]);
                                            pics[i, n + 1] = null;
                                            labels[i, n + 1] = null;
                                        }
                                    }
                                }
                            }
                        }
                    }
                break;
                case "Down":
                    for (int k = 2; k >= 0; k--)
                    {
                        for (int l = 0; l < 4; l++)
                        {
                            if (map[k, l] == 1)
                            {
                                for (int j = k + 1; j < 4; j++)
                                {
                                    if (map[j, l] == 0)
                                    {
                                        ifPicWasMoved = true;
                                        map[j - 1, l] = 0;
                                        map[j, l] = 1;
                                        pics[j, l] = pics[j - 1, l];
                                        pics[j - 1, l] = null;
                                        labels[j, l] = labels[j - 1, l];
                                        labels[j - 1, l] = null;
                                        pics[j, l].Location = new Point(pics[j, l].Location.X, pics[j, l].Location.Y + 106);
                                    }
                                    else
                                    {
                                        int a = int.Parse(labels[j, l].Text);
                                        int b = int.Parse(labels[j - 1, l].Text);
                                        if (a == b)
                                        {
                                            ifPicWasMoved = true;
                                            labels[j, l].Text = (a + b).ToString();
                                            score += (a + b);
                                            ChangeColor(a + b, j, l);
                                            label1.Text = "Total score: " + score;
                                            map[j - 1, l] = 0;
                                            this.Controls.Remove(pics[j - 1, l]);
                                            this.Controls.Remove(labels[j - 1, l]);
                                            pics[j - 1, l] = null;
                                            labels[j - 1, l] = null;
                                        }
                                    }
                                }
                            }
                        }
                    }
                break;
                case "Up":
                    for (int k = 1; k < 4; k++)
                    {
                        for (int l = 0; l < 4; l++)
                        {
                            if (map[k, l] == 1)
                            {
                                for (int j = k - 1; j >= 0; j--)
                                {
                                    if (map[j, l] == 0)
                                    {
                                        ifPicWasMoved = true;
                                        map[j + 1, l] = 0;
                                        map[j, l] = 1;
                                        pics[j, l] = pics[j + 1, l];
                                        pics[j + 1, l] = null;
                                        labels[j, l] = labels[j + 1, l];
                                        labels[j + 1, l] = null;
                                        pics[j, l].Location = new Point(pics[j, l].Location.X, pics[j, l].Location.Y - 106);
                                    }
                                    else
                                    {
                                        int a = int.Parse(labels[j, l].Text);
                                        int b = int.Parse(labels[j + 1, l].Text);
                                        if (a == b)
                                        {
                                            ifPicWasMoved = true;
                                            labels[j, l].Text = (a + b).ToString();
                                            score += (a + b);
                                            ChangeColor(a + b, j, l);
                                            label1.Text = "Total score: " + score;
                                            map[j + 1, l] = 0;
                                            this.Controls.Remove(pics[j + 1, l]);
                                            this.Controls.Remove(labels[j + 1, l]);
                                            pics[j + 1, l] = null;
                                            labels[j + 1, l] = null;
                                        }
                                    }
                                }
                            }
                        }
                    }
                break;
            }

            if (ifPicWasMoved)
                GenerateNewPic();
        }
	}   
}
