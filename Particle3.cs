using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;

namespace CSharpGDI
{
    /// <summary>
    /// 粒子效果3——文字粒子
    /// </summary>
    public class Particle3:IParticle
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
        List<ParticleObj> particles = new List<ParticleObj>();

        Bitmap dstBitmap;
        Graphics g;
        int R = 80;//圆的半径
        string txtShow = DateTime.Now.ToString("HH:mm:ss");
        int fontSize = 150;//文字大小

        //初始化
        public  void Start()
        {
            dstBitmap = new Bitmap(CanvasWindow.width, CanvasWindow.height, PixelFormat.Format24bppRgb);
            g= Graphics.FromImage(dstBitmap);
            txtToParticles(txtShow,0);
        }

        /// <summary>
        /// 帧率刷新
        /// </summary>
        /// <param name="mouseX">鼠标x坐标</param>
        /// <param name="mouseY"></param>
        /// <param name="w">宽</param>
        /// <param name="h">高</param>
        /// <param name="R">圆的半径</param>
        public Bitmap Update()
        {
            string txtNow = DateTime.Now.ToString("HH:mm:ss");
            if (txtNow != txtShow)
            {
                // 格式: HH:mm:ss
                // 索引: 012345678

                // 必须按从左到右顺序判断，避免低位变化被误判为高位变化
                if (txtNow[0] != txtShow[0])      // 小时十位 (索引0)
                    txtToParticles(txtNow, 0);
                else if (txtNow[1] != txtShow[1]) // 小时个位 (索引1)
                    txtToParticles(txtNow, 110);
                else if (txtNow[3] != txtShow[3]) // 分钟十位 (索引3)
                    txtToParticles(txtNow, 340);
                else if (txtNow[4] != txtShow[4]) // 分钟个位 (索引4)
                    txtToParticles(txtNow, 460);
                else if (txtNow[6] != txtShow[6]) // 秒钟十位 (索引6)
                    txtToParticles(txtNow, 620);
                else if (txtNow[7] != txtShow[7]) // 秒钟个位 (索引7)
                    txtToParticles(txtNow, 750);

                txtShow = txtNow;
            }



            if (particles.Count == 0) 
                Start();
            g.Clear(Color.Black);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            for (var i = 0; i < particles.Count; i++)
            {
                var p = particles[i];
                p.accX = (double)(p.dx - p.x) / 100;
                p.accY = (double)(p.dy - p.y) / 100;
                p.vx += p.accX;
                p.vy += p.accY;
                p.vx *= p.friction;
                p.vy *= p.friction;

                p.x +=p.vx;
                p.y +=p.vy;

                int a = (int)p.x - CanvasWindow.mouseX;
                int b = (int)p.y - CanvasWindow.mouseY;
                double distance=Math.Sqrt(a*a + b*b);
                if (distance < R)
                {
                    p.accX = (double)(p.x - CanvasWindow.mouseX) / 100;
                    p.accY = (double)(p.y - CanvasWindow.mouseY) / 100;
                    p.vx += p.accX;
                    p.vy += p.accY;
                }
                particles[i] = p;
                g.FillEllipse(p.b, (int)p.x - p.d / 2, (int)p.y - p.d / 2, p.d, p.d);
            }
            return dstBitmap;
        }

        /// <summary>
        /// 画文字
        /// </summary>
        /// <param name="txt">文字内容</param>
        /// <param name="beginY">其实y坐标 秒十位：630 秒个位：750</param>
        private void txtToParticles(string txt,int beginX)
        {
            g.Clear(Color.Black);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Font txtFont = new Font("微软雅黑", fontSize);
            SizeF sf = g.MeasureString(txt, txtFont);
            g.DrawString(txt, txtFont, Brushes.White, (CanvasWindow.width - sf.Width) / 2, (CanvasWindow.height - sf.Height) / 2);



            BitmapData data = dstBitmap.LockBits(new Rectangle(0, 0, CanvasWindow.width, CanvasWindow.height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            int length = CanvasWindow.height * data.Stride;
            byte[] ARGB = new byte[length];
            System.IntPtr Scan0 = data.Scan0;
            System.Runtime.InteropServices.Marshal.Copy(Scan0, ARGB, 0, length);

            List<ParticleObj> todel= particles.Where(p=>p.dx>= beginX).ToList();
            todel.ForEach(d => particles.Remove(d));
            var colors = new Int32[] { 0x468966, 0xFFF0A5, 0xFFB03B, 0xB64926, 0x8E2800 };
            Random rnd = new Random();
            int i = 0;
            //获取像素点
            for (var y = 0; y < CanvasWindow.height; y = y + (int)CanvasWindow.height / 100)
            {
                i = y * data.Stride;
                for (var x = beginX; x < CanvasWindow.width; x = x + (int)CanvasWindow.width / 100)
                {
                    if (ARGB[i + x * 3] > 150)
                    {
                        ParticleObj p = new ParticleObj();
                        p.d = rnd.Next(4, 14);
                        p.x = CanvasWindow.width * rnd.NextDouble();
                        p.y = CanvasWindow.height * rnd.NextDouble();
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
        }
    }
}

