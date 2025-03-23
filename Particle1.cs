using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpGDI
{
    /// <summary>
    /// 粒子效果1——抹纱窗
    /// </summary>
    internal class Particle1
    {
        public struct ParticleObj
        {
            public int x;
            public int y;
            //public int d;//直径
           
            public int vx; //方向x向量
            public int vy; //方向y向量
            public int vspeed; //速度
        }
        static List<ParticleObj> particles = new List<ParticleObj>();

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="g">Graphics对象</param>
        /// <param name="w">宽</param>
        /// <param name="h">高</param>
        /// <param name="span">间距</param>
        public static void Init(Graphics g,int w,int h,int span=6)
        {
            particles.Clear();
            Bitmap bmp=new Bitmap(w,h, PixelFormat.Format24bppRgb);
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int length = h * data.Stride;
            byte[] RGB = new byte[length];
            System.IntPtr Scan0 = data.Scan0;
            System.Runtime.InteropServices.Marshal.Copy(Scan0, RGB, 0, length);
            
            for (int y = 0; y < h; y++)
            {
                int index = y * data.Stride;
                for (int x = 0; x < w; x++)
                {
                    if (x % span == 0 && y % span == 0)
                    {
                        particles.Add(new ParticleObj()
                        {
                            x = x,
                            y = y,
                            vx = x,
                            vy = y,
                            vspeed = 2
                        });

                        RGB[index + 3 * x] = 255;
                        RGB[index + 3 * x+1] = 255;
                        RGB[index + 3 * x+2] = 255;
                    }
                }
            }
            System.Runtime.InteropServices.Marshal.Copy(RGB, 0, Scan0, length);
            bmp.UnlockBits(data);
            g.DrawImage(bmp,0,0);

        }

        //动画 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mouseX1">鼠标x坐标</param>
        /// <param name="mouseY1"></param>
        /// <param name="w">宽</param>
        /// <param name="h">高</param>
        /// <param name="R">圆的半径</param>
        /// <param name="V">移动方向 0：x  1：y</param>
        public static Bitmap Start(int mouseX1, int mouseY1, int w, int h, int R,int V)
        {
            for (int i = 0; i < particles.Count; i++) 
            {
                ParticleObj p= particles[i];
                if (p.x > mouseX1 - R && p.x < mouseX1 + R
                    && p.y > mouseY1 - R && p.y < mouseY1 + R)
                {
                    //当前点在圆内时
                    if (R * R > (p.x - mouseX1) * (p.x - mouseX1) + (p.y - mouseY1) * (p.y - mouseY1))
                    {
                        //发散
                        if (V == 0) //移动y轴
                        {
                            double r1 = Math.Sqrt(R * R - (mouseX1 - p.x) * (mouseX1 - p.x));
                            if (p.y > mouseY1)
                            {
                                p.y = (int)(mouseY1 + r1 * 3);
                            }
                            else
                            {
                                p.y = (int)(mouseY1 - r1 * 3);
                            }
                        }
                        else  //移动x轴
                        {
                            double r2 = Math.Sqrt(R * R - (mouseY1 - p.y) * (mouseY1 - p.y));
                            if (p.x > mouseX1)
                            {
                                p.x = (int)(mouseX1 + r2*3);
                            }
                            else
                            {
                                p.x = (int)(mouseX1 - r2*3);
                            }
                        }

                        particles[i] = p;
                        continue;
                    }
                }

                if (p.x != p.vx || p.y != p.vy)
                {
                    if (p.x > p.vx)
                    {
                        p.x = p.x - p.vspeed < p.vx ? p.vx : (p.x - p.vspeed);
                    }
                    if (p.x < p.vx)
                    {
                        p.x = p.x + p.vspeed > p.vx ? p.vx : (p.x + p.vspeed);
                    }
                    if (p.y > p.vy)
                    {
                        p.y = p.y - p.vspeed < p.vy ? p.vy : (p.y - p.vspeed);
                    }
                    if (p.y < p.vy)
                    {
                        p.y = p.y + p.vspeed > p.vy ? p.vy : (p.y + p.vspeed);
                    }

                    particles[i] = p;
                }
            }

            //还原图片
            Bitmap dstBitmap = new Bitmap(w, h, PixelFormat.Format24bppRgb);
            BitmapData dstBmData = dstBitmap.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int dst_bytes = h * dstBmData.Stride;
            byte[] dstValues = new byte[dst_bytes];


            foreach (ParticleObj p in particles)
            {
                if (p.y < 0 || p.y >= h || p.x < 0 || p.x >= w) continue;
                int n = p.y * dstBmData.Stride + p.x * 3;
                //只处理每行中图像像素数据,舍弃未用空间
                //注意位图结构中RGB按BGR的顺序存储
                if (p.x != p.vx || p.y != p.vy)
                {
                    dstValues[n] = 204; //B
                    dstValues[n + 1] = 209;//G
                    dstValues[n + 2] = 72;//R
                }
                else
                {
                    dstValues[n] = 255;
                    dstValues[n + 1] = 255;
                    dstValues[n + 2] = 255;
                }
            }
            System.IntPtr dstPtr = dstBmData.Scan0;
            System.Runtime.InteropServices.Marshal.Copy(dstValues, 0, dstPtr, dst_bytes);
            dstBitmap.UnlockBits(dstBmData);
            return dstBitmap;
        }
    }
}
