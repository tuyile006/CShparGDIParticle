using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace CSharpGDI
{
    /// <summary>
    /// 粒子效果1——抹纱窗
    /// </summary>
    public class Particle1: IParticle
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
        List<ParticleObj> particles = new List<ParticleObj>();

        public void Start()
        {
            int span = 6;
            particles.Clear();
            Bitmap bmp=new Bitmap(CanvasWindow.width, CanvasWindow.height, PixelFormat.Format24bppRgb);
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, CanvasWindow.width, CanvasWindow.height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int length = CanvasWindow.height * data.Stride;
            byte[] RGB = new byte[length];
            System.IntPtr Scan0 = data.Scan0;
            System.Runtime.InteropServices.Marshal.Copy(Scan0, RGB, 0, length);
            
            for (int y = 0; y < CanvasWindow.height; y++)
            {
                int index = y * data.Stride;
                for (int x = 0; x < CanvasWindow.width; x++)
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
        }
       
        public Bitmap Update()
        {
            int R = 100;//圆的半径
            int V = 0;//移动方向 0：x  1：y

            for (int i = 0; i < particles.Count; i++)
            {
                ParticleObj p = particles[i];
                if (p.x > CanvasWindow.mouseX - R && p.x < CanvasWindow.mouseX + R
                    && p.y > CanvasWindow.mouseY - R && p.y < CanvasWindow.mouseY + R)
                {
                    //当前点在圆内时
                    if (R * R > (p.x - CanvasWindow.mouseX) * (p.x - CanvasWindow.mouseX) + (p.y - CanvasWindow.mouseY) * (p.y - CanvasWindow.mouseY))
                    {
                        //发散
                        if (V == 0) //移动y轴
                        {
                            double r1 = Math.Sqrt(R * R - (CanvasWindow.mouseX - p.x) * (CanvasWindow.mouseX - p.x));
                            if (p.y > CanvasWindow.mouseY)
                            {
                                p.y = (int)(CanvasWindow.mouseY + r1 * 3);
                            }
                            else
                            {
                                p.y = (int)(CanvasWindow.mouseY - r1 * 3);
                            }
                        }
                        else  //移动x轴
                        {
                            double r2 = Math.Sqrt(R * R - (CanvasWindow.mouseY - p.y) * (CanvasWindow.mouseY - p.y));
                            if (p.x > CanvasWindow.mouseX)
                            {
                                p.x = (int)(CanvasWindow.mouseX + r2 * 3);
                            }
                            else
                            {
                                p.x = (int)(CanvasWindow.mouseX - r2 * 3);
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
            Bitmap dstBitmap = new Bitmap(CanvasWindow.width, CanvasWindow.height, PixelFormat.Format24bppRgb);
            BitmapData dstBmData = dstBitmap.LockBits(new Rectangle(0, 0, CanvasWindow.width, CanvasWindow.height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int dst_bytes = CanvasWindow.height * dstBmData.Stride;
            byte[] dstValues = new byte[dst_bytes];


            foreach (ParticleObj p in particles)
            {
                if (p.y < 0 || p.y >= CanvasWindow.height || p.x < 0 || p.x >= CanvasWindow.width) continue;
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

