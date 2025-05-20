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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnNextMath = new System.Windows.Forms.Button();
            this.trackBar1_split = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnMathArc = new System.Windows.Forms.Button();
            this.btnMouseParticle = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFluid = new System.Windows.Forms.Button();
            this.btn3dparticle = new System.Windows.Forms.Button();
            this.btnFontParticle = new System.Windows.Forms.Button();
            this.btnParticle2 = new System.Windows.Forms.Button();
            this.btnparticle1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1_split)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.MediumTurquoise;
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.btnMathArc);
            this.splitContainer1.Panel1.Controls.Add(this.btnMouseParticle);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.btnFluid);
            this.splitContainer1.Panel1.Controls.Add(this.btn3dparticle);
            this.splitContainer1.Panel1.Controls.Add(this.btnFontParticle);
            this.splitContainer1.Panel1.Controls.Add(this.btnParticle2);
            this.splitContainer1.Panel1.Controls.Add(this.btnparticle1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pictureBox1);
            this.splitContainer1.Size = new System.Drawing.Size(1650, 940);
            this.splitContainer1.SplitterDistance = 438;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnNextMath);
            this.groupBox1.Controls.Add(this.trackBar1_split);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(29, 459);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(389, 170);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Visible = false;
            // 
            // btnNextMath
            // 
            this.btnNextMath.Location = new System.Drawing.Point(126, 101);
            this.btnNextMath.Name = "btnNextMath";
            this.btnNextMath.Size = new System.Drawing.Size(143, 40);
            this.btnNextMath.TabIndex = 23;
            this.btnNextMath.Text = "下一个曲线";
            this.btnNextMath.UseVisualStyleBackColor = true;
            this.btnNextMath.Click += new System.EventHandler(this.btnNextMath_Click);
            // 
            // trackBar1_split
            // 
            this.trackBar1_split.BackColor = System.Drawing.Color.MediumTurquoise;
            this.trackBar1_split.Location = new System.Drawing.Point(116, 36);
            this.trackBar1_split.Maximum = 300;
            this.trackBar1_split.Minimum = 5;
            this.trackBar1_split.Name = "trackBar1_split";
            this.trackBar1_split.Size = new System.Drawing.Size(248, 69);
            this.trackBar1_split.TabIndex = 19;
            this.trackBar1_split.TickFrequency = 20;
            this.trackBar1_split.Value = 5;
            this.trackBar1_split.ValueChanged += new System.EventHandler(this.trackBar1_split_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 18);
            this.label3.TabIndex = 20;
            this.label3.Text = "刻度尺寸：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 835);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 18);
            this.label2.TabIndex = 17;
            // 
            // btnMathArc
            // 
            this.btnMathArc.BackColor = System.Drawing.SystemColors.Control;
            this.btnMathArc.Location = new System.Drawing.Point(50, 408);
            this.btnMathArc.Margin = new System.Windows.Forms.Padding(4);
            this.btnMathArc.Name = "btnMathArc";
            this.btnMathArc.Size = new System.Drawing.Size(348, 44);
            this.btnMathArc.TabIndex = 18;
            this.btnMathArc.Text = "数学曲线";
            this.btnMathArc.UseVisualStyleBackColor = false;
            this.btnMathArc.Click += new System.EventHandler(this.btnMathArc_Click);
            // 
            // btnMouseParticle
            // 
            this.btnMouseParticle.BackColor = System.Drawing.SystemColors.Control;
            this.btnMouseParticle.Location = new System.Drawing.Point(50, 342);
            this.btnMouseParticle.Margin = new System.Windows.Forms.Padding(4);
            this.btnMouseParticle.Name = "btnMouseParticle";
            this.btnMouseParticle.Size = new System.Drawing.Size(348, 44);
            this.btnMouseParticle.TabIndex = 16;
            this.btnMouseParticle.Text = "爱心效果";
            this.btnMouseParticle.UseVisualStyleBackColor = false;
            this.btnMouseParticle.Click += new System.EventHandler(this.btnMouseParticle_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 872);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 18);
            this.label1.TabIndex = 8;
            this.label1.Text = "fps=0";
            // 
            // btnFluid
            // 
            this.btnFluid.BackColor = System.Drawing.SystemColors.Control;
            this.btnFluid.Location = new System.Drawing.Point(50, 280);
            this.btnFluid.Margin = new System.Windows.Forms.Padding(4);
            this.btnFluid.Name = "btnFluid";
            this.btnFluid.Size = new System.Drawing.Size(348, 44);
            this.btnFluid.TabIndex = 15;
            this.btnFluid.Text = "水流体效果";
            this.btnFluid.UseVisualStyleBackColor = false;
            this.btnFluid.Click += new System.EventHandler(this.btnFluid_Click);
            // 
            // btn3dparticle
            // 
            this.btn3dparticle.BackColor = System.Drawing.SystemColors.Control;
            this.btn3dparticle.Location = new System.Drawing.Point(50, 219);
            this.btn3dparticle.Margin = new System.Windows.Forms.Padding(4);
            this.btn3dparticle.Name = "btn3dparticle";
            this.btn3dparticle.Size = new System.Drawing.Size(348, 44);
            this.btn3dparticle.TabIndex = 14;
            this.btn3dparticle.Text = "3D旋转粒子效果";
            this.btn3dparticle.UseVisualStyleBackColor = false;
            this.btn3dparticle.Click += new System.EventHandler(this.btn3dparticle_Click);
            // 
            // btnFontParticle
            // 
            this.btnFontParticle.BackColor = System.Drawing.SystemColors.Control;
            this.btnFontParticle.Location = new System.Drawing.Point(50, 158);
            this.btnFontParticle.Margin = new System.Windows.Forms.Padding(4);
            this.btnFontParticle.Name = "btnFontParticle";
            this.btnFontParticle.Size = new System.Drawing.Size(348, 44);
            this.btnFontParticle.TabIndex = 12;
            this.btnFontParticle.Text = "文字粒子效果";
            this.btnFontParticle.UseVisualStyleBackColor = false;
            this.btnFontParticle.Click += new System.EventHandler(this.btnFontParticle_Click);
            // 
            // btnParticle2
            // 
            this.btnParticle2.BackColor = System.Drawing.SystemColors.Control;
            this.btnParticle2.Location = new System.Drawing.Point(50, 96);
            this.btnParticle2.Margin = new System.Windows.Forms.Padding(4);
            this.btnParticle2.Name = "btnParticle2";
            this.btnParticle2.Size = new System.Drawing.Size(348, 44);
            this.btnParticle2.TabIndex = 11;
            this.btnParticle2.Text = "鼓泡泡效果";
            this.btnParticle2.UseVisualStyleBackColor = false;
            this.btnParticle2.Click += new System.EventHandler(this.btnParticle2_Click);
            // 
            // btnparticle1
            // 
            this.btnparticle1.BackColor = System.Drawing.SystemColors.Control;
            this.btnparticle1.Location = new System.Drawing.Point(50, 34);
            this.btnparticle1.Margin = new System.Windows.Forms.Padding(4);
            this.btnparticle1.Name = "btnparticle1";
            this.btnparticle1.Size = new System.Drawing.Size(348, 44);
            this.btnparticle1.TabIndex = 10;
            this.btnparticle1.Text = "抹纱窗粒子效果";
            this.btnparticle1.UseVisualStyleBackColor = false;
            this.btnparticle1.Click += new System.EventHandler(this.btnparticle1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1206, 940);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1650, 940);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "GDI绘图测试";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1_split)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnNextMath;
    }
}

