namespace AlgoritmoRecorte
{
    partial class FrmRecorte
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.graphic = new System.Windows.Forms.GroupBox();
            this.dgwPixels = new System.Windows.Forms.DataGridView();
            this.PicCanvas = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.BtnReset = new System.Windows.Forms.Button();
            this.BtnCut = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.graphic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwPixels)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicCanvas)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgwPixels);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(683, 183);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(393, 386);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Salida de pixeles";
            // 
            // graphic
            // 
            this.graphic.Controls.Add(this.PicCanvas);
            this.graphic.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.graphic.Location = new System.Drawing.Point(36, 31);
            this.graphic.Name = "graphic";
            this.graphic.Size = new System.Drawing.Size(634, 538);
            this.graphic.TabIndex = 1;
            this.graphic.TabStop = false;
            this.graphic.Text = "Gráfica";
            // 
            // dgwPixels
            // 
            this.dgwPixels.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgwPixels.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwPixels.GridColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgwPixels.Location = new System.Drawing.Point(7, 22);
            this.dgwPixels.Margin = new System.Windows.Forms.Padding(4);
            this.dgwPixels.Name = "dgwPixels";
            this.dgwPixels.RowHeadersWidth = 51;
            this.dgwPixels.Size = new System.Drawing.Size(379, 357);
            this.dgwPixels.TabIndex = 10;
            // 
            // PicCanvas
            // 
            this.PicCanvas.BackColor = System.Drawing.SystemColors.Control;
            this.PicCanvas.Location = new System.Drawing.Point(7, 22);
            this.PicCanvas.Margin = new System.Windows.Forms.Padding(4);
            this.PicCanvas.Name = "PicCanvas";
            this.PicCanvas.Size = new System.Drawing.Size(620, 509);
            this.PicCanvas.TabIndex = 8;
            this.PicCanvas.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.BtnReset);
            this.groupBox2.Controls.Add(this.BtnCut);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(698, 42);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(371, 122);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Opciones";
            // 
            // BtnReset
            // 
            this.BtnReset.Location = new System.Drawing.Point(216, 47);
            this.BtnReset.Margin = new System.Windows.Forms.Padding(4);
            this.BtnReset.Name = "BtnReset";
            this.BtnReset.Size = new System.Drawing.Size(100, 28);
            this.BtnReset.TabIndex = 13;
            this.BtnReset.Text = "Resetear";
            this.BtnReset.UseVisualStyleBackColor = true;
            this.BtnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // BtnCut
            // 
            this.BtnCut.Location = new System.Drawing.Point(54, 47);
            this.BtnCut.Margin = new System.Windows.Forms.Padding(4);
            this.BtnCut.Name = "BtnCut";
            this.BtnCut.Size = new System.Drawing.Size(100, 28);
            this.BtnCut.TabIndex = 12;
            this.BtnCut.Text = "Recortar";
            this.BtnCut.UseVisualStyleBackColor = true;
            this.BtnCut.Click += new System.EventHandler(this.BtnCut_Click);
            // 
            // FrmRecorte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1088, 609);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.graphic);
            this.Name = "FrmRecorte";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.graphic.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgwPixels)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicCanvas)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgwPixels;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox graphic;
        private System.Windows.Forms.PictureBox PicCanvas;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button BtnReset;
        private System.Windows.Forms.Button BtnCut;
    }
}

