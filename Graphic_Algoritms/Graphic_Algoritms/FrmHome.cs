using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AlgoritmosGraficar;

namespace Graphic_Algoritms
{
    public partial class FrmHome : Form
    {
        private ToolTip toolTip = new ToolTip();
        private StatusStrip statusStrip = new StatusStrip();
        private ToolStripStatusLabel lblCoords = new ToolStripStatusLabel("X: 0, Y: 0");
        private ToolStripStatusLabel lblAlgoritmo = new ToolStripStatusLabel("Algoritmo: Ninguno");

        private readonly Dictionary<string, string[]> opcionesAlgoritmos = new Dictionary<string, string[]>
            {
                { "Rasterizado", new string[] { "DDA", "Bresenham", "Círculo", "Elipse" } },
                { "Relleno", new string[] { "Flood Fill", "Scanline" } },
                { "Recorte", new string[] { "Cohen–Sutherland (líneas)", "Sutherland–Hodgman (polígonos)" } },
                { "Curvas", new string[] { "Bézier", "B-spline" } }
            };

        private string algoritmoSeleccionado = "";

        // Variables para manejo de clics en el canvas
        private Point? primerPunto = null;
        private Point? segundoPunto = null;
        private bool esperandoPunto = false;

        // Variable para el algoritmo de flood fill
        private FloodFill floodFillAlgoritmo;

        public FrmHome()
        {
            InitializeComponent();

            // Aplicar diseño desde la clase FrmHomeDesign
            FrmHomeDesign.ApplyDesign(
                this,
                new GroupBox[] { groupBox1, groupBox2, groupBox3, groupBox4, groupBox5 },
                new Button[] { btn_Raster, btn_FillAlg, btn_CutAlg, btn_CurvAlg, btn_reset },
                pictureBox1,
                textBox1,
                statusStrip,
                lblCoords,
                lblAlgoritmo,
                toolTip
            );

            SetupEventHandlers();
            SetupInitialState();
        }

        private void SetupEventHandlers()
        {
            // Coordenadas del mouse mejoradas
            pictureBox1.MouseMove += (sender, e) =>
            {
                lblCoords.Text = $"📍 X: {e.X:000}, Y: {e.Y:000}";
            };

            pictureBox1.MouseLeave += (sender, e) =>
            {
                lblCoords.Text = "📍 X: ---, Y: ---";
            };

            // ✅ NUEVO: Evento de clic para dibujar con algoritmos
            pictureBox1.MouseClick += PictureBox1_MouseClick;

            // Evento de reset mejorado
            btn_reset.Click += (sender, e) =>
            {
                ResetCanvas();
                ShowNotification("Área de dibujo reseteada correctamente", true);
            };

            // Eventos de hover para botones principales
            foreach (var btn in new[] { btn_Raster, btn_FillAlg, btn_CutAlg, btn_CurvAlg })
            {
                btn.MouseEnter += (s, e) => btn.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
                btn.MouseLeave += (s, e) => btn.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            }
        }

        // ✅ CORREGIDO: Método para manejar clics en el canvas
        private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (string.IsNullOrEmpty(algoritmoSeleccionado))
            {
                ShowNotification("Seleccione un algoritmo antes de dibujar", false);
                return;
            }

            switch (algoritmoSeleccionado)
            {
                case "DDA":
                case "Bresenham":
                    Point puntoCanvas = ConvertirCoordenadas(e.Location);
                    ManejgarAlgoritmoLinea(puntoCanvas);
                    break;
                case "Círculo":
                    Point puntoCanvasCirculo = ConvertirCoordenadas(e.Location);
                    ManejarAlgoritmoCirculo(puntoCanvasCirculo);
                    break;
                case "Elipse":
                    Point puntoCanvasElipse = ConvertirCoordenadas(e.Location);
                    ManejarAlgoritmoElipse(puntoCanvasElipse);
                    break;
                case "Flood Fill":
                    ManejarFloodFill(e.Location); // Usar coordenadas directas del mouse
                    break;
                default:
                    ShowNotification($"Algoritmo {algoritmoSeleccionado} no implementado", false);
                    break;
            }
        }

        // ✅ NUEVO: Maneja algoritmo de flood fill
        private void ManejarFloodFill(Point puntoMouse)
        {
            if (floodFillAlgoritmo == null)
            {
                floodFillAlgoritmo = new FloodFill(pictureBox1);
                floodFillAlgoritmo.IniciarCreacionPoligono();
                ShowNotification("Haga clic para crear vértices del polígono. Clic derecho para completar.");
            }
            else if (floodFillAlgoritmo.TienePoligonoCompleto)
            {
                // Aplicar flood fill
                bool resultado = floodFillAlgoritmo.ProcesarClicFloodFill(puntoMouse, Color.Orange, 50);
                if (resultado)
                {
                    ShowNotification("Flood Fill aplicado correctamente");
                }
                else
                {
                    ShowNotification("Haga clic dentro del polígono para rellenar", false);
                }
            }
        }

        // ✅ NUEVO: Maneja algoritmos de línea (DDA y Bresenham)
        private void ManejgarAlgoritmoLinea(Point puntoCanvas)
        {
            if (primerPunto == null)
            {
                // Primer clic: guardar punto inicial
                primerPunto = puntoCanvas;
                esperandoPunto = true;
                ShowNotification($"Punto inicial: ({puntoCanvas.X}, {puntoCanvas.Y}). Haga clic para el punto final.");
                DibujarPuntoTemporal(puntoCanvas, Color.Red);
            }
            else
            {
                // Segundo clic: dibujar línea
                segundoPunto = puntoCanvas;
                esperandoPunto = false;

                EjecutarAlgoritmoLinea(primerPunto.Value, segundoPunto.Value);

                // Resetear para próxima línea
                primerPunto = null;
                segundoPunto = null;
            }
        }

        // ✅ NUEVO: Maneja algoritmo de círculo
        private void ManejarAlgoritmoCirculo(Point puntoCanvas)
        {
            if (primerPunto == null)
            {
                // Primer clic: guardar centro del círculo
                primerPunto = puntoCanvas;
                esperandoPunto = true;
                ShowNotification($"Centro: ({puntoCanvas.X}, {puntoCanvas.Y}). Haga clic para definir el radio.");
                DibujarPuntoTemporal(puntoCanvas, Color.Blue);
            }
            else
            {
                // Segundo clic: calcular radio y dibujar círculo
                int radio = CalcularDistancia(primerPunto.Value, puntoCanvas);
                esperandoPunto = false;

                EjecutarAlgoritmoCirculo(primerPunto.Value, radio);

                // Resetear para próximo círculo
                primerPunto = null;
            }
        }

        // ✅ NUEVO: Maneja algoritmo de elipse
        private void ManejarAlgoritmoElipse(Point puntoCanvas)
        {
            if (primerPunto == null)
            {
                // Primer clic: centro de la elipse
                primerPunto = puntoCanvas;
                esperandoPunto = true;
                ShowNotification($"Centro: ({puntoCanvas.X}, {puntoCanvas.Y}). Haga clic para definir los semiejes.");
                DibujarPuntoTemporal(puntoCanvas, Color.Purple);
            }
            else
            {
                // Segundo clic: define los semiejes
                segundoPunto = puntoCanvas;
                esperandoPunto = false;

                EjecutarAlgoritmoElipse(primerPunto.Value, segundoPunto.Value);

                // Resetear para próxima elipse
                primerPunto = null;
                segundoPunto = null;
            }
        }

        // ✅ NUEVO: Ejecuta algoritmos de línea
        private void EjecutarAlgoritmoLinea(Point inicio, Point fin)
        {
            try
            {
                Algoritmo algoritmo = null;

                switch (algoritmoSeleccionado)
                {
                    case "DDA":
                        algoritmo = new DDA();
                        break;
                    case "Bresenham":
                        algoritmo = new Bresenham();
                        break;
                }

                if (algoritmo != null)
                {
                    algoritmo.PuntoInicial = inicio;
                    algoritmo.PuntoFinal = fin;
                    algoritmo.CalcularPuntos();

                    var animacion = new Animation(algoritmo.Puntos, pictureBox1, Color.Black, 1, 100);
                    animacion.Iniciar();

                    ShowNotification($"Animación iniciada con {algoritmoSeleccionado}: ({inicio.X},{inicio.Y}) → ({fin.X},{fin.Y})");
                }
            }
            catch (Exception ex)
            {
                ShowNotification($"Error al ejecutar {algoritmoSeleccionado}: {ex.Message}", false);
            }
        }

        // ✅ NUEVO: Ejecuta algoritmo de círculo
        private void EjecutarAlgoritmoCirculo(Point centro, int radio)
        {
            try
            {
                var circulo = new Circulo();
                circulo.PuntoInicial = centro;
                circulo.Radio = radio;
                circulo.CalcularPuntos();

                var animacion = new Animation(circulo.Puntos, pictureBox1, Color.Black, 1, 100);
                animacion.Iniciar();

                ShowNotification($"Animación iniciada para círculo: Centro({centro.X},{centro.Y}) Radio={radio}");
            }
            catch (Exception ex)
            {
                ShowNotification($"Error al dibujar círculo: {ex.Message}", false);
            }
        }

        // ✅ NUEVO: Ejecuta algoritmo de elipse
        private void EjecutarAlgoritmoElipse(Point centro, Point puntoRadio)
        {
            try
            {
                var elipse = new Elipse();
                elipse.PuntoInicial = centro;
                elipse.PuntoFinal = puntoRadio;
                elipse.CalcularPuntos();

                var animacion = new Animation(elipse.Puntos, pictureBox1, Color.Black, 1, 100);
                animacion.Iniciar();

                int rx = Math.Abs(puntoRadio.X - centro.X);
                int ry = Math.Abs(puntoRadio.Y - centro.Y);
                ShowNotification($"Animación iniciada para elipse: Centro({centro.X},{centro.Y}) Rx={rx} Ry={ry}");
            }
            catch (Exception ex)
            {
                ShowNotification($"Error al dibujar elipse: {ex.Message}", false);
            }
        }

        // ✅ NUEVO: Convierte coordenadas del mouse a coordenadas del sistema centrado
        private Point ConvertirCoordenadas(Point mousePoint)
        {
            int centroX = pictureBox1.Width / 2;
            int centroY = pictureBox1.Height / 2;

            // Convertir a coordenadas del sistema centrado
            int x = mousePoint.X - centroX;
            int y = centroY - mousePoint.Y; // Invertir Y para que crezca hacia arriba

            return new Point(x, y);
        }

        // ✅ NUEVO: Calcula la distancia entre dos puntos (para el radio del círculo)
        private int CalcularDistancia(Point p1, Point p2)
        {
            int dx = p2.X - p1.X;
            int dy = p2.Y - p1.Y;
            return (int)Math.Round(Math.Sqrt(dx * dx + dy * dy));
        }

        // ✅ NUEVO: Dibuja un punto temporal para indicar selección
        private void DibujarPuntoTemporal(Point punto, Color color)
        {
            if (pictureBox1.Image == null)
            {
                pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            }

            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                using (SolidBrush brush = new SolidBrush(color))
                {
                    int centroX = pictureBox1.Width / 2;
                    int centroY = pictureBox1.Height / 2;
                    int pixelX = centroX + punto.X;
                    int pixelY = centroY - punto.Y;

                    // Dibujar un pequeño círculo para marcar el punto
                    g.FillEllipse(brush, pixelX - 3, pixelY - 3, 6, 6);
                }
            }
            pictureBox1.Refresh();
        }

        private void SetupInitialState()
        {
            textBox1.Text = "🎨 Bienvenido al Sistema de Algoritmos Gráficos\r\n\r\n" +
                           "📋 Instrucciones:\r\n" +
                           "1. Seleccione una categoría de algoritmo\r\n" +
                           "2. Elija el algoritmo específico\r\n" +
                           "3. Dibuje en el área blanca\r\n\r\n" +
                           "✨ ¡Comience seleccionando un algoritmo!";
            groupBox3.Visible = false;
        }

        private void ShowNotification(string mensaje, bool esExito = true)
        {
            var color = FrmHomeDesign.GetNotificationColor(esExito);
            textBox1.BackColor = color;
            textBox1.ForeColor = Color.White;
            textBox1.Text = $"{(esExito ? "✅" : "❌")} {mensaje}";

            var timer = new Timer { Interval = 2000 };
            timer.Tick += (s, e) =>
            {
                textBox1.BackColor = FrmHomeDesign.GetTextBoxResetColor();
                textBox1.ForeColor = FrmHomeDesign.ColorTexto;
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();
        }

        private void ResetCanvas()
        {
            pictureBox1.Image = null;
            pictureBox1.Invalidate();
            groupBox3.Visible = false;
            algoritmoSeleccionado = "";
            lblAlgoritmo.Text = "🎯 Algoritmo: Ninguno";

            // ✅ NUEVO: Resetear estado de clics y flood fill
            primerPunto = null;
            segundoPunto = null;
            esperandoPunto = false;
            floodFillAlgoritmo = null;
        }

        private void ActualizarOpcionesAlgoritmo(string categoria)
        {
            groupBox3.Controls.Clear();
            groupBox3.Text = $"🔧 Opciones de {categoria}";
            if (!opcionesAlgoritmos.ContainsKey(categoria)) return;

            var opciones = opcionesAlgoritmos[categoria];
            int y = 35;

            for (int i = 0; i < opciones.Length; i++)
            {
                RadioButton rb = FrmHomeDesign.CreateStyledRadioButton(
                    opciones[i],
                    new Point(20, y),
                    $"rb_{categoria}_{i}"
                );

                rb.CheckedChanged += (s, e) =>
                {
                    if (rb.Checked)
                    {
                        algoritmoSeleccionado = rb.Text;
                        lblAlgoritmo.Text = $"🎯 Algoritmo: {algoritmoSeleccionado}";
                        ShowNotification($"Algoritmo {algoritmoSeleccionado} seleccionado");

                        // ✅ NUEVO: Actualizar instrucciones según el algoritmo
                        ActualizarInstrucciones();

                        // Resetear estado de clics
                        primerPunto = null;
                        segundoPunto = null;
                        esperandoPunto = false;
                        floodFillAlgoritmo = null;
                    }
                };

                groupBox3.Controls.Add(rb);
                y += 35;
            }

            groupBox3.Visible = true;
        }

        // ✅ CORREGIDO: Actualiza las instrucciones según el algoritmo seleccionado
        private void ActualizarInstrucciones()
        {
            string instrucciones = "";

            switch (algoritmoSeleccionado)
            {
                case "DDA":
                case "Bresenham":
                    instrucciones = $"🎯 {algoritmoSeleccionado} - Dibujo de Líneas\r\n\r\n" +
                                   "📋 Instrucciones:\r\n" +
                                   "1. Haga clic para definir el punto inicial\r\n" +
                                   "2. Haga clic para definir el punto final\r\n" +
                                   "3. La línea se dibujará automáticamente\r\n\r\n" +
                                   "💡 Puede dibujar múltiples líneas";
                    break;

                case "Círculo":
                    instrucciones = "🎯 Algoritmo de Círculo\r\n\r\n" +
                                   "📋 Instrucciones:\r\n" +
                                   "1. Haga clic para definir el centro\r\n" +
                                   "2. Haga clic para definir el radio\r\n" +
                                   "3. El círculo se dibujará automáticamente\r\n\r\n" +
                                   "💡 Puede dibujar múltiples círculos";
                    break;

                case "Elipse":
                    instrucciones = "🎯 Algoritmo de Elipse\r\n\r\n" +
                                   "📋 Instrucciones:\r\n" +
                                   "1. Haga clic para definir el centro\r\n" +
                                   "2. Haga clic para definir los semiejes (Rx, Ry)\r\n" +
                                   "3. La elipse se dibujará automáticamente\r\n\r\n" +
                                   "💡 Puede dibujar múltiples elipses";
                    break;

                case "Flood Fill":
                    instrucciones = "🎨 Algoritmo Flood Fill\r\n\r\n" +
                                   "📋 Instrucciones:\r\n" +
                                   "1. Haga clics para crear vértices del polígono\r\n" +
                                   "2. Clic derecho para completar el polígono\r\n" +
                                   "3. Haga clic dentro para rellenar\r\n\r\n" +
                                   "💡 Puede crear polígonos personalizados";
                    break;

                default:
                    instrucciones = $"🎯 {algoritmoSeleccionado}\r\n\r\n" +
                                   "⚠️ Este algoritmo está seleccionado pero\r\n" +
                                   "aún no está implementado.\r\n\r\n" +
                                   "Seleccione DDA, Bresenham, Círculo, Elipse\r\n" +
                                   "o Flood Fill para comenzar a dibujar.";
                    break;
            }

            if (!string.IsNullOrEmpty(instrucciones))
            {
                textBox1.Text = instrucciones;
            }
        }

        // Eventos de botones
        private void rasterizacoónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btn_Raster.PerformClick();
        }

        private void btn_Raster_Click(object sender, EventArgs e)
        {
            textBox1.Text = "🎯 Rasterización de Primitivas\r\n\r\n" +
                           "Algoritmos disponibles para dibujar líneas, círculos y elipses.\r\n" +
                           "Seleccione un algoritmo específico y dibuje en el área blanca.";
            ActualizarOpcionesAlgoritmo("Rasterizado");
        }

        private void btn_FillAlg_Click(object sender, EventArgs e)
        {
            textBox1.Text = "🎨 Algoritmos de Relleno\r\n\r\n" +
                           "Técnicas para rellenar regiones cerradas con colores o patrones.\r\n" +
                           "Seleccione un algoritmo y haga clic en una región para rellenar.";
            ActualizarOpcionesAlgoritmo("Relleno");
        }

        private void btn_CutAlg_Click(object sender, EventArgs e)
        {
            textBox1.Text = "✂️ Algoritmos de Recorte\r\n\r\n" +
                           "Técnicas para recortar primitivas gráficas contra ventanas.\r\n" +
                           "Útil para optimizar el renderizado y eliminar partes no visibles.";
            ActualizarOpcionesAlgoritmo("Recorte");
        }

        private void btn_CurvAlg_Click(object sender, EventArgs e)
        {
            textBox1.Text = "📈 Curvas Paramétricas\r\n\r\n" +
                           "Algoritmos para generar curvas suaves y complejas.\r\n" +
                           "Ideales para diseño gráfico y modelado matemático.";
            ActualizarOpcionesAlgoritmo("Curvas");
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            ResetCanvas();
        }
    }
}
