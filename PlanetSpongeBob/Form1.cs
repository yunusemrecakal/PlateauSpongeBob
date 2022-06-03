using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace PlanetSpongeBob
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Width = 800;
            this.Height = 500;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Location = new Point(this.Width - pictureBox1.Width - gridWidth , 0);
        }

        int gridWidth = 50;
        int rightCount;
        int leftCount;
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.R)
            {
                rightCount++;
                Bitmap bitmap = (Bitmap)pictureBox1.Image;
                pictureBox1.Image = (Image)(RotateImg(bitmap, 90.0f));
            }
            if (e.KeyCode == Keys.M)
            {
                string direction = FindRotate();

                switch (direction)
                {
                    case "E":
                        if (pictureBox1.Location.X <= this.Width-pictureBox1.Width-gridWidth - 10)
                        {
                            pictureBox1.Location = new Point(pictureBox1.Location.X + gridWidth, pictureBox1.Location.Y);
                        }
                        else MessageBox.Show("You can't leave away from SpongeBob Plateau");
                        break;
                    case "W":
                        if (pictureBox1.Location.X >= gridWidth )
                        {
                            pictureBox1.Location = new Point(pictureBox1.Location.X - gridWidth, pictureBox1.Location.Y);
                        }
                        else MessageBox.Show("You can't leave away from SpongeBob Plateau");
                        break;
                    case "N":
                        if (pictureBox1.Location.Y >= gridWidth )
                        {
                            pictureBox1.Location = new Point(pictureBox1.Location.X, pictureBox1.Location.Y - gridWidth);
                        }
                        else MessageBox.Show("You can't leave away from SpongeBob Plateau");
                        break;
                    case "S":
                        if (pictureBox1.Location.Y <= this.Height-pictureBox1.Height-gridWidth - 10)
                        {
                            pictureBox1.Location = new Point(pictureBox1.Location.X, pictureBox1.Location.Y + gridWidth);
                        }
                        else MessageBox.Show("You can't leave away from SpongeBob Plateau");
                        break;
                }
            }
            if (e.KeyCode == Keys.L)
            {
                leftCount++;
                Bitmap bitmap = (Bitmap)pictureBox1.Image;
                pictureBox1.Image = (Image)(RotateImg(bitmap, -90.0f));
            }
        }

        public string FindRotate()
        {
            int result = rightCount - leftCount;
            result = result % 4;
            switch (result)
            {
                case 0: return "N"; break;
                case 1: return "E"; break;
                case 2: return "S"; break;
                case 3: return "W"; break;
                case -1: return "W"; break;
                case -2: return "S"; break;
                case -3: return "E"; break;
            }
            return "";
        }

        public static Bitmap RotateImg(Bitmap bmp, float angle)
        {

            int w = bmp.Width;

            int h = bmp.Height;

            Bitmap tempImg = new Bitmap(w, h);

            Graphics g = Graphics.FromImage(tempImg);

            g.DrawImageUnscaled(bmp, 1, 1);

            g.Dispose();

            GraphicsPath path = new GraphicsPath();

            path.AddRectangle(new RectangleF(0f, 0f, w, h));

            Matrix mtrx = new Matrix();

            mtrx.Rotate(angle);

            RectangleF rct = path.GetBounds(mtrx);

            Bitmap newImg = new Bitmap(Convert.ToInt32(rct.Width), Convert.ToInt32(rct.Height));

            g = Graphics.FromImage(newImg);

            g.TranslateTransform(-rct.X, -rct.Y);

            g.RotateTransform(angle);

            g.InterpolationMode = InterpolationMode.HighQualityBilinear;

            g.DrawImageUnscaled(tempImg, 0, 0);

            g.Dispose();

            tempImg.Dispose();

            return newImg;

        }

        private void pictureBox1_LocationChanged(object sender, EventArgs e)
        {
            label1.Text = pictureBox1.Location.X.ToString() + "," + pictureBox1.Location.Y.ToString() + "," + FindRotate();
        }
    }
}
