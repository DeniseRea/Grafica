using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Graphic_Algoritms
{
    public static class FrmHomeDesign
    {
        // Colores del tema moderno
        public static readonly Color ColorPrimario = Color.FromArgb(41, 128, 185);
        public static readonly Color ColorSecundario = Color.FromArgb(52, 152, 219);
        public static readonly Color ColorAcento = Color.FromArgb(231, 76, 60);
        public static readonly Color ColorFondo = Color.FromArgb(236, 240, 241);
        public static readonly Color ColorTexto = Color.FromArgb(44, 62, 80);
        public static readonly Color ColorBotonHover = Color.FromArgb(30, 96, 138);

        public static void ApplyDesign(Form form, GroupBox[] groupBoxes, Button[] buttons, PictureBox pictureBox, TextBox textBox, StatusStrip statusStrip, ToolStripStatusLabel lblCoords, ToolStripStatusLabel lblAlgoritmo, ToolTip toolTip)
        {
            // Configuración del formulario principal
            SetupFormStyle(form);

            // Configuración de la barra de estado moderna
            SetupModernStatusBar(form, statusStrip, lblCoords, lblAlgoritmo);

            // Configuración de ToolTips mejorados
            SetupEnhancedToolTips(toolTip, buttons, pictureBox);

            // Estilizado de controles principales
            SetupGroupBoxStyles(groupBoxes);
            SetupButtonStyles(buttons);
            SetupPictureBoxStyle(pictureBox);
            SetupTextBoxStyle(textBox);
        }

        private static void SetupFormStyle(Form form)
        {
            form.BackColor = ColorFondo;
            form.Text = "Algoritmos Gráficos - Sistema Avanzado";
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ClientSize = new Size(1150, 730);
            form.MinimumSize = new Size(1150, 730);
            form.MaximumSize = new Size(1150, 730);
            form.FormBorderStyle = FormBorderStyle.FixedSingle;
            form.MaximizeBox = false;
        }

        private static void SetupModernStatusBar(Form form, StatusStrip statusStrip, ToolStripStatusLabel lblCoords, ToolStripStatusLabel lblAlgoritmo)
        {
            statusStrip.BackColor = ColorPrimario;
            statusStrip.ForeColor = Color.White;
            statusStrip.Font = new Font("Segoe UI", 9F, FontStyle.Regular);

            lblCoords.ForeColor = Color.White;
            lblCoords.BorderSides = ToolStripStatusLabelBorderSides.Right;
            lblCoords.BorderStyle = Border3DStyle.Etched;

            lblAlgoritmo.ForeColor = Color.White;
            lblAlgoritmo.BorderSides = ToolStripStatusLabelBorderSides.Right;
            lblAlgoritmo.BorderStyle = Border3DStyle.Etched;

            statusStrip.Items.Clear();
            statusStrip.Items.AddRange(new ToolStripItem[] {
                    lblCoords,
                    lblAlgoritmo,
                    new ToolStripStatusLabel("", null, null, "spring") { Spring = true }
                });

            form.Controls.Add(statusStrip);
            statusStrip.Dock = DockStyle.Bottom;
        }

        private static void SetupEnhancedToolTips(ToolTip toolTip, Button[] buttons, PictureBox pictureBox)
        {
            toolTip.BackColor = Color.FromArgb(44, 62, 80);
            toolTip.ForeColor = Color.White;
            toolTip.ToolTipIcon = ToolTipIcon.Info;
            toolTip.IsBalloon = true;
            toolTip.ShowAlways = true;

            // Encontrar botones específicos por su texto
            var btnRaster = buttons.FirstOrDefault(b => b.Text.Contains("Rasterizado"));
            var btnFill = buttons.FirstOrDefault(b => b.Text.Contains("Relleno"));
            var btnCut = buttons.FirstOrDefault(b => b.Text.Contains("Recorte"));
            var btnCurv = buttons.FirstOrDefault(b => b.Text.Contains("Curvas"));

            if (btnRaster != null)
                toolTip.SetToolTip(btnRaster, "🎯 Algoritmos de rasterización\n• DDA\n• Bresenham\n• Círculo y Elipse");
            if (btnFill != null)
                toolTip.SetToolTip(btnFill, "🎨 Algoritmos de relleno\n• Flood Fill\n• Scanline");
            if (btnCut != null)
                toolTip.SetToolTip(btnCut, "✂️ Algoritmos de recorte\n• Cohen-Sutherland\n• Sutherland-Hodgman");
            if (btnCurv != null)
                toolTip.SetToolTip(btnCurv, "📈 Curvas paramétricas\n• Bézier\n• B-spline");

            toolTip.SetToolTip(pictureBox, "🖼️ Área de dibujo\nHaga clic para dibujar con el algoritmo seleccionado");
        }

        private static void SetupGroupBoxStyles(GroupBox[] groupBoxes)
        {
            foreach (var gb in groupBoxes)
            {
                gb.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                gb.ForeColor = ColorTexto;
                gb.BackColor = Color.White;
                gb.FlatStyle = FlatStyle.Flat;
            }

            // Configuraciones específicas
            var groupBox3 = groupBoxes.FirstOrDefault(gb => gb.Name == "groupBox3");
            if (groupBox3 != null)
            {
                groupBox3.BackColor = Color.FromArgb(250, 250, 250);
            }
        }

        private static void SetupButtonStyles(Button[] buttons)
        {
            foreach (var btn in buttons)
            {
                if (btn.Text.Contains("Resetear") || btn.Text.Contains("🔄"))
                {
                    StyleResetButton(btn);
                }
                else
                {
                    StyleMainButton(btn);
                }
            }
        }

        private static void StyleMainButton(Button btn)
        {
            btn.BackColor = ColorPrimario;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = ColorBotonHover;
            btn.FlatAppearance.MouseDownBackColor = ColorAcento;
            btn.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;

            // Efecto de sombra con Paint
            btn.Paint += (s, e) =>
            {
                var button = s as Button;
                using (var brush = new SolidBrush(button.BackColor))
                {
                    e.Graphics.FillRectangle(brush, button.ClientRectangle);
                }

                TextRenderer.DrawText(e.Graphics, button.Text, button.Font,
                    button.ClientRectangle, button.ForeColor,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            };
        }

        private static void StyleResetButton(Button btn)
        {
            btn.BackColor = ColorAcento;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(192, 57, 43);
            btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            btn.Text = "Resetear";
        }

        private static void SetupPictureBoxStyle(PictureBox pictureBox)
        {
            pictureBox.BackColor = Color.White;
            pictureBox.BorderStyle = BorderStyle.None;

            pictureBox.Paint += (s, e) =>
            {
                using (var pen = new Pen(ColorPrimario, 3))
                {
                    var rect = new Rectangle(0, 0, pictureBox.Width - 1, pictureBox.Height - 1);
                    e.Graphics.DrawRectangle(pen, rect);
                }
            };
        }

        private static void SetupTextBoxStyle(TextBox textBox)
        {
            textBox.BackColor = Color.FromArgb(250, 250, 250);
            textBox.ForeColor = ColorTexto;
            textBox.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            textBox.BorderStyle = BorderStyle.None;
            textBox.ReadOnly = true;
            textBox.Multiline = true;
            textBox.ScrollBars = ScrollBars.Vertical;
        }

        public static RadioButton CreateStyledRadioButton(string text, Point location, string name)
        {
            return new RadioButton
            {
                Text = text,
                Location = location,
                AutoSize = true,
                Name = name,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                ForeColor = ColorTexto,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };
        }

        public static Color GetNotificationColor(bool esExito)
        {
            return esExito ? Color.FromArgb(39, 174, 96) : ColorAcento;
        }

        public static Color GetTextBoxResetColor()
        {
            return Color.FromArgb(250, 250, 250);
        }
    }
}
