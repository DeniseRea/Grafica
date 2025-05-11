namespace Rea_Leccion
{
    partial class FrmFigurescs
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.figurasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pentagono = new System.Windows.Forms.ToolStripMenuItem();
            this.hexagono = new System.Windows.Forms.ToolStripMenuItem();
            this.octagono = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.figurasToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // figurasToolStripMenuItem
            // 
            this.figurasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pentagono,
            this.hexagono,
            this.octagono});
            this.figurasToolStripMenuItem.Name = "figurasToolStripMenuItem";
            this.figurasToolStripMenuItem.Size = new System.Drawing.Size(70, 24);
            this.figurasToolStripMenuItem.Text = "Figuras";
            this.figurasToolStripMenuItem.Click += new System.EventHandler(this.figurasToolStripMenuItem_Click);
            // 
            // pentagono
            // 
            this.pentagono.Name = "pentagono";
            this.pentagono.Size = new System.Drawing.Size(224, 26);
            this.pentagono.Text = "Pentágono";
            this.pentagono.Click += new System.EventHandler(this.pentagono_Click);
            // 
            // hexagono
            // 
            this.hexagono.Name = "hexagono";
            this.hexagono.Size = new System.Drawing.Size(224, 26);
            this.hexagono.Text = "Hexágono";
            this.hexagono.Click += new System.EventHandler(this.hexagono_Click);
            // 
            // octagono
            // 
            this.octagono.Name = "octagono";
            this.octagono.Size = new System.Drawing.Size(224, 26);
            this.octagono.Text = "Octágono";
            // 
            // FrmFigurescs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmFigurescs";
            this.Text = "FrmFigurescs";
            this.Load += new System.EventHandler(this.FrmFigurescs_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem figurasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pentagono;
        private System.Windows.Forms.ToolStripMenuItem hexagono;
        private System.Windows.Forms.ToolStripMenuItem octagono;
    }
}