namespace Figures
{
    partial class FrmTriangulo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTriangulo));
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_image = new System.Windows.Forms.Button();
            this.grp_output = new System.Windows.Forms.GroupBox();
            this.txt_Perimeter = new System.Windows.Forms.TextBox();
            this.txt_Area = new System.Windows.Forms.TextBox();
            this.lbl_Perimeter = new System.Windows.Forms.Label();
            this.lbl_Area = new System.Windows.Forms.Label();
            this.grp_Input = new System.Windows.Forms.GroupBox();
            this.txt_CatetoC = new System.Windows.Forms.TextBox();
            this.lbl_height = new System.Windows.Forms.Label();
            this.btn_reset = new System.Windows.Forms.Button();
            this.btn_Calculate = new System.Windows.Forms.Button();
            this.txt_CatetoB = new System.Windows.Forms.TextBox();
            this.lbl_heigth = new System.Windows.Forms.Label();
            this.txt_CatetoA = new System.Windows.Forms.TextBox();
            this.lbl_Base = new System.Windows.Forms.Label();
            this.lbl_mainTitle = new System.Windows.Forms.Label();
            this.grp_output.SuspendLayout();
            this.grp_Input.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Close
            // 
            this.btn_Close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_Close.Font = new System.Drawing.Font("Mongolian Baiti", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Close.ForeColor = System.Drawing.Color.White;
            this.btn_Close.Location = new System.Drawing.Point(519, 421);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(159, 41);
            this.btn_Close.TabIndex = 28;
            this.btn_Close.Text = "Salir";
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_image
            // 
            this.btn_image.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_image.BackgroundImage")));
            this.btn_image.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_image.Enabled = false;
            this.btn_image.Location = new System.Drawing.Point(437, 113);
            this.btn_image.Name = "btn_image";
            this.btn_image.Size = new System.Drawing.Size(328, 302);
            this.btn_image.TabIndex = 27;
            this.btn_image.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_image.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_image.UseVisualStyleBackColor = true;
            // 
            // grp_output
            // 
            this.grp_output.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.grp_output.Controls.Add(this.txt_Perimeter);
            this.grp_output.Controls.Add(this.txt_Area);
            this.grp_output.Controls.Add(this.lbl_Perimeter);
            this.grp_output.Controls.Add(this.lbl_Area);
            this.grp_output.Font = new System.Drawing.Font("Mongolian Baiti", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_output.Location = new System.Drawing.Point(29, 364);
            this.grp_output.Name = "grp_output";
            this.grp_output.Size = new System.Drawing.Size(371, 120);
            this.grp_output.TabIndex = 26;
            this.grp_output.TabStop = false;
            this.grp_output.Text = "Salida";
            // 
            // txt_Perimeter
            // 
            this.txt_Perimeter.Enabled = false;
            this.txt_Perimeter.Location = new System.Drawing.Point(151, 80);
            this.txt_Perimeter.Name = "txt_Perimeter";
            this.txt_Perimeter.Size = new System.Drawing.Size(185, 31);
            this.txt_Perimeter.TabIndex = 3;
            // 
            // txt_Area
            // 
            this.txt_Area.Enabled = false;
            this.txt_Area.Location = new System.Drawing.Point(151, 33);
            this.txt_Area.Name = "txt_Area";
            this.txt_Area.Size = new System.Drawing.Size(185, 31);
            this.txt_Area.TabIndex = 2;
            // 
            // lbl_Perimeter
            // 
            this.lbl_Perimeter.AutoSize = true;
            this.lbl_Perimeter.Location = new System.Drawing.Point(20, 86);
            this.lbl_Perimeter.Name = "lbl_Perimeter";
            this.lbl_Perimeter.Size = new System.Drawing.Size(100, 21);
            this.lbl_Perimeter.TabIndex = 2;
            this.lbl_Perimeter.Text = "Perímetro";
            // 
            // lbl_Area
            // 
            this.lbl_Area.AutoSize = true;
            this.lbl_Area.Location = new System.Drawing.Point(20, 33);
            this.lbl_Area.Name = "lbl_Area";
            this.lbl_Area.Size = new System.Drawing.Size(53, 21);
            this.lbl_Area.TabIndex = 1;
            this.lbl_Area.Text = "Área";
            // 
            // grp_Input
            // 
            this.grp_Input.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.grp_Input.Controls.Add(this.txt_CatetoC);
            this.grp_Input.Controls.Add(this.lbl_height);
            this.grp_Input.Controls.Add(this.btn_reset);
            this.grp_Input.Controls.Add(this.btn_Calculate);
            this.grp_Input.Controls.Add(this.txt_CatetoB);
            this.grp_Input.Controls.Add(this.lbl_heigth);
            this.grp_Input.Controls.Add(this.txt_CatetoA);
            this.grp_Input.Controls.Add(this.lbl_Base);
            this.grp_Input.Font = new System.Drawing.Font("Mongolian Baiti", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_Input.Location = new System.Drawing.Point(29, 113);
            this.grp_Input.Name = "grp_Input";
            this.grp_Input.Size = new System.Drawing.Size(371, 245);
            this.grp_Input.TabIndex = 25;
            this.grp_Input.TabStop = false;
            this.grp_Input.Text = "Entrada";
            // 
            // txt_CatetoC
            // 
            this.txt_CatetoC.Location = new System.Drawing.Point(152, 115);
            this.txt_CatetoC.Name = "txt_CatetoC";
            this.txt_CatetoC.Size = new System.Drawing.Size(185, 31);
            this.txt_CatetoC.TabIndex = 9;
            // 
            // lbl_height
            // 
            this.lbl_height.AutoSize = true;
            this.lbl_height.Location = new System.Drawing.Point(21, 118);
            this.lbl_height.Name = "lbl_height";
            this.lbl_height.Size = new System.Drawing.Size(89, 21);
            this.lbl_height.TabIndex = 8;
            this.lbl_height.Text = "Cateto C";
            // 
            // btn_reset
            // 
            this.btn_reset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btn_reset.Location = new System.Drawing.Point(190, 166);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(119, 41);
            this.btn_reset.TabIndex = 7;
            this.btn_reset.Text = "Limpiar";
            this.btn_reset.UseVisualStyleBackColor = false;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // btn_Calculate
            // 
            this.btn_Calculate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btn_Calculate.Location = new System.Drawing.Point(24, 166);
            this.btn_Calculate.Name = "btn_Calculate";
            this.btn_Calculate.Size = new System.Drawing.Size(119, 41);
            this.btn_Calculate.TabIndex = 6;
            this.btn_Calculate.Text = "Calcular";
            this.btn_Calculate.UseVisualStyleBackColor = false;
            this.btn_Calculate.Click += new System.EventHandler(this.btn_Calculate_Click);
            // 
            // txt_CatetoB
            // 
            this.txt_CatetoB.Location = new System.Drawing.Point(152, 78);
            this.txt_CatetoB.Name = "txt_CatetoB";
            this.txt_CatetoB.Size = new System.Drawing.Size(185, 31);
            this.txt_CatetoB.TabIndex = 3;
            // 
            // lbl_heigth
            // 
            this.lbl_heigth.AutoSize = true;
            this.lbl_heigth.Location = new System.Drawing.Point(21, 81);
            this.lbl_heigth.Name = "lbl_heigth";
            this.lbl_heigth.Size = new System.Drawing.Size(89, 21);
            this.lbl_heigth.TabIndex = 2;
            this.lbl_heigth.Text = "Cateto B";
            // 
            // txt_CatetoA
            // 
            this.txt_CatetoA.Location = new System.Drawing.Point(152, 40);
            this.txt_CatetoA.Name = "txt_CatetoA";
            this.txt_CatetoA.Size = new System.Drawing.Size(185, 31);
            this.txt_CatetoA.TabIndex = 1;
            // 
            // lbl_Base
            // 
            this.lbl_Base.AutoSize = true;
            this.lbl_Base.Location = new System.Drawing.Point(21, 43);
            this.lbl_Base.Name = "lbl_Base";
            this.lbl_Base.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_Base.Size = new System.Drawing.Size(90, 21);
            this.lbl_Base.TabIndex = 0;
            this.lbl_Base.Text = "Cateto A";
            // 
            // lbl_mainTitle
            // 
            this.lbl_mainTitle.AutoSize = true;
            this.lbl_mainTitle.Enabled = false;
            this.lbl_mainTitle.Font = new System.Drawing.Font("Goudy Stout", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_mainTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lbl_mainTitle.Location = new System.Drawing.Point(213, 55);
            this.lbl_mainTitle.Name = "lbl_mainTitle";
            this.lbl_mainTitle.Size = new System.Drawing.Size(327, 39);
            this.lbl_mainTitle.TabIndex = 24;
            this.lbl_mainTitle.Text = "Triángulo";
            // 
            // FrmTriangulo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 498);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.btn_image);
            this.Controls.Add(this.grp_output);
            this.Controls.Add(this.grp_Input);
            this.Controls.Add(this.lbl_mainTitle);
            this.Name = "FrmTriangulo";
            this.Text = "Triángulo";
            this.grp_output.ResumeLayout(false);
            this.grp_output.PerformLayout();
            this.grp_Input.ResumeLayout(false);
            this.grp_Input.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_image;
        private System.Windows.Forms.GroupBox grp_output;
        private System.Windows.Forms.TextBox txt_Perimeter;
        private System.Windows.Forms.TextBox txt_Area;
        private System.Windows.Forms.Label lbl_Perimeter;
        private System.Windows.Forms.Label lbl_Area;
        private System.Windows.Forms.GroupBox grp_Input;
        private System.Windows.Forms.TextBox txt_CatetoC;
        private System.Windows.Forms.Label lbl_height;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.Button btn_Calculate;
        private System.Windows.Forms.TextBox txt_CatetoB;
        private System.Windows.Forms.Label lbl_heigth;
        private System.Windows.Forms.TextBox txt_CatetoA;
        private System.Windows.Forms.Label lbl_Base;
        private System.Windows.Forms.Label lbl_mainTitle;
    }
}