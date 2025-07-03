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
using Cut_Algorithms; // ✅ AGREGAR ESTA LÍNEA
using CurvasParametricas; // ✅ AGREGAR ESTA LÍNEA PARA CURVAS
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
        public FloodFill floodFillAlgoritmo;
        private ScanLine scanLineAlgoritmo;
        private bool modoCreacionPoligono = false; // ✅ NUEVO: Controla el modo de creación
        private bool poligonoListo = false; // ✅ NUEVO: Indica si el polígono está listo para flood fill



        // Variables para algoritmos de recorte
        private List<Cut_Algorithms.IShape> formasParaRecortar = new List<Cut_Algorithms.IShape>();
        private RectangleF ventanaRecorte; // Ventana calculada automáticamente (octante central)
        private Cut_Algorithms.CohenSutherlandClipper cohenSutherlandClipper = new Cut_Algorithms.CohenSutherlandClipper();
        private Cut_Algorithms.SutherlandHodgmanClipper sutherlandHodgmanClipper = new Cut_Algorithms.SutherlandHodgmanClipper();
        private string estadoRecorte = ""; // Estado actual del proceso de recorte

        // Variables para creación de formas de recorte
        private Point? puntoInicialLinea = null;
        private List<Point2D> verticesPoligonoRecorte = new List<Point2D>();
        // Variables para curvas paramétricas
        private AdministradorCurvas administradorCurvas;

        // ✅ NUEVO: UX Enhancer para mejoras visuales
        private UXEnhancer uxEnhancer;


        public FrmHome()
        {
            InitializeComponent();

            // Inicializar administrador de curvas
            administradorCurvas = new AdministradorCurvas(pictureBox1);
            administradorCurvas.CurvaCompletada += (sender, args) =>
            {
                ShowNotification($"Curva {args.Curva.GetType().Name} completada correctamente.");
            };

            // ✅ NUEVO: Inicializar UX Enhancer
            uxEnhancer = new UXEnhancer(pictureBox1);

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

        // ✅ NUEVO: Evento del botón completar polígono
        private void btn_CompletarPoligono_Click(object sender, EventArgs e)
        {
            if (floodFillAlgoritmo != null && modoCreacionPoligono)
            {
                floodFillAlgoritmo.CompletarPoligono();
                modoCreacionPoligono = false;
                poligonoListo = true;
                btn_CompletarPoligono.Visible = false;

                ShowNotification("Polígono completado. Ahora haga clic dentro para aplicar Flood Fill.");

                // Actualizar instrucciones
                textBox1.Text = "🎨 Polígono Completado - Flood Fill\r\n\r\n" +
                               "📋 Instrucciones:\r\n" +
                               "🔒 El polígono ha sido cerrado correctamente\r\n" +
                               "🎯 Haga clic DENTRO del polígono para rellenarlo\r\n" +
                               "🎨 El relleno se animará automáticamente\r\n\r\n" +
                               "💡 Use 'Resetear' para crear un nuevo polígono";
            }
        }

        // ✅ MODIFICADO: Método para manejar clics en el canvas
        private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (string.IsNullOrEmpty(algoritmoSeleccionado))
            {
                ShowNotification("Seleccione un algoritmo antes de dibujar", false);
                return;
            }

            // ✅ NUEVO: Registrar click para visualización UX
            uxEnhancer.RegistrarClick(e.Location);

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
                    ManejarFloodFill(e.Location);
                    break;
                case "Scanline": // ✅ NUEVO
                    ManejarScanLine(e.Location);
                    break;
                case "Cohen–Sutherland (líneas)": // ✅ NUEVO
                    ManejarRecorteCohenSutherland(e.Location);
                    break;
                case "Sutherland–Hodgman (polígonos)": // ✅ NUEVO
                    ManejarRecorteSutherlandHodgman(e.Location);
                    break;
                case "Bézier": // ✅ NUEVO PARA CURVAS
                    ManejarCurvaBezier(e.Location);
                    break;
                case "B-spline": // ✅ NUEVO PARA CURVAS
                    ManejarCurvaBSpline(e.Location);
                    break;
                default:
                    ShowNotification($"Algoritmo {algoritmoSeleccionado} no implementado", false);
                    break;
            }
        }

        // ✅ MODIFICADO: Maneja algoritmo de flood fill
        /* private void ManejarFloodFill(Point puntoMouse)
         {
             if (floodFillAlgoritmo == null)
             {
                 // Iniciar creación de polígono
                 floodFillAlgoritmo = new FloodFill(pictureBox1);
                 floodFillAlgoritmo.IniciarCreacionPoligono();
                 modoCreacionPoligono = true;
                 poligonoListo = false;

                 // Mostrar botón de dibujo
                 btn_Draw.Visible = true;
                 btn_Paint.Visible = false;

                 ShowNotification("Modo creación: Haga clics para agregar vértices del polígono.");
             }
             else if (modoCreacionPoligono)
             {
                 // Seguir agregando vértices
                 ShowNotification($"Vértice agregado. Total: {floodFillAlgoritmo.NumeroVertices + 1}. Use el botón 'Dibujar' cuando termine.");
             }
             else if (poligonoListo && floodFillAlgoritmo.TienePoligonoCompleto && !btn_Paint.Enabled)
             {
                 // Aplicar flood fill
                 //bool resultado = floodFillAlgoritmo.ProcesarClicFloodFill(puntoMouse, Color.Orange, 450);
                 bool resultado = true;
                     floodFillAlgoritmo.FloodFillAnimado(pictureBox1, puntoMouse.X, puntoMouse.Y, Color.Orange, 150);
                /* if (resultado)
                 {
                     ShowNotification("🎨 Flood Fill aplicado correctamente - Animación en progreso");
                     btn_Paint.Enabled = true; // Reactivar el botón para permitir más pintados
                 }
                 else
                 {
                     ShowNotification("❌ Haga clic DENTRO del polígono para rellenar", false);
                 }*/
        //   }
        //}
        // ✅ CORREGIDO: Maneja algoritmo de flood fill
        private void ManejarFloodFill(Point puntoMouse)
        {
            if (floodFillAlgoritmo == null)
            {
                // Iniciar creación de polígono
                floodFillAlgoritmo = new FloodFill(pictureBox1);
                floodFillAlgoritmo.IniciarCreacionPoligono();
                modoCreacionPoligono = true;
                poligonoListo = false;

                // Mostrar botón de dibujo
                btn_Draw.Visible = true;
                //btn_Paint.Visible = false;

                ShowNotification("Modo creación: Haga clics para agregar vértices del polígono.");
            }
            else if (modoCreacionPoligono)
            {
                // Seguir agregando vértices - el PolygonDrawer maneja esto automáticamente
                ShowNotification($"Vértice agregado. Total: {floodFillAlgoritmo.NumeroVertices + 1}. Use el botón 'Completar Polígono' cuando termine.");
            }
            else if (poligonoListo && floodFillAlgoritmo.TienePoligonoCompleto)
            {
                // ✅ CAMBIO CLAVE: Remover la condición !btn_Paint.Enabled
                // ✅ CAMBIO CLAVE: Usar ProcesarClicFloodFill en lugar de llamada directa
                bool resultado = floodFillAlgoritmo.ProcesarClicFloodFill(puntoMouse, Color.Orange, 50);
                if (resultado)
                {
                    ShowNotification("🎨 Flood Fill aplicado correctamente - Animación en progreso");
                    // btn_Paint.Enabled = true; // Reactivar el botón para permitir más pintados
                }
                else
                {
                    ShowNotification("❌ Haga clic DENTRO del polígono para rellenar", false);
                }
            }
        }


        // ✅ RESTO DE MÉTODOS SIN CAMBIOS...
        private void ManejgarAlgoritmoLinea(Point puntoCanvas)
        {
            if (primerPunto == null)
            {
                primerPunto = puntoCanvas;
                esperandoPunto = true;
                ShowNotification($"Punto inicial: ({puntoCanvas.X}, {puntoCanvas.Y}). Haga clic para el punto final.");
                DibujarPuntoTemporal(puntoCanvas, Color.Red);
            }
            else
            {
                segundoPunto = puntoCanvas;
                esperandoPunto = false;
                EjecutarAlgoritmoLinea(primerPunto.Value, segundoPunto.Value);
                primerPunto = null;
                segundoPunto = null;
            }
        }

        // ✅ MEJORADO: Maneja el algoritmo Cohen-Sutherland simplificado
        private void ManejarRecorteCohenSutherland(Point puntoMouse)
        {
            switch (estadoRecorte)
            {
                case "":
                    // Iniciar proceso: crear líneas directamente
                    estadoRecorte = "crear_lineas";
                    CalcularVentanaRecorteAutomatica();
                    DibujarVentanaRecorte();
                    ShowNotification("Cohen-Sutherland iniciado. Ventana de recorte calculada automáticamente en el centro del canvas. Cree líneas haciendo clic en pares de puntos.");
                    ActualizarInstrucciones();
                    break;

                case "crear_lineas":
                    ManejarCreacionLinea(puntoMouse);
                    break;
            }
        }

        // ✅ MEJORADO: Maneja el algoritmo Sutherland-Hodgman simplificado
        private void ManejarRecorteSutherlandHodgman(Point puntoMouse)
        {
            switch (estadoRecorte)
            {
                case "":
                    // Iniciar proceso: crear polígonos directamente
                    estadoRecorte = "crear_poligonos";
                    CalcularVentanaRecorteAutomatica();
                    DibujarVentanaRecorte();
                    ShowNotification("Sutherland-Hodgman iniciado. Ventana de recorte calculada automáticamente en el centro del canvas. Cree polígonos haciendo clic en vértices.");
                    ActualizarInstrucciones();
                    break;

                case "crear_poligonos":
                    ManejarCreacionPoligonoRecorte(puntoMouse);
                    break;
            }
        }

        // ✅ MEJORADO: Calcula la ventana de recorte automáticamente usando el octante central
        // Según los algoritmos clásicos de Cohen-Sutherland y Sutherland-Hodgman
        private void CalcularVentanaRecorteAutomatica()
        {
            // ✅ NUEVO: Usar el área de recorte del UX Enhancer
            ventanaRecorte = uxEnhancer.ObtenerAreaRecorte();

            // Si el UX Enhancer no tiene área configurada, calcular manualmente
            if (ventanaRecorte.IsEmpty)
            {
                // Obtener dimensiones del canvas
                int anchoCanvas = pictureBox1.Width;
                int altoCanvas = pictureBox1.Height;

                // Calcular centro del canvas
                int centroX = anchoCanvas / 2;
                int centroY = altoCanvas / 2;

                // Definir la ventana de recorte como el octante central
                // Usando aproximadamente 40% del área total, centrada
                int anchoVentana = (int)(anchoCanvas * 0.4);
                int altoVentana = (int)(altoCanvas * 0.4);

                // Calcular posición para centrar la ventana
                int x = centroX - (anchoVentana / 2);
                int y = centroY - (altoVentana / 2);

                // Asegurar que la ventana esté dentro de los límites del canvas
                x = Math.Max(0, Math.Min(x, anchoCanvas - anchoVentana));
                y = Math.Max(0, Math.Min(y, altoCanvas - altoVentana));

                ventanaRecorte = new RectangleF(x, y, anchoVentana, altoVentana);
            }
        }

        // ✅ MEJORADO: Maneja la creación de líneas para Cohen-Sutherland
        private void ManejarCreacionLinea(Point puntoMouse)
        {
            if (puntoInicialLinea == null)
            {
                puntoInicialLinea = puntoMouse;
                ShowNotification($"Punto inicial de línea: ({puntoMouse.X}, {puntoMouse.Y}). Haga clic para el punto final.");
                DibujarPuntoTemporal(puntoMouse, Color.Blue);
            }
            else
            {
                // ✅ NUEVO: Agregar línea al UX Enhancer para visualización
                uxEnhancer.AgregarLineaOriginal(
                    new PointF(puntoInicialLinea.Value.X, puntoInicialLinea.Value.Y),
                    new PointF(puntoMouse.X, puntoMouse.Y)
                );

                // Crear línea para algoritmo de recorte
                var linea = new Cut_Algorithms.Line(
                    new Cut_Algorithms.Point2D(puntoInicialLinea.Value.X, puntoInicialLinea.Value.Y),
                    new Cut_Algorithms.Point2D(puntoMouse.X, puntoMouse.Y)
                );

                formasParaRecortar.Add(linea);

                puntoInicialLinea = null;

                // Habilitar botón de recorte si hay al menos una línea
                if (formasParaRecortar.Count > 0)
                {
                    btn_Paint.Enabled = true;
                }

                ShowNotification($"Línea creada. Total: {formasParaRecortar.Count}. Continúe creando líneas o use 'Aplicar Recorte'.");
                ActualizarInstrucciones();
            }
        }

        // ✅ MEJORADO: Maneja la creación de polígonos para Sutherland-Hodgman
        private void ManejarCreacionPoligonoRecorte(Point puntoMouse)
        {
            verticesPoligonoRecorte.Add(new Cut_Algorithms.Point2D(puntoMouse.X, puntoMouse.Y));
            DibujarPuntoTemporal(puntoMouse, Color.Purple);

            if (verticesPoligonoRecorte.Count > 1)
            {
                // Dibujar línea temporal del polígono
                DibujarLineaTemporal(
                    new Point((int)verticesPoligonoRecorte[verticesPoligonoRecorte.Count - 2].X,
                             (int)verticesPoligonoRecorte[verticesPoligonoRecorte.Count - 2].Y),
                    new Point((int)verticesPoligonoRecorte[verticesPoligonoRecorte.Count - 1].X,
                             (int)verticesPoligonoRecorte[verticesPoligonoRecorte.Count - 1].Y),
                    Color.Purple
                );
            }

            ShowNotification($"Vértice {verticesPoligonoRecorte.Count} agregado. Mínimo 3 vértices. Use 'Completar Polígono' cuando termine.");

            // Habilitar botón para completar polígono con al menos 3 vértices
            if (verticesPoligonoRecorte.Count >= 3)
            {
                btn_Draw.Enabled = true;
                btn_Draw.Text = "Completar Polígono";
            }

            ActualizarInstrucciones();
        }

        // ✅ NUEVO: Completa el polígono para recorte
        private void CompletarPoligonoRecorte()
        {
            if (verticesPoligonoRecorte.Count >= 3)
            {
                // ✅ NUEVO: Convertir a lista de PointF para UX Enhancer
                List<PointF> vertices = new List<PointF>();
                foreach (var vertice in verticesPoligonoRecorte)
                {
                    vertices.Add(new PointF((float)vertice.X, (float)vertice.Y));
                }

                // ✅ NUEVO: Agregar polígono al UX Enhancer para visualización
                uxEnhancer.AgregarPoligonoOriginal(vertices);

                // Crear polígono para algoritmo de recorte
                var poligono = new Cut_Algorithms.Polygon(verticesPoligonoRecorte);
                formasParaRecortar.Add(poligono);

                // Limpiar vértices temporales
                verticesPoligonoRecorte.Clear();

                // Habilitar botón de recorte
                btn_Paint.Enabled = true;
                btn_Draw.Text = "Nuevo Polígono";

                ShowNotification($"Polígono completado con {vertices.Count} vértices. Use 'Aplicar Recorte' o cree más polígonos.");
                ActualizarInstrucciones();
            }
        }

        // ✅ NUEVO: Limpia el canvas manteniendo el fondo blanco
        private void LimpiarCanvas()
        {
            if (pictureBox1.Image == null)
            {
                pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            }

            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                g.Clear(Color.White);
            }
            pictureBox1.Refresh();
        }


        // ✅ NUEVO: Dibuja una forma en el canvas
        private void DibujarForma(Cut_Algorithms.IShape forma, Color color)
        {
            if (pictureBox1.Image == null)
            {
                pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                {
                    g.Clear(Color.White);
                }
            }

            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                if (forma is Cut_Algorithms.Line linea)
                {
                    using (Pen pen = new Pen(color, 2))
                    {
                        g.DrawLine(pen, linea.Start.X, linea.Start.Y, linea.End.X, linea.End.Y);
                    }
                }
                else if (forma is Cut_Algorithms.Polygon poligono)
                {
                    var puntos = poligono.GetPoints().Select(p => new PointF(p.X, p.Y)).ToArray();
                    using (Pen pen = new Pen(color, 2))
                    {
                        if (puntos.Length > 2)
                        {
                            g.DrawPolygon(pen, puntos);
                        }
                        else if (puntos.Length == 2)
                        {
                            g.DrawLine(pen, puntos[0], puntos[1]);
                        }
                        else if (puntos.Length == 1)
                        {
                            g.FillEllipse(new SolidBrush(color), puntos[0].X - 2, puntos[0].Y - 2, 4, 4);
                        }
                    }
                }
            }

            pictureBox1.Refresh();
        }

        // ✅ MEJORADO: Dibuja la ventana de recorte con información adicional
        private void DibujarVentanaRecorte()
        {
            // ✅ NUEVO: El UX Enhancer ya maneja la visualización del área de recorte
            // Solo necesitamos refrescar la visualización
            uxEnhancer?.RefrescarVisualizacion();
            pictureBox1.Invalidate();
        }



        //  Maneja algoritmo de scan line
        private void ManejarScanLine(Point puntoMouse)
        {
            if (scanLineAlgoritmo == null)
            {
                // Iniciar creación de polígono
                scanLineAlgoritmo = new ScanLine(pictureBox1);
                scanLineAlgoritmo.IniciarCreacionPoligono();
                modoCreacionPoligono = true;
                poligonoListo = false;

                // Mostrar botón de dibujo
                btn_Draw.Visible = true;
                btn_Paint.Visible = false;

                ShowNotification("Modo creación: Haga clics para agregar vértices del polígono.");
            }
            else if (modoCreacionPoligono)
            {
                ShowNotification($"Vértice agregado. Total: {scanLineAlgoritmo.NumeroVertices + 1}. Use el botón 'Completar Polígono' cuando termine.");
            }
            else if (poligonoListo && scanLineAlgoritmo.TienePoligonoCompleto)
            {
                bool resultado = scanLineAlgoritmo.ProcesarClicScanLine(Color.LightBlue, 50);
                if (resultado)
                {
                    ShowNotification("🎨 ScanLine aplicado correctamente - Animación línea por línea");
                }
                else
                {
                    ShowNotification("❌ Error al aplicar ScanLine", false);
                }
            }
        }


        private void ManejarAlgoritmoCirculo(Point puntoCanvas)
        {
            if (primerPunto == null)
            {
                primerPunto = puntoCanvas;
                esperandoPunto = true;
                ShowNotification($"Centro: ({puntoCanvas.X}, {puntoCanvas.Y}). Haga clic para definir el radio.");
                DibujarPuntoTemporal(puntoCanvas, Color.Blue);
            }
            else
            {
                int radio = CalcularDistancia(primerPunto.Value, puntoCanvas);
                esperandoPunto = false;
                EjecutarAlgoritmoCirculo(primerPunto.Value, radio);
                primerPunto = null;
            }
        }

        private void ManejarAlgoritmoElipse(Point puntoCanvas)
        {
            if (primerPunto == null)
            {
                primerPunto = puntoCanvas;
                esperandoPunto = true;
                ShowNotification($"Centro: ({puntoCanvas.X}, {puntoCanvas.Y}). Haga clic para definir los semiejes.");
                DibujarPuntoTemporal(puntoCanvas, Color.Purple);
            }
            else
            {
                segundoPunto = puntoCanvas;
                esperandoPunto = false;
                EjecutarAlgoritmoElipse(primerPunto.Value, segundoPunto.Value);
                primerPunto = null;
                segundoPunto = null;
            }
        }

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

        private Point ConvertirCoordenadas(Point mousePoint)
        {
            int centroX = pictureBox1.Width / 2;
            int centroY = pictureBox1.Height / 2;
            int x = mousePoint.X - centroX;
            int y = centroY - mousePoint.Y;
            return new Point(x, y);
        }

        private int CalcularDistancia(Point p1, Point p2)
        {
            int dx = p2.X - p1.X;
            int dy = p2.Y - p1.Y;
            return (int)Math.Round(Math.Sqrt(dx * dx + dy * dy));
        }

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

            // Ocultar botones al inicio
            btn_Draw.Text = "Completar Polígono";
            btn_Draw.Visible = false;
            btn_Paint.Visible = false;
            btn_Paint.Enabled = false;

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

            // Resetear estado de clics y flood fill
            primerPunto = null;
            segundoPunto = null;
            esperandoPunto = false;
            floodFillAlgoritmo = null;
            modoCreacionPoligono = false;
            poligonoListo = false;

            // Resetear estado de algoritmos de recorte
            formasParaRecortar.Clear();
            puntoInicialLinea = null;
            verticesPoligonoRecorte.Clear();
            estadoRecorte = "";

            // Resetear estado de botones
            btn_Draw.Visible = false;
            btn_Paint.Visible = false;
            btn_Paint.Enabled = true;
        }

        // ✅ MEJORADO: Método para limpiar canvas automáticamente al cambiar categorías
        private void LimpiarCanvasAutomatico()
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image = null;
                pictureBox1.Invalidate();
            }

            // Resetear estados específicos sin afectar la selección de algoritmo
            primerPunto = null;
            segundoPunto = null;
            esperandoPunto = false;
            floodFillAlgoritmo = null;
            modoCreacionPoligono = false;
            poligonoListo = false;

            // Resetear estado de recorte
            formasParaRecortar.Clear();
            puntoInicialLinea = null;
            verticesPoligonoRecorte.Clear();
            estadoRecorte = "";

            // Resetear curvas
            administradorCurvas?.LimpiarCurvas();

            // ✅ MEJORADO: Limpiar visualización UX y elementos de recorte
            uxEnhancer?.LimpiarVisualizacion();
            uxEnhancer?.LimpiarElementosRecorte();
        }

        private void ActualizarOpcionesAlgoritmo(string categoria)
        {
            groupBox3.Controls.Clear();
            groupBox3.Controls.Add(btn_Draw);
            groupBox3.Controls.Add(btn_Paint);

            btn_Draw.Visible = false;
            btn_Paint.Visible = false;
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
                        ActualizarInstrucciones();

                        // Resetear estados
                        primerPunto = null;
                        segundoPunto = null;
                        esperandoPunto = false;
                        floodFillAlgoritmo = null;
                        modoCreacionPoligono = false;
                        poligonoListo = false;

                        // Inicializar curvas si es necesario
                        if (algoritmoSeleccionado == "Bézier")
                        {
                            administradorCurvas.IniciarCreacionCurva("Bézier");
                        }
                        else if (algoritmoSeleccionado == "B-spline")
                        {
                            administradorCurvas.IniciarCreacionCurva("B-spline");
                        }

                    }
                };

                groupBox3.Controls.Add(rb);
                y += 35;
            }

            groupBox3.Visible = true;
        }

        // ✅ MODIFICADO: Instrucciones mejoradas para Flood Fill
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
                                   "2. Use el botón '🔒 Completar Polígono'\r\n" +
                                   "3. Haga clic DENTRO para rellenar\r\n\r\n" +
                                   "💡 El botón aparecerá automáticamente";
                    break;
                // Dentro del método ActualizarInstrucciones():

                case "Cohen–Sutherland (líneas)":
                    if (estadoRecorte == "")
                        instrucciones = "✂️ Cohen-Sutherland - Recorte de Líneas\r\n\r\n" +
                                       "📋 Algoritmo para recortar líneas contra ventana rectangular\r\n\r\n" +
                                       "🎯 Funcionamiento:\r\n" +
                                       "• Ventana de recorte automática en el centro\r\n" +
                                       "• Clasifica extremos de líneas por región\r\n" +
                                       "• Calcula intersecciones con bordes\r\n" +
                                       "• Muestra solo segmentos visibles\r\n\r\n" +
                                       "💡 Haga clic para iniciar y crear líneas";
                    else if (estadoRecorte == "crear_lineas")
                        instrucciones = "✂️ Creando Líneas - Cohen-Sutherland\r\n\r\n" +
                                       "📋 Estado actual:\r\n" +
                                       "� Ventana de recorte: Centrada automáticamente\r\n" +
                                       "�📏 Haga clic en pares de puntos para crear líneas\r\n" +
                                       $"📊 Líneas creadas: {formasParaRecortar.Count}\r\n\r\n" +
                                       "✨ Use 'Aplicar Recorte' para ver el resultado\r\n" +
                                       "💡 Solo las partes dentro de la ventana roja se conservan";
                    break;

                case "Sutherland–Hodgman (polígonos)":
                    if (estadoRecorte == "")
                        instrucciones = "✂️ Sutherland-Hodgman - Recorte de Polígonos\r\n\r\n" +
                                       "📋 Algoritmo para recortar polígonos contra ventana rectangular\r\n\r\n" +
                                       "🎯 Funcionamiento:\r\n" +
                                       "• Ventana de recorte automática en el centro\r\n" +
                                       "• Recorta secuencialmente contra cada borde\r\n" +
                                       "• Conserva la forma del polígono resultante\r\n" +
                                       "• Maneja intersecciones complejas\r\n\r\n" +
                                       "💡 Haga clic para iniciar y crear polígonos";
                    else if (estadoRecorte == "crear_poligonos")
                        instrucciones = "✂️ Creando Polígonos - Sutherland-Hodgman\r\n\r\n" +
                                       "📋 Estado actual:\r\n" +
                                       "🔲 Ventana de recorte: Centrada automáticamente\r\n" +
                                       "🔺 Haga clics para agregar vértices del polígono\r\n" +
                                       $"📊 Vértices actuales: {verticesPoligonoRecorte.Count}\r\n" +
                                       $"📊 Polígonos completados: {formasParaRecortar.Count}\r\n\r\n" +
                                       "✨ Use 'Completar Forma' para finalizar polígono\r\n" +
                                       "💡 Mínimo 3 vértices por polígono";
                    break;

                case "Bézier":
                    instrucciones = "📈 Curva de Bézier Cúbica\r\n\r\n" +
                                   "📋 Curva suave definida por 4 puntos de control\r\n\r\n" +
                                   "🎯 Características:\r\n" +
                                   "• P₀: Punto de inicio de la curva\r\n" +
                                   "• P₁: Primer punto de control (tangente inicial)\r\n" +
                                   "• P₂: Segundo punto de control (tangente final)\r\n" +
                                   "• P₃: Punto final de la curva\r\n\r\n" +
                                   $"📊 Puntos agregados: {(administradorCurvas?.ObtenerInformacionEstado().Split('-').LastOrDefault()?.Trim() ?? "0")}\r\n" +
                                   "💡 Se completa automáticamente con 4 puntos";
                    break;

                case "B-spline":
                    instrucciones = "📈 Curva B-Spline Cúbica\r\n\r\n" +
                                   "📋 Curva suave que pasa cerca de los puntos de control\r\n\r\n" +
                                   "🎯 Características:\r\n" +
                                   "• Continuidad suave entre segmentos\r\n" +
                                   "• Control local (modificar un punto afecta localmente)\r\n" +
                                   "• Interpolación aproximada de puntos\r\n" +
                                   "• Acepta cualquier número de puntos (≥4)\r\n\r\n" +
                                   $"📊 {administradorCurvas?.ObtenerInformacionEstado() ?? "Puntos: 0"}\r\n" +
                                   "💡 Use 'Completar Curva' para finalizar (mínimo 4 puntos)";
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
            LimpiarCanvasAutomatico(); // ✅ NUEVO: Limpiar al cambiar categoría

            // ✅ NUEVO: Configurar UX para rasterización
            uxEnhancer.ConfigurarCategoria("Rasterizado");

            textBox1.Text = "🎯 Rasterización de Primitivas\r\n\r\n" +
                           "📊 Plano cartesiano visible para precisión\r\n" +
                           "🔍 Los puntos de click se muestran temporalmente\r\n" +
                           "📏 Coordenadas y cuadrícula de referencia\r\n\r\n" +
                           "Algoritmos disponibles para dibujar líneas, círculos y elipses.\r\n" +
                           "Seleccione un algoritmo específico y dibuje en el área blanca.";
            ActualizarOpcionesAlgoritmo("Rasterizado");
        }

        private void btn_FillAlg_Click(object sender, EventArgs e)
        {
            LimpiarCanvasAutomatico(); // ✅ NUEVO: Limpiar al cambiar categoría

            // ✅ NUEVO: Configurar UX para relleno
            uxEnhancer.ConfigurarCategoria("Relleno");

            textBox1.Text = "🎨 Algoritmos de Relleno\r\n\r\n" +
                           "🔍 Los puntos de click se muestran temporalmente\r\n" +
                           "📍 Visualización clara de vértices del polígono\r\n" +
                           "🎯 Área limpia para crear figuras\r\n\r\n" +
                           "Técnicas para rellenar regiones cerradas con colores o patrones.\r\n" +
                           "Seleccione un algoritmo y haga clic en una región para rellenar.";
            ActualizarOpcionesAlgoritmo("Relleno");
            groupBox3.Visible = true;
            btn_Draw.Visible = true;
            btn_Draw.Enabled = true;
            btn_Draw.Text = "Completar Polígono";
        }

        private void btn_CutAlg_Click(object sender, EventArgs e)
        {
            LimpiarCanvasAutomatico(); // ✅ NUEVO: Limpiar al cambiar categoría

            // ✅ NUEVO: Configurar UX para recorte
            uxEnhancer.ConfigurarCategoria("Recorte");

            textBox1.Text = "✂️ Algoritmos de Recorte\r\n\r\n" +
                           "🟢 Área de recorte visible automáticamente\r\n" +
                           "🔢 Códigos Cohen-Sutherland mostrados\r\n" +
                           "🎯 Ventana centrada al 40% del canvas\r\n" +
                           "📍 Puntos de click visibles temporalmente\r\n\r\n" +
                           "Técnicas avanzadas para recortar primitivas gráficas contra ventanas.\r\n" +
                           "Ventana de recorte automática centrada. Útil para optimización y culling.";
            ActualizarOpcionesAlgoritmo("Recorte");

            // ✅ MEJORADO: Configuración simplificada para recorte
            groupBox3.Visible = true;
            btn_Draw.Visible = true;
            btn_Draw.Enabled = true;
            btn_Draw.Text = "Iniciar Algoritmo";
            btn_Paint.Visible = true;
            btn_Paint.Enabled = false;
            btn_Paint.Text = "Aplicar Recorte";
        }

        private void btn_CurvAlg_Click(object sender, EventArgs e)
        {
            LimpiarCanvasAutomatico(); // ✅ NUEVO: Limpiar al cambiar categoría
            administradorCurvas.LimpiarCurvas(); // ✅ LIMPIAR CURVAS

            // ✅ NUEVO: Configurar UX para curvas
            uxEnhancer.ConfigurarCategoria("Curvas");

            textBox1.Text = "📈 Curvas Paramétricas\r\n\r\n" +
                           "📊 Plano cartesiano visible para precisión\r\n" +
                           "🔍 Los puntos de click se muestran temporalmente\r\n" +
                           "📏 Coordenadas y cuadrícula de referencia\r\n" +
                           "🎯 Puntos de control visibles\r\n\r\n" +
                           "Algoritmos avanzados para generar curvas suaves y complejas.\r\n" +
                           "Bézier: 4 puntos exactos. B-Spline: mínimo 4 puntos, máximo ilimitado.";
            ActualizarOpcionesAlgoritmo("Curvas");

            // ✅ CONFIGURAR BOTONES PARA CURVAS
            groupBox3.Visible = true;
            btn_Draw.Visible = true;
            btn_Draw.Enabled = true;
            btn_Draw.Text = "Completar Curva";
            btn_Paint.Visible = true;
            btn_Paint.Enabled = true;
            btn_Paint.Text = "Alternar Puntos";
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            ResetCanvas();
        }


        private void btn_Paint_Click(object sender, EventArgs e)
        {
            if (algoritmoSeleccionado == "Flood Fill")
            {
                if (poligonoListo && floodFillAlgoritmo != null && floodFillAlgoritmo.TienePoligonoCompleto)
                {
                    ShowNotification("Modo pintado activado. Haga clic dentro del polígono para rellenar.");
                }
            }
            else if (algoritmoSeleccionado == "Scanline")
            {
                if (poligonoListo && scanLineAlgoritmo != null && scanLineAlgoritmo.TienePoligonoCompleto)
                {
                    bool resultado = scanLineAlgoritmo.ProcesarClicScanLine(Color.LightBlue, 50);
                    if (resultado)
                    {
                        ShowNotification("🎨 ScanLine aplicado correctamente - Animación línea por línea");
                    }
                }
            }
            else if (algoritmoSeleccionado == "Cohen–Sutherland (líneas)") // ✅ NUEVO
            {
                EjecutarRecorteCohenSutherland();
            }
            else if (algoritmoSeleccionado == "Sutherland–Hodgman (polígonos)") // ✅ NUEVO
            {
                EjecutarRecorteSutherlandHodgman();
            }
            else if (algoritmoSeleccionado == "Bézier" || algoritmoSeleccionado == "B-spline")
            {
                // Alternar visualización de puntos de control
                administradorCurvas.AlternarPuntosControl();
                ShowNotification("Visualización de puntos de control alternada.");
            }
            else
            {
                ShowNotification("Complete la figura primero usando el botón correspondiente", false);
            }
        }





        private void btn_Draw_Click(object sender, EventArgs e)
        {
            if (algoritmoSeleccionado == "Flood Fill")
            {
                if (floodFillAlgoritmo != null && modoCreacionPoligono)
                {
                    floodFillAlgoritmo.CompletarPoligono();
                    modoCreacionPoligono = false;
                    poligonoListo = true;

                    btn_Draw.Visible = false;
                    btn_Paint.Visible = true;

                    ShowNotification("Polígono completado. Ahora use el botón 'Pintar' para rellenar.");
                }
            }
            else if (algoritmoSeleccionado == "Scanline")
            {
                if (scanLineAlgoritmo != null && modoCreacionPoligono)
                {
                    scanLineAlgoritmo.CompletarPoligono();
                    modoCreacionPoligono = false;
                    poligonoListo = true;

                    btn_Draw.Visible = false;
                    btn_Paint.Visible = true;

                    ShowNotification("Polígono completado. Ahora use el botón 'Pintar' para rellenar.");
                }
            }
            else if (algoritmoSeleccionado == "Cohen–Sutherland (líneas)")
            {
                if (estadoRecorte == "")
                {
                    // Iniciar proceso directamente
                    estadoRecorte = "crear_lineas";
                    CalcularVentanaRecorteAutomatica();
                    btn_Draw.Text = "Listo para Recortar";
                    ShowNotification("Ventana automática creada. Ahora cree líneas haciendo clic en pares de puntos.");
                    ActualizarInstrucciones();
                }
                else if (estadoRecorte == "crear_lineas")
                {
                    // Cambiar a modo de finalización
                    btn_Draw.Text = "Iniciar Algoritmo";
                    estadoRecorte = "";
                    ShowNotification("Puede continuar creando líneas o usar 'Aplicar Recorte'.");
                }
            }
            else if (algoritmoSeleccionado == "Sutherland–Hodgman (polígonos)")
            {
                if (estadoRecorte == "")
                {
                    // Iniciar proceso directamente
                    estadoRecorte = "crear_poligonos";
                    CalcularVentanaRecorteAutomatica();
                    btn_Draw.Text = "Completar Polígono";
                    ShowNotification("Ventana automática creada. Ahora cree polígonos haciendo clic en vértices.");
                    ActualizarInstrucciones();
                }
                else if (estadoRecorte == "crear_poligonos")
                {
                    // ✅ NUEVO: Completar polígono actual
                    CompletarPoligonoRecorte();
                }

                // Habilitar recorte
                btn_Paint.Enabled = true;
                ShowNotification("Líneas creadas. Use 'Aplicar Recorte' para ver el resultado del algoritmo Cohen-Sutherland.");
            }


            else if (algoritmoSeleccionado == "Sutherland–Hodgman (polígonos)")
            {
                if (estadoRecorte == "")
                {
                    // Iniciar proceso directamente
                    estadoRecorte = "crear_poligonos";
                    CalcularVentanaRecorteAutomatica();
                    DibujarVentanaRecorte();
                    btn_Draw.Text = "Completar Forma";
                    btn_Paint.Text = "Aplicar Recorte";
                    ShowNotification("Ventana automática creada. Cree polígonos haciendo clic en vértices.");
                    ActualizarInstrucciones();
                }
                else if (estadoRecorte == "crear_poligonos")
                {
                    // Completar polígono actual
                    CompletarPoligonoRecorte();
                    ActualizarInstrucciones();
                }
            }
            else if (algoritmoSeleccionado == "Bézier")
            {
                // Las curvas de Bézier se completan automáticamente
                ShowNotification("Las curvas de Bézier se completan automáticamente con 4 puntos de control.");
            }
            else if (algoritmoSeleccionado == "B-spline")
            {
                // Completar curva B-Spline
                administradorCurvas.CompletarCurva();
                ShowNotification("Curva B-Spline completada. Puede crear una nueva curva.");
                ActualizarInstrucciones();
            }
        }

        // ✅ MEJORADO: Ejecuta el recorte Cohen-Sutherland con mejor visualización
        private void EjecutarRecorteCohenSutherland()
        {
            if (formasParaRecortar.Count == 0)
            {
                ShowNotification("No hay líneas para recortar. Cree algunas líneas primero.", false);
                return;
            }

            try
            {
                LimpiarCanvas();
                CalcularVentanaRecorteAutomatica();

                // Redibujar área de recorte
                uxEnhancer.RefrescarVisualizacion();

                int lineasRecortadas = 0;
                foreach (var forma in formasParaRecortar)
                {
                    if (forma is Cut_Algorithms.Line linea)
                    {
                        try
                        {
                            var lineaRecortada = cohenSutherlandClipper.ClipShape(linea, ventanaRecorte);
                            if (lineaRecortada != null)
                            {
                                // Dibujar parte recortada en verde brillante
                                DibujarForma(lineaRecortada, Color.LimeGreen);
                                lineasRecortadas++;
                            }
                        }
                        catch (Exception ex)
                        {
                            ShowNotification($"Error al recortar línea: {ex.Message}", false);
                        }
                    }
                }

                ShowNotification($"✂️ Cohen-Sutherland completado: {lineasRecortadas} líneas recortadas de {formasParaRecortar.Count}");
            }
            catch (Exception ex)
            {
                ShowNotification($"Error en Cohen-Sutherland: {ex.Message}", false);
            }
        }

        // ✅ MEJORADO: Ejecuta el recorte Sutherland-Hodgman con mejor visualización
        private void EjecutarRecorteSutherlandHodgman()
        {
            if (formasParaRecortar.Count == 0)
            {
                ShowNotification("No hay polígonos para recortar. Cree algunos polígonos primero.", false);
                return;
            }

            try
            {
                LimpiarCanvas();
                CalcularVentanaRecorteAutomatica();

                // Redibujar área de recorte
                uxEnhancer.RefrescarVisualizacion();

                int poligonosRecortados = 0;
                foreach (var forma in formasParaRecortar)
                {
                    if (forma is Cut_Algorithms.Polygon poligono)
                    {
                        try
                        {
                            var poligonoRecortado = sutherlandHodgmanClipper.ClipShape(poligono, ventanaRecorte);
                            if (poligonoRecortado != null)
                            {
                                // Dibujar parte recortada en verde brillante
                                DibujarForma(poligonoRecortado, Color.LimeGreen);
                                poligonosRecortados++;
                            }
                        }
                        catch (Exception ex)
                        {
                            ShowNotification($"Error al recortar polígono: {ex.Message}", false);
                        }
                    }
                }

                ShowNotification($"✂️ Sutherland-Hodgman completado: {poligonosRecortados} polígonos recortados de {formasParaRecortar.Count}");
            }
            catch (Exception ex)
            {
                ShowNotification($"Error en Sutherland-Hodgman: {ex.Message}", false);
            }
        }

        // ✅ NUEVO: Dibuja una línea temporal
        private void DibujarLineaTemporal(Point p1, Point p2, Color color)
        {
            if (pictureBox1.Image == null)
            {
                pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                {
                    g.Clear(Color.White);
                }
            }

            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                using (Pen pen = new Pen(color, 1))
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    g.DrawLine(pen, p1, p2);
                }
            }
            pictureBox1.Refresh();
        }

        // ✅ NUEVO: Maneja la creación de curvas de Bézier
        private void ManejarCurvaBezier(Point puntoMouse)
        {
            if (administradorCurvas != null)
            {
                administradorCurvas.IniciarCreacionCurva("Bézier");
                administradorCurvas.AgregarPuntoControl(puntoMouse);
                ShowNotification($"Punto de control agregado. Bézier necesita exactamente 4 puntos.");
                ActualizarInstrucciones();
            }
        }

        // ✅ NUEVO: Maneja la creación de curvas B-Spline
        private void ManejarCurvaBSpline(Point puntoMouse)
        {
            if (administradorCurvas != null)
            {
                administradorCurvas.IniciarCreacionCurva("B-Spline");
                administradorCurvas.AgregarPuntoControl(puntoMouse);
                ShowNotification($"Punto de control agregado. B-Spline necesita mínimo 4 puntos.");
                ActualizarInstrucciones();
            }
        }

        /// <summary>
        /// ✅ NUEVO: Limpia recursos al cerrar el formulario
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // ✅ NUEVO: Liberar recursos del UX Enhancer
            uxEnhancer?.Dispose();

            base.OnFormClosing(e);
        }


    } }