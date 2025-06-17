using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgoritmoRecorte
{
    public partial class FrmRecorte : Form
    {
        private CohenSutherland cohen;
        private Point? firstClickPoint = null; // Punto del primer clic
        private Point? tempPreviewPoint = null; // Punto temporal para vista previa

        public FrmRecorte()
        {
            InitializeComponent();
            cohen = new CohenSutherland(PicCanvas.ClientSize);
            SetupDataGridView();

            // Configurar eventos del PictureBox
            PicCanvas.Paint += PicCanvas_Paint;
            PicCanvas.MouseClick += PicCanvas_MouseClick;
            PicCanvas.MouseMove += PicCanvas_MouseMove;
        }

        private void BtnCut_Click(object sender, EventArgs e)
        {
            cohen.ClipLines();
            PicCanvas.Invalidate();
            PopulateDataGridView();
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            PicCanvas.Image = null;
            PicCanvas.Invalidate();
            cohen = new CohenSutherland(PicCanvas.ClientSize);
            dgwPixels.Rows.Clear();
            firstClickPoint = null;
            tempPreviewPoint = null;
        }

        private void PicCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Dibujar región de recorte
            using (Pen clipPen = new Pen(Color.Red, 1))
            {
                g.DrawRectangle(clipPen, cohen.GetClipRegion());
            }

            // Dibujar las líneas almacenadas
            cohen.Draw(g);

            // Dibujar la cuadrícula
            cohen.DrawGrid(g, PicCanvas.ClientSize);

            // Dibujar línea de vista previa si estamos en el proceso de dibujar
            if (firstClickPoint.HasValue && tempPreviewPoint.HasValue)
            {
                using (Pen previewPen = new Pen(Color.Gray, 1))
                {
                    g.DrawLine(previewPen, firstClickPoint.Value, tempPreviewPoint.Value);
                }
            }
        }

        private void PicCanvas_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Si no hay primer punto, guardarlo
                if (!firstClickPoint.HasValue)
                {
                    firstClickPoint = e.Location;
                }
                // Si ya hay primer punto, añadir la línea y reiniciar
                else
                {
                    cohen.AddLine(firstClickPoint.Value, e.Location);
                    firstClickPoint = null; // Reiniciar para la próxima línea
                    PicCanvas.Invalidate();
                    PopulateDataGridView(); // Actualizar el DataGridView con la nueva línea
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                // Si hay un primer punto seleccionado, cancelar la operación
                if (firstClickPoint.HasValue)
                {
                    firstClickPoint = null;
                    PicCanvas.Invalidate();
                }
                // Si no, eliminar segmentos externos cercanos al punto
                else if (cohen.RemoveExternalSegmentsAtPoint(e.Location))
                {
                    PicCanvas.Invalidate();
                    PopulateDataGridView(); // Actualizar tras eliminar
                }
            }
        }

        private void PicCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            // Actualizar punto temporal para vista previa
            if (firstClickPoint.HasValue)
            {
                tempPreviewPoint = e.Location;
                PicCanvas.Invalidate();
            }
        }

        private void SetupDataGridView()
        {
            dgwPixels.Columns.Clear();
            dgwPixels.Columns.Add("StartPoint", "Inicial");
            dgwPixels.Columns.Add("EndPoint", " Final");
            dgwPixels.Columns.Add("BinaryValue", "Valor Binario");
            dgwPixels.Columns.Add("OctantCode", "Código Octante");
            dgwPixels.AllowUserToAddRows = false;
        }

        private void PopulateDataGridView()
        {
            dgwPixels.Rows.Clear();

            var clippedLines = cohen.GetClippedLineSegments();
            foreach (var line in clippedLines)
            {
                if (!line.start.IsEmpty && !line.end.IsEmpty)
                {
                    // Convertir las coordenadas a binario
                    string startBinary = $"({Convert.ToString(line.start.X, 2)}, {Convert.ToString(line.start.Y, 2)})";
                    string endBinary = $"({Convert.ToString(line.end.X, 2)}, {Convert.ToString(line.end.Y, 2)})";

                    // Calcular código de octante
                    int octantCode = CalculateOctantCode(line.start, line.end);

                    dgwPixels.Rows.Add(
                        $"({line.start.X}, {line.start.Y})",
                        $"({line.end.X}, {line.end.Y})",
                        $"Inicio: {startBinary}, Fin: {endBinary}",
                        $"Octante: {octantCode}"
                    );
                }
            }
        }

        // Método para calcular el código de octante de una línea
        private int CalculateOctantCode(Point start, Point end)
        {
            // Calcular las diferencias en X e Y
            int dx = end.X - start.X;
            int dy = end.Y - start.Y;

            // Determinar el octante basado en el signo de dx, dy y sus magnitudes relativas
            if (dx >= 0 && dy >= 0) // Primer cuadrante
            {
                if (dx >= dy) return 0;
                else return 1;
            }
            else if (dx < 0 && dy >= 0) // Segundo cuadrante
            {
                if (-dx < dy) return 2;
                else return 3;
            }
            else if (dx < 0 && dy < 0) // Tercer cuadrante
            {
                if (-dx >= -dy) return 4;
                else return 5;
            }
            else // Cuarto cuadrante (dx >= 0 && dy < 0)
            {
                if (dx < -dy) return 6;
                else return 7;
            }
        }
    }
}
