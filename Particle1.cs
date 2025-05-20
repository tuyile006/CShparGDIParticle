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
            Bitmap bmp=new Bitmap(GameWindow.width, GameWindow.height, PixelFormat.Format24bppRgb);
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, GameWindow.width, GameWindow.height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int length = GameWindow.height * data.Stride;
            byte[] RGB = new byte[length];
            System.IntPtr Scan0 = data.Scan0;
            System.Runtime.InteropServices.Marshal.Copy(Scan0, RGB, 0, length);
            
            for (int y = 0; y < GameWindow.height; y++)
            {
                int index = y * data.Stride;
                for (int x = 0; x < GameWindow.width; x++)
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
                if (p.x > GameWindow.mouseX - R && p.x < GameWindow.mouseX + R
                    && p.y > GameWindow.mouseY - R && p.y < GameWindow.mouseY + R)
                {
                    //当前点在圆内时
                    if (R * R > (p.x - GameWindow.mouseX) * (p.x - GameWindow.mouseX) + (p.y - GameWindow.mouseY) * (p.y - GameWindow.mouseY))
                    {
                        //发散
                        if (V == 0) //移动y轴
                        {
                            double r1 = Math.Sqrt(R * R - (GameWindow.mouseX - p.x) * (GameWindow.mouseX - p.x));
                            if (p.y > GameWindow.mouseY)
                            {
                                p.y = (int)(GameWindow.mouseY + r1 * 3);
                            }
                            else
                            {
                                p.y = (int)(GameWindow.mouseY - r1 * 3);
                            }
                        }
                        else  //移动x轴
                        {
                            double r2 = Math.Sqrt(R * R - (GameWindow.mouseY - p.y) * (GameWindow.mouseY - p.y));
                            if (p.x > GameWindow.mouseX)
                            {
                                p.x = (int)(GameWindow.mouseX + r2 * 3);
                            }
                            else
                            {
                                p.x = (int)(GameWindow.mouseX - r2 * 3);
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
            Bitmap dstBitmap = new Bitmap(GameWindow.width, GameWindow.height, PixelFormat.Format24bppRgb);
            BitmapData dstBmData = dstBitmap.LockBits(new Rectangle(0, 0, GameWindow.width, GameWindow.height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int dst_bytes = GameWindow.height * dstBmData.Stride;
            byte[] dstValues = new byte[dst_bytes];


            foreach (ParticleObj p in particles)
            {
                if (p.y < 0 || p.y >= GameWindow.height || p.x < 0 || p.x >= GameWindow.width) continue;
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
