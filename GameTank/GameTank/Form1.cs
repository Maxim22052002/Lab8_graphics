using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameTank
{
    public partial class Form1 : Form
    {
        PictureBox Tank = new PictureBox();//картинка танка
        
        PictureBox Lives = new PictureBox();
        PictureBox Bomb1 = new PictureBox();
        PictureBox Bomb2 = new PictureBox();
        PictureBox Bomb3 = new PictureBox();
        PictureBox Bomb4 = new PictureBox();
        PictureBox Bomb5 = new PictureBox();
        PictureBox Tree1 = new PictureBox();
        PictureBox Tree2 = new PictureBox();
        PictureBox Tree3 = new PictureBox();
        PictureBox Tree4 = new PictureBox();
        PictureBox Tree5 = new PictureBox();

        int lives = 2;
        int TankSpeed = 5;
        Timer timer;
        int x, y;
        public Form1() { InitializeComponent(); }

        private void Form1_Load(object sender, EventArgs e)
        {
            Tank.LocationChanged += Tank_LocationChanged;
            Bomb1.VisibleChanged += Bomb1_VisibleChanged;
            Bomb2.VisibleChanged += Bomb2_VisibleChanged;
            Bomb3.VisibleChanged += Bomb3_VisibleChanged;
            Bomb4.VisibleChanged += Bomb4_VisibleChanged;
            Bomb5.VisibleChanged += Bomb5_VisibleChanged;
            
            timer = new Timer();
            timer.Interval = 100;
            timer.Tick += new EventHandler(timer1_Tick);

            label1.Text = lives.ToString();

            Tank.Image = Image.FromFile(@"tank.png");
            Tank.BackColor = Color.Transparent;
            Tank.Location = new Point(0, pictureBox1.Size.Height / 2);
            Tank.Size = new Size(Tank.Image.Width, Tank.Image.Height);
            Tank.Name = "Game_Tank";
            pictureBox1.Controls.Add(Tank); 
            Tank.BringToFront();
            PrintBomb(Bomb1,700,800);
            PrintBomb(Bomb2, 1700, 700);
            PrintBomb(Bomb3, 1000,200);
            PrintBomb(Bomb4, 2000,1000);
            PrintBomb(Bomb5, 1700, 1300);

            PrintTree(Tree1, 100, 550);
            PrintTree(Tree2, 500, 500);
            PrintTree(Tree3, 700, 100);
            PrintTree(Tree4, 900, 10);
            PrintTree(Tree5, 400, 200);

            Lives.Image = Image.FromFile(@"lives.png");
            Lives.BackColor = Color.Transparent;
            Lives.Location = new Point(pictureBox1.Size.Width-150, 10);
            Lives.Size = new Size(Lives.Image.Width, Lives.Image.Height);
            Lives.Name = "Game_Lives";
            pictureBox1.Controls.Add(Lives);
            Lives.BringToFront();


        }
        private void PrintTree(PictureBox Tree, int width, int height)
        {
            Tree.Image = Image.FromFile(@"tree.png");
            Tree.BackColor = Color.Transparent;
            Tree.Location = new Point(width, height);
            Tree.Size = new Size(Tree.Image.Width, Tree.Image.Height);
            Tree.Name = "Game_Tree";
            pictureBox1.Controls.Add(Tree);
            Tree.BringToFront();
        }
        private void PrintBomb(PictureBox Bomb,int width,int height)
        {
            Bomb.Image = Image.FromFile(@"bomb.png");
            Bomb.BackColor = Color.Transparent;
            Bomb.Location = new Point(width / 2, height / 2);
            Bomb.Size = new Size(Bomb.Image.Width, Bomb.Image.Height);
            Bomb.Name = "Game_Bomb";
            pictureBox1.Controls.Add(Bomb); 
            Bomb.BringToFront();
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Tank.Left > pictureBox1.Image.Width)
            {
                timer1.Enabled = false;
                this.KeyPreview = false;
                
                var result = MessageBox.Show("Вы дошли до конца."+"Ваше здоровье: "+lives.ToString()+ "/2 Начать заново?", "Game over!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                if (result == DialogResult.OK)
                    Restart();


            }
            if (lives == 0)
            {
                timer1.Enabled = false;
                this.KeyPreview = false;
                var result = MessageBox.Show("Вы проиграли. Начать заново?", "Game over!",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                if (result == DialogResult.OK)
                    Restart();
            }
            
        }

        
       
        private void pictureBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = true;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            timer1.Enabled = true;
            Rectangle tankRec = Tank.DisplayRectangle;
            tankRec.Location = Tank.Location;
            Rectangle treeRec1 = Tree1.DisplayRectangle;
            treeRec1.Location = Tree1.Location;
            Rectangle treeRec2 = Tree2.DisplayRectangle;
            treeRec2.Location = Tree2.Location;
            Rectangle treeRec3 = Tree3.DisplayRectangle;
            treeRec3.Location = Tree3.Location;
            Rectangle treeRec4 = Tree4.DisplayRectangle;
            treeRec4.Location = Tree4.Location;
            Rectangle treeRec5 = Tree5.DisplayRectangle;
            treeRec5.Location = Tree5.Location;

            if (!tankRec.IntersectsWith(treeRec1)&&
                !tankRec.IntersectsWith(treeRec2)&&
                !tankRec.IntersectsWith(treeRec3)&& 
                !tankRec.IntersectsWith(treeRec4)&& 
                !tankRec.IntersectsWith(treeRec5))
            {
                x = Tank.Location.X;
                y = Tank.Location.Y;
                if (e.KeyCode == Keys.Right)
                {
                    if (Tank.Left + Tank.Width + TankSpeed < pictureBox1.Size.Width - 1 + Tank.Width)
                    { Tank.Left = Tank.Left + TankSpeed;   }
                }
                
                if (e.KeyCode == Keys.Left)
                {
                    if (Tank.Left >= TankSpeed) { Tank.Left = Tank.Left - TankSpeed;  }
                }
                if (e.KeyCode == Keys.Down)
                {
                    if (Tank.Top + Tank.Height + TankSpeed < pictureBox1.Size.Height - 1)
                    { Tank.Top = Tank.Top + TankSpeed;  }
                }
                if (e.KeyCode == Keys.Up)
                {
                    if (Tank.Top >= TankSpeed) { Tank.Top = Tank.Top - TankSpeed;  }
                }
               
            }
            else
            {
                Tank.Location = new Point(x, y);
                
            }

            



        }
        private void timer2_Tick_1(object sender, EventArgs e)
        {

        }

        private void Tank_LocationChanged(object sender, EventArgs e)
        {
            if (Tank.Bounds.IntersectsWith(Bomb1.Bounds)) 
                Bomb1.Visible = false;
            if (Tank.Bounds.IntersectsWith(Bomb2.Bounds)) 
               Bomb2.Visible = false;
            if (Tank.Bounds.IntersectsWith(Bomb3.Bounds))
                Bomb3.Visible = false;
            if (Tank.Bounds.IntersectsWith(Bomb4.Bounds))
                Bomb4.Visible = false;
            if (Tank.Bounds.IntersectsWith(Bomb5.Bounds))
                Bomb5.Visible = false;
        }

        private void Bomb1_VisibleChanged(object sender, EventArgs e)
        {
            if (Bomb1.Visible == false)
                lives--;
            label1.Text = lives.ToString()+"/2";
        }
        private void Bomb2_VisibleChanged(object sender, EventArgs e)
        {
            if (Bomb2.Visible == false)
                lives--;
            label1.Text = lives.ToString()+"/2";
        }
        private void Bomb3_VisibleChanged(object sender, EventArgs e)
        {
            if (Bomb3.Visible == false)
                lives--;
            label1.Text = lives.ToString() + "/2";
        }
        private void Bomb4_VisibleChanged(object sender, EventArgs e)
        {
            if (Bomb4.Visible == false)
                lives--;
            label1.Text = lives.ToString() + "/2";
        }
        private void Bomb5_VisibleChanged(object sender, EventArgs e)
        {
            if (Bomb5.Visible == false)
                lives--;
            label1.Text = lives.ToString() + "/2";
        }

        private void pictureBox1_VisibleChanged(object sender, EventArgs e)
        {

        }
        private void Restart()
        {
            Bomb1.Visible = true;
            Bomb2.Visible = true;
            Bomb3.Visible = true;
            Bomb4.Visible = true;
            Bomb5.Visible = true;
            lives = 2;
            label1.Text = lives.ToString() + "/2";
            Tank.Location = new Point(0, pictureBox1.Size.Height / 2);
        }







    }

}
