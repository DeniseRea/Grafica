namespace AlgoritmosGraficar
{
    partial class FrmGraficar
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
            this.btn_Draw = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_radius = new System.Windows.Forms.Label();
            this.lbl_final = new System.Windows.Forms.Label();
            this.lbl_inicio = new System.Windows.Forms.Label();
            this.txt_Radius = new System.Windows.Forms.TextBox();
            this.txt_Yfinal = new System.Windows.Forms.TextBox();
            this.txt_Xfinal = new System.Windows.Forms.TextBox();
            this.txt_Ystart = new System.Windows.Forms.TextBox();
            this.txt_Xstart = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdBtn_Circulo = new System.Windows.Forms.RadioButton();
            this.rdBtn_Bresenham = new System.Windows.Forms.RadioButton();
            this.radBtn_DDA = new System.Windows.Forms.RadioButton();
            this.grpOptions = new System.Windows.Forms.GroupBox();
            this.btn_limpiar = new System.Windows.Forms.Button();
            this.btn_reset = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txt_output = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.grpOptions.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_Draw);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lbl_radius);
            this.groupBox1.Controls.Add(this.lbl_final);
            this.groupBox1.Controls.Add(this.lbl_inicio);
            this.groupBox1.Controls.Add(this.txt_Radius);
            this.groupBox1.Controls.Add(this.txt_Yfinal);
            this.groupBox1.Controls.Add(this.txt_Xfinal);
            this.groupBox1.Controls.Add(this.txt_Ystart);
            this.groupBox1.Controls.Add(this.txt_Xstart);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.grpOptions);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(28, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(324, 320);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Entrada";
            // 
            // btn_Draw
            // 
            this.btn_Draw.Location = new System.Drawing.Point(90, 281);
            this.btn_Draw.Name = "btn_Draw";
            this.btn_Draw.Size = new System.Drawing.Size(116, 33);
            this.btn_Draw.TabIndex = 4;
            this.btn_Draw.Text = "Dibujar";
            this.btn_Draw.UseVisualStyleBackColor = true;
            this.btn_Draw.Click += new System.EventHandler(this.btn_Draw_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(186, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 20);
            this.label5.TabIndex = 14;
            this.label5.Text = "Y";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(140, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 20);
            this.label4.TabIndex = 13;
            this.label4.Text = "X";
            // 
            // lbl_radius
            // 
            this.lbl_radius.AutoSize = true;
            this.lbl_radius.Location = new System.Drawing.Point(26, 138);
            this.lbl_radius.Name = "lbl_radius";
            this.lbl_radius.Size = new System.Drawing.Size(57, 20);
            this.lbl_radius.TabIndex = 12;
            this.lbl_radius.Text = "Radio";
            // 
            // lbl_final
            // 
            this.lbl_final.AutoSize = true;
            this.lbl_final.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold);
            this.lbl_final.Location = new System.Drawing.Point(26, 94);
            this.lbl_final.Name = "lbl_final";
            this.lbl_final.Size = new System.Drawing.Size(50, 20);
            this.lbl_final.TabIndex = 11;
            this.lbl_final.Text = "Final";
            // 
            // lbl_inicio
            // 
            this.lbl_inicio.AutoSize = true;
            this.lbl_inicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_inicio.Location = new System.Drawing.Point(26, 48);
            this.lbl_inicio.Name = "lbl_inicio";
            this.lbl_inicio.Size = new System.Drawing.Size(59, 20);
            this.lbl_inicio.TabIndex = 10;
            this.lbl_inicio.Text = "Inicial";
            // 
            // txt_Radius
            // 
            this.txt_Radius.Location = new System.Drawing.Point(135, 135);
            this.txt_Radius.Name = "txt_Radius";
            this.txt_Radius.Size = new System.Drawing.Size(79, 27);
            this.txt_Radius.TabIndex = 9;
            // 
            // txt_Yfinal
            // 
            this.txt_Yfinal.Location = new System.Drawing.Point(181, 87);
            this.txt_Yfinal.Name = "txt_Yfinal";
            this.txt_Yfinal.Size = new System.Drawing.Size(34, 27);
            this.txt_Yfinal.TabIndex = 8;
            // 
            // txt_Xfinal
            // 
            this.txt_Xfinal.Location = new System.Drawing.Point(135, 87);
            this.txt_Xfinal.Name = "txt_Xfinal";
            this.txt_Xfinal.Size = new System.Drawing.Size(34, 27);
            this.txt_Xfinal.TabIndex = 7;
            // 
            // txt_Ystart
            // 
            this.txt_Ystart.Location = new System.Drawing.Point(181, 45);
            this.txt_Ystart.Name = "txt_Ystart";
            this.txt_Ystart.Size = new System.Drawing.Size(34, 27);
            this.txt_Ystart.TabIndex = 6;
            // 
            // txt_Xstart
            // 
            this.txt_Xstart.Location = new System.Drawing.Point(135, 45);
            this.txt_Xstart.Name = "txt_Xstart";
            this.txt_Xstart.Size = new System.Drawing.Size(34, 27);
            this.txt_Xstart.TabIndex = 4;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdBtn_Circulo);
            this.groupBox3.Controls.Add(this.rdBtn_Bresenham);
            this.groupBox3.Controls.Add(this.radBtn_DDA);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.groupBox3.Location = new System.Drawing.Point(6, 168);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(163, 107);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Algorimos";
            // 
            // rdBtn_Circulo
            // 
            this.rdBtn_Circulo.AutoSize = true;
            this.rdBtn_Circulo.Location = new System.Drawing.Point(6, 83);
            this.rdBtn_Circulo.Name = "rdBtn_Circulo";
            this.rdBtn_Circulo.Size = new System.Drawing.Size(123, 24);
            this.rdBtn_Circulo.TabIndex = 17;
            this.rdBtn_Circulo.TabStop = true;
            this.rdBtn_Circulo.Text = "Punto medio";
            this.rdBtn_Circulo.UseVisualStyleBackColor = true;
            // 
            // rdBtn_Bresenham
            // 
            this.rdBtn_Bresenham.AutoSize = true;
            this.rdBtn_Bresenham.Location = new System.Drawing.Point(6, 56);
            this.rdBtn_Bresenham.Name = "rdBtn_Bresenham";
            this.rdBtn_Bresenham.Size = new System.Drawing.Size(107, 24);
            this.rdBtn_Bresenham.TabIndex = 16;
            this.rdBtn_Bresenham.TabStop = true;
            this.rdBtn_Bresenham.Text = "Breseham";
            this.rdBtn_Bresenham.UseVisualStyleBackColor = true;
            // 
            // radBtn_DDA
            // 
            this.radBtn_DDA.AutoSize = true;
            this.radBtn_DDA.Location = new System.Drawing.Point(6, 26);
            this.radBtn_DDA.Name = "radBtn_DDA";
            this.radBtn_DDA.Size = new System.Drawing.Size(67, 24);
            this.radBtn_DDA.TabIndex = 15;
            this.radBtn_DDA.TabStop = true;
            this.radBtn_DDA.Text = "DDA";
            this.radBtn_DDA.UseVisualStyleBackColor = true;
            this.radBtn_DDA.Click += new System.EventHandler(this.RadioButton_CheckedChanged);
            // 
            // grpOptions
            // 
            this.grpOptions.Controls.Add(this.btn_limpiar);
            this.grpOptions.Controls.Add(this.btn_reset);
            this.grpOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.grpOptions.Location = new System.Drawing.Point(175, 168);
            this.grpOptions.Name = "grpOptions";
            this.grpOptions.Size = new System.Drawing.Size(143, 107);
            this.grpOptions.TabIndex = 4;
            this.grpOptions.TabStop = false;
            this.grpOptions.Text = "Opciones";
            // 
            // btn_limpiar
            // 
            this.btn_limpiar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.btn_limpiar.Location = new System.Drawing.Point(6, 68);
            this.btn_limpiar.Name = "btn_limpiar";
            this.btn_limpiar.Size = new System.Drawing.Size(115, 33);
            this.btn_limpiar.TabIndex = 1;
            this.btn_limpiar.Text = "limpiar";
            this.btn_limpiar.UseVisualStyleBackColor = true;
            this.btn_limpiar.Click += new System.EventHandler(this.Btn_limpiar_Click);
            // 
            // btn_reset
            // 
            this.btn_reset.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.btn_reset.Location = new System.Drawing.Point(6, 26);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(115, 36);
            this.btn_reset.TabIndex = 0;
            this.btn_reset.Text = "resetear";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.Btn_reset_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold);
            this.groupBox2.Location = new System.Drawing.Point(358, 30);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(588, 506);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Gráfico";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(6, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(571, 469);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txt_output);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold);
            this.groupBox4.Location = new System.Drawing.Point(34, 373);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(312, 163);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Salida";
            // 
            // txt_output
            // 
            this.txt_output.Location = new System.Drawing.Point(6, 21);
            this.txt_output.Multiline = true;
            this.txt_output.Name = "txt_output";
            this.txt_output.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_output.Size = new System.Drawing.Size(300, 136);
            this.txt_output.TabIndex = 0;
            // 
            // FrmGraficar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(974, 598);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmGraficar";
            this.Text = "Algoritmos";
            this.Load += new System.EventHandler(this.Graficar_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.grpOptions.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox grpOptions;
        private System.Windows.Forms.Button btn_limpiar;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.TextBox txt_Radius;
        private System.Windows.Forms.TextBox txt_Yfinal;
        private System.Windows.Forms.TextBox txt_Xfinal;
        private System.Windows.Forms.TextBox txt_Ystart;
        private System.Windows.Forms.TextBox txt_Xstart;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btn_Draw;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbl_radius;
        private System.Windows.Forms.Label lbl_final;
        private System.Windows.Forms.Label lbl_inicio;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txt_output;
        private System.Windows.Forms.RadioButton rdBtn_Circulo;
        private System.Windows.Forms.RadioButton rdBtn_Bresenham;
        private System.Windows.Forms.RadioButton radBtn_DDA;
    }
}