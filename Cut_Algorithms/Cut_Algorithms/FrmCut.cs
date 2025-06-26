using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cut_Algorithms
{
    public partial class FrmCut : Form
    {
        // Canvas para manejar las operaciones de dibujo y recorte
        private ClippingCanvas canvas;

        // Almacena puntos temporales para construir polígonos
        private List<Point2D> tempPoints = new List<Point2D>();

        // Indica si estamos creando una línea o un polígono
        private bool isCohenSutherlandMode = true;

        // Indica que estamos en proceso de dibujo
        private bool isDrawing = false;


        // Panel de estado para mostrar coordenadas y estado actual
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblStatus;
        private ToolStripStatusLabel lblCoordinates;

        public FrmCut()
        {
            InitializeComponent();

            // Inicializar el canvas con el PictureBox
            canvas = new ClippingCanvas(pic_Canvas);

            // Configurar controles adicionales
            SetupAdditionalControls();

            // Configurar eventos
            SetupEvents();

            // Seleccionar Cohen-Sutherland por defecto
            rad_CohenSutherland.Checked = true;

            // Inicializar instrucciones
            UpdateInstructions(GetLblInstructions());
        }

        // Elimina o comenta estas líneas en FrmCut.cs (NO en el Designer):
        // Estas líneas cambian el Dock y el orden de los controles en tiempo de ejecución,
        // lo que causa que el formulario se vea diferente a como lo diseñaste.

        private void SetupAdditionalControls()
        {
            // Ya no necesitamos crear lblInstructions aquí
            // Elimina estas líneas:
            // lblInstructions = new Label();
            // lblInstructions.AutoSize = true;
            // lblInstructions.Location = new Point(38, 400);
            // lblInstructions.MaximumSize = new Size(186, 0);
            // lblInstructions.Name = "lblInstructions";
            // lblInstructions.Text = "Seleccione un algoritmo para comenzar.";

            // Crear la barra de estado
            statusStrip = new StatusStrip();
            lblStatus = new ToolStripStatusLabel();
            lblCoordinates = new ToolStripStatusLabel();

            // Configurar la barra de estado
            statusStrip.Items.Add(lblStatus);
            statusStrip.Items.Add(lblCoordinates);

            // Configurar el estado inicial
            lblStatus.Spring = true;
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            lblCoordinates.Alignment = ToolStripItemAlignment.Right;

            // Añadir controles al formulario (ya no necesitamos añadir lblInstructions)
            // this.Controls.Add(lblInstructions);
            this.Controls.Add(statusStrip);

            // Configuración del PictureBox
            pic_Canvas.BackColor = Color.White;
            pic_Canvas.BorderStyle = BorderStyle.FixedSingle;
        }

        private void SetupEvents()
        {
            // Configurar eventos para cambiar de algoritmo
            rad_CohenSutherland.CheckedChanged += AlgorithmRadioButton_CheckedChanged;
            rad_SutherlandHondgam.CheckedChanged += AlgorithmRadioButton_CheckedChanged;

            // Configurar eventos del PictureBox
            pic_Canvas.MouseClick += Pic_Canvas_MouseClick;
            pic_Canvas.Paint += Pic_Canvas_Paint;
            pic_Canvas.MouseMove += Pic_Canvas_MouseMove;

            // Configurar eventos de los botones
            btn_cut.Click += Btn_cut_Click;
            btn_clean.Click += Btn_clean_Click;

            // Evento para manejar el redimensionamiento del formulario
            this.Resize += FrmCut_Resize;

            // Al cargar el formulario
            this.Load += FrmCut_Load;
        }

        private void FrmCut_Load(object sender, EventArgs e)
        {
            // Establecer tamaño mínimo para el formulario
            this.MinimumSize = new Size(800, 600);

            // Actualizar estado
            UpdateStatus("Aplicación iniciada");
        }

        private void FrmCut_Resize(object sender, EventArgs e)
        {
            // Esto asegura que el canvas se redibuje correctamente al cambiar el tamaño
            pic_Canvas.Invalidate();
        }

        private void AlgorithmRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // Actualizar el modo basado en el algoritmo seleccionado
            isCohenSutherlandMode = rad_CohenSutherland.Checked;

            // Limpiar los puntos temporales y reiniciar el dibujo
            ResetDrawing();

            // Actualizar instrucciones para el usuario
            UpdateInstructions(GetLblInstructions());

            // Actualizar estado
            UpdateStatus(isCohenSutherlandMode
                ? "Modo de recorte: Cohen-Sutherland (líneas)"
                : "Modo de recorte: Sutherland-Hodgman (polígonos)");
        }

        private void Pic_Canvas_MouseClick(object sender, MouseEventArgs e)
        {
            // Solo procesar clics del botón izquierdo
            if (e.Button != MouseButtons.Left)
                return;

            if (!isDrawing)
            {
                // Iniciar un nuevo proceso de dibujo
                tempPoints.Clear();
                isDrawing = true;

                UpdateStatus(isCohenSutherlandMode
                    ? "Dibujando línea: Primer punto colocado"
                    : "Dibujando polígono: Primer punto colocado");
            }

            // Añadir el punto al arreglo temporal
            Point2D newPoint = new Point2D(e.X, e.Y);
            tempPoints.Add(newPoint);

            // Actualizar información en la barra de estado
            UpdateStatus(isCohenSutherlandMode
                ? $"Dibujando línea: {tempPoints.Count}/2 puntos"
                : $"Dibujando polígono: {tempPoints.Count} puntos (clic derecho para finalizar)");

            // Si estamos en modo Cohen-Sutherland (línea), dos puntos son suficientes
            if (isCohenSutherlandMode && tempPoints.Count == 2)
            {
                // Crear una línea con los dos puntos
                Line line = new Line(tempPoints[0], tempPoints[1]);
                canvas.AddShape(line);

                // Reiniciar el proceso de dibujo
                tempPoints.Clear();
                isDrawing = false;

                UpdateStatus("Línea creada. Puede dibujar otra línea o recortar");
            }

            // Para el modo de polígono, permitir finalizar con clic derecho
            if (e.Button == MouseButtons.Right && !isCohenSutherlandMode && tempPoints.Count >= 3)
            {
                FinishPolygon();
            }

            // Redibujar para mostrar los puntos actuales
            pic_Canvas.Invalidate();
        }

        private void FinishPolygon()
        {
            if (tempPoints.Count >= 3)
            {
                // Crear un polígono con los puntos recolectados
                Polygon polygon = new Polygon(tempPoints);
                canvas.AddShape(polygon);

                // Reiniciar el proceso de dibujo
                tempPoints.Clear();
                isDrawing = false;

                UpdateStatus("Polígono creado. Puede dibujar otro polígono o recortar");
            }
            else
            {
                UpdateStatus("Se necesitan al menos 3 puntos para crear un polígono");
            }

            pic_Canvas.Invalidate();
        }

        private void Pic_Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            // Mostrar coordenadas actuales en la barra de estado
            lblCoordinates.Text = $"X: {e.X}, Y: {e.Y}";

            // Si estamos dibujando, actualizar la vista previa
            if (isDrawing && tempPoints.Count > 0)
            {
                pic_Canvas.Invalidate();
            }
        }

        private void Pic_Canvas_Paint(object sender, PaintEventArgs e)
        {
            // Mejorar la calidad del dibujo
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Dibuja líneas temporales entre los puntos para visualización
            if (tempPoints.Count > 0)
            {
                // Dibujar los puntos existentes
                using (Pen pen = new Pen(Color.Blue, 1.5f))
                {
                    for (int i = 0; i < tempPoints.Count - 1; i++)
                    {
                        e.Graphics.DrawLine(
                            pen,
                            tempPoints[i].X,
                            tempPoints[i].Y,
                            tempPoints[i + 1].X,
                            tempPoints[i + 1].Y
                        );
                    }

                    // Si estamos en modo polígono y tenemos más de 2 puntos,
                    // mostrar una línea que cierre el polígono
                    if (!isCohenSutherlandMode && tempPoints.Count > 2)
                    {
                        using (Pen dashedPen = new Pen(Color.Gray, 1))
                        {
                            dashedPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                            e.Graphics.DrawLine(
                                dashedPen,
                                tempPoints[tempPoints.Count - 1].X,
                                tempPoints[tempPoints.Count - 1].Y,
                                tempPoints[0].X,
                                tempPoints[0].Y
                            );
                        }
                    }

                    // Si el mouse está sobre el canvas y estamos dibujando,
                    // mostrar una línea desde el último punto hasta la posición actual del mouse
                    if (isDrawing && pic_Canvas.ClientRectangle.Contains(pic_Canvas.PointToClient(Cursor.Position)))
                    {
                        Point mousePos = pic_Canvas.PointToClient(Cursor.Position);
                        using (Pen dashedPen = new Pen(Color.Gray, 1))
                        {
                            dashedPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                            e.Graphics.DrawLine(
                                dashedPen,
                                tempPoints[tempPoints.Count - 1].X,
                                tempPoints[tempPoints.Count - 1].Y,
                                mousePos.X,
                                mousePos.Y
                            );
                        }
                    }
                }
            }

            // Dibuja los puntos temporales
            foreach (var point in tempPoints)
            {
                using (Brush brush = new SolidBrush(Color.Blue))
                {
                    e.Graphics.FillEllipse(
                        brush,
                        point.X - point.Radius,
                        point.Y - point.Radius,
                        2 * point.Radius,
                        2 * point.Radius
                    );
                }
            }

            // Numerar los puntos para mayor claridad
            using (Font font = new Font("Arial", 8))
            using (Brush textBrush = new SolidBrush(Color.Black))
            using (Brush backBrush = new SolidBrush(Color.White))
            {
                for (int i = 0; i < tempPoints.Count; i++)
                {
                    string text = (i + 1).ToString();
                    SizeF textSize = e.Graphics.MeasureString(text, font);

                    float y = tempPoints[i].Y - tempPoints[i].Radius - textSize.Height; // Fixed line

                    e.Graphics.FillRectangle(
                        backBrush,
                        (float)(tempPoints[i].X + tempPoints[i].Radius) - 1,
                        y - 1,
                        textSize.Width + 2,
                        textSize.Height + 2
                    );

                    e.Graphics.DrawString(
                        text,
                        font,
                        textBrush,
                        (float)(tempPoints[i].X + tempPoints[i].Radius),
                        y
                    );
                }
            }
        }

        private void Btn_cut_Click(object sender, EventArgs e)
        {
            // Si hay un polígono en construcción, finalizarlo
            if (isDrawing && !isCohenSutherlandMode && tempPoints.Count >= 3)
            {
                FinishPolygon();
            }

            // Aplicar recorte basado en el algoritmo seleccionado
            int algorithmIndex = isCohenSutherlandMode ? 0 : 1;
            canvas.ApplyClipping(algorithmIndex);

            // Reiniciar el proceso de dibujo si es necesario
            ResetDrawing();

            // Actualizar estado
            UpdateStatus("Recorte aplicado utilizando algoritmo " +
                (isCohenSutherlandMode ? "Cohen-Sutherland" : "Sutherland-Hodgman"));
        }

        private void Btn_clean_Click(object sender, EventArgs e)
        {
            // Limpiar el canvas y reiniciar el proceso de dibujo
            canvas.Clear();
            ResetDrawing();

            // Actualizar estado
            UpdateStatus("Canvas limpiado");

            // Actualizar instrucciones
            UpdateInstructions(GetLblInstructions());
        }

        private void ResetDrawing()
        {
            // Limpiar los puntos temporales y reiniciar el estado de dibujo
            tempPoints.Clear();
            isDrawing = false;
            pic_Canvas.Invalidate();
        }

        private TextBox GetLblInstructions()
        {
            // Devuelve el control txt_instrucciones creado en el diseñador
            return txt_instrucciones;
        }

        private void UpdateInstructions(TextBox txtInstructions)
        {
            // Verificar que el TextBox no sea nulo antes de intentar actualizar su texto
            if (txtInstructions == null)
            {
                return; // Salir del método si el TextBox es nulo
            }

            if (isCohenSutherlandMode)
            {
                txtInstructions.Text = "Algoritmo Cohen-Sutherland: Haga clic para colocar 2 puntos y crear una línea." +
                    " Presione 'Recortar' para aplicar el algoritmo a todas las líneas.";
            }
            else
            {
                txtInstructions.Text = "Algoritmo Sutherland-Hodgman: Haga clic para colocar puntos y crear un polígono." +
                    " Haga clic derecho o presione 'Recortar' para finalizar el polígono y aplicar el algoritmo.";
            }
        }

        private void UpdateStatus(string message)
        {
            lblStatus.Text = message;
        }

        // Método para manejar la tecla Escape para cancelar el dibujo actual
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape && isDrawing)
            {
                // Cancelar el dibujo actual
                tempPoints.Clear();
                isDrawing = false;
                pic_Canvas.Invalidate();
                UpdateStatus("Dibujo cancelado");
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
