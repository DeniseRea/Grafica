namespace Figures
{
    partial class FrmFigures
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFigures));
            this.lbl_mainTitle = new System.Windows.Forms.Label();
            this.lbl_text = new System.Windows.Forms.Label();
            this.cmb_figures = new System.Windows.Forms.ComboBox();
            this.btn_select = new System.Windows.Forms.Button();
            this.btn_image = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_mainTitle
            // 
            this.lbl_mainTitle.AutoSize = true;
            this.lbl_mainTitle.Enabled = false;
            this.lbl_mainTitle.Font = new System.Drawing.Font("Eras Bold ITC", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_mainTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lbl_mainTitle.Location = new System.Drawing.Point(200, 9);
            this.lbl_mainTitle.Name = "lbl_mainTitle";
            this.lbl_mainTitle.Size = new System.Drawing.Size(402, 69);
            this.lbl_mainTitle.TabIndex = 0;
            this.lbl_mainTitle.Text = "MathShapes";
            this.lbl_mainTitle.Click += new System.EventHandler(this.label1_Click);
            // 
            // lbl_text
            // 
            this.lbl_text.AutoSize = true;
            this.lbl_text.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_text.Location = new System.Drawing.Point(79, 96);
            this.lbl_text.Name = "lbl_text";
            this.lbl_text.Size = new System.Drawing.Size(398, 43);
            this.lbl_text.TabIndex = 1;
            this.lbl_text.Text = "Bienvenido, escoge tu figura";
            // 
            // cmb_figures
            // 
            this.cmb_figures.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_figures.FormattingEnabled = true;
            this.cmb_figures.Location = new System.Drawing.Point(87, 155);
            this.cmb_figures.Name = "cmb_figures";
            this.cmb_figures.Size = new System.Drawing.Size(308, 33);
            this.cmb_figures.TabIndex = 2;
            this.cmb_figures.Text = "Figuras";
            // 
            // btn_select
            // 
            this.btn_select.BackColor = System.Drawing.Color.GreenYellow;
            this.btn_select.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_select.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btn_select.Location = new System.Drawing.Point(98, 372);
            this.btn_select.Name = "btn_select";
            this.btn_select.Size = new System.Drawing.Size(125, 37);
            this.btn_select.TabIndex = 3;
            this.btn_select.Text = "seleccionar";
            this.btn_select.UseVisualStyleBackColor = false;
            // 
            // btn_image
            // 
            this.btn_image.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_image.BackgroundImage")));
            this.btn_image.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_image.Enabled = false;
            this.btn_image.Location = new System.Drawing.Point(522, 70);
            this.btn_image.Name = "btn_image";
            this.btn_image.Size = new System.Drawing.Size(313, 312);
            this.btn_image.TabIndex = 4;
            this.btn_image.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_image.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_image.UseVisualStyleBackColor = true;
            // 
            // FrmFigures
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(869, 467);
            this.Controls.Add(this.btn_image);
            this.Controls.Add(this.btn_select);
            this.Controls.Add(this.cmb_figures);
            this.Controls.Add(this.lbl_text);
            this.Controls.Add(this.lbl_mainTitle);
            this.Name = "FrmFigures";
            this.Text = "MathShapes";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_mainTitle;
        private System.Windows.Forms.Label lbl_text;
        private System.Windows.Forms.ComboBox cmb_figures;
        private System.Windows.Forms.Button btn_select;
        private System.Windows.Forms.Button btn_image;
    }
}