using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static CSharpGDI.Form1;

namespace CSharpGDI
{
    //像素数据结构
    public struct pixelStr
    {
        public byte R;
        public byte G;
        public byte B;
        public int x;
        public int y;
    }
    internal class Helper
    {
        //内存法提取图片像素数据 
        public static List<pixelStr> GetImagePixel(Bitmap curBitmap)
        {
            List<pixelStr> result = new List<pixelStr>();
            if (curBitmap != null)
            {
                int width = curBitmap.Width;
                int height = curBitmap.Height;
                BitmapData data = curBitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                int length = height * data.Stride;
                byte[] RGB = new byte[length];
                System.IntPtr Scan0 = data.Scan0;
                System.Runtime.InteropServices.Marshal.Copy(Scan0, RGB, 0, length);

                int iPoint = 0;
                for (int i = 0; i < height; i++)
                {
                    iPoint = i * data.Stride;
                    for (int j = 0; j < width; j++)
                    {
                        //只处理每行中图像像素数据,舍弃未用空间
                        //注意位图结构中RGB按BGR的顺序存储
                        result.Add(new pixelStr
                        {
                            R = RGB[iPoint + 3 * j + 2],
                            G = RGB[iPoint + 3 * j + 1],
                            B = RGB[iPoint + 3 * j],
                            x = j,
                            y = i
                        });
                    }
                }

                //变换后更新图像
                //double gray = 0;
                //for (int i = 0; i < RGB.Length; i = i + 3)
                //{
                //    //gray = RGB[i + 2] * 0.3 + RGB[i + 1] * 0.59 + RGB[i] * 0.11;
                //    //RGB[i + 2] = RGB[i + 1] = RGB[i] = (byte)gray;
                //}
                //System.Runtime.InteropServices.Marshal.Copy(RGB, 0, Scan0, length);
                curBitmap.UnlockBits(data);
            }
            return result;
        }
        //提取图像像素数据 （此方法因为性能慢被淘汰！）
        public static List<pixelStr> GetImagePixel2(Bitmap img)
        {
            List<pixelStr> result = new List<pixelStr>();
            //int n = 0;
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    result.Add(new pixelStr
                    {
                        R = img.GetPixel(j, i).R,
                        G = img.GetPixel(j, i).G,
                        B = img.GetPixel(j, i).B,
                        x = j,
                        y = i
                    });
                }
            }
            return result;
        }
        

        //水平翻转
        public static List<pixelStr> FlipHorizontal(List<pixelStr> input,int width,int height)
        {
            pixelStr[] result = new pixelStr[input.Count];
           
            int n = 0;
            for (int y = 0; y < height;y++)
            {
                n = width * y;
                for (int x = 0; x < width; x++)
                {
                    result[n + x] = input[n + width - x-1];
                    result[n + x].x = x;
                    result[n + x].y = y;
                }
            }
            return result.ToList();
        }

        //灰度变换
        public static List<pixelStr> GrayTrans(List<pixelStr> input)
        {
            List<pixelStr> result = new List<pixelStr>();
            foreach (pixelStr p in input)
            {
                //根据Y=0.299*R+0.114*G+0.587*B,Y为亮度  并非唯一，但是最佳视觉
                byte gray = (byte)(0.299 * p.R + 0.114 * p.G + 0.587 * p.B);
                result.Add(new pixelStr
                {
                    x = p.x,
                    y = p.y,
                    B= gray,
                    G= gray,
                    R= gray
                });
            }
            return result;
        }

        //灰度变换带宽度
        public static List<pixelStr> GrayTransByWidth(List<pixelStr> input,int width)
        {
            List<pixelStr> result = new List<pixelStr>();
            foreach (pixelStr p in input)
            {
                if (p.x < width)
                {
                    //根据Y=0.299*R+0.114*G+0.587*B,Y为亮度  并非唯一，但是最佳视觉
                    byte gray = (byte)(0.299 * p.R + 0.114 * p.G + 0.587 * p.B);
                    result.Add(new pixelStr
                    {
                        x = p.x,
                        y = p.y,
                        B = gray,
                        G = gray,
                        R = gray
                    });
                }
                else if(p.x>=width&&p.x<width+3)
                {
                    result.Add(new pixelStr
                    {
                        x = p.x,
                        y = p.y,
                        B = 0,
                        G = 0,
                        R = 255
                    });
                }
                else
                {
                    result.Add(p);
                }
                
            }
            return result;
        }


        /// <summary>
        /// RGB变换
        /// </summary>
        /// <param name="input">输入像素数据</param>
        /// <param name="R">红色变化值0-255</param>
        /// <param name="Rbool">是否更改R的值</param>
        /// <param name="G">绿色变化值0-255</param>
        /// <param name="Gbool">是否更改G的值</param>
        /// <param name="B">蓝色变化值0-255</param>
        /// <param name="Bbool">是否更改B的值</param>
        /// <returns></returns>
        public static List<pixelStr> RGBTrans(List<pixelStr> input, byte R, bool Rbool, byte G, bool Gbool, byte B, bool Bbool)
        {
            List<pixelStr> result = new List<pixelStr>();
            foreach (pixelStr p in input)
            {
                result.Add(new pixelStr
                {
                    x = p.x,
                    y = p.y,
                    R = Rbool?R:p.R,
                    G = Gbool?G:p.G,
                    B = Bbool?B:p.B
                });
            }
            return result;
        }

       
        /// <summary>
        /// 爆炸动画 获取像素爆炸随机位置（反复调用）
        /// </summary>
        /// <param name="bombPixels">爆炸后位置,初始为空</param>
        /// <param name="input">原图像素数据，不可为空</param>
        /// <param name="radius">回归半径，若随机到此半径内则回归原位置</param>
        /// <returns>新的爆炸后像素数据</returns>
        public static List<pixelStr> BombTrans(List<pixelStr> bombPixels,List<pixelStr> input, int width, int height,int radius,out bool isFinish)
        {
            if (bombPixels == null)
            {
                List<pixelStr> result = new List<pixelStr>();
                var random = new Random();
                foreach (pixelStr p in input)
                {
                    result.Add(new pixelStr
                    {
                        x = random.Next(width),
                        y = random.Next(height),
                        R = p.R,
                        G = p.G,
                        B = p.B
                    });
                }
                isFinish = false;
                return result;
            }
            else
            {
                var random = new Random();
                bool finish = true; //是否已经全部归位
                for (int i=0;i< input.Count;i++)
                {
                    if (bombPixels[i].x == input[i].x && bombPixels[i].y == input[i].y) continue;
                    //如果接近归一半径，则归一
                    if (Math.Abs(bombPixels[i].x - input[i].x) < radius && Math.Abs(bombPixels[i].y - input[i].y) < radius)
                    {
                        pixelStr tp = bombPixels[i];
                        tp.x = input[i].x;
                        tp.y = input[i].y;
                        bombPixels[i] = tp;

                    }
                    else //否则继续乱动
                    {
                        pixelStr tp = bombPixels[i];
                        tp.x = random.Next(width);
                        tp.y = random.Next(height);
                        bombPixels[i] = tp;
                    }
                    finish = false;
                }
                isFinish = finish;
                return bombPixels;
            }
        }

        //实验
        public static List<pixelStr> moTrans(List<pixelStr> input, int width, int height,int x,int y, int radius)
        {
            List <pixelStr> result = new List<pixelStr>();
            //以x,y坐标周围像素变色
            int xFrom = (x - radius)< 0? 0:(x-radius);
            int xEnd = (x + radius)>width? width:(x+radius);

            int yFrom = (y - radius) < 0 ? 0 : (y - radius);
            int yEnd = (y + radius) > height ? height : (y+ radius);

            var random = new Random();
            for (int i = 0; i < input.Count; i++)
            {
                pixelStr p=input[i];
                if (p.x > xFrom && p.x < xEnd
                    &&p.y>yFrom && p.y<yEnd)
                {
                    double d = Math.Sqrt((p.x - x) * (p.x - x) + (p.y - y) * (p.y - y));
                    if (d<radius)
                    {
                        //byte color= (byte)(random.Next(p.R));
                        p.R = (byte)(random.Next(p.R));
                        p.G = (byte)(random.Next(p.G));
                        p.B = (byte)(random.Next(p.B));
                       
                    }

                    if (p.x == x || p.y == y|| Math.Abs(d-radius)<=1)
                    { 
                        p.R=p.G=p.B=255;
                    }
                   
                }
                result.Add(p);
            }
            return result;
        }

        //将像素数据还原成图片
        public static Bitmap PushImgData(List<pixelStr> input, int width, int height)
        {
            Bitmap dstBitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
            BitmapData dstBmData = dstBitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int dst_bytes = height * dstBmData.Stride;
            byte[] dstValues = new byte[dst_bytes];
            

            foreach (pixelStr p in input) {
                int n = p.y * dstBmData.Stride + p.x * 3;
                //只处理每行中图像像素数据,舍弃未用空间
                //注意位图结构中RGB按BGR的顺序存储
                dstValues[n] = p.B;
                dstValues[n + 1] = p.G;
                dstValues[n + 2] = p.R;
            }
            System.IntPtr dstPtr = dstBmData.Scan0;
            System.Runtime.InteropServices.Marshal.Copy(dstValues, 0, dstPtr, dst_bytes);
            dstBitmap.UnlockBits(dstBmData);

            return dstBitmap;
        }


        #region Debounce策略 解决频繁事件性能问题，如mouseMove
        /// <summary> 
        /// 在指定时间过后执行指定的表达式 
        /// </summary> 
        /// <param name="interval">事件之间经过的时间（以毫秒为单位）</param> 
        /// <param name="action">要执行的表达式</param> 
        public static System.Timers.Timer SetTimeout(Action action, double interval)
        {
            System.Timers.Timer timer = new System.Timers.Timer(interval);
            timer.Elapsed += delegate (object sender, System.Timers.ElapsedEventArgs e)
            {
                timer.Enabled = false;
                action();
            };
            timer.Enabled = true;
            return timer;
        }

        public static void ClearTimer(System.Timers.Timer timer)
        {
            if (timer != null)
            {
                timer.Stop();
                timer.Enabled = false;
                timer = null;
            }
        }
        public static System.Timers.Timer SetInterval(Action action, double interval)
        {
            System.Timers.Timer timer = new System.Timers.Timer(interval);
            timer.Elapsed += delegate (object sender, System.Timers.ElapsedEventArgs e)
            {
                action();
            };
            timer.Enabled = true;
            return timer;
        }

        static System.Timers.Timer timer = null;
        /// <summary>
        /// 使用Debounce策略减少连续频繁执行某个动作
        /// </summary>
        /// <param name="action">要执行的动作</param>
        /// <param name="interval">至少间隔毫秒数</param>
        /// <returns></returns>
        public static void Debounce(Action action,double interval)
        {
           ClearTimer(timer);
           timer = SetTimeout(action, interval);
        }
        #endregion
    }
}
