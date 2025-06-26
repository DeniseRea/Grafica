namespace Cut_Algorithms
{
    partial class FrmCut
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
            this.rad_SutherlandHondgam = new System.Windows.Forms.RadioButton();
            this.rad_CohenSutherland = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pic_Canvas = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_clean = new System.Windows.Forms.Button();
            this.btn_cut = new System.Windows.Forms.Button();
            this.lbl_instrucciones = new System.Windows.Forms.Label();
            this.txt_instrucciones = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Canvas)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rad_SutherlandHondgam);
            this.groupBox1.Controls.Add(this.rad_CohenSutherland);
            this.groupBox1.Location = new System.Drawing.Point(38, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(186, 159);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Escoge un algoritmo";
            // 
            // rad_SutherlandHondgam
            // 
            this.rad_SutherlandHondgam.AutoSize = true;
            this.rad_SutherlandHondgam.Location = new System.Drawing.Point(15, 100);
            this.rad_SutherlandHondgam.Name = "rad_SutherlandHondgam";
            this.rad_SutherlandHondgam.Size = new System.Drawing.Size(156, 20);
            this.rad_SutherlandHondgam.TabIndex = 1;
            this.rad_SutherlandHondgam.TabStop = true;
            this.rad_SutherlandHondgam.Text = "Sutherland-Hondgam";
            this.rad_SutherlandHondgam.UseVisualStyleBackColor = true;
            // 
            // rad_CohenSutherland
            // 
            this.rad_CohenSutherland.AutoSize = true;
            this.rad_CohenSutherland.Location = new System.Drawing.Point(15, 51);
            this.rad_CohenSutherland.Name = "rad_CohenSutherland";
            this.rad_CohenSutherland.Size = new System.Drawing.Size(135, 20);
            this.rad_CohenSutherland.TabIndex = 0;
            this.rad_CohenSutherland.TabStop = true;
            this.rad_CohenSutherland.Text = "Cohen-Sutherland";
            this.rad_CohenSutherland.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pic_Canvas);
            this.groupBox2.Location = new System.Drawing.Point(250, 134);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(762, 479);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Dibujo";
            // 
            // pic_Canvas
            // 
            this.pic_Canvas.Location = new System.Drawing.Point(20, 39);
            this.pic_Canvas.Name = "pic_Canvas";
            this.pic_Canvas.Size = new System.Drawing.Size(718, 422);
            this.pic_Canvas.TabIndex = 0;
            this.pic_Canvas.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn_clean);
            this.groupBox3.Controls.Add(this.btn_cut);
            this.groupBox3.Location = new System.Drawing.Point(38, 214);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(186, 156);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Opciones";
            // 
            // btn_clean
            // 
            this.btn_clean.Location = new System.Drawing.Point(24, 69);
            this.btn_clean.Name = "btn_clean";
            this.btn_clean.Size = new System.Drawing.Size(115, 31);
            this.btn_clean.TabIndex = 3;
            this.btn_clean.Text = "Limpiar";
            this.btn_clean.UseVisualStyleBackColor = true;
            // 
            // btn_cut
            // 
            this.btn_cut.Location = new System.Drawing.Point(24, 21);
            this.btn_cut.Name = "btn_cut";
            this.btn_cut.Size = new System.Drawing.Size(115, 31);
            this.btn_cut.TabIndex = 2;
            this.btn_cut.Text = "Recortar";
            this.btn_cut.UseVisualStyleBackColor = true;
            // 
            // lbl_instrucciones
            // 
            this.lbl_instrucciones.AutoSize = true;
            this.lbl_instrucciones.Location = new System.Drawing.Point(267, 34);
            this.lbl_instrucciones.Name = "lbl_instrucciones";
            this.lbl_instrucciones.Size = new System.Drawing.Size(0, 16);
            this.lbl_instrucciones.TabIndex = 3;
            // 
            // txt_instrucciones
            // 
            this.txt_instrucciones.Enabled = false;
            this.txt_instrucciones.Location = new System.Drawing.Point(261, 44);
            this.txt_instrucciones.Multiline = true;
            this.txt_instrucciones.Name = "txt_instrucciones";
            this.txt_instrucciones.Size = new System.Drawing.Size(727, 61);
            this.txt_instrucciones.TabIndex = 4;
            // 
            // FrmCut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 625);
            this.Controls.Add(this.txt_instrucciones);
            this.Controls.Add(this.lbl_instrucciones);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmCut";
            this.Text = "FrmCut";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_Canvas)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rad_SutherlandHondgam;
        private System.Windows.Forms.RadioButton rad_CohenSutherland;
        private System.Windows.Forms.PictureBox pic_Canvas;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btn_clean;
        private System.Windows.Forms.Button btn_cut;
        private System.Windows.Forms.Label lbl_instrucciones;
        private System.Windows.Forms.TextBox txt_instrucciones;
    }
}