using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection.Emit;
using System.Timers;
using System.Threading;
using System.Drawing.Drawing2D;


namespace CSharpGDI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            DoubleBuffered = true;  //设置双缓冲
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            boxGraph = pictureBox1.CreateGraphics();

            CanvasWindow.width = pictureBox1.Width;
            CanvasWindow.height = pictureBox1.Height;
        }
        Bitmap currbmp = null;
        IParticle curParticle = null;
        string curParticleName = "";
        
        private void ResetButtons()
        {
            btnparticle1.Text = "抹纱窗粒子效果";
            btnParticle2.Text = "鼓泡泡效果";
            btnFontParticle.Text = "文字粒子效果";
            btn3dparticle.Text = "3D旋转粒子效果";
            btnFluid.Text = "水流体效果";
            btnMouseParticle.Text = "爱心效果";
            btnMathArc.Text = "数学曲线";
            btnBGtimer.Text = "八卦时钟";
            btnMagnetism.Text = "磁吸效果";
            MathGroup.Visible = false;
            label2.Text = "";
            limitFps = 0;
            fps = 0;
        }
        
        
        Graphics boxGraph = null;
        int fps = 0;
        int limitFps = 0;//限制fps,如果大于0，则起作用
        int limitFps_msPerFrame = 0;//每帧间隔毫秒数
        DateTime limitFps_lastCalTime = DateTime.Now;

        int iR = 0;//重绘次数
        DateTime lastCalTime = DateTime.Now;

        private void SetLimitFps(int limit)
        {
            limitFps = limit;
            limitFps_msPerFrame = 1000 / limit;
            limitFps_lastCalTime=DateTime.Now.AddSeconds(-1);
        }
        private void ReStartCalFps()
        {
            iR = 0;
            lastCalTime = DateTime.Now;
        }
        private void CalFps()
        {
            iR++;
            if ((DateTime.Now - lastCalTime).TotalMilliseconds >= 1000)
            {
                fps = iR;
                label1.Text = "fps="+fps;
                ReStartCalFps();
            }
        }
        //重绘事件
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            
            if (curParticle == null) return;
            //限制fps
            if (limitFps > 0
                && (DateTime.Now - limitFps_lastCalTime).TotalMilliseconds < limitFps_msPerFrame)
            {
                pictureBox1.Refresh();
                return;
            }
            limitFps_lastCalTime = DateTime.Now;

            currbmp = curParticle.Update();
            pictureBox1.Image = currbmp;
            CalFps();
        }

        int V = 0;//移动方向 0：x  1：y
        //鼠标移动事件
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Math.Abs(e.X - CanvasWindow.mouseX) > Math.Abs(e.Y - CanvasWindow.mouseY))
                V = 0;
            else
                V = 1;

            CanvasWindow.mouseX = e.Location.X;
            CanvasWindow.mouseY = e.Location.Y;
            CanvasWindow.mouse_V = V;

            if (curParticle!=null&&curParticleName == "爱心效果")
            {
                ((Particle6)curParticle).CreateParticles(e.X, e.Y, 0);
            }
         }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            CanvasWindow.isMouseDown = true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            CanvasWindow.isMouseDown = false;
            if (curParticle != null && curParticleName == "爱心效果")
            {
                ((Particle6)curParticle).CreateParticles(e.X, e.Y, 1);
            }
        }
        //鼠标单击事件
        private void butonClick(Button b, IParticle p)
        {
            if (b.Text.Contains("停止"))
            {
                b.Text = b.Text.Replace("停止","");
                curParticle = null;
            }
            else
            {
                ResetButtons();
                curParticle = p;
                //初始化
                curParticle.Start();
                curParticleName = b.Text;
                ReStartCalFps();
                pictureBox1.Refresh();
                b.Text = "停止" + b.Text;
            }
        }

        //抹纱窗粒子效果
        private void btnparticle1_Click(object sender, EventArgs e)
        {
            butonClick((Button)sender, new Particle1());
        }

        private void btnParticle2_Click(object sender, EventArgs e)
        {
            butonClick((Button)sender, new Particle2());
        }

        private void btnFontParticle_Click(object sender, EventArgs e)
        {
            butonClick((Button)sender, new Particle3());
        }

       
        private void btn3dparticle_Click(object sender, EventArgs e)
        {
            butonClick((Button)sender, new Particle4());
        }

        private void btnFluid_Click(object sender, EventArgs e)
        {
            butonClick((Button)sender, new Particle5());
            label2.Text = "有鼠标点击效果";
           
        }
        private void btnBGtimer_Click(object sender, EventArgs e)
        {
            butonClick((Button)sender, new Particle9());
        }
        private void btnMouseParticle_Click(object sender, EventArgs e)
        {
            butonClick((Button)sender, new Particle6());
            //需要限制fps//需要限制fps
            if (btnMouseParticle.Text.Contains("停止"))
                SetLimitFps(90);
            else
                limitFps = 0;

        }

        private void btnMathArc_Click(object sender, EventArgs e)
        {
            butonClick((Button)sender, new Particle7());
            if (btnMathArc.Text.Contains("停止"))
            {
                MathGroup.Visible = true;
                trackBar1_split.Value = 100;
                //需要限制fps
                SetLimitFps(90);
            }
            else
            {
                limitFps = 0;
            }
        }
        private void btnMagnetism_Click(object sender, EventArgs e)
        {
            butonClick((Button)sender, new Particle8());
        }

        private void trackBar1_split_ValueChanged(object sender, EventArgs e)
        {
            if(curParticle!=null)
                ((Particle7)curParticle).coSplit = trackBar1_split.Value;
        }

        private void btnNextMath_Click(object sender, EventArgs e)
        {
            if (curParticle != null)
                ((Particle7)curParticle).curMath = ((Particle7)curParticle).curMath + 1;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            CanvasWindow.width = pictureBox1.Width;
            CanvasWindow.height = pictureBox1.Height;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            CanvasWindow.isMouseIn = true;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            CanvasWindow.isMouseIn = false;
        }

       
    }
}

