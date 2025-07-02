using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Cut_Algorithms
{
    /// <summary>
    /// Administra las formas geométricas y coordina el recorte y visualización.
    /// </summary>
    public class ClippingCanvas
    {
        /// <summary>
        /// Colección de formas geométricas.
        /// </summary>
        private List<IShape> shapes = new List<IShape>();

        /// <summary>
        /// Límite de recorte.
        /// </summary>
        private ClipBoundary clipBoundary;

        /// <summary>
        /// Algoritmo de recorte para líneas.
        /// </summary>
        private CohenSutherlandClipper lineClipper = new CohenSutherlandClipper();

        /// <summary>
        /// Algoritmo de recorte para polígonos.
        /// </summary>
        private SutherlandHodgmanClipper polygonClipper = new SutherlandHodgmanClipper();

        /// <summary>
        /// Control PictureBox asociado al canvas.
        /// </summary>
        private PictureBox pictureBox;

        /// <summary>
        /// Indica si se deben mostrar las formas originales o solo las recortadas.
        /// </summary>
        public bool ShowOriginalShapes { get; set; } = true;

        /// <summary>
        /// Indica si se debe mostrar el límite de recorte.
        /// </summary>
        public bool ShowClipBoundary { get; set; } = true;

        /// <summary>
        /// Obtiene o establece el color del fondo.
        /// </summary>
        public Color BackgroundColor { get; set; } = Color.White;

        /// <summary>
        /// Inicializa una nueva instancia de la clase ClippingCanvas.
        /// </summary>
        /// <param name="pictureBox">Control PictureBox que se utilizará para dibujar.</param>
        /// <param name="clipBoundary">Límite de recorte inicial.</param>
        public ClippingCanvas(PictureBox pictureBox, ClipBoundary clipBoundary = null)
        {
            this.pictureBox = pictureBox ?? throw new ArgumentNullException(nameof(pictureBox));

            // Si no se proporciona un límite de recorte, crear uno basado en el PictureBox
            this.clipBoundary = clipBoundary ??
                new ClipBoundary(
                    50, 50,
                    pictureBox.Width - 50,
                    pictureBox.Height - 50
                );

            // Configurar eventos
            ConfigureEvents();
        }

        /// <summary>
        /// Configura los eventos necesarios para el canvas.
        /// </summary>
        private void ConfigureEvents()
        {
            // Configurar el evento Paint del PictureBox
            pictureBox.Paint += (sender, e) => Draw(e.Graphics);

            // Asegurar que el PictureBox se redibuje cuando cambie de tamaño
            pictureBox.Resize += (sender, e) => pictureBox.Invalidate();
        }

        /// <summary>
        /// Establece un nuevo límite de recorte.
        /// </summary>
        /// <param name="clipBoundary">El nuevo límite de recorte.</param>
        public void SetClipBoundary(ClipBoundary clipBoundary)
        {
            this.clipBoundary = clipBoundary ?? throw new ArgumentNullException(nameof(clipBoundary));
            pictureBox.Invalidate(); // Volver a dibujar con el nuevo límite
        }

        /// <summary>
        /// Añade una forma a la colección de formas.
        /// </summary>
        /// <param name="shape">La forma a añadir.</param>
        public void AddShape(IShape shape)
        {
            if (shape == null) throw new ArgumentNullException(nameof(shape));
            shapes.Add(shape);
            pictureBox.Invalidate(); // Volver a dibujar con la nueva forma
        }

        /// <summary>
        /// Añade un punto a la colección de formas.
        /// </summary>
        /// <param name="x">Coordenada X del punto.</param>
        /// <param name="y">Coordenada Y del punto.</param>
        public void AddPoint(float x, float y)
        {
            AddShape(new Point2D(x, y));
        }

        /// <summary>
        /// Añade una línea a la colección de formas.
        /// </summary>
        /// <param name="x1">Coordenada X del punto inicial.</param>
        /// <param name="y1">Coordenada Y del punto inicial.</param>
        /// <param name="x2">Coordenada X del punto final.</param>
        /// <param name="y2">Coordenada Y del punto final.</param>
        public void AddLine(float x1, float y1, float x2, float y2)
        {
            AddShape(new Line(x1, y1, x2, y2));
        }

        /// <summary>
        /// Añade un polígono a la colección de formas.
        /// </summary>
        /// <param name="vertices">Lista de vértices del polígono.</param>
        public void AddPolygon(IEnumerable<Point2D> vertices)
        {
            if (vertices == null) throw new ArgumentNullException(nameof(vertices));
            AddShape(new Polygon(vertices));
        }

        /// <summary>
        /// Limpia todas las formas del canvas.
        /// </summary>
        public void Clear()
        {
            shapes.Clear();
            pictureBox.Invalidate(); // Volver a dibujar el canvas vacío
        }

        /// <summary>
        /// Dibuja todas las formas y el límite de recorte en el contexto gráfico proporcionado.
        /// </summary>
        /// <param name="graphics">El contexto gráfico donde dibujar.</param>
        public void Draw(Graphics graphics)
        {
            if (graphics == null) throw new ArgumentNullException(nameof(graphics));

            // Limpiar el canvas con el color de fondo
            graphics.Clear(BackgroundColor);

            // Dibujar las formas originales si está habilitado
            if (ShowOriginalShapes)
            {
                foreach (var shape in shapes)
                {
                    shape.Draw(graphics);
                }
            }

            // Dibujar el límite de recorte si está habilitado
            if (ShowClipBoundary && clipBoundary != null)
            {
                clipBoundary.Draw(graphics);
            }

            // Dibujar las formas recortadas
            DrawClippedShapes(graphics);
        }

        /// <summary>
        /// Dibuja las formas recortadas según el límite de recorte.
        /// </summary>
        /// <param name="graphics">El contexto gráfico donde dibujar.</param>
        private void DrawClippedShapes(Graphics graphics)
        {
            if (clipBoundary == null) return;

            RectangleF clipRect = clipBoundary.ToRectangle();

            foreach (var shape in shapes)
            {
                IShape clippedShape = null;

                // Aplicar el algoritmo de recorte adecuado según el tipo de forma
                if (shape is Line)
                {
                    if (lineClipper.NeedsClipping(shape, clipRect))
                    {
                        clippedShape = lineClipper.ClipShape(shape, clipRect);
                    }
                }
                else if (shape is Polygon)
                {
                    if (polygonClipper.NeedsClipping(shape, clipRect))
                    {
                        clippedShape = polygonClipper.ClipShape(shape, clipRect);
                    }
                }
                else if (shape is Point2D && clipBoundary.Contains(((Point2D)shape).ToPointF()))
                {
                    // Los puntos se muestran si están dentro del límite
                    clippedShape = shape;
                }

                // Dibujar la forma recortada con un color diferente
                if (clippedShape != null)
                {
                    // Eliminar el uso incorrecto de Graphics.Save()
                    if (clippedShape is Line)
                    {
                        Line line = (Line)clippedShape;
                        using (Pen pen = new Pen(Color.Red, 1))
                        {
                            graphics.DrawLine(pen, line.Start.X, line.Start.Y, line.End.X, line.End.Y);
                        }
                    }
                    else if (clippedShape is Polygon)
                    {
                        Polygon poly = (Polygon)clippedShape;
                        PointF[] points = poly.GetPoints().ToArray();
                        if (points.Length >= 3)
                        {
                            using (Pen pen = new Pen(Color.Red, 1))
                            {
                                graphics.DrawPolygon(pen, points);
                            }
                        }
                    }
                    else
                    {
                        clippedShape.Draw(graphics);
                    }
                }
            }
        }

        /// <summary>
        /// Aplica el recorte a todas las formas del canvas.
        /// </summary>
        /// <param name="algorithm">Algoritmo de recorte a utilizar: 0 para Cohen-Sutherland, 1 para Sutherland-Hodgman.</param>
        public void ApplyClipping(int algorithm)
        {
            // Simplemente invalidar el PictureBox para que se redibuje aplicando el recorte
            pictureBox.Invalidate();
        }

        /// <summary>
        /// Configura el canvas para capturar los clics y crear formas.
        /// </summary>
        /// <param name="shapeType">Tipo de forma a crear: 0 para punto, 1 para línea, 2 para polígono.</param>
        public void SetDrawingMode(int shapeType)
        {
            // Implementar la lógica para capturar clics y crear formas
            // Esta implementación dependería de cómo se desea manejar la interacción del usuario

            // Por ejemplo, para dibujar líneas:
            Point? startPoint = null;

            pictureBox.MouseDown += (sender, e) =>
            {
                if (!startPoint.HasValue)
                {
                    startPoint = e.Location;
                }
                else
                {
                    // Crear una línea desde el punto inicial hasta el punto actual
                    AddLine(startPoint.Value.X, startPoint.Value.Y, e.Location.X, e.Location.Y);
                    startPoint = null;
                }
            };
        }
    }
}

