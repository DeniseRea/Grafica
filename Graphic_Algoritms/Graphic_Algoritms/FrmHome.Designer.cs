namespace Graphic_Algoritms
{
    partial class FrmHome
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_CurvAlg = new System.Windows.Forms.Button();
            this.btn_CutAlg = new System.Windows.Forms.Button();
            this.btn_FillAlg = new System.Windows.Forms.Button();
            this.btn_Raster = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btn_reset = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdBtn_op4 = new System.Windows.Forms.RadioButton();
            this.rdBtn_op3 = new System.Windows.Forms.RadioButton();
            this.rdBtn_op2 = new System.Windows.Forms.RadioButton();
            this.rdBtn_op1 = new System.Windows.Forms.RadioButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_CurvAlg);
            this.groupBox1.Controls.Add(this.btn_CutAlg);
            this.groupBox1.Controls.Add(this.btn_FillAlg);
            this.groupBox1.Controls.Add(this.btn_Raster);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(199, 671);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Algoritmos";
            // 
            // btn_CurvAlg
            // 
            this.btn_CurvAlg.Location = new System.Drawing.Point(24, 480);
            this.btn_CurvAlg.Name = "btn_CurvAlg";
            this.btn_CurvAlg.Size = new System.Drawing.Size(154, 62);
            this.btn_CurvAlg.TabIndex = 3;
            this.btn_CurvAlg.Text = "Curvas";
            this.btn_CurvAlg.UseVisualStyleBackColor = true;
            this.btn_CurvAlg.Click += new System.EventHandler(this.btn_CurvAlg_Click);
            // 
            // btn_CutAlg
            // 
            this.btn_CutAlg.Location = new System.Drawing.Point(24, 334);
            this.btn_CutAlg.Name = "btn_CutAlg";
            this.btn_CutAlg.Size = new System.Drawing.Size(154, 62);
            this.btn_CutAlg.TabIndex = 2;
            this.btn_CutAlg.Text = "Recorte";
            this.btn_CutAlg.UseVisualStyleBackColor = true;
            this.btn_CutAlg.Click += new System.EventHandler(this.btn_CutAlg_Click);
            // 
            // btn_FillAlg
            // 
            this.btn_FillAlg.Location = new System.Drawing.Point(24, 199);
            this.btn_FillAlg.Name = "btn_FillAlg";
            this.btn_FillAlg.Size = new System.Drawing.Size(154, 62);
            this.btn_FillAlg.TabIndex = 1;
            this.btn_FillAlg.Text = "Relleno";
            this.btn_FillAlg.UseVisualStyleBackColor = true;
            this.btn_FillAlg.Click += new System.EventHandler(this.btn_FillAlg_Click);
            // 
            // btn_Raster
            // 
            this.btn_Raster.Location = new System.Drawing.Point(24, 75);
            this.btn_Raster.Name = "btn_Raster";
            this.btn_Raster.Size = new System.Drawing.Size(154, 62);
            this.btn_Raster.TabIndex = 0;
            this.btn_Raster.Text = "Rasterizado";
            this.btn_Raster.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_Raster.UseVisualStyleBackColor = true;
            this.btn_Raster.Click += new System.EventHandler(this.btn_Raster_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F);
            this.groupBox2.Location = new System.Drawing.Point(236, 26);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(881, 671);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.textBox1);
            this.groupBox5.Location = new System.Drawing.Point(28, 21);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(838, 171);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Instrucciones";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(0, 21);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(832, 144);
            this.textBox1.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btn_reset);
            this.groupBox4.Location = new System.Drawing.Point(28, 509);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 122);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Opciones";
            // 
            // btn_reset
            // 
            this.btn_reset.Location = new System.Drawing.Point(25, 52);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(153, 44);
            this.btn_reset.TabIndex = 0;
            this.btn_reset.Text = "Resetear";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdBtn_op4);
            this.groupBox3.Controls.Add(this.rdBtn_op3);
            this.groupBox3.Controls.Add(this.rdBtn_op2);
            this.groupBox3.Controls.Add(this.rdBtn_op1);
            this.groupBox3.Location = new System.Drawing.Point(28, 215);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 275);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Escoge";
            // 
            // rdBtn_op4
            // 
            this.rdBtn_op4.AutoSize = true;
            this.rdBtn_op4.Location = new System.Drawing.Point(23, 234);
            this.rdBtn_op4.Name = "rdBtn_op4";
            this.rdBtn_op4.Size = new System.Drawing.Size(74, 27);
            this.rdBtn_op4.TabIndex = 3;
            this.rdBtn_op4.TabStop = true;
            this.rdBtn_op4.Text = "Elipse";
            this.rdBtn_op4.UseVisualStyleBackColor = true;
            // 
            // rdBtn_op3
            // 
            this.rdBtn_op3.AutoSize = true;
            this.rdBtn_op3.Location = new System.Drawing.Point(23, 178);
            this.rdBtn_op3.Name = "rdBtn_op3";
            this.rdBtn_op3.Size = new System.Drawing.Size(84, 27);
            this.rdBtn_op3.TabIndex = 2;
            this.rdBtn_op3.TabStop = true;
            this.rdBtn_op3.Text = "Círculo";
            this.rdBtn_op3.UseVisualStyleBackColor = true;
            // 
            // rdBtn_op2
            // 
            this.rdBtn_op2.AutoSize = true;
            this.rdBtn_op2.Location = new System.Drawing.Point(23, 119);
            this.rdBtn_op2.Name = "rdBtn_op2";
            this.rdBtn_op2.Size = new System.Drawing.Size(116, 27);
            this.rdBtn_op2.TabIndex = 1;
            this.rdBtn_op2.TabStop = true;
            this.rdBtn_op2.Text = "Bresenham";
            this.rdBtn_op2.UseVisualStyleBackColor = true;
            // 
            // rdBtn_op1
            // 
            this.rdBtn_op1.AutoSize = true;
            this.rdBtn_op1.Location = new System.Drawing.Point(25, 63);
            this.rdBtn_op1.Name = "rdBtn_op1";
            this.rdBtn_op1.Size = new System.Drawing.Size(66, 27);
            this.rdBtn_op1.TabIndex = 0;
            this.rdBtn_op1.TabStop = true;
            this.rdBtn_op1.Text = "DDA";
            this.rdBtn_op1.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(253, 215);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(613, 433);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // FrmHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1129, 709);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmHome";
            this.Text = "FrmHome";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_Raster;
        private System.Windows.Forms.Button btn_CurvAlg;
        private System.Windows.Forms.Button btn_CutAlg;
        private System.Windows.Forms.Button btn_FillAlg;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.RadioButton rdBtn_op4;
        private System.Windows.Forms.RadioButton rdBtn_op3;
        private System.Windows.Forms.RadioButton rdBtn_op2;
        private System.Windows.Forms.RadioButton rdBtn_op1;
    }
}