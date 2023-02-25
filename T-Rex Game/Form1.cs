using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T_Rex_Game
{
    public partial class Form1 : Form
    {
        int position;
        int ObstacleSpeed = 10;
        Random ran = new Random();
        int force = 12;
        bool Jumping = false;
        bool IsGameOver = false;
        int score = 0;
        int JumpSpeed = 12;

        public Form1()
        {
            InitializeComponent();
            //GameReset();
        }

        private void MainGameTimer(object sender, EventArgs e)
        {
            trex.Top += JumpSpeed;
           textScore.Text = "Score : " + score;
           
            if (Jumping==true && force<0)
            {
                Jumping = false;
            }
            if (Jumping == true )
            {
                force -= 1;
                JumpSpeed =- 12;
            }
            else
            {
                JumpSpeed = 12;
            }
            if (trex.Top > 280 && Jumping == false )
            {
                force = 12;
                JumpSpeed = 0;
                trex.Top = 287;
            }
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "obstacle")
                {
                    x.Left -= ObstacleSpeed;
                    if (x.Left < -100)
                    {
                        position = this.ClientSize.Width +ran.Next(500,800)+ (x.Width * 25);
                        x.Left = position;
                        score++;
                        if (score >= 10)
                        {
                            ObstacleSpeed = 15;
                        }

                    }

                    if (trex.Bounds.IntersectsWith(x.Bounds))
                    {
                        gameTimer.Stop();
                        trex.Image = Properties.Resources.dead;
                        textScore.Text += "   Press R for Restart the game!";
                        IsGameOver = true;
                    }
                }
            }
        }
        private void GameReset()
        {
            force = 12;
            Jumping = false;
            JumpSpeed=0;
            score = 0;
            ObstacleSpeed = 10;
            trex.Image = Properties.Resources.running;
            IsGameOver = false;
            trex.Top = 287;
            textScore.Text = "Score :" + score;
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "obstacle")
                {
                    position = this.ClientSize.Width+ran.Next(500, 800)+(x.Width*25);
                    x.Left = position;
                }
            }
            gameTimer.Start();
            

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && Jumping == false)
            {
                Jumping = true;
            }
           
        }
     

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (Jumping == true)
            {
                Jumping = false;
            }
            if (e.KeyCode == Keys.R && IsGameOver == true)
            {
                GameReset();
            }
        }

      
    }
}
