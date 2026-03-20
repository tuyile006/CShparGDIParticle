using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Net.Mail;

namespace CSharpGDI
{
    /// <summary>
    /// 粒子效果9——八卦时钟
    /// </summary>
    public class Particle9: IParticle
    {
        Bitmap dstBitmap;
        Graphics g;
        static int CanvasWidth = 850;
        static int CanvasHeight = 850;
        static Font txtFont = new Font("微软雅黑", 12);
        int _centerX = CanvasWidth/ 2;
        int _centerY = CanvasHeight/ 2;
       
        // 环形层级配置
        private  Layer[] _layers = new[]
        {
            new Layer { Radius = 85, TextFont= txtFont,Type = LayerType.Month, Max = 12 },
            new Layer { Radius = 140,TextFont= txtFont,Type = LayerType.Day, Max = 31 },
            new Layer { Radius = 200,TextFont= txtFont,Type = LayerType.Week, Max = 7 },
            new Layer { Radius = 255,TextFont= txtFont,Type = LayerType.Hour, Max = 12 },
            new Layer { Radius = 310,TextFont= txtFont,Type = LayerType.Minute, Max = 59 },
            new Layer { Radius = 380,TextFont= txtFont,Type = LayerType.Second, Max = 59 }
           };

        public void Start()
        {
            dstBitmap = new Bitmap(CanvasWidth, CanvasHeight, PixelFormat.Format24bppRgb);
            g = Graphics.FromImage(dstBitmap);
            g.Clear(Color.Black);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            // 设置文本渲染提示为高质量抗锯齿
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            // 设置像素偏移模式为高质量
            //g.PixelOffsetMode = PixelOffsetMode.HighQuality;
        }

        public Bitmap Update()
        {
            g.Clear(Color.Black);
          
            // 获取当前时间数据
            TimeData timeData = GetTimeData();

            // 1. 绘制圆心年份+天干地支
            DrawCenterYear(g, timeData.Year);

            // 2. 绘制各环形刻度
            DrawLayerWithStaticCurrent(g, _layers[0], timeData.Month);  // 月
            DrawLayerWithStaticCurrent(g, _layers[1], timeData.Day);    // 日
            DrawLayerWithStaticCurrent(g, _layers[2], timeData.Week);   // 周
            DrawLayerWithStaticCurrent(g, _layers[3], timeData.Hour);   // 时
            DrawLayerWithStaticCurrent(g, _layers[4], timeData.Minute); // 分
            DrawLayerWithStaticCurrent(g, _layers[5], timeData.Second); // 秒

            return dstBitmap;
        }
        // 获取当前时间数据
        private TimeData GetTimeData()
        {
            DateTime now = DateTime.Now;
            int weekNum = (int)now.DayOfWeek;
            // 转换：1=周一，7=周日
            weekNum = weekNum == 0 ? 7 : weekNum;

            return new TimeData
            {
                Year = now.Year,
                Week = weekNum,
                Month = now.Month,
                Day = now.Day,
                Hour = now.Hour % 12 == 0 ? 12 : now.Hour % 12,
                Minute = now.Minute,
                Second = now.Second
            };
        }

        // 绘制环形刻度
        private void DrawLayerWithStaticCurrent(Graphics g, Layer layer, int currentValue)
        {
            // 外层先保存一次状态
            GraphicsState outerState = g.Save();

            for (int i = 1; i <= layer.Max; i++)
            {
                // 计算角度--弧度
                double angle = ((i - currentValue) / (double)layer.Max) * Math.PI * 2;
                // 计算文字位置
                float x = _centerX + (float)(layer.Radius * Math.Cos(angle));
                float y = _centerY + (float)(layer.Radius * Math.Sin(angle));

                // 1. 保存当前绘图状态（返回状态ID，确保一一对应）
                GraphicsState stateId = g.Save();

                try
                {
                    // 2. 平移+旋转（对应原translate+rotate）
                    g.TranslateTransform(x, y);
                    g.RotateTransform((float)(angle * 180 / Math.PI));

                    // 设置字体和颜色
                    Brush brush = i == currentValue ? Brushes.Gold : Brushes.White;

                    // 获取要显示的文字
                    string text = GetLayerText(layer.Type, i);

                    // 绘制文字（居中对齐）
                    SizeF textSize = g.MeasureString(text, layer.TextFont);
                    g.DrawString(text, layer.TextFont, brush, -textSize.Width / 2, -textSize.Height / 2);

                }
                finally
                {
                    // 3. 必须恢复状态（和Save一一对应）
                    g.Restore(stateId);
                }
            }

            // 恢复外层状态
            g.Restore(outerState);
        }

        // 绘制圆心年份（中文年份+天干地支）
        private void DrawCenterYear(Graphics g, int year)
        {
            // 第一行：中文年份
            Font yearFont = new Font("微软雅黑", 14);
            string chineseYear = GetChineseYear(year);
            SizeF yearTextSize = g.MeasureString(chineseYear, yearFont);
            g.DrawString(chineseYear, yearFont, Brushes.Gold,
                _centerX - yearTextSize.Width / 2,
                _centerY - 15 - yearTextSize.Height / 2);

            // 第二行：天干地支
            Font ganZhiFont = new Font("微软雅黑", 12);
            string ganZhi = GetGanZhiYear(year);
            SizeF ganZhiTextSize = g.MeasureString(ganZhi, ganZhiFont);
            g.DrawString(ganZhi, ganZhiFont, Brushes.Gold,
                _centerX - ganZhiTextSize.Width / 2,
                _centerY + 15 - ganZhiTextSize.Height / 2);

            // 释放资源
            yearFont.Dispose();
            ganZhiFont.Dispose();
        }

        // 数字转中文（对应原toChineseNum）
        private string ToChineseNum(int num)
        {
            string[] units = { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九" };
            string[] tens = { "", "十", "二十", "三十", "四十", "五十", "六十", "七十", "八十", "九十" };

            if (num == 0) return "零";
            if (num < 10) return units[num];

            int ten = num / 10;
            int unit = num % 10;

            if (ten == 1 && unit == 0) return "十";
            if (ten == 1 && unit > 0) return "十" + units[unit];

            return tens[ten] + (unit > 0 ? units[unit] : "");
        }

        // 年份转中文（对应原getChineseYear）
        private string GetChineseYear(int year)
        {
            string[] units = { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九" };
            string yearStr = year.ToString();
            string chineseYear = "";

            foreach (char c in yearStr)
            {
                chineseYear += units[int.Parse(c.ToString())];
            }

            return chineseYear + "年";
        }

        // 天干地支计算（对应原getGanZhiYear）
        private string GetGanZhiYear(int year)
        {
            string[] tianGan = { "甲", "乙", "丙", "丁", "戊", "己", "庚", "辛", "壬", "癸" };
            string[] diZhi = { "子", "丑", "寅", "卯", "辰", "巳", "午", "未", "申", "酉", "戌", "亥" };
            string[] shengXiao = { "鼠", "牛", "虎", "兔", "龙", "蛇", "马", "羊", "猴", "鸡", "狗", "猪" };

            int baseYear = 1900;
            int offset = year - baseYear;
            int ganIndex = offset % 10;
            int zhiIndex = offset % 12;

            // 处理负数索引（兼容1900年前的年份）
            if (ganIndex < 0) ganIndex += 10;
            if (zhiIndex < 0) zhiIndex += 12;

            return $"{tianGan[ganIndex]}{diZhi[zhiIndex]}年（{shengXiao[zhiIndex]}）";
        }

        // 星期转换（对应原getChineseWeek）
        private string GetChineseWeek(int weekNum)
        {
            string[] weekNames = { "", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六", "星期日" };
            return weekNames[weekNum];
        }

        // 获取环形文字（适配不同类型）
        private string GetLayerText(LayerType type, int num)
        {
            switch(type)
            {
                case LayerType.Week: return GetChineseWeek(num);
                case LayerType.Month:return ToChineseNum(num) + "月";
                case LayerType.Day: return ToChineseNum(num) + "日";
                case LayerType.Hour: return ToChineseNum(num) + "时";
                case LayerType.Minute: return ToChineseNum(num) + "分";
                case LayerType.Second: return ToChineseNum(num) + "秒";
                default: return "";
            }
        }
        // 辅助类：环形层级
        private class Layer
        {
            public int Radius { get; set; }
            public Font TextFont { get; set; }
            public LayerType Type { get; set; }
            public int Max { get; set; }
        }

        // 辅助枚举：环形类型
        private enum LayerType
        {
            Week, Month, Day, Hour, Minute, Second
        }

        // 辅助类：时间数据
        private class TimeData
        {
            public int Year { get; set; }
            public int Week { get; set; }
            public int Month { get; set; }
            public int Day { get; set; }
            public int Hour { get; set; }
            public int Minute { get; set; }
            public int Second { get; set; }
        }
    }

}

