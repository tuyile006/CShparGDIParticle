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
            this.btnMouseParticle = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFluid = new System.Windows.Forms.Button();
            this.btn3dparticle = new System.Windows.Forms.Button();
            this.btnFontParticle = new System.Windows.Forms.Button();
            this.btnParticle2 = new System.Windows.Forms.Button();
            this.btnparticle1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.MediumTurquoise;
            this.splitContainer1.Panel1.Controls.Add(this.label2);
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
            this.splitContainer1.Size = new System.Drawing.Size(1100, 627);
            this.splitContainer1.SplitterDistance = 292;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnMouseParticle
            // 
            this.btnMouseParticle.BackColor = System.Drawing.SystemColors.Control;
            this.btnMouseParticle.Location = new System.Drawing.Point(33, 228);
            this.btnMouseParticle.Name = "btnMouseParticle";
            this.btnMouseParticle.Size = new System.Drawing.Size(232, 29);
            this.btnMouseParticle.TabIndex = 16;
            this.btnMouseParticle.Text = "鼠标粒子效果";
            this.btnMouseParticle.UseVisualStyleBackColor = false;
            this.btnMouseParticle.Click += new System.EventHandler(this.btnMouseParticle_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 581);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "fps=0";
            // 
            // btnFluid
            // 
            this.btnFluid.BackColor = System.Drawing.SystemColors.Control;
            this.btnFluid.Location = new System.Drawing.Point(33, 187);
            this.btnFluid.Name = "btnFluid";
            this.btnFluid.Size = new System.Drawing.Size(232, 29);
            this.btnFluid.TabIndex = 15;
            this.btnFluid.Text = "水流体效果";
            this.btnFluid.UseVisualStyleBackColor = false;
            this.btnFluid.Click += new System.EventHandler(this.btnFluid_Click);
            // 
            // btn3dparticle
            // 
            this.btn3dparticle.BackColor = System.Drawing.SystemColors.Control;
            this.btn3dparticle.Location = new System.Drawing.Point(33, 146);
            this.btn3dparticle.Name = "btn3dparticle";
            this.btn3dparticle.Size = new System.Drawing.Size(232, 29);
            this.btn3dparticle.TabIndex = 14;
            this.btn3dparticle.Text = "3D旋转粒子效果";
            this.btn3dparticle.UseVisualStyleBackColor = false;
            this.btn3dparticle.Click += new System.EventHandler(this.btn3dparticle_Click);
            // 
            // btnFontParticle
            // 
            this.btnFontParticle.BackColor = System.Drawing.SystemColors.Control;
            this.btnFontParticle.Location = new System.Drawing.Point(33, 105);
            this.btnFontParticle.Name = "btnFontParticle";
            this.btnFontParticle.Size = new System.Drawing.Size(232, 29);
            this.btnFontParticle.TabIndex = 12;
            this.btnFontParticle.Text = "文字粒子效果";
            this.btnFontParticle.UseVisualStyleBackColor = false;
            this.btnFontParticle.Click += new System.EventHandler(this.btnFontParticle_Click);
            // 
            // btnParticle2
            // 
            this.btnParticle2.BackColor = System.Drawing.SystemColors.Control;
            this.btnParticle2.Location = new System.Drawing.Point(33, 64);
            this.btnParticle2.Name = "btnParticle2";
            this.btnParticle2.Size = new System.Drawing.Size(232, 29);
            this.btnParticle2.TabIndex = 11;
            this.btnParticle2.Text = "鼓泡泡效果";
            this.btnParticle2.UseVisualStyleBackColor = false;
            this.btnParticle2.Click += new System.EventHandler(this.btnParticle2_Click);
            // 
            // btnparticle1
            // 
            this.btnparticle1.BackColor = System.Drawing.SystemColors.Control;
            this.btnparticle1.Location = new System.Drawing.Point(33, 23);
            this.btnparticle1.Name = "btnparticle1";
            this.btnparticle1.Size = new System.Drawing.Size(232, 29);
            this.btnparticle1.TabIndex = 10;
            this.btnparticle1.Text = "抹纱窗粒子效果";
            this.btnparticle1.UseVisualStyleBackColor = false;
            this.btnparticle1.Click += new System.EventHandler(this.btnparticle1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(804, 627);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 282);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 12);
            this.label2.TabIndex = 17;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 627);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "GDI绘图测试";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
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
    }
}

