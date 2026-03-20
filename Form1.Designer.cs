namespace CSharpGDI
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            splitContainer1 = new SplitContainer();
            btnBGtimer = new Button();
            btnMagnetism = new Button();
            label2 = new Label();
            btnMathArc = new Button();
            btnMouseParticle = new Button();
            label1 = new Label();
            btnFluid = new Button();
            btn3dparticle = new Button();
            btnFontParticle = new Button();
            btnParticle2 = new Button();
            btnparticle1 = new Button();
            MathGroup = new GroupBox();
            btnNextMath = new Button();
            trackBar1_split = new TrackBar();
            label3 = new Label();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            MathGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar1_split).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Margin = new Padding(4);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.BackColor = Color.MediumTurquoise;
            splitContainer1.Panel1.Controls.Add(btnBGtimer);
            splitContainer1.Panel1.Controls.Add(btnMagnetism);
            splitContainer1.Panel1.Controls.Add(label2);
            splitContainer1.Panel1.Controls.Add(btnMathArc);
            splitContainer1.Panel1.Controls.Add(btnMouseParticle);
            splitContainer1.Panel1.Controls.Add(label1);
            splitContainer1.Panel1.Controls.Add(btnFluid);
            splitContainer1.Panel1.Controls.Add(btn3dparticle);
            splitContainer1.Panel1.Controls.Add(btnFontParticle);
            splitContainer1.Panel1.Controls.Add(btnParticle2);
            splitContainer1.Panel1.Controls.Add(btnparticle1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(MathGroup);
            splitContainer1.Panel2.Controls.Add(pictureBox1);
            splitContainer1.Size = new Size(1090, 709);
            splitContainer1.SplitterDistance = 179;
            splitContainer1.SplitterWidth = 5;
            splitContainer1.TabIndex = 0;
            // 
            // btnBGtimer
            // 
            btnBGtimer.BackColor = SystemColors.Control;
            btnBGtimer.Location = new Point(15, 438);
            btnBGtimer.Margin = new Padding(4);
            btnBGtimer.Name = "btnBGtimer";
            btnBGtimer.Size = new Size(150, 41);
            btnBGtimer.TabIndex = 23;
            btnBGtimer.Text = "八卦时钟";
            btnBGtimer.UseVisualStyleBackColor = false;
            btnBGtimer.Click += btnBGtimer_Click;
            // 
            // btnMagnetism
            // 
            btnMagnetism.BackColor = SystemColors.Control;
            btnMagnetism.Location = new Point(15, 380);
            btnMagnetism.Margin = new Padding(4);
            btnMagnetism.Name = "btnMagnetism";
            btnMagnetism.Size = new Size(150, 41);
            btnMagnetism.TabIndex = 22;
            btnMagnetism.Text = "磁吸效果";
            btnMagnetism.UseVisualStyleBackColor = false;
            btnMagnetism.Click += btnMagnetism_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 611);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(0, 17);
            label2.TabIndex = 17;
            // 
            // btnMathArc
            // 
            btnMathArc.BackColor = SystemColors.Control;
            btnMathArc.Location = new Point(15, 497);
            btnMathArc.Margin = new Padding(4);
            btnMathArc.Name = "btnMathArc";
            btnMathArc.Size = new Size(150, 41);
            btnMathArc.TabIndex = 18;
            btnMathArc.Text = "数学曲线";
            btnMathArc.UseVisualStyleBackColor = false;
            btnMathArc.Click += btnMathArc_Click;
            // 
            // btnMouseParticle
            // 
            btnMouseParticle.BackColor = SystemColors.Control;
            btnMouseParticle.Location = new Point(15, 322);
            btnMouseParticle.Margin = new Padding(4);
            btnMouseParticle.Name = "btnMouseParticle";
            btnMouseParticle.Size = new Size(150, 41);
            btnMouseParticle.TabIndex = 16;
            btnMouseParticle.Text = "爱心效果";
            btnMouseParticle.UseVisualStyleBackColor = false;
            btnMouseParticle.Click += btnMouseParticle_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 641);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(42, 17);
            label1.TabIndex = 8;
            label1.Text = "fps=0";
            // 
            // btnFluid
            // 
            btnFluid.BackColor = SystemColors.Control;
            btnFluid.Location = new Point(15, 264);
            btnFluid.Margin = new Padding(4);
            btnFluid.Name = "btnFluid";
            btnFluid.Size = new Size(150, 41);
            btnFluid.TabIndex = 15;
            btnFluid.Text = "水流体效果";
            btnFluid.UseVisualStyleBackColor = false;
            btnFluid.Click += btnFluid_Click;
            // 
            // btn3dparticle
            // 
            btn3dparticle.BackColor = SystemColors.Control;
            btn3dparticle.Location = new Point(15, 206);
            btn3dparticle.Margin = new Padding(4);
            btn3dparticle.Name = "btn3dparticle";
            btn3dparticle.Size = new Size(150, 41);
            btn3dparticle.TabIndex = 14;
            btn3dparticle.Text = "3D旋转粒子效果";
            btn3dparticle.UseVisualStyleBackColor = false;
            btn3dparticle.Click += btn3dparticle_Click;
            // 
            // btnFontParticle
            // 
            btnFontParticle.BackColor = SystemColors.Control;
            btnFontParticle.Location = new Point(15, 148);
            btnFontParticle.Margin = new Padding(4);
            btnFontParticle.Name = "btnFontParticle";
            btnFontParticle.Size = new Size(150, 41);
            btnFontParticle.TabIndex = 12;
            btnFontParticle.Text = "文字粒子效果";
            btnFontParticle.UseVisualStyleBackColor = false;
            btnFontParticle.Click += btnFontParticle_Click;
            // 
            // btnParticle2
            // 
            btnParticle2.BackColor = SystemColors.Control;
            btnParticle2.Location = new Point(15, 90);
            btnParticle2.Margin = new Padding(4);
            btnParticle2.Name = "btnParticle2";
            btnParticle2.Size = new Size(150, 41);
            btnParticle2.TabIndex = 11;
            btnParticle2.Text = "鼓泡泡效果";
            btnParticle2.UseVisualStyleBackColor = false;
            btnParticle2.Click += btnParticle2_Click;
            // 
            // btnparticle1
            // 
            btnparticle1.BackColor = SystemColors.Control;
            btnparticle1.Location = new Point(15, 33);
            btnparticle1.Margin = new Padding(4);
            btnparticle1.Name = "btnparticle1";
            btnparticle1.Size = new Size(150, 41);
            btnparticle1.TabIndex = 10;
            btnparticle1.Text = "抹纱窗粒子效果";
            btnparticle1.UseVisualStyleBackColor = false;
            btnparticle1.Click += btnparticle1_Click;
            // 
            // MathGroup
            // 
            MathGroup.Controls.Add(btnNextMath);
            MathGroup.Controls.Add(trackBar1_split);
            MathGroup.Controls.Add(label3);
            MathGroup.Location = new Point(674, 0);
            MathGroup.Margin = new Padding(12, 14, 12, 14);
            MathGroup.Name = "MathGroup";
            MathGroup.Padding = new Padding(12, 14, 12, 14);
            MathGroup.Size = new Size(232, 156);
            MathGroup.TabIndex = 21;
            MathGroup.TabStop = false;
            MathGroup.Text = "曲线控制";
            MathGroup.Visible = false;
            // 
            // btnNextMath
            // 
            btnNextMath.Location = new Point(90, 103);
            btnNextMath.Margin = new Padding(2, 3, 2, 3);
            btnNextMath.Name = "btnNextMath";
            btnNextMath.Size = new Size(111, 38);
            btnNextMath.TabIndex = 23;
            btnNextMath.Text = "下一个曲线";
            btnNextMath.UseVisualStyleBackColor = true;
            btnNextMath.Click += btnNextMath_Click;
            // 
            // trackBar1_split
            // 
            trackBar1_split.BackColor = SystemColors.Control;
            trackBar1_split.Location = new Point(90, 34);
            trackBar1_split.Margin = new Padding(2, 3, 2, 3);
            trackBar1_split.Maximum = 300;
            trackBar1_split.Minimum = 5;
            trackBar1_split.Name = "trackBar1_split";
            trackBar1_split.Size = new Size(130, 45);
            trackBar1_split.TabIndex = 19;
            trackBar1_split.TickFrequency = 20;
            trackBar1_split.Value = 5;
            trackBar1_split.ValueChanged += trackBar1_split_ValueChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(20, 55);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(68, 17);
            label3.TabIndex = 20;
            label3.Text = "刻度尺寸：";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Black;
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Margin = new Padding(4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(906, 709);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Paint += pictureBox1_Paint;
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1.MouseEnter += pictureBox1_MouseEnter;
            pictureBox1.MouseLeave += pictureBox1_MouseLeave;
            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.MouseUp += pictureBox1_MouseUp;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1090, 709);
            Controls.Add(splitContainer1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "Form1";
            SizeGripStyle = SizeGripStyle.Hide;
            Text = "GDI绘图测试";
            Load += Form1_Load;
            Resize += Form1_Resize;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            MathGroup.ResumeLayout(false);
            MathGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar1_split).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnparticle1;
        private System.Windows.Forms.Button btnParticle2;
        private System.Windows.Forms.Button btnFontParticle;
        private System.Windows.Forms.Button btn3dparticle;
        private System.Windows.Forms.Button btnFluid;
        private System.Windows.Forms.Button btnMouseParticle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnMathArc;
        private System.Windows.Forms.TrackBar trackBar1_split;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox MathGroup;
        private System.Windows.Forms.Button btnNextMath;
        private System.Windows.Forms.Button btnMagnetism;
        private System.Windows.Forms.Button btnBGtimer;
    }
}


