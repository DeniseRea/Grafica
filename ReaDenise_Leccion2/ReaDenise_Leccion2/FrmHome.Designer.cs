namespace ReaDenise_Leccion2
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_Pentagono = new System.Windows.Forms.Button();
            this.btn_Malla = new System.Windows.Forms.Button();
            this.btn_clear = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(352, 71);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(530, 416);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.btn_clear);
            this.groupBox1.Controls.Add(this.btn_Malla);
            this.groupBox1.Controls.Add(this.btn_Pentagono);
            this.groupBox1.Location = new System.Drawing.Point(30, 71);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(265, 423);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Opciones";
            // 
            // btn_Pentagono
            // 
            this.btn_Pentagono.Location = new System.Drawing.Point(48, 63);
            this.btn_Pentagono.Name = "btn_Pentagono";
            this.btn_Pentagono.Size = new System.Drawing.Size(154, 30);
            this.btn_Pentagono.TabIndex = 0;
            this.btn_Pentagono.Text = "Pentagono";
            this.btn_Pentagono.UseVisualStyleBackColor = true;
            this.btn_Pentagono.Click += new System.EventHandler(this.btn_Pentagono_Click);
            // 
            // btn_Malla
            // 
            this.btn_Malla.Location = new System.Drawing.Point(48, 119);
            this.btn_Malla.Name = "btn_Malla";
            this.btn_Malla.Size = new System.Drawing.Size(154, 30);
            this.btn_Malla.TabIndex = 1;
            this.btn_Malla.Text = "Malla";
            this.btn_Malla.UseVisualStyleBackColor = true;
            this.btn_Malla.Click += new System.EventHandler(this.btn_Malla_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.Location = new System.Drawing.Point(48, 184);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(154, 33);
            this.btn_clear.TabIndex = 2;
            this.btn_clear.Text = "limpiar";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(42, 266);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(180, 57);
            this.textBox1.TabIndex = 3;
            // 
            // FrmHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(912, 590);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "FrmHome";
            this.Text = "FrmHome";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_Malla;
        private System.Windows.Forms.Button btn_Pentagono;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.TextBox textBox1;
    }
}