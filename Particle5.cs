using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace CSharpGDI
{
    /// <summary>
    /// 粒子效果5——水流体
    /// </summary>
    public class Particle5:IParticle
    {
        static List<ParticleObj> particles = null;
        static double kRadius = 0;
        static Grid grid = new Grid(100);
        static Container container = new Container();
        static double kGravity = 0.04;
        static int kDensity = 3;
        static double kRendering = 0.45;
        //画布宽和高
        static int canvasW;
        static int canvasH;
        //鼠标事件
        //每帧返回的图片
        static Bitmap dstBitmap;
        static Graphics g;
        static Pen linPen=new Pen(Brushes.Black);
        static Pen BoxBordPen = new Pen(Brushes.Black);

       
        public void Start()
        {
            int particleNum = 1200;//粒子数量
            dstBitmap = new Bitmap(GameWindow.width, GameWindow.height, PixelFormat.Format24bppRgb);
            g = Graphics.FromImage(dstBitmap);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            canvasW = GameWindow.width; 
            canvasH = GameWindow.height;
            SolidBrush brush = new SolidBrush(Color.FromArgb(72, 209, 204));
            linPen = new Pen(brush);
            BoxBordPen = new Pen(brush, 3);

            kRadius = Math.Round(0.04 * Math.Sqrt(GameWindow.width * GameWindow.height));
            container.init(0.35);
            particles = new List<ParticleObj>();
            double s = container.scale;
            double x = GameWindow.width * s * 0.5;
            double y = GameWindow.height * s * 0.5;
            for (int i = 0; i < particleNum; ++i)
            {
                particles.Add(new ParticleObj(x, y));
                x += kRadius / 2.5;
                if (x > GameWindow.width * (1 - s * 0.5))
                {
                    x = GameWindow.width * s * 0.5;
                    y += kRadius / 3;
                }
            }
            grid.initSize(GameWindow.width, GameWindow.height, (int)kRadius);
            grid.fill(particles);

        }

        public Bitmap Update()
        {
            if (particles==null||particles.Count == 0) Start();
            g.ResetTransform();
            g.Clear(Color.FromArgb(160,221,219));//"#bebebf"
          

            container.rotate();

            foreach (var p in particles)
                p.integrate();

            grid.update(particles);
            
            foreach (var p in particles)
                p.fluid();

            return dstBitmap;
        }


        //粒子对象
        class ParticleObj
        {
            public double x;//点坐标x
            public double y;//点坐标y
            public double px; //目标位置x
            public double py; //目标位置y
            public ParticleObj(double x1,double y1) 
            {
                this.x = x1;
                this.y = y1;
                this.px = x1;
                this.py = y1;
            }
            
            public void turbine()
            {
                double dx = GameWindow.mouseX - this.x;
                double dy = GameWindow.mouseY - this.y;
                double d = Math.Sqrt(dx * dx + dy * dy);
                if (d < 2 * kRadius&& d !=-1)
                {
                    double angle = Math.Atan2(dy, dx) + kRadius / (d + 1);
                    this.x += Math.Cos(angle);
                    this.y += Math.Sin(angle);
                }
            }

            public void integrate()
            {
                container.limit(this);
                if (GameWindow.isMouseDown) this.turbine();
                double x0 = this.x;
                double y0 = this.y;
                this.x += x0 - this.px;
                this.y += y0 - this.py + kGravity;
                this.px = x0;
                this.py = y0;
            }

            public void fluid()
            {
                double pressure = 0;
                double presnear = 0;
                List<Contact> neighbors = new List<Contact>();
                int xc = (int) (1 + this.x / grid.size);
                int yc = (int)(1 + this.y / grid.size);
                for (int x = xc - 1; x < xc + 2; ++x)
                {
                    for (int y = yc - 1; y < yc + 2; ++y)
                    {
                        int index = y * grid.width + x;
                        if (index >= grid.cellsSize.Length||index<0) continue;
                        for (
                            int k = grid.max * index, end = k + grid.cellsSize[index];
                            k < end;
                            ++k
                        )
                        {
                            ParticleObj pn = grid.cells[k];
                            if (pn != this&&pn!=null)
                            {
                                double vx = pn.x - this.x;
                                double vy = pn.y - this.y;
                                double slen = vx * vx + vy * vy;
                                if (slen < kRadius * kRadius)
                                {
                                    double vlen =Math.Sqrt(slen);
                                    double q = 1.0 - vlen / kRadius;
                                    pressure += q * q;
                                    presnear += q * q * q;
                                    if(vlen!=0)
                                     neighbors.Add(new Contact(pn, q, (double)(vx / vlen * q), (double)(vy / vlen * q)));
                                }
                            }
                        }
                    }
                }
                pressure = (pressure - kDensity) * 1.0;
                presnear *= 0.5;
                foreach (Contact p in neighbors)
                {
                    double pr = pressure + presnear * p.q;
                    double dx = p.vx * pr;
                    double dy = p.vy * pr;
                    p.n.x += dx;
                    p.n.y += dy;
                    this.x -= dx;
                    this.y -= dy;
                    if (p.q > kRendering)
                    {
                        g.DrawLine(linPen, (float)this.x, (float)this.y, (float)p.n.x, (float)p.n.y);
                    }
                }
            }
        }
        class Contact
        {
            public ParticleObj n;
            public double q;
            public double vx;
            public double vy;
            public Contact(ParticleObj n1, double q1, double vx1, double vy1)
            {
                this.n = n1;
                this.q = q1;
                this.vx = vx1;
                this.vy = vy1;
            }
        }
        class Grid
        {
            public int max;
            public int width;
            public int height;
            public int size;
            public ParticleObj[] cells;
            public int[] cellsSize;
            public Grid(int maxParticlesPerCell)
            {
                this.max = maxParticlesPerCell;
            }
            public void initSize(int w,int h,int s)
            {
                this.width = (int)(2 + w / s);
                this.height = (int)(2 + w / s);
                this.size = s;
                this.cells = new ParticleObj[this.width * this.height * this.max];
                this.cellsSize = new int[this.width * this.height];
            }
            public void fill(List<ParticleObj> particles)
            {
                foreach (ParticleObj p in particles)
                {
                    int index =((int)(1 + p.y / this.size)) * this.width + ((int)(1 + p.x / this.size));
                    if (index < 0 || index >= this.cellsSize.Length) continue;
                    if (this.cellsSize[index] < this.max)
                    {
                        int cellPos = this.cellsSize[index];
                        this.cellsSize[index]++;
                        this.cells[index * this.max + cellPos] = p;
                    }
                }
            }
            public void update(List<ParticleObj> particles)
            {
                for (int i = 0; i < this.width * this.height; ++i)
                {
                    for (int j = 0; j < this.cellsSize[i]; ++j)
                    {
                        ParticleObj p = this.cells[i * this.max + j];
                        if (p == null) continue;
                        int index =((int)(1 + p.y / this.size)) * this.width + ((int)(1 + p.x / this.size));
                        if (index > 0
                            &&index < this.cellsSize.Length
                            &&index != i 
                            && this.cellsSize[index] < this.max)
                        {
                            this.cells[index * this.max + this.cellsSize[index]] = p;
                            this.cellsSize[index]++;
                            this.cellsSize[i]--;
                            this.cells[i * this.max + j] = this.cells[ i * this.max + this.cellsSize[i]];
                        }
                    }
                }
            }
        }

        class Container{
            public double ai;
            public double scale;
            public List<Plane> borders;
            public void init(double s)
            {
                this.ai = 0;
                this.scale = s;
                this.borders = new List<Plane>() {

                new Plane(),

                new Plane(),

                new Plane(),

                new Plane()

            };
            }
            public class Plane
            {
                public double x;
                public double y;
                public double d;
                public Plane()
                {
                    this.x = 0;
                    this.y = 0;
                    this.d = 0;
                }
                public double distanceToPlane(ParticleObj p)
                {
                    return (
                        (p.x - canvasW * 0.5) * this.x +
                        (p.y - canvasH * 0.5) * this.y +
                        this.d
                    );
                }
                public void update(double x1, double y1, double d1)
                {
                    this.x = x1;
                    this.y = y1;
                    this.d = d1;
                }
            }
            public void rotate()
            {
                double w = canvasW;
                double h = canvasH;
                double s = this.scale;
                this.ai += GameWindow.isMouseDown ? 0 : 0.05;
                double angle = Math.Sin(this.ai) * s * Math.Min(1.0, h / w);
                double cos = Math.Cos(angle);
                double sin = Math.Sin(angle);
                this.borders[0].update(-sin, cos, -h * s);
                this.borders[1].update(cos, sin, -w * s);
                this.borders[2].update(-cos, -sin, -w * s);
                this.borders[3].update(sin, -cos, -h * s);
                
                g.TranslateTransform((float)(w * 0.5), (float)(h * 0.5));
                g.RotateTransform((float)(angle*180/Math.PI));

                g.FillRectangle(Brushes.White, (float)(-w * s), (float)(-h * s), (float)(w * s * 2), (float)(h * s * 2));
                g.DrawRectangle(BoxBordPen, (float)(-w * s), (float)(-h * s), (float)(w * s * 2), (float)(h * s * 2));
                g.ResetTransform();
            }
            public void limit(ParticleObj p, int radius = 0)
            {
                Random rnd = new Random();
                foreach (var b in this.borders)
                {
                    double d = b.distanceToPlane(p) + radius + 0;
                    if (d > 0)
                    {
                        p.x += b.x * -d + (rnd.NextDouble() * 0.1 - 0.05);
                        p.y += b.y * -d + (rnd.NextDouble() * 0.1 - 0.05);
                    }
                }
            }
        }
       
        
    }
}
