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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

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
            animations.Clear();
        }
        Bitmap currbmp = null;

        private void ResetButtons()
        {
            btnparticle1.Text = "抹纱窗粒子效果";
            btnParticle2.Text = "鼓泡泡效果";
            btnFontParticle.Text = "文字粒子效果";
            btn3dparticle.Text = "3D旋转粒子效果";
            btnFluid.Text = "水流体效果";
            btnMouseParticle.Text = "鼠标粒子效果";
            groupBox1.Visible = false;
            label2.Text = "";
            limitFps = 0;
            fps = 0;
        }
        
        List<string> animations = new List<string>();

        
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
            
            if (animations.Count==0) return;
            //限制fps
            if (limitFps > 0
                && (DateTime.Now - limitFps_lastCalTime).TotalMilliseconds < limitFps_msPerFrame)
            {
                pictureBox1.Refresh();
                return;
            }
            limitFps_lastCalTime = DateTime.Now;
            if (animations.Contains("抹纱窗"))
            {
                currbmp=Particle1.Start(mousePoint.X,mousePoint.Y,pictureBox1.Width,pictureBox1.Height,100,V);
                pictureBox1.Image = currbmp;
                CalFps();
            }

            if (animations.Contains("鼓泡泡"))
            {
                currbmp = Particle2.Start(mousePoint.X, mousePoint.Y, pictureBox1.Width, pictureBox1.Height,150);
                pictureBox1.Image = currbmp;
                CalFps();
            }

            if (animations.Contains("文字粒子"))
            {
                currbmp = Particle3.Start(mousePoint.X, mousePoint.Y, pictureBox1.Width, pictureBox1.Height, 100);
                pictureBox1.Image = currbmp;
                CalFps();
            }

            if (animations.Contains("3D粒子"))
            {
                currbmp = Particle4.Start(pictureBox1.Width, pictureBox1.Height);
                pictureBox1.Image = currbmp;
                CalFps();
            }

            if (animations.Contains("水流体"))
            {
                currbmp = Particle5.Start(mousePoint.X, mousePoint.Y, isMouseDown, pictureBox1.Width, pictureBox1.Height);
                pictureBox1.Image = currbmp;
                CalFps();
            }

            if (animations.Contains("鼠标粒子"))
            {
                currbmp = Particle6.Start(pictureBox1.Width, pictureBox1.Height);
                pictureBox1.Image = currbmp;
                CalFps();
            }

            if (animations.Contains("数学曲线"))
            {
                currbmp = Particle7.Start(pictureBox1.Width, pictureBox1.Height);
                pictureBox1.Image = currbmp;
                CalFps();
            }
        }

        Point mousePoint=new Point(0,0);
        int V = 0;//移动方向 0：x  1：y
        bool isMouseDown = false;
        //鼠标移动事件
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Math.Abs(e.X - mousePoint.X) > Math.Abs(e.Y - mousePoint.Y))
                V = 0;
            else
                V = 1;

            mousePoint = e.Location;

            if (animations.Contains("鼠标粒子"))
            {
                Particle6.CreateParticles(e.X, e.Y, 0);
            }
         }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            if (animations.Contains("鼠标粒子"))
            {
                Particle6.CreateParticles(e.X, e.Y, 1);
            }
        }

        //抹纱窗粒子效果
        private void btnparticle1_Click(object sender, EventArgs e)
        {
            if (btnparticle1.Text.Contains("停止"))
            {
                btnparticle1.Text = "抹纱窗粒子效果";
                animations.Clear();
            }
            else
            {
                animations.Clear();
                ResetButtons();
                btnparticle1.Text = "停止抹纱窗粒子效果";
                //初始化
                Particle1.Init(boxGraph, pictureBox1.Width, pictureBox1.Height);

                animations.Add("抹纱窗");
                ReStartCalFps();
                pictureBox1.Refresh();
            }
        }

        private void btnParticle2_Click(object sender, EventArgs e)
        {

            if (btnParticle2.Text.Contains("停止"))
            {
                btnParticle2.Text = "鼓泡泡效果";
                animations.Clear();
            }
            else
            {
                animations.Clear();
                ResetButtons();
                btnParticle2.Text = "停止鼓泡泡效果";

                animations.Add("鼓泡泡");
                ReStartCalFps();
                pictureBox1.Refresh();
            }
        }

        private void btnFontParticle_Click(object sender, EventArgs e)
        {
            if (btnFontParticle.Text.Contains("停止"))
            {
                btnFontParticle.Text = "文字粒子效果";
                animations.Clear();
            }
            else
            {
                animations.Clear();
                ResetButtons();
                btnFontParticle.Text = "停止文字粒子效果";
                //初始化
                pictureBox1.Image = Particle3.Init(pictureBox1.Width, pictureBox1.Height,"Happy",180);

                animations.Add("文字粒子");
                ReStartCalFps();
                pictureBox1.Refresh();
            }

        }

       

        private void btn3dparticle_Click(object sender, EventArgs e)
        {
            if (btn3dparticle.Text.Contains("停止"))
            {
                btn3dparticle.Text = "3D旋转粒子效果";
                animations.Clear();
            }
            else
            {
                animations.Clear();
                ResetButtons();
                btn3dparticle.Text = "停止3D旋转粒子效果";

                animations.Add("3D粒子");
                ReStartCalFps();
                pictureBox1.Refresh();
            }
        }

        private void btnFluid_Click(object sender, EventArgs e)
        {
            if (btnFluid.Text.Contains("停止"))
            {
                btnFluid.Text = "水流体效果";
                animations.Clear();
            }
            else
            {
                animations.Clear();
                ResetButtons();
                btnFluid.Text = "停止水流体效果";
                label2.Text = "有鼠标点击效果";
                animations.Add("水流体");
                ReStartCalFps();
                pictureBox1.Refresh();
            }

            
        }
        private void btnMouseParticle_Click(object sender, EventArgs e)
        {
            if (btnMouseParticle.Text.Contains("停止"))
            {
                btnMouseParticle.Text = "鼠标粒子效果";
                animations.Clear();
                limitFps = 0;
            }
            else
            {
                animations.Clear();
                ResetButtons();
                btnMouseParticle.Text = "停止鼠标粒子效果";
                label2.Text = "有鼠标点击、拖动效果";
                //需要限制fps
                SetLimitFps(90);
                animations.Add("鼠标粒子");
                ReStartCalFps();
                pictureBox1.Refresh();
            }
        }
      

        private void btnMathArc_Click(object sender, EventArgs e)
        {
            if (btnMathArc.Text.Contains("停止"))
            {
                groupBox1.Visible = false;
                btnMathArc.Text = "数学曲线";
                animations.Clear();
                limitFps = 0;
            }
            else
            {
                animations.Clear();
                ResetButtons();
                btnMathArc.Text = "停止数学曲线效果";
                
                groupBox1.Visible = true;
                trackBar1_split.Value = Particle7.coSplit;

                //需要限制fps
                SetLimitFps(90);
                animations.Add("数学曲线");
                ReStartCalFps();
                pictureBox1.Refresh();
            }
        }

        private void trackBar1_split_ValueChanged(object sender, EventArgs e)
        {
            Particle7.coSplit = trackBar1_split.Value;
        }

        private void btnNextMath_Click(object sender, EventArgs e)
        {
            Particle7.curMath = Particle7.curMath + 1;
        }
    }
}
