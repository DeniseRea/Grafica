namespace Padre_Hijo
{
    partial class FrmHilogram
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
            this.grp_Graph = new System.Windows.Forms.GroupBox();
            this.pc_Graph = new System.Windows.Forms.PictureBox();
            this.grp_Options = new System.Windows.Forms.GroupBox();
            this.btn_draw = new System.Windows.Forms.Button();
            this.btn_exit = new System.Windows.Forms.Button();
            this.btn_Reset = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.grp_Graph.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pc_Graph)).BeginInit();
            this.grp_Options.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // grp_Graph
            // 
            this.grp_Graph.Controls.Add(this.pc_Graph);
            this.grp_Graph.Font = new System.Drawing.Font("Myanmar Text", 12F, System.Drawing.FontStyle.Bold);
            this.grp_Graph.Location = new System.Drawing.Point(392, 12);
            this.grp_Graph.Name = "grp_Graph";
            this.grp_Graph.Size = new System.Drawing.Size(664, 485);
            this.grp_Graph.TabIndex = 0;
            this.grp_Graph.TabStop = false;
            this.grp_Graph.Text = "Gráfica";
            this.grp_Graph.Enter += new System.EventHandler(this.grp_Graph_Enter);
            // 
            // pc_Graph
            // 
            this.pc_Graph.Location = new System.Drawing.Point(6, 45);
            this.pc_Graph.Name = "pc_Graph";
            this.pc_Graph.Size = new System.Drawing.Size(640, 434);
            this.pc_Graph.TabIndex = 0;
            this.pc_Graph.TabStop = false;
            // 
            // grp_Options
            // 
            this.grp_Options.BackColor = System.Drawing.Color.Silver;
            this.grp_Options.Controls.Add(this.trackBar1);
            this.grp_Options.Controls.Add(this.btn_draw);
            this.grp_Options.Controls.Add(this.btn_exit);
            this.grp_Options.Controls.Add(this.btn_Reset);
            this.grp_Options.Font = new System.Drawing.Font("Myanmar Text", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_Options.Location = new System.Drawing.Point(12, 85);
            this.grp_Options.Name = "grp_Options";
            this.grp_Options.Size = new System.Drawing.Size(380, 282);
            this.grp_Options.TabIndex = 3;
            this.grp_Options.TabStop = false;
            this.grp_Options.Text = "Opciones";
            // 
            // btn_draw
            // 
            this.btn_draw.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btn_draw.Location = new System.Drawing.Point(61, 113);
            this.btn_draw.Name = "btn_draw";
            this.btn_draw.Size = new System.Drawing.Size(127, 41);
            this.btn_draw.TabIndex = 4;
            this.btn_draw.Text = "Dibujar";
            this.btn_draw.UseVisualStyleBackColor = false;
            // 
            // btn_exit
            // 
            this.btn_exit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_exit.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_exit.Location = new System.Drawing.Point(61, 207);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(127, 41);
            this.btn_exit.TabIndex = 3;
            this.btn_exit.Text = "Salir";
            this.btn_exit.UseVisualStyleBackColor = false;
            // 
            // btn_Reset
            // 
            this.btn_Reset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btn_Reset.Location = new System.Drawing.Point(61, 160);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Size = new System.Drawing.Size(127, 41);
            this.btn_Reset.TabIndex = 0;
            this.btn_Reset.Text = "Limpiar";
            this.btn_Reset.UseVisualStyleBackColor = false;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(6, 45);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(356, 56);
            this.trackBar1.TabIndex = 4;
            //this.trackBar1.Scroll += new System.EventHandler(this.TrkNumberHylo_Scroll);
            // 
            // FrmHilogram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1068, 573);
            this.Controls.Add(this.grp_Options);
            this.Controls.Add(this.grp_Graph);
            this.Name = "FrmHilogram";
            this.Text = "Hilogram";
            this.grp_Graph.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pc_Graph)).EndInit();
            this.grp_Options.ResumeLayout(false);
            this.grp_Options.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grp_Graph;
        private System.Windows.Forms.PictureBox pc_Graph;
        private System.Windows.Forms.GroupBox grp_Options;
        private System.Windows.Forms.Button btn_draw;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.Button btn_Reset;
        private System.Windows.Forms.TrackBar trackBar1;
    }
}