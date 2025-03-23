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
    /// 粒子效果4——3D粒子
    /// </summary>
    internal class Particle4
    {
        public struct Particle 
        {
            public double x;
            public double y;
            public double z;
            public double vy;
            public double radius;
            public double color;
            public double dist;
        }
        public class Particle4Vars
        {
            public  int w;//宽
            public  int h;//高
            public  int frameNo;
            public  double camX;
            public  double camY;
            public  double camZ;
            public  double pitch;
            public  double yaw;
            public  int cx;
            public  int cy;
            public  int scale;
            public  double floor;
            public  List<Particle> points;

            public  int initParticles;
            public  double initV;
            public  int distributionRadius ;
            public  int vortexHeight;
            public Graphics g;
            public Bitmap dstBitmap;
        }
        public struct tmpPoint
        {
            public double x;
            public double y;
            public double d;
        }

        static Particle4Vars vars=null;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="w">宽</param>
        /// <param name="h">高</param>
        public static Bitmap Init(int w,int h)
        {
            Bitmap dstBitmap = new Bitmap(w, h, PixelFormat.Format24bppRgb);
            Graphics g= Graphics.FromImage(dstBitmap);
            g.Clear(Color.Black);
            g.SmoothingMode = SmoothingMode.AntiAlias;
           
            vars= new Particle4Vars();
            vars.g = g;
            vars.dstBitmap = dstBitmap;
            vars.frameNo = 0;
            vars.w = w;
            vars.h = h;
            vars.camX = 0;
            vars.camY = 0;
            vars.camZ = -14;
            vars.pitch = Elevation(vars.camX, vars.camZ, vars.camY) - Math.PI / 2;
            vars.yaw = 0; //偏角
            vars.cx = w / 2;
            vars.cy = h / 2;
            vars.scale = 500;
            vars.floor = 26.5;

            vars.points =new List<Particle>();
            vars.initParticles = 2000;
            vars.initV = .01;
            vars.distributionRadius = 800;
            vars.vortexHeight = 25;

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
        public static Bitmap Start(int w, int h)
        {
            if (vars == null) Init(w, h);
            vars.frameNo++;
           
            ReCalc();
            ReDrawParticles();

            return vars.dstBitmap;
        }



        //计算俯视角度
        private static double Elevation(double x, double y, double z)
        {

            var dist = Math.Sqrt(x * x + y * y + z * z);
            if (dist != 0 && z / dist >= -1 && z / dist <= 1) 
                return Math.Acos(z / dist);
            else
                return 0.00000001;
        }
        //生产粒子
        private static void SpawnParticle()
        {
            Random rd= new Random(); 
            double p, ls;
            Particle pt = new Particle();
            p = Math.PI * 2 * rd.NextDouble();
            ls = Math.Sqrt(rd.NextDouble() * vars.distributionRadius);
            pt.x = Math.Sin(p) * ls;
            pt.y = -vars.vortexHeight / 2;
            pt.vy = vars.initV / 20 + rd.NextDouble() * vars.initV;
            pt.z = Math.Cos(p) * ls;
            pt.radius = 200 + 800 * rd.NextDouble();
            pt.color = pt.radius / 1000 + vars.frameNo / 250;
            vars.points.Add(pt);
        }

       
        private static int[] rgbArray(double col)
        {

            col += 0.000001;
            var r = (int)((0.5 + Math.Sin(col) * 0.5) * 256);
            var g = (int)((0.5 + Math.Cos(col) * 0.5) * 256);
            var b = (int)((0.5 - Math.Sin(col) * 0.5) * 256);
            return new int[] { r, g, b };
        }
        private static int[] interpolateColors(int[] RGB1,int[] RGB2,double degree)
        {

            var w2 = degree;
            var w1 = 1 - w2;
            int r = (int)(w1 * RGB1[0] + w2 * RGB2[0]);
            int g = (int)(w1 * RGB1[1] + w2 * RGB2[1]);
            int b = (int)(w1 * RGB1[2] + w2 * RGB2[2]);
            return new int[] {r, g, b};
        }
        private static tmpPoint project3D(double x,double y,double z)
        {
            double p, d;
            x -= vars.camX;
            y -= vars.camY - 8;
            z -= vars.camZ;
            p = Math.Atan2(x, z);
            d = Math.Sqrt(x * x + z * z);
            x = Math.Sin(p - vars.yaw) * d;
            z = Math.Cos(p - vars.yaw) * d;
            p = Math.Atan2(y, z);
            d = Math.Sqrt(y * y + z * z);
            y = Math.Sin(p - vars.pitch) * d;
            z = Math.Cos(p - vars.pitch) * d;
            var rx1 = -1000;
            var ry1 = 1;
            var rx2 = 1000;
            var ry2 = 1;
            var rx3 = 0;
            var ry3 = 0;
            var rx4 = x;
            var ry4 = z;
            var uc = (ry4 - ry3) * (rx2 - rx1) - (rx4 - rx3) * (ry2 - ry1);
            var ua = ((rx4 - rx3) * (ry1 - ry3) - (ry4 - ry3) * (rx1 - rx3)) / uc;
            var ub = ((rx2 - rx1) * (ry1 - ry3) - (ry2 - ry1) * (rx1 - rx3)) / uc;
            if (z==0) z = 0.000000001;
            if (ua > 0 && ua < 1 && ub > 0 && ub < 1)
            {
                return new tmpPoint(){
                    x=vars.cx + (rx1 + ua * (rx2 - rx1)) * vars.scale,
                    y=vars.cy + y / z * vars.scale,
                    d=(x * x + y * y + z * z)
                };
            }
            else
            {
                return new tmpPoint() { d=-1 };
            }
        }
        //画背景粒子
        private static void DrawFloor()
        {
            double x, y, z, d, a, size;
            for (var i = -25; i <= 25; i += 1)
            {
                for (var j = -25; j <= 25; j += 1)
                {
                    x = i * 2;
                    z = j * 2;
                    y = vars.floor;
                    d = Math.Sqrt(x * x + z * z);
                    var point = project3D(x, y - d * d / 85, z);
                    if (point.d != -1)
                    {
                        size = 1 + 15000 / (1 + point.d);
                        a = 0.55 - Math.Pow(d / 50, 4) * 0.15;
                        if (a > 0)
                        {
                            a = a * 255;
                            int[] color = interpolateColors(rgbArray(d / 26 - vars.frameNo / 40), new int[] { 0, 128, 32 }, .5 + Math.Sin(d / 6 - vars.frameNo / 8) / 2);
                            SolidBrush brush= new SolidBrush(Color.FromArgb((int)a, Color.FromArgb(color[0], color[1], color[2])));
                            //SolidBrush brush= new SolidBrush(Color.FromArgb(255, Color.FromArgb(color[0], color[1], color[2])));
                            vars.g.FillRectangle(brush, (int)(point.x - size / 2), (int)(point.y - size / 2), (int)size, (int)size);
                        }
                    }
                }
            }
            
            for (var i = -25; i <= 25; i += 1)
            {
                for (var j = -25; j <= 25; j += 1)
                {
                    x = i * 2;
                    z = j * 2;
                    y = -vars.floor;
                    d = Math.Sqrt(x * x + z * z);
                    var  point = project3D(x, y + d * d / 85, z);
                    if (point.d != -1)
                    {
                        size = 1 + 15000 / (1 + point.d);
                        a = 0.55 - Math.Pow(d / 50, 4) * 0.15;
                        if (a > 0)
                        {
                            a = a * 255;
                            int[] color = interpolateColors(rgbArray(-d / 26 - vars.frameNo / 40), new int[] { 32, 0, 128 }, .5 + Math.Sin(-d / 6 - vars.frameNo / 8) / 2);
                            SolidBrush brush = new SolidBrush(Color.FromArgb((int)a, Color.FromArgb(color[0], color[1], color[2])));
                            vars.g.FillRectangle(brush, (int)(point.x - size / 2), (int)(point.y - size / 2), (int)size, (int)size);
                        }
                    }
                }
            }
        }


        //计算粒子位置
        private static void  ReCalc()
        {
            if (vars.points.Count < vars.initParticles)
            {
                for (var i = 0; i < 5; ++i)
                {
                    SpawnParticle();
                }
            }
            
            double p, d, t;

            p = Math.Atan2(vars.camX, vars.camZ);
            d = Math.Sqrt(vars.camX * vars.camX + vars.camZ * vars.camZ);
            d -= Math.Sin(vars.frameNo / 80) / 25;
            t = Math.Cos(vars.frameNo / 300) / 165;
            vars.camX = Math.Sin(p + t) * d;
            vars.camZ = Math.Cos(p + t) * d;
            vars.camY = -Math.Sin(vars.frameNo / 220) * 15;
            vars.yaw = Math.PI + p + t;
            vars.pitch = Elevation(vars.camX, vars.camZ, vars.camY) - Math.PI / 2;

            double x,y,z;
            for (var i = 0; i < vars.points.Count; ++i)
            {
                Particle pt = vars.points[i];
                x = pt.x;
                y = pt.y;
                z = pt.z;
                d = Math.Sqrt(x * x + z * z) / 1.0075;
                t = .1 / (1 + d * d / 5);
                p = Math.Atan2(x, z) + t;
                pt.x = Math.Sin(p) * d;
                pt.z = Math.Cos(p) * d;
                pt.y += vars.points[i].vy * t * ((Math.Sqrt(vars.distributionRadius) - d) * 2);
                vars.points[i] = pt;
                if (pt.y > vars.vortexHeight / 2 || d < .25)
                {
                    vars.points.RemoveAt(i);
                    SpawnParticle();
                }
            }
        }
        //画粒子
        private static void ReDrawParticles()
        {
            //不加尾巴
            //vars.g.Clear(Color.Black); 
            //加尾巴
            Brush br = new SolidBrush(Color.FromArgb(30, Color.Black));
            vars.g.FillRectangle(br, 0, 0, vars.w, vars.h);
            DrawFloor();

            double   a, size,d;
            for (var i = 0; i < vars.points.Count; ++i)
            {
                Particle pt= vars.points[i];
                var point = project3D(pt.x, pt.y, pt.z);
                if (point.d != -1)
                {
                    pt.dist = point.d;
                    size = 1 + pt.radius / (1 + point.d);
                    d = Math.Abs(pt.y);
                    a = .8 - Math.Pow(d / (vars.vortexHeight / 2), 1000) * .8;

                    a = (a >= 0 && a <= 1) ? a*255 : 0;
                    vars.points[i] = pt;
                    if (point.x > -1 && point.x < vars.w && point.y > -1 && point.y < vars.h)
                    {
                        int[] color = rgbArray(vars.points[i].color);
                        SolidBrush brush = new SolidBrush(Color.FromArgb((int)a, Color.FromArgb(color[0], color[1], color[2])));
                        vars.g.FillRectangle(brush, (int)(point.x - size / 2), (int)(point.y - size / 2), (int)size, (int)size);
                    }
                }
            }

            vars.points.Sort((p1, p2) => {
                if (p1.dist < p2.dist)
                    return -1;
                else if (p1.dist == p2.dist)
                    return 0;
                else
                    return 1; });
        }

        
    }
}
