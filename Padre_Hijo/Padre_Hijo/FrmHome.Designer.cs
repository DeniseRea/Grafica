namespace Padre_Hijo
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ToolStrip_Cuadrangulos = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip_Cuadrado = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrio_Rectangulo = new System.Windows.Forms.ToolStripMenuItem();
            this.florMargaritaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip_Lineas = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.rotarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStrip_Cuadrangulos,
            this.toolStrip_Lineas,
            this.rotarToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 30);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(808, 30);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ToolStrip_Cuadrangulos
            // 
            this.ToolStrip_Cuadrangulos.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStrip_Cuadrado,
            this.toolStrio_Rectangulo,
            this.florMargaritaToolStripMenuItem});
            this.ToolStrip_Cuadrangulos.ForeColor = System.Drawing.Color.Black;
            this.ToolStrip_Cuadrangulos.Name = "ToolStrip_Cuadrangulos";
            this.ToolStrip_Cuadrangulos.Size = new System.Drawing.Size(114, 26);
            this.ToolStrip_Cuadrangulos.Text = "Cuadrángulos";
            this.ToolStrip_Cuadrangulos.Click += new System.EventHandler(this.ToolStrip_Cuadrangulos_Click);
            // 
            // toolStrip_Cuadrado
            // 
            this.toolStrip_Cuadrado.Name = "toolStrip_Cuadrado";
            this.toolStrip_Cuadrado.Size = new System.Drawing.Size(224, 26);
            this.toolStrip_Cuadrado.Text = "Cuadrado";
            this.toolStrip_Cuadrado.Click += new System.EventHandler(this.toolStrip_Cuadrado_Click);
            // 
            // toolStrio_Rectangulo
            // 
            this.toolStrio_Rectangulo.Name = "toolStrio_Rectangulo";
            this.toolStrio_Rectangulo.Size = new System.Drawing.Size(224, 26);
            this.toolStrio_Rectangulo.Text = "Rectángulo";
            // 
            // florMargaritaToolStripMenuItem
            // 
            this.florMargaritaToolStripMenuItem.Name = "florMargaritaToolStripMenuItem";
            this.florMargaritaToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.florMargaritaToolStripMenuItem.Text = "Flor Margarita";
            this.florMargaritaToolStripMenuItem.Click += new System.EventHandler(this.florMargaritaToolStripMenuItem_Click);
            // 
            // toolStrip_Lineas
            // 
            this.toolStrip_Lineas.Name = "toolStrip_Lineas";
            this.toolStrip_Lineas.Size = new System.Drawing.Size(64, 26);
            this.toolStrip_Lineas.Text = "Lineas";
            this.toolStrip_Lineas.Click += new System.EventHandler(this.toolStrip_Lineas_Click);
            // 
            // menuStrip2
            // 
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(808, 30);
            this.menuStrip2.TabIndex = 3;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // rotarToolStripMenuItem
            // 
            this.rotarToolStripMenuItem.Name = "rotarToolStripMenuItem";
            this.rotarToolStripMenuItem.Size = new System.Drawing.Size(59, 26);
            this.rotarToolStripMenuItem.Text = "Rotar";
            // 
            // FrmHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 501);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.menuStrip2);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmHome";
            this.Text = "Home";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStrip_Cuadrangulos;
        private System.Windows.Forms.ToolStripMenuItem toolStrip_Cuadrado;
        private System.Windows.Forms.ToolStripMenuItem toolStrio_Rectangulo;
        private System.Windows.Forms.ToolStripMenuItem toolStrip_Lineas;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem florMargaritaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rotarToolStripMenuItem;
    }
}