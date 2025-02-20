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
    /// 粒子效果3
    /// </summary>
    internal class Particle3
    {
        public struct ParticleObj
        {
            public double x;//圆点坐标x
            public double y;//圆点坐标y
            public int d;//直径
            public Int32 c;//颜色
            public int a;//透明度
            public Brush b;//画刷
            public int dx; //原x
            public int dy; //原y

            public double vx; //x速度
            public double vy; //y速度
            public double accX;//x加速度
            public double accY;//y加速度
            public double friction;//摩擦系数

        }
        static List<ParticleObj> particles = new List<ParticleObj>();

        static int speed0 = 5;//初速度
        static Bitmap dstBitmap;
        static Graphics g;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="g">Graphics对象</param>
        /// <param name="w">宽</param>
        /// <param name="h">高</param>
        /// <param name="txt">文字内容</param>
        /// <param name="size">文字大小</param>
        public static Bitmap Init(int w,int h,string txt="Happy",int size=150)
        {
            dstBitmap = new Bitmap(w, h, PixelFormat.Format24bppRgb);
            g= Graphics.FromImage(dstBitmap);
            g.Clear(Color.Black);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            string txtShow = "Happy";
            Font txtFont = new Font("Baloo", size);
            SizeF sf = g.MeasureString(txtShow, txtFont);
            g.DrawString(txtShow, txtFont, Brushes.White, (w - sf.Width) / 2, (h-sf.Height)/2);
            
            

            BitmapData data = dstBitmap.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            int length = h * data.Stride;
            byte[] ARGB = new byte[length];
            System.IntPtr Scan0 = data.Scan0;
            System.Runtime.InteropServices.Marshal.Copy(Scan0, ARGB, 0, length);

            particles.Clear();
            var colors = new Int32[] { 0x468966, 0xFFF0A5, 0xFFB03B, 0xB64926, 0x8E2800 };
            Random rnd=new Random();
            int i = 0;
            //获取像素点
            for (var y = 0; y < h; y = y +(int)h/100)
            {
                i = y * data.Stride;
                for (var x = 0; x < w; x=x+ (int)w/100)
                {
                    if (ARGB[i + x * 3] > 150)
                    {
                        ParticleObj p = new ParticleObj();
                        p.d = rnd.Next(4, 14);
                        p.x = w * rnd.NextDouble();
                        p.y = h * rnd.NextDouble();
                        //p.x = x;
                        //p.y = y;
                        p.dx = x;
                        p.dy = y;
                        p.a = rnd.Next(150, 255);
                        p.c = colors[rnd.Next(colors.Length)];
                        p.b = new SolidBrush(Color.FromArgb(p.a, Color.FromArgb(p.c)));

                        p.vx = (rnd.NextDouble() - 0.5) * 20;
                        p.vy = (rnd.NextDouble() - 0.5) * 20;
                        p.accX = 0;
                        p.accY = 0;
                        p.friction = rnd.NextDouble() * 0.05 + 0.94;

                        particles.Add(p);

                    }
                }
            }
            dstBitmap.UnlockBits(data);

            //画图
            g.Clear(Color.Black);
            for (int j = 0; j < particles.Count; j++)
            { 
                var p = particles[j];
                g.FillEllipse(p.b, (int)p.x - p.d / 2, (int)p.y - p.d / 2, p.d, p.d);
            }
            return dstBitmap;
        }

        //动画 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mouseX">鼠标x坐标</param>
        /// <param name="mouseY"></param>
        /// <param name="w">宽</param>
        /// <param name="h">高</param>
        /// <param name="R">圆的半径</param>
        public static Bitmap Start(int mouseX, int mouseY, int w, int h, int R=80)
        {
            if (particles.Count == 0) 
                Init(w,h);
            g.Clear(Color.Black);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            for (var i = 0; i < particles.Count; i++)
            {
                var p = particles[i];
                p.accX = (double)(p.dx - p.x) / 1000;
                p.accY = (double)(p.dy - p.y) / 1000;
                p.vx += p.accX;
                p.vy += p.accY;
                p.vx *= p.friction;
                p.vy *= p.friction;

                p.x +=p.vx;
                p.y +=p.vy;

                int a = (int)p.x - mouseX;
                int b = (int)p.y - mouseY;
                double distance=Math.Sqrt(a*a + b*b);
                if (distance < R)
                {
                    p.accX = (double)(p.x - mouseX) / 100;
                    p.accY = (double)(p.y - mouseY) / 100;
                    p.vx += p.accX;
                    p.vy += p.accY;
                }
                particles[i] = p;
                g.FillEllipse(p.b, (int)p.x - p.d / 2, (int)p.y - p.d / 2, p.d, p.d);
            }
            return dstBitmap;
        }
    }
}
