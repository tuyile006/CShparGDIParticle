using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpGDI
{
    public class GameWindow
    {
        public static int width;
        public static int height;
        public static int mouseX;
        public static int mouseY;
        public static int mouse_V; //鼠标移动方向 0：x  1：y
        public static bool isMouseDown; //鼠标是否按下
        public static bool isMouseIn; //鼠标是否在区域内
    }
}
