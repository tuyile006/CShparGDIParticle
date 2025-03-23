using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace CSharpGDI
{
    /// <summary>
    /// 粒子效果6——鼠标粒子
    /// </summary>
    internal class Particle6
    {
        public struct ParticleObj
        {
            public double x;//圆点坐标x
            public double y;//圆点坐标y
            public double mx; //x移动距离
            public double my; //y移动距离
            public double size;
            public double decay;
            public double speed;
            public double spread;
            public double spreadX;
            public double spreadY;
            public SolidBrush b;

        }
        static List<ParticleObj> particles = new List<ParticleObj>();

        //鼠标相关
        static int MouseX;
        static int MouseY;
        static int LastMouseX;
        static int LastMouseY;

        static Bitmap dstBitmap;
        static Graphics g;
        static int hue=0;//色相（hue）HSL色系中的参数
        static int step = 0;//步数

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="w">宽</param>
        /// <param name="h">高</param>
        public static Bitmap Init(int w,int h)
        {
            dstBitmap = new Bitmap(w, h, PixelFormat.Format24bppRgb);
            g= Graphics.FromImage(dstBitmap);
            g.Clear(Color.Black);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            hue = 0;

            return dstBitmap;
        }

       
        /// <summary>
        /// 动画 
        /// </summary>
        /// <param name="w">宽</param>
        /// <param name="h">高</param>
        public static Bitmap Start(int w, int h)
        {
            if (dstBitmap==null) 
                Init(w,h);
            g.Clear(Color.Black);

            step++;
            step = step > 200 ? 0 : step;

            DrawHeart(w, h, step);

            for (int i = 0; i < particles.Count; i++)
            {
                if (particles[i].size <= 0.1)
                {
                    particles.RemoveAt(i);
                    i--;
                    continue; 
                }

                ParticleObj p = particles[i];

                g.FillEllipse(p.b,(float) p.x, (float)p.y, (float)p.size, (float)p.size);
               
                p.x += p.spreadX * p.size;
                p.y += p.spreadY * p.size;
                p.size -= p.decay;
                particles[i] = p;

            }
            return dstBitmap;
        }

        //创建一簇粒子
        /// <summary>
        /// 创建粒子 用于鼠标事件
        /// </summary>
        /// <param name="mouseX"></param>
        /// <param name="mouseY"></param>
        /// <param name="eventType">事件类型 0--move;   1--click</param>
        public static void  CreateParticles(int mouseX, int mouseY,int eventType) 
        {
            LastMouseX = MouseX;
            LastMouseY = MouseY;
            MouseX = mouseX;
            MouseY = mouseY;
            int count=20;
            double speed=0, spread=0;
            Random rnd = new Random();
            if (eventType == 1)//click
            {
                count = 300;
                speed = rnd.NextDouble() + 1;
                spread = rnd.NextDouble() + 50;
            }
            if (eventType == 0)//move
            {
                count = 20;
                double a = mouseX- LastMouseX;
                double b = mouseY- LastMouseY;
                speed = Math.Floor(Math.Sqrt(a * a + b * b));
                spread = 1;
            }
            hue += 3;
            hue = hue > 360 ? 0 :hue;
            Color c= HslToRgb(hue, 90, 60);
            SolidBrush brush =  new SolidBrush(c);

            for (int i = 0; i<count; i++) 
            {
                ParticleObj p = new ParticleObj();
                p.x = mouseX;
                p.y = mouseY;
                p.mx = (mouseX-LastMouseX) * 0.1;
                p.my = (mouseY-LastMouseY) * 0.1;
                p.size= rnd.NextDouble() +4;
                p.decay = 0.03;
                p.speed = speed * 0.08;
                p.spread = spread*p.speed;
                p.spreadX = (rnd.NextDouble() - 0.5) * p.spread - p.mx;
                p.spreadY = (rnd.NextDouble() - 0.5) * p.spread - p.my;
              
                p.b = brush;
                particles.Add(p);
            }
        }



        static double lastx=0; //用于DrawHeart 使之不干扰鼠标
        static double lasty=0; //用于DrawHeart
        /// <summary>
        /// 画心型，一点一点画
        /// </summary>
        /// <param name="w"></param>
        /// <param name="h"></param>
        /// <param name="step">步数0-200</param>
        static void DrawHeart(int w, int h,int step)
        {
            double t = (double)step / 100 * Math.PI;
            double x = 16 * (Math.Pow(Math.Sin(t), 3));  //心形曲线函数X
            double y = -(13 * Math.Cos(t) - 5 * Math.Cos(2 * t) - 2 * Math.Cos(3 * t) - Math.Cos(4 * t));  //心形曲线函数Y
            x *= 10;
            y *= 10;
            x += w / 2;
            y += h / 2;
            hue += 3;
            hue = hue > 360 ? 0 : hue;
            Color c = HslToRgb(hue, 90, 60);
            SolidBrush brush = new SolidBrush(c);
            Random rnd = new Random();
            double speed = Math.Floor(Math.Sqrt((x - lastx) * (x - lastx) + (y - lasty) * (y - lasty)));
            for (int i = 0; i < 20; i++)
            {
                ParticleObj p = new ParticleObj();
                p.x = x;
                p.y = y;
                p.mx = (x - lastx) * 0.1;
                p.my = (y - lasty) * 0.1;
                p.size = rnd.NextDouble() + 4;
                p.decay = 0.03;
                p.speed = speed * 0.08;
                p.spread = 1 * p.speed;
                p.spreadX = (rnd.NextDouble() - 0.5) * p.spread - p.mx;
                p.spreadY = (rnd.NextDouble() - 0.5) * p.spread - p.my;

                p.b = brush;
                particles.Add(p);
            }
            lastx = x;
            lasty = y;
           
        }

        /// <summary>
        /// 将HSL色系转换成Color对象
        /// </summary>
        /// <param name="Hue">色相（hue）0-360</param>
        /// <param name="Saturation">饱和度（saturation）0-100</param>
        /// <param name="Lightness">亮度（lightness）0-100</param>
        /// <returns>Color</returns>
        static Color HslToRgb(int Hue, int Saturation, int Lightness)
        {
            double r = 0.0;
            double g = 0.0;
            double b = 0.0;
            double h = ((double)Hue) % 360.0;
            double s = ((double)Saturation) / 100.0;
            double l = ((double)Lightness) / 100.0;
            if (s == 0.0)
            {
                r = l;
                g = l;
                b = l;
            }
            else
            {
                double d = h / 60.0;
                int num11 = (int)Math.Floor(d);
                double num10 = d - num11;
                double num7 = l * (1.0 - s);
                double num8 = l * (1.0 - (s * num10));
                double num9 = l * (1.0 - (s * (1.0 - num10)));
                switch (num11)
                {
                    case 0:
                        r = l;
                        g = num9;
                        b = num7;
                        break;
                    case 1:
                        r = num8;
                        g = l;
                        b = num7;
                        break;
                    case 2:
                        r = num7;
                        g = l;
                        b = num9;
                        break;
                    case 3:
                        r = num7;
                        g = num8;
                        b = l;
                        break;
                    case 4:
                        r = num9;
                        g = num7;
                        b = l;
                        break;
                    case 5:
                        r = l;
                        g = num7;
                        b = num8;
                        break;
                }
            }
            int R = Convert.ToInt32(r * 255.0f);
            int G = Convert.ToInt32(g * 255.0f);
            int B = Convert.ToInt32(b * 255.0f);

            return Color.FromArgb(R, G, B);
        }


    }
}
