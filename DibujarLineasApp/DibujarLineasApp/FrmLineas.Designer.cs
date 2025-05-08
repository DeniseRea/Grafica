namespace DibujarLineasApp
{
    partial class FrmLineas
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
            this.lbl_Art = new System.Windows.Forms.Label();
            this.grp_Options = new System.Windows.Forms.GroupBox();
            this.btn_exit = new System.Windows.Forms.Button();
            this.btn_Reset = new System.Windows.Forms.Button();
            this.grp_picCanvas = new System.Windows.Forms.GroupBox();
            this.pct_Graphic = new System.Windows.Forms.PictureBox();
            this.lbl_Line = new System.Windows.Forms.Label();
            this.grp_Options.SuspendLayout();
            this.grp_picCanvas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pct_Graphic)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_Art
            // 
            this.lbl_Art.AutoSize = true;
            this.lbl_Art.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Art.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lbl_Art.Font = new System.Drawing.Font("Bookman Old Style", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Art.ForeColor = System.Drawing.Color.Goldenrod;
            this.lbl_Art.Location = new System.Drawing.Point(369, 9);
            this.lbl_Art.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_Art.Name = "lbl_Art";
            this.lbl_Art.Size = new System.Drawing.Size(172, 93);
            this.lbl_Art.TabIndex = 1;
            this.lbl_Art.Text = "Art";
            // 
            // grp_Options
            // 
            this.grp_Options.BackColor = System.Drawing.Color.Silver;
            this.grp_Options.Controls.Add(this.btn_exit);
            this.grp_Options.Controls.Add(this.btn_Reset);
            this.grp_Options.Font = new System.Drawing.Font("Myanmar Text", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_Options.Location = new System.Drawing.Point(129, 105);
            this.grp_Options.Name = "grp_Options";
            this.grp_Options.Size = new System.Drawing.Size(535, 121);
            this.grp_Options.TabIndex = 2;
            this.grp_Options.TabStop = false;
            this.grp_Options.Text = "Opciones";
            // 
            // btn_exit
            // 
            this.btn_exit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_exit.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_exit.Location = new System.Drawing.Point(337, 45);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(127, 41);
            this.btn_exit.TabIndex = 3;
            this.btn_exit.Text = "Salir";
            this.btn_exit.UseVisualStyleBackColor = false;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // btn_Reset
            // 
            this.btn_Reset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btn_Reset.Location = new System.Drawing.Point(107, 45);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Size = new System.Drawing.Size(112, 41);
            this.btn_Reset.TabIndex = 0;
            this.btn_Reset.Text = "Limpiar";
            this.btn_Reset.UseVisualStyleBackColor = false;
            this.btn_Reset.Click += new System.EventHandler(this.btn_Reset_Click);
            // 
            // grp_picCanvas
            // 
            this.grp_picCanvas.BackColor = System.Drawing.Color.Silver;
            this.grp_picCanvas.Controls.Add(this.pct_Graphic);
            this.grp_picCanvas.Font = new System.Drawing.Font("Myanmar Text", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_picCanvas.Location = new System.Drawing.Point(35, 243);
            this.grp_picCanvas.Name = "grp_picCanvas";
            this.grp_picCanvas.Size = new System.Drawing.Size(755, 288);
            this.grp_picCanvas.TabIndex = 3;
            this.grp_picCanvas.TabStop = false;
            this.grp_picCanvas.Text = "Gráfico";
            // 
            // pct_Graphic
            // 
            this.pct_Graphic.BackColor = System.Drawing.Color.White;
            this.pct_Graphic.Location = new System.Drawing.Point(27, 45);
            this.pct_Graphic.Name = "pct_Graphic";
            this.pct_Graphic.Size = new System.Drawing.Size(690, 229);
            this.pct_Graphic.TabIndex = 0;
            this.pct_Graphic.TabStop = false;
            this.pct_Graphic.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pct_Graphic_MouseClick);
            // 
            // lbl_Line
            // 
            this.lbl_Line.AutoSize = true;
            this.lbl_Line.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Line.Font = new System.Drawing.Font("Bradley Hand ITC", 48F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Line.ForeColor = System.Drawing.Color.SaddleBrown;
            this.lbl_Line.Location = new System.Drawing.Point(168, 2);
            this.lbl_Line.Name = "lbl_Line";
            this.lbl_Line.Size = new System.Drawing.Size(198, 100);
            this.lbl_Line.TabIndex = 4;
            this.lbl_Line.Text = "Line";
            // 
            // FrmLineas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(802, 538);
            this.Controls.Add(this.lbl_Line);
            this.Controls.Add(this.grp_picCanvas);
            this.Controls.Add(this.grp_Options);
            this.Controls.Add(this.lbl_Art);
            this.Name = "FrmLineas";
            this.Text = "LineArt";
            this.Load += new System.EventHandler(this.FrmLineas_Load);
            this.grp_Options.ResumeLayout(false);
            this.grp_picCanvas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pct_Graphic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbl_Art;
        private System.Windows.Forms.GroupBox grp_Options;
        private System.Windows.Forms.Button btn_Reset;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.GroupBox grp_picCanvas;
        private System.Windows.Forms.PictureBox pct_Graphic;
        private System.Windows.Forms.Label lbl_Line;
    }
}