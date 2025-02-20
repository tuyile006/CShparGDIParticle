using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpGDI
{
    /// <summary>
    /// 粒子效果2
    /// </summary>
    internal class Particle2
    {
        public struct ParticleObj
        {
            public int px;//圆点坐标x
            public int py;//圆点坐标y
            public int d;//直径
            public int c;//颜色标识 1有颜色，0无色
            public int a;//透明度
            public Brush b;//画刷

            public int vx; //原x
            public int vy; //原y
            public int vd; //原直径
        }
        static List<ParticleObj> particles = new List<ParticleObj>();
        static int speed =1;
        static Bitmap bmp = null; 
        static Graphics g = null;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="w">宽</param>
        /// <param name="h">高</param>
        /// <param name="span">间距</param>
        public static void Init(int w,int h,int size=15,int span=5)
        {
            particles.Clear();
            Random rnd=new Random();
            int r0 = Math.Min(w / size, h / size);
            for (var x = 0; x < w / r0; x++)
            {
                for (var y = 0; y < h / r0; y++)
                {
                    ParticleObj p = new ParticleObj()
                    {
                        px = x * r0 + (r0 - span)/2,
                        py = y * r0 + (r0 - span)/2,
                        d = r0-span,
                        c = rnd.Next(2),
                        a = rnd.Next(100, 255)
                    };
                    p.vx = p.px;
                    p.vy = p.py;
                    p.vd = p.d;
                   

                    
                    Brush b = new SolidBrush(Color.FromArgb(p.a, 255, 255, 255));
                    if (p.c == 1)
                        b = new SolidBrush(Color.FromArgb(p.a,72,209,204));
                    p.b = b;
                    particles.Add(p);
                }
            }

            bmp = new Bitmap(w, h, PixelFormat.Format32bppArgb);
            g = Graphics.FromImage(bmp);
        }

        /// <summary>
        /// 动画 
        /// </summary>
        /// <param name="mouseX">鼠标x坐标</param>
        /// <param name="mouseY"></param>
        /// <param name="w">宽</param>
        /// <param name="h">高</param>
        /// <param name="R">圆的半径</param>
        public static Bitmap Start(int mouseX, int mouseY, int w, int h, int R=150)
        {
            if (particles.Count == 0) 
                Init(w,h);

            g.Clear(Color.Black);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Random rnd = new Random();
            for (var i = 0; i < particles.Count; i++)
            {
                var p = particles[i];
                if (R * R > (p.px - mouseX) * (p.px - mouseX) + (p.py - mouseY) * (p.py - mouseY))
                {
                    if (p.px > mouseX)
                        p.px += speed ;
                    if (p.px < mouseX)
                        p.px -= speed;
                    if (p.py > mouseY)
                        p.py += speed ;
                    if (p.py < mouseY)
                        p.py -= speed;

                    if (rnd.Next(2) == 1)
                    {
                        p.d = (p.d+speed*5 >w/2?w/2:p.d+speed*5);
                    }
                    else
                    {
                        p.d =(p.d- speed*5<=0?10:p.d-speed*5);
                    }
                }
                else
                {
                    if (p.px > p.vx)
                        p.px = p.px - speed < p.vx ? p.vx : p.px - speed;
                    if (p.px < p.vx)
                        p.px = p.px + speed > p.vx ? p.vx : p.px + speed;
                    if (p.py > p.vy)
                        p.py = p.py - speed < p.vy ? p.vy : p.py - speed;
                    if (p.py < p.vy)
                        p.py = p.py + speed > p.vy ? p.vy : p.py + speed;

                    if (p.d > p.vd)
                    {
                        p.d -= speed;
                    }
                    if (p.d < p.vd)
                    {
                        p.d += speed;
                    }
                }
                particles[i] = p;
                g.FillEllipse(p.b, p.px - p.d / 2, p.py - p.d / 2, p.d, p.d);


            }
            return bmp;
        }
    }
}
