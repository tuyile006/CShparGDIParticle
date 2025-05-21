using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Net.Mail;

namespace CSharpGDI
{
    /// <summary>
    /// 粒子效果1——磁铁
    /// </summary>
    public class Particle8: IParticle
    {
        public struct ParticleObj
        {
            public int x;
            public int y;
            public Pen pen; //内层线颜色
            public Pen starPen;  //星光颜色
            public double starDis;  //星光位置
        }

        int d1=40;//直径1  边距d1/2
        int d2=60;//直径2
        int lineWidth=3;//宽度
        int starSize = 10; //星光大小
        double starSpeed = 0.01;//星光移动速度
        int padding = 100;//边距
        int hue = 0;//颜色

        List<ParticleObj> particles = new List<ParticleObj>();
        Bitmap dstBitmap;
        Graphics g;
        Pen linePen;

        public void Start()
        {
            particles.Clear();
            int w = (int)(Math.Floor((decimal)(GameWindow.width - padding * 2)/(d2 + d1)));
            int h = (int)(Math.Floor((decimal)(GameWindow.height - padding * 2)/(d2 + d1)));
            Random rnd = new Random();
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    hue += 5;
                    hue = hue > 360 ? 0 : hue;
                    Color c = Helper.HslToRgb(hue, 90, 60);
                    Pen p = new Pen(c, lineWidth-1);

                    particles.Add(new ParticleObj()
                    {
                        x = (d2 + d1)*x + padding+(d2+d1)/2,
                        y = (d2 + d1) * y + padding + (d2 + d1) / 2,
                        pen=p,
                        starPen=new Pen(c),
                        starDis=rnd.Next(1000)*0.001
                    });
                }
            }

            dstBitmap = new Bitmap(GameWindow.width, GameWindow.height, PixelFormat.Format24bppRgb);
            g = Graphics.FromImage(dstBitmap);
            g.Clear(Color.Black);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            linePen = new Pen(Color.White, lineWidth);
        }
       
        public Bitmap Update()
        {
            if (particles.Count == 0) Start();

            g.Clear(Color.Black);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            

            if (GameWindow.isMouseIn)
            {
                //初始化 
                for (int i = 0; i < particles.Count; i++)
                {
                    ParticleObj p = particles[i];
                    //长线
                    double mousedis = Math.Sqrt(Math.Pow((p.x - GameWindow.mouseX), 2) + Math.Pow((p.y - GameWindow.mouseY), 2));
                    double sin= (p.x - GameWindow.mouseX) / mousedis;
                    double cos= (p.y - GameWindow.mouseY) / mousedis;

                    double y0= sin * d2/2; //y轴增量
                    double x0= cos * d2/2; //x轴增量
                    //短线
                    mousedis = mousedis - lineWidth;
                    double sx = p.x-sin*lineWidth;
                    double sy = p.y-cos*lineWidth;

                    double y1 = sin * d1 / 2;
                    double x1 = cos * d1 / 2;

                    //星光
                    p.starDis += starSpeed;
                    if (p.starDis >= 1) p.starDis = 0;
                    double starX1= p.x - sin * p.starDis* mousedis;
                    double starY1= p.y - cos * p.starDis* mousedis;

                    double starX2 = starX1 - sin * starSize;
                    double starY2 = starY1 - cos * starSize;

                    g.DrawLine(linePen,(float)(p.x+x0),(float)( p.y - y0), (float)(p.x - x0), (float)(p.y + y0));
                    g.DrawLine(p.pen, (float)(sx + x1), (float)(sy - y1), (float)(sx - x1), (float)(sy + y1));

                    g.DrawLine(p.pen, (float)starX1, (float)starY1, (float)starX2, (float)starY2);

                    particles[i] = p;
                }
            }
            else
            {
                //初始化 
                for (int i = 0; i < particles.Count; i++)
                {
                    ParticleObj p = particles[i];
                    
                    g.DrawLine(linePen, p.x,p.y-d2/2,p.x,p.y + d2 / 2);
                    g.DrawLine(p.pen,p.x+ lineWidth, p.y-d1/2,p.x+ lineWidth, p.y + d1 / 2);
                }
            }
            return dstBitmap;
        }
    }
}
