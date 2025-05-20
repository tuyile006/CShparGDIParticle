using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace CSharpGDI
{
    /// <summary>
    /// 粒子效果7——数学曲线
    /// </summary>
    public class Particle7:IParticle
    {
        //public struct ParticleObj
        //{
        //    public double x;//圆点坐标x
        //    public double y;//圆点坐标y
        //    public double size;
        //    public SolidBrush b;
        //}
        // List<ParticleObj> particles = new List<ParticleObj>();

         Bitmap dstBitmap;
         Graphics g;
         int width;
         int height;
         int curFrame = 0;//当前帧数
        
        /*
            hue += 3;
            hue = hue > 360 ? 0 : hue;
            Color c = HslToRgb(hue, 90, 60);
            SolidBrush brush = new SolidBrush(c);
         */
         int hue=0;//色相（hue）HSL色系中的参数
         int coordinateSystem_x = 0; //坐标系x位置
         int coordinateSystem_y = 0; //坐标系y位置
         List<string> mathList = new List<string>();//曲线队列
         int padding = 20; //四周间隔
        public  int coSplit = 100;//间隔200  代表200个像素为1个单位长度。
        public  int curMath = 0;//当前函数曲线index

        /// <summary>
        /// 初始化
        /// </summary>
        public  void Start()
        {
            dstBitmap = new Bitmap(GameWindow.width, GameWindow.height, PixelFormat.Format24bppRgb);
            g= Graphics.FromImage(dstBitmap);
            g.Clear(Color.Black);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            width = GameWindow.width;
            height = GameWindow.height;
            hue = 0;
            coordinateSystem_x = GameWindow.width / 2;
            coordinateSystem_y = GameWindow.height / 2;

            mathList.Add("sin");
            mathList.Add("cos");
            mathList.Add("tan");
            mathList.Add("xsin");
            mathList.Add("pow2");
            mathList.Add("pow3");
            mathList.Add("pow-1");
            mathList.Add("pow1/2");
            mathList.Add("heart");
            mathList.Add("rose");
            mathList.Add("arch");

        }

       
        /// <summary>
        /// 动画 
        /// </summary>
        /// <param name="w">宽</param>
        /// <param name="h">高</param>
        public  Bitmap Update()
        {
            if (dstBitmap==null) 
                Start();
            g.Clear(Color.Black);
            curFrame++;

            if (curMath >= mathList.Count)
                curMath = 0;

            switch(mathList[curMath])
            {
                case "sin":
                    DrawSin();
                    break;
                case "cos":
                    DrawCos();
                    break;
                case "tan":
                    DrawTan();
                    break;
                case "xsin":
                    DrawXSin();
                    break;
                case "pow2":
                    DrawPow(2);
                    break;
                case "pow3":
                    DrawPow(3);
                    break;
                case "pow-1":
                    DrawPow(-1);
                    break;
                case "pow1/2":
                    DrawSqrt();
                    break;
                case "heart":
                    DrawHeart();
                    break;
                case "rose":
                    DrawRose();
                    break;
                case "arch":
                    DrawArch();
                    break;
                default:
                    DrawSin();
                    break;
            }
            
            return dstBitmap;
        }

        /// <summary>
        /// sin曲线
        /// </summary>
        private  void DrawSin()
        {
            CreateCoordinateSystem(coSplit,padding);
            SolidBrush brush = new SolidBrush(Color.FromArgb(72, 209, 204));
            SolidBrush brush2 = new SolidBrush(Color.White);
            for (int x= padding; x<width- padding; x=x+2)
            {
                float y = coordinateSystem_y - (float)Math.Sin((x - coordinateSystem_x) /(double) coSplit) * coSplit;
                g.FillEllipse(brush, x-1,y-1, 2, 2);

                //画滚动球
                if (curFrame == x)
                {
                    g.FillEllipse(brush2, x-3, y-3, 6,6);
                }
            }

            if (curFrame == width - padding)
            {
                curFrame = padding;
            }

            //标题
            Font f2 = new Font("宋体", 20);
            Brush b2 = new SolidBrush(Color.FromArgb(72, 209, 204));
            g.DrawString("函数方程：y=Sin(x)", f2, b2, 20, 20);
        }
        /// <summary>
        /// X*Sin曲线
        /// </summary>
        private  void DrawXSin()
        {
            CreateCoordinateSystem(coSplit, padding);
            SolidBrush brush = new SolidBrush(Color.FromArgb(72, 209, 204));
            SolidBrush brush2 = new SolidBrush(Color.White);
            for (int x = padding; x < width - padding; x = x + 2)
            {
                double rx = (x - coordinateSystem_x) / (double)coSplit;
                float y = coordinateSystem_y -(float)(rx*Math.Sin(rx) * coSplit);
                g.FillEllipse(brush, x - 1, y - 1, 2, 2);

                //画滚动球
                if (curFrame == x)
                {
                    g.FillEllipse(brush2, x - 3, y - 3, 6, 6);
                }
            }

            if (curFrame == width - padding)
            {
                curFrame = padding;
            }

            //标题
            Font f2 = new Font("宋体", 20);
            Brush b2 = new SolidBrush(Color.FromArgb(72, 209, 204));
            g.DrawString("函数方程：y=xSin(x)", f2, b2, 20, 20);
        }

        /// <summary>
        /// cos曲线
        /// </summary>
        private  void DrawCos()
        {
            CreateCoordinateSystem(coSplit, padding);
            SolidBrush brush = new SolidBrush(Color.FromArgb(72, 209, 204));
            SolidBrush brush2 = new SolidBrush(Color.White);
            for (int x = padding; x < width - padding; x = x + 2)
            {
                float y = coordinateSystem_y - (float)Math.Cos((x - coordinateSystem_x) / (double)coSplit) * coSplit;
                g.FillEllipse(brush, x - 1, y - 1, 2, 2);

                //画滚动球
                if (curFrame == x)
                {
                    g.FillEllipse(brush2, x - 3, y - 3, 6, 6);
                }
            }

            if (curFrame == width - padding)
            {
                curFrame = padding;
            }

            //标题
            Font f2 = new Font("宋体", 20);
            Brush b2 = new SolidBrush(Color.FromArgb(72, 209, 204));
            g.DrawString("函数方程：y=Cos(x)", f2, b2, 20, 20);
        }

        /// <summary>
        /// tan曲线
        /// </summary>
        private  void DrawTan()
        {
            CreateCoordinateSystem(coSplit, padding);
            SolidBrush brush = new SolidBrush(Color.FromArgb(72, 209, 204));
            SolidBrush brush2 = new SolidBrush(Color.White);
            for (int x = padding; x < width - padding; x = x + 2)
            {
                float y = coordinateSystem_y - (float)Math.Tan((x - coordinateSystem_x) / (double)coSplit) * coSplit;
                g.FillEllipse(brush, x - 1, y - 1, 2, 2);

                //画滚动球
                if (curFrame == x)
                {
                    g.FillEllipse(brush2, x - 3,y - 3, 6, 6);
                }
            }

            if (curFrame == width - padding)
            {
                curFrame = padding;
            }

            //标题
            Font f2 = new Font("宋体", 20);
            Brush b2 = new SolidBrush(Color.FromArgb(72, 209, 204));
            g.DrawString("函数方程：y=Tan(x)", f2, b2, 20, 20);
        }
        /// <summary>
        /// pow曲线
        /// </summary>
        private  void DrawPow(int fy)
        {
            CreateCoordinateSystem(coSplit, padding);
            SolidBrush brush = new SolidBrush(Color.FromArgb(72, 209, 204));
            SolidBrush brush2 = new SolidBrush(Color.White);
            for (int x = padding; x < width - padding; x = x + 2)
            {
                float y = coordinateSystem_y - (float)Math.Pow((x - coordinateSystem_x) / (double)coSplit,fy) * coSplit;
                g.FillEllipse(brush, x - 1, y - 1, 2, 2);

                //画滚动球
                if (curFrame == x)
                {
                    g.FillEllipse(brush2,x - 3, y - 3, 6, 6);
                }
            }

            if (curFrame == width - padding)
            {
                curFrame = padding;
            }

            //标题
            Font f2 = new Font("宋体", 20);
            Brush b2 = new SolidBrush(Color.FromArgb(72, 209, 204));
            string txtShow = "函数方程：y=x";
            SizeF sf = g.MeasureString(txtShow, f2);
            g.DrawString("函数方程：y=x", f2, b2, 20, 20);
            Font f3 = new Font("宋体", 10);
            g.DrawString(fy.ToString(), f3, b2, 20+ sf.Width, 20);

        }
        /// <summary>
        /// 平方根曲线
        /// </summary>
        private  void DrawSqrt()
        {
            CreateCoordinateSystem(coSplit, padding);
            SolidBrush brush = new SolidBrush(Color.FromArgb(72, 209, 204));
            SolidBrush brush2 = new SolidBrush(Color.White);
            for (int x = coordinateSystem_x; x < width - padding; x = x + 2)
            {
                float y = coordinateSystem_y - (float)Math.Sqrt((x - coordinateSystem_x) / (double)coSplit) * coSplit;
                g.FillEllipse(brush, x - 1, y - 1, 2, 2);

                //画滚动球
                if (curFrame == x)
                {
                    g.FillEllipse(brush2, x - 3, y - 3, 6, 6);
                }
            }

            if (curFrame == width - padding)
            {
                curFrame = coordinateSystem_x;
            }

            //标题
            Font f2 = new Font("宋体", 20);
            Brush b2 = new SolidBrush(Color.FromArgb(72, 209, 204));
            string txtShow = "函数方程：y=x";
            SizeF sf = g.MeasureString(txtShow, f2);
            g.DrawString("函数方程：y=x", f2, b2, 20, 20);
            Font f3 = new Font("宋体", 10);
            g.DrawString("1/2", f3, b2, 20 + sf.Width, 20);
        }
        /// <summary>
        /// 心形线
        /// </summary>
        private  void DrawHeart()
        {
            CreateCoordinateSystem(coSplit, padding);
            SolidBrush brush = new SolidBrush(Color.FromArgb(72, 209, 204));
            SolidBrush brush2 = new SolidBrush(Color.White);

            for (int x = padding; x < width - padding; x = x + 2)
            {
                double datas = (x - coordinateSystem_x) / (double)coSplit;
                float x1 = coordinateSystem_x + coSplit * (float)(1 - Math.Cos(datas)) * (float)(Math.Cos(datas));
                float y1 = coordinateSystem_y + coSplit * (float)(1 - Math.Cos(datas)) * (float)(Math.Sin(datas));

                g.FillEllipse(brush, x1 - 1, y1 - 1, 2, 2);

                //画滚动球
                if (curFrame == x)
                {
                    g.FillEllipse(brush2, x1 - 3, y1 - 3, 6, 6);
                }
            }

            if (curFrame == width - padding)
            {
                curFrame = padding;
            }

            //标题
            Font f2 = new Font("宋体", 20);
            Brush b2 = new SolidBrush(Color.FromArgb(72, 209, 204));
            g.DrawString("心形线：r=a(1-cosθ) (a>0)", f2, b2, 20, 20);
        }

        /// <summary>
        /// 玫瑰线
        /// </summary>
        private  void DrawRose()
        {
            CreateCoordinateSystem(coSplit, padding);
            SolidBrush brush = new SolidBrush(Color.FromArgb(72, 209, 204));
            SolidBrush brush2 = new SolidBrush(Color.White);

            for (int x = padding; x < width - padding; x = x + 2)
            {
                double datas = (x - coordinateSystem_x) / (double)coSplit;
                float x1 = coordinateSystem_x + coSplit * (float)(Math.Sin(3*datas)) * (float)(Math.Cos(datas));
                float y1 = coordinateSystem_y + coSplit * (float)(Math.Sin(3*datas)) * (float)(Math.Sin(datas));

                g.FillEllipse(brush, x1 - 1, y1 - 1, 2, 2);

                //画滚动球
                if (curFrame == x)
                {
                    g.FillEllipse(brush2, x1 - 3, y1 - 3, 6, 6);
                }
            }

            if (curFrame == width - padding)
            {
                curFrame = padding;
            }

            //标题
            Font f2 = new Font("宋体", 20);
            Brush b2 = new SolidBrush(Color.FromArgb(72, 209, 204));
            g.DrawString("玫瑰线：r=asin(3θ) (a>0)", f2, b2, 20, 20);
        }

        /// <summary>
        /// 阿基米德螺线
        /// </summary>
        private  void DrawArch()
        {
            CreateCoordinateSystem(coSplit, padding);
            SolidBrush brush = new SolidBrush(Color.FromArgb(72, 209, 204));
            SolidBrush brush2 = new SolidBrush(Color.White);

            for (int x = coordinateSystem_x; x < width - padding; x = x + 2)
            {
                double datas = (x - coordinateSystem_x) / (double)coSplit;
                float x1 = coordinateSystem_x + (float)(coSplit * datas * Math.Cos(datas));
                float y1 = coordinateSystem_y + (float)(coSplit * datas * Math.Sin(datas));
                g.FillEllipse(brush, x1 - 1, y1 - 1, 2, 2);

                //画滚动球
                if (curFrame == x)
                {
                    g.FillEllipse(brush2, x1 - 3, y1 - 3, 6, 6);
                }
            }

            if (curFrame == width - padding)
            {
                curFrame = coordinateSystem_x;
            }

            //标题
            Font f2 = new Font("宋体", 20);
            Brush b2 = new SolidBrush(Color.FromArgb(72, 209, 204));
            g.DrawString("阿基米德螺线：r=aθ(a>0,θ>=0)", f2, b2, 20, 20);
        }


        /// <summary>
        /// 画坐标系
        /// </summary>
        /// <param name="coSplit">坐标系刻度间隔</param>
        private  void CreateCoordinateSystem(int coSplit, int padding)
        {
            
            Pen p = new Pen(Color.White, 2);
            //画x轴
            g.DrawLine(p, padding, coordinateSystem_y, width - padding, coordinateSystem_y);
            //画y轴
            g.DrawLine(p, coordinateSystem_x,height - padding, coordinateSystem_x, padding);
            //画刻度
            Pen p2 = new Pen(Color.FromArgb(72,209,204), 3);
            Font f2= new Font("Arial", 7);
            Brush b2 = new SolidBrush(Color.FromArgb(72, 209, 204));
            for (int x = padding; x < width- padding; x++)
            {
                for (int y = padding; y < height-padding; y++)
                {
                    if (y == coordinateSystem_y)
                    {
                        if ((x - coordinateSystem_x) % coSplit == 0 &&x!= coordinateSystem_x&&x<width-padding- 20)
                        {
                            g.DrawLine(p2, x, coordinateSystem_y, x, coordinateSystem_y-5);
                            int split = (x - coordinateSystem_x) / coSplit;
                            g.DrawString(split.ToString(), f2, b2, x-10, coordinateSystem_y - 15);
                        }
                    }

                    if (x == coordinateSystem_x)
                    {
                        if ((y - coordinateSystem_y) % coSplit == 0 && y != coordinateSystem_y && y >  padding + 20)
                        {
                            g.DrawLine(p2, coordinateSystem_x, y, coordinateSystem_x-5, y);
                            int split = (coordinateSystem_y-y) / coSplit;
                            g.DrawString(split.ToString(), f2, b2, coordinateSystem_x - 20, y-10);
                        }
                    }
                }
            }
            //画箭头
            g.DrawLine(p, coordinateSystem_x, padding, coordinateSystem_x - 5, padding+8);
            g.DrawLine(p, coordinateSystem_x, padding, coordinateSystem_x + 5, padding+8);

            g.DrawLine(p, width - padding - 8, coordinateSystem_y - 5, width - padding, coordinateSystem_y);
            g.DrawLine(p, width - padding - 8, coordinateSystem_y + 5, width - padding, coordinateSystem_y);
        }



        /// <summary>
        /// 将HSL色系转换成Color对象
        /// </summary>
        /// <param name="Hue">色相（hue）0-360</param>
        /// <param name="Saturation">饱和度（saturation）0-100</param>
        /// <param name="Lightness">亮度（lightness）0-100</param>
        /// <returns>Color</returns>
         Color HslToRgb(int Hue, int Saturation, int Lightness)
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
