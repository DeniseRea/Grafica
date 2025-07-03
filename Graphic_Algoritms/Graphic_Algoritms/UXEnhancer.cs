using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Graphic_Algoritms
{
    /// <summary>
    /// Clase para manejar las mejoras de experiencia de usuario (UX)
    /// Incluye visualizaciones específicas para cada categoría de algoritmo
    /// </summary>
    public class UXEnhancer
    {
        private PictureBox pictureBox;
        private Dictionary<Point, DateTime> puntosClicados;
        private Timer timerPuntos;
        private bool mostrarPlano;
        private bool mostrarAreaRecorte;
        private RectangleF areaRecorte;
        private string categoriaActual;
        
        // ✅ NUEVO: Para evitar parpadeo
        private Bitmap imagenBase;
        private bool necesitaRedibujo;
        
        // ✅ NUEVO: Para visualizar elementos de recorte
        private List<LineF> lineasOriginales;
        private List<List<PointF>> poligonosOriginales;

        // Colores para diferentes elementos visuales
        private readonly Color colorPlano = Color.FromArgb(15, 200, 200, 200);
        private readonly Color colorEjes = Color.FromArgb(80, 100, 100, 100);
        private readonly Color colorPuntoClick = Color.FromArgb(200, 255, 0, 0);
        private readonly Color colorAreaRecorte = Color.FromArgb(40, 0, 255, 0);
        private readonly Color colorBordeRecorte = Color.FromArgb(120, 0, 200, 0);
        private readonly Color colorLineaOriginal = Color.FromArgb(150, 0, 0, 255);
        private readonly Color colorPoligonoOriginal = Color.FromArgb(150, 128, 0, 128);

        public UXEnhancer(PictureBox pictureBox)
        {
            this.pictureBox = pictureBox;
            this.puntosClicados = new Dictionary<Point, DateTime>();
            this.lineasOriginales = new List<LineF>();
            this.poligonosOriginales = new List<List<PointF>>();
            this.necesitaRedibujo = true;
            
            // Timer para desvanecer puntos de click
            timerPuntos = new Timer();
            timerPuntos.Interval = 150; // Reducir frecuencia para evitar parpadeo
            timerPuntos.Tick += TimerPuntos_Tick;
            timerPuntos.Start();
        }

        /// <summary>
        /// Estructura para representar una línea
        /// </summary>
        public struct LineF
        {
            public PointF Start;
            public PointF End;
            public Color Color;
            
            public LineF(PointF start, PointF end, Color color)
            {
                Start = start;
                End = end;
                Color = color;
            }
        }

        /// <summary>
        /// Configura la visualización según la categoría de algoritmo seleccionada
        /// </summary>
        /// <param name="categoria">Categoría del algoritmo (Rasterizado, Relleno, Recorte, Curvas)</param>
        public void ConfigurarCategoria(string categoria)
        {
            categoriaActual = categoria;
            necesitaRedibujo = true;
            
            // Limpiar elementos de recorte al cambiar categoría
            if (categoria != "Recorte")
            {
                lineasOriginales.Clear();
                poligonosOriginales.Clear();
            }
            
            // Asegurar que tenemos una imagen en el PictureBox
            if (pictureBox.Image == null)
            {
                pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
            }
            
            // Crear imagen base si no existe
            if (imagenBase == null || imagenBase.Width != pictureBox.Width || imagenBase.Height != pictureBox.Height)
            {
                imagenBase?.Dispose();
                imagenBase = new Bitmap(pictureBox.Width, pictureBox.Height);
            }
            
            switch (categoria)
            {
                case "Rasterizado":
                    mostrarPlano = true;
                    mostrarAreaRecorte = false;
                    DibujarPlanoCartesiano();
                    break;
                    
                case "Relleno":
                    mostrarPlano = false;
                    mostrarAreaRecorte = false;
                    LimpiarImagenBase();
                    break;
                    
                case "Recorte":
                    mostrarPlano = false;
                    mostrarAreaRecorte = true;
                    CalcularAreaRecorte();
                    DibujarAreaRecorte();
                    break;
                    
                case "Curvas":
                    mostrarPlano = true;
                    mostrarAreaRecorte = false;
                    DibujarPlanoCartesiano();
                    break;
            }
            
            ActualizarVisualizacion();
        }

        /// <summary>
        /// Agrega una línea original para visualización en recorte
        /// </summary>
        /// <param name="start">Punto inicial</param>
        /// <param name="end">Punto final</param>
        public void AgregarLineaOriginal(PointF start, PointF end)
        {
            lineasOriginales.Add(new LineF(start, end, colorLineaOriginal));
            if (categoriaActual == "Recorte")
            {
                ActualizarVisualizacion();
            }
        }

        /// <summary>
        /// Agrega un polígono original para visualización en recorte
        /// </summary>
        /// <param name="vertices">Vértices del polígono</param>
        public void AgregarPoligonoOriginal(List<PointF> vertices)
        {
            if (vertices.Count >= 3)
            {
                poligonosOriginales.Add(new List<PointF>(vertices));
                if (categoriaActual == "Recorte")
                {
                    ActualizarVisualizacion();
                }
            }
        }

        /// <summary>
        /// Limpia las líneas y polígonos originales
        /// </summary>
        public void LimpiarElementosRecorte()
        {
            lineasOriginales.Clear();
            poligonosOriginales.Clear();
            if (categoriaActual == "Recorte")
            {
                ActualizarVisualizacion();
            }
        }

        /// <summary>
        /// Registra un punto donde el usuario hizo click
        /// </summary>
        /// <param name="punto">Punto donde se hizo click</param>
        public void RegistrarClick(Point punto)
        {
            puntosClicados[punto] = DateTime.Now;
            ActualizarVisualizacion();
        }

        /// <summary>
        /// Limpia la imagen base
        /// </summary>
        private void LimpiarImagenBase()
        {
            if (imagenBase != null)
            {
                using (Graphics g = Graphics.FromImage(imagenBase))
                {
                    g.Clear(Color.White);
                }
                necesitaRedibujo = true;
            }
        }

        /// <summary>
        /// Dibuja el plano cartesiano para rasterización y curvas
        /// </summary>
        private void DibujarPlanoCartesiano()
        {
            if (imagenBase == null) return;

            using (Graphics g = Graphics.FromImage(imagenBase))
            {
                g.Clear(Color.White);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                int width = imagenBase.Width;
                int height = imagenBase.Height;
                int centerX = width / 2;
                int centerY = height / 2;

                // Dibujar fondo de plano suave
                using (Brush brush = new SolidBrush(colorPlano))
                {
                    g.FillRectangle(brush, 0, 0, width, height);
                }

                // Dibujar líneas de cuadrícula más sutiles
                using (Pen penCuadricula = new Pen(Color.FromArgb(30, 150, 150, 150), 1))
                {
                    penCuadricula.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    
                    // Líneas verticales cada 20 píxeles
                    for (int x = 0; x <= width; x += 20)
                    {
                        g.DrawLine(penCuadricula, x, 0, x, height);
                    }
                    
                    // Líneas horizontales cada 20 píxeles
                    for (int y = 0; y <= height; y += 20)
                    {
                        g.DrawLine(penCuadricula, 0, y, width, y);
                    }
                }

                // Dibujar ejes principales
                using (Pen penEjes = new Pen(colorEjes, 2))
                {
                    // Eje X
                    g.DrawLine(penEjes, 0, centerY, width, centerY);
                    // Eje Y
                    g.DrawLine(penEjes, centerX, 0, centerX, height);
                }

                // Dibujar marcadores en los ejes más sutiles
                using (Pen penMarcadores = new Pen(Color.FromArgb(60, 80, 80, 80), 1))
                using (Font font = new Font("Arial", 7))
                using (Brush brushTexto = new SolidBrush(Color.FromArgb(80, 60, 60, 60)))
                {
                    // Marcadores en X cada 40 píxeles
                    for (int x = 40; x <= width; x += 40)
                    {
                        if (Math.Abs(x - centerX) > 20) // Evitar el centro
                        {
                            g.DrawLine(penMarcadores, x, centerY - 3, x, centerY + 3);
                            string texto = (x - centerX).ToString();
                            SizeF tamaño = g.MeasureString(texto, font);
                            g.DrawString(texto, font, brushTexto, x - tamaño.Width / 2, centerY + 5);
                        }
                    }
                    
                    // Marcadores en Y cada 40 píxeles
                    for (int y = 40; y <= height; y += 40)
                    {
                        if (Math.Abs(y - centerY) > 20) // Evitar el centro
                        {
                            g.DrawLine(penMarcadores, centerX - 3, y, centerX + 3, y);
                            string texto = (centerY - y).ToString();
                            SizeF tamaño = g.MeasureString(texto, font);
                            g.DrawString(texto, font, brushTexto, centerX - tamaño.Width - 5, y - tamaño.Height / 2);
                        }
                    }
                    
                    // Origen
                    g.DrawString("(0,0)", font, brushTexto, centerX + 3, centerY + 3);
                }
            }
            
            necesitaRedibujo = true;
        }

        /// <summary>
        /// Calcula el área de recorte automáticamente
        /// </summary>
        private void CalcularAreaRecorte()
        {
            if (imagenBase == null) return;
            
            int width = imagenBase.Width;
            int height = imagenBase.Height;
            int centerX = width / 2;
            int centerY = height / 2;
            
            // Área de recorte centrada (40% del tamaño total)
            int areaWidth = (int)(width * 0.4);
            int areaHeight = (int)(height * 0.4);
            int areaX = centerX - areaWidth / 2;
            int areaY = centerY - areaHeight / 2;
            
            areaRecorte = new RectangleF(areaX, areaY, areaWidth, areaHeight);
        }

        /// <summary>
        /// Dibuja el área de recorte para algoritmos de clipping
        /// </summary>
        private void DibujarAreaRecorte()
        {
            if (imagenBase == null) return;

            using (Graphics g = Graphics.FromImage(imagenBase))
            {
                g.Clear(Color.White);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // Dibujar área de recorte
                using (Brush brush = new SolidBrush(colorAreaRecorte))
                {
                    g.FillRectangle(brush, areaRecorte);
                }

                // Dibujar borde del área de recorte
                using (Pen pen = new Pen(colorBordeRecorte, 2))
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    g.DrawRectangle(pen, Rectangle.Round(areaRecorte));
                }

                // Dibujar códigos Cohen-Sutherland
                DibujarCodigosCohenSutherland(g);

                // Dibujar información del área
                using (Font font = new Font("Arial", 9, FontStyle.Bold))
                using (Brush brushTexto = new SolidBrush(Color.FromArgb(120, 0, 100, 0)))
                {
                    string info = $"Área de Recorte\\n({areaRecorte.X:F0}, {areaRecorte.Y:F0}) - ({areaRecorte.Right:F0}, {areaRecorte.Bottom:F0})";
                    g.DrawString(info, font, brushTexto, areaRecorte.X, areaRecorte.Y - 25);
                }
            }
            
            necesitaRedibujo = true;
        }

        /// <summary>
        /// Dibuja los códigos de región Cohen-Sutherland
        /// </summary>
        private void DibujarCodigosCohenSutherland(Graphics g)
        {
            using (Font font = new Font("Arial", 8, FontStyle.Bold))
            using (Brush brushTexto = new SolidBrush(Color.FromArgb(100, 0, 80, 0)))
            {
                float x = areaRecorte.X;
                float y = areaRecorte.Y;
                float w = areaRecorte.Width;
                float h = areaRecorte.Height;
                
                // Códigos de las 9 regiones
                g.DrawString("1001", font, brushTexto, x - 35, y - 15);      // Arriba-Izquierda
                g.DrawString("1000", font, brushTexto, x + w/2 - 12, y - 15); // Arriba-Centro
                g.DrawString("1010", font, brushTexto, x + w + 8, y - 15);   // Arriba-Derecha
                
                g.DrawString("0001", font, brushTexto, x - 35, y + h/2 - 8); // Centro-Izquierda
                g.DrawString("0000", font, brushTexto, x + w/2 - 12, y + h/2 - 8); // Centro (dentro)
                g.DrawString("0010", font, brushTexto, x + w + 8, y + h/2 - 8); // Centro-Derecha
                
                g.DrawString("0101", font, brushTexto, x - 35, y + h + 5);   // Abajo-Izquierda
                g.DrawString("0100", font, brushTexto, x + w/2 - 12, y + h + 5); // Abajo-Centro
                g.DrawString("0110", font, brushTexto, x + w + 8, y + h + 5); // Abajo-Derecha
            }
        }

        /// <summary>
        /// Dibuja las líneas y polígonos originales en algoritmos de recorte
        /// </summary>
        private void DibujarElementosRecorte(Graphics g)
        {
            // Dibujar líneas originales
            using (Pen penLinea = new Pen(colorLineaOriginal, 2))
            {
                foreach (var linea in lineasOriginales)
                {
                    g.DrawLine(penLinea, linea.Start, linea.End);
                    
                    // Dibujar puntos de inicio y fin
                    using (Brush brush = new SolidBrush(colorLineaOriginal))
                    {
                        g.FillEllipse(brush, linea.Start.X - 3, linea.Start.Y - 3, 6, 6);
                        g.FillEllipse(brush, linea.End.X - 3, linea.End.Y - 3, 6, 6);
                    }
                }
            }

            // Dibujar polígonos originales
            using (Pen penPoligono = new Pen(colorPoligonoOriginal, 2))
            using (Brush brushPoligono = new SolidBrush(Color.FromArgb(30, colorPoligonoOriginal)))
            {
                foreach (var poligono in poligonosOriginales)
                {
                    if (poligono.Count >= 3)
                    {
                        // Rellenar polígono
                        g.FillPolygon(brushPoligono, poligono.ToArray());
                        
                        // Dibujar contorno
                        g.DrawPolygon(penPoligono, poligono.ToArray());
                        
                        // Dibujar vértices
                        using (Brush brushVertice = new SolidBrush(colorPoligonoOriginal))
                        {
                            foreach (var vertice in poligono)
                            {
                                g.FillEllipse(brushVertice, vertice.X - 4, vertice.Y - 4, 8, 8);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Actualiza la visualización completa
        /// </summary>
        private void ActualizarVisualizacion()
        {
            if (pictureBox.Image == null || imagenBase == null) return;

            // Copiar imagen base al PictureBox
            using (Graphics g = Graphics.FromImage(pictureBox.Image))
            {
                g.Clear(Color.White);
                g.DrawImage(imagenBase, 0, 0);
                
                // Dibujar elementos de recorte si corresponde
                if (categoriaActual == "Recorte")
                {
                    DibujarElementosRecorte(g);
                }
                
                // Dibujar puntos de click activos
                foreach (var kvp in puntosClicados)
                {
                    DibujarPuntoClick(g, kvp.Key);
                }
            }
            
            pictureBox.Invalidate();
        }

        /// <summary>
        /// Dibuja un punto donde el usuario hizo click
        /// </summary>
        private void DibujarPuntoClick(Graphics g, Point punto)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Círculo con efecto de ripple
            using (Brush brush = new SolidBrush(colorPuntoClick))
            {
                g.FillEllipse(brush, punto.X - 4, punto.Y - 4, 8, 8);
            }

            // Borde del círculo
            using (Pen pen = new Pen(Color.FromArgb(255, 200, 0, 0), 2))
            {
                g.DrawEllipse(pen, punto.X - 4, punto.Y - 4, 8, 8);
            }

            // Efecto de cruz para marcar el punto exacto
            using (Pen penCruz = new Pen(Color.White, 1))
            {
                g.DrawLine(penCruz, punto.X - 2, punto.Y, punto.X + 2, punto.Y);
                g.DrawLine(penCruz, punto.X, punto.Y - 2, punto.X, punto.Y + 2);
            }
        }

        /// <summary>
        /// Timer para desvanecer puntos de click antiguos
        /// </summary>
        private void TimerPuntos_Tick(object sender, EventArgs e)
        {
            if (puntosClicados.Count == 0) return;

            List<Point> puntosAEliminar = new List<Point>();
            DateTime ahora = DateTime.Now;

            foreach (var kvp in puntosClicados)
            {
                // Eliminar puntos después de 2.5 segundos
                if ((ahora - kvp.Value).TotalSeconds > 2.5)
                {
                    puntosAEliminar.Add(kvp.Key);
                }
            }

            // Eliminar puntos caducados
            if (puntosAEliminar.Count > 0)
            {
                foreach (var punto in puntosAEliminar)
                {
                    puntosClicados.Remove(punto);
                }
                ActualizarVisualizacion();
            }
        }

        /// <summary>
        /// Refresca la visualización completa
        /// </summary>
        public void RefrescarVisualizacion()
        {
            if (!string.IsNullOrEmpty(categoriaActual))
            {
                ConfigurarCategoria(categoriaActual);
            }
        }

        /// <summary>
        /// Limpia toda la visualización
        /// </summary>
        public void LimpiarVisualizacion()
        {
            puntosClicados.Clear();
            lineasOriginales.Clear();
            poligonosOriginales.Clear();
            mostrarPlano = false;
            mostrarAreaRecorte = false;
            categoriaActual = "";
            
            if (imagenBase != null)
            {
                using (Graphics g = Graphics.FromImage(imagenBase))
                {
                    g.Clear(Color.White);
                }
            }
        }

        /// <summary>
        /// Obtiene el área de recorte actual
        /// </summary>
        public RectangleF ObtenerAreaRecorte()
        {
            return areaRecorte;
        }

        /// <summary>
        /// Verifica si un punto está dentro del área de recorte
        /// </summary>
        public bool EstaDentroAreaRecorte(Point punto)
        {
            return areaRecorte.Contains(punto);
        }

        public void Dispose()
        {
            timerPuntos?.Stop();
            timerPuntos?.Dispose();
            imagenBase?.Dispose();
        }
    

            if (pictureBox.Image == null)
            {
                pictureBox.Image = new Bitmap(pictureBox.Width, pictureBox.Height);
            }
            
            switch (categoria)
            {
                case "Rasterizado":
                    mostrarPlano = true;
                    mostrarAreaRecorte = false;
                    DibujarPlanoCartesiano();
                    break;
                    
                case "Relleno":
                    mostrarPlano = false;
                    mostrarAreaRecorte = false;
                    // Para relleno, solo limpiar y dejar área limpia
                    LimpiarCanvas();
                    break;
                    
                case "Recorte":
                    mostrarPlano = false;
                    mostrarAreaRecorte = true;
                    CalcularAreaRecorte();
                    DibujarAreaRecorte();
                    break;
                    
                case "Curvas":
                    mostrarPlano = true;
                    mostrarAreaRecorte = false;
                    DibujarPlanoCartesiano();
                    break;
            }
        }

        /// <summary>
        /// Registra un punto donde el usuario hizo click
        /// </summary>
        /// <param name="punto">Punto donde se hizo click</param>
        public void RegistrarClick(Point punto)
        {
            puntosClicados[punto] = DateTime.Now;
            DibujarPuntoClick(punto);
        }

        /// <summary>
        /// Dibuja el plano cartesiano para rasterización y curvas
        /// </summary>
        private void DibujarPlanoCartesiano()
        {
            if (pictureBox.Image == null) return;

            using (Graphics g = Graphics.FromImage(pictureBox.Image))
            {
                // Limpiar canvas primero
                g.Clear(Color.White);
                
                // Configurar antialiasing para líneas suaves
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                int width = pictureBox.Width;
                int height = pictureBox.Height;
                int centerX = width / 2;
                int centerY = height / 2;

                // Dibujar fondo de plano suave
                using (Brush brush = new SolidBrush(colorPlano))
                {
                    g.FillRectangle(brush, 0, 0, width, height);
                }

                // Dibujar líneas de cuadrícula
                using (Pen penCuadricula = new Pen(Color.FromArgb(50, 150, 150, 150), 1))
                {
                    penCuadricula.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    
                    // Líneas verticales
                    for (int x = 0; x <= width; x += 20)
                    {
                        g.DrawLine(penCuadricula, x, 0, x, height);
                    }
                    
                    // Líneas horizontales
                    for (int y = 0; y <= height; y += 20)
                    {
                        g.DrawLine(penCuadricula, 0, y, width, y);
                    }
                }

                // Dibujar ejes principales
                using (Pen penEjes = new Pen(colorEjes, 2))
                {
                    // Eje X
                    g.DrawLine(penEjes, 0, centerY, width, centerY);
                    // Eje Y
                    g.DrawLine(penEjes, centerX, 0, centerX, height);
                }

                // Dibujar marcadores en los ejes
                using (Pen penMarcadores = new Pen(Color.FromArgb(100, 80, 80, 80), 1))
                using (Font font = new Font("Arial", 8))
                using (Brush brushTexto = new SolidBrush(Color.FromArgb(120, 60, 60, 60)))
                {
                    // Marcadores en X
                    for (int x = 0; x <= width; x += 40)
                    {
                        if (x != centerX)
                        {
                            g.DrawLine(penMarcadores, x, centerY - 5, x, centerY + 5);
                            string texto = (x - centerX).ToString();
                            SizeF tamaño = g.MeasureString(texto, font);
                            g.DrawString(texto, font, brushTexto, x - tamaño.Width / 2, centerY + 8);
                        }
                    }
                    
                    // Marcadores en Y
                    for (int y = 0; y <= height; y += 40)
                    {
                        if (y != centerY)
                        {
                            g.DrawLine(penMarcadores, centerX - 5, y, centerX + 5, y);
                            string texto = (centerY - y).ToString();
                            SizeF tamaño = g.MeasureString(texto, font);
                            g.DrawString(texto, font, brushTexto, centerX - tamaño.Width - 8, y - tamaño.Height / 2);
                        }
                    }
                    
                    // Origen
                    g.DrawString("(0,0)", font, brushTexto, centerX + 5, centerY + 5);
                }
            }
            
            pictureBox.Invalidate();
        }

        /// <summary>
        /// Calcula el área de recorte automáticamente
        /// </summary>
        private void CalcularAreaRecorte()
        {
            int width = pictureBox.Width;
            int height = pictureBox.Height;
            int centerX = width / 2;
            int centerY = height / 2;
            
            // Área de recorte centrada (40% del tamaño total)
            int areaWidth = (int)(width * 0.4);
            int areaHeight = (int)(height * 0.4);
            int areaX = centerX - areaWidth / 2;
            int areaY = centerY - areaHeight / 2;
            
            areaRecorte = new RectangleF(areaX, areaY, areaWidth, areaHeight);
        }

        /// <summary>
        /// Dibuja el área de recorte para algoritmos de clipping
        /// </summary>
        private void DibujarAreaRecorte()
        {
            if (pictureBox.Image == null) return;

            using (Graphics g = Graphics.FromImage(pictureBox.Image))
            {
                // Limpiar canvas primero
                g.Clear(Color.White);
                
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // Dibujar área de recorte
                using (Brush brush = new SolidBrush(colorAreaRecorte))
                {
                    g.FillRectangle(brush, areaRecorte);
                }

                // Dibujar borde del área de recorte
                using (Pen pen = new Pen(colorBordeRecorte, 3))
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    g.DrawRectangle(pen, Rectangle.Round(areaRecorte));
                }

                // Dibujar etiquetas de los códigos Cohen-Sutherland
                if (categoriaActual == "Recorte")
                {
                    DibujarCodigosCohenSutherland(g);
                }

                // Dibujar información del área
                using (Font font = new Font("Arial", 10, FontStyle.Bold))
                using (Brush brushTexto = new SolidBrush(Color.FromArgb(180, 0, 120, 0)))
                {
                    string info = $"Área de Recorte\n({areaRecorte.X:F0}, {areaRecorte.Y:F0}) - ({areaRecorte.Right:F0}, {areaRecorte.Bottom:F0})";
                    g.DrawString(info, font, brushTexto, areaRecorte.X, areaRecorte.Y - 30);
                }
            }
            
            pictureBox.Invalidate();
        }

        /// <summary>
        /// Dibuja los códigos de región Cohen-Sutherland
        /// </summary>
        private void DibujarCodigosCohenSutherland(Graphics g)
        {
            using (Font font = new Font("Arial", 9, FontStyle.Bold))
            using (Brush brushTexto = new SolidBrush(Color.FromArgb(150, 0, 100, 0)))
            {
                float x = areaRecorte.X;
                float y = areaRecorte.Y;
                float w = areaRecorte.Width;
                float h = areaRecorte.Height;
                
                // Códigos de las 9 regiones
                g.DrawString("1001", font, brushTexto, x - 40, y - 20);      // Arriba-Izquierda
                g.DrawString("1000", font, brushTexto, x + w/2 - 15, y - 20); // Arriba-Centro
                g.DrawString("1010", font, brushTexto, x + w + 10, y - 20);   // Arriba-Derecha
                
                g.DrawString("0001", font, brushTexto, x - 40, y + h/2 - 10); // Centro-Izquierda
                g.DrawString("0000", font, brushTexto, x + w/2 - 15, y + h/2 - 10); // Centro (dentro)
                g.DrawString("0010", font, brushTexto, x + w + 10, y + h/2 - 10); // Centro-Derecha
                
                g.DrawString("0101", font, brushTexto, x - 40, y + h + 10);   // Abajo-Izquierda
                g.DrawString("0100", font, brushTexto, x + w/2 - 15, y + h + 10); // Abajo-Centro
                g.DrawString("0110", font, brushTexto, x + w + 10, y + h + 10); // Abajo-Derecha
            }
        }

        /// <summary>
        /// Dibuja un punto donde el usuario hizo click
        /// </summary>
        private void DibujarPuntoClick(Point punto)
        {
            if (pictureBox.Image == null) return;

            using (Graphics g = Graphics.FromImage(pictureBox.Image))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // Círculo con efecto de ripple
                using (Brush brush = new SolidBrush(colorPuntoClick))
                {
                    g.FillEllipse(brush, punto.X - 5, punto.Y - 5, 10, 10);
                }

                // Borde del círculo
                using (Pen pen = new Pen(Color.FromArgb(255, 200, 0, 0), 2))
                {
                    g.DrawEllipse(pen, punto.X - 5, punto.Y - 5, 10, 10);
                }

                // Efecto de cruz para marcar el punto exacto
                using (Pen penCruz = new Pen(Color.White, 1))
                {
                    g.DrawLine(penCruz, punto.X - 3, punto.Y, punto.X + 3, punto.Y);
                    g.DrawLine(penCruz, punto.X, punto.Y - 3, punto.X, punto.Y + 3);
                }
            }
            
            pictureBox.Invalidate();
        }

        /// <summary>
        /// Limpia el canvas
        /// </summary>
        private void LimpiarCanvas()
        {
            if (pictureBox.Image == null) return;

            using (Graphics g = Graphics.FromImage(pictureBox.Image))
            {
                g.Clear(Color.White);
            }
            
            pictureBox.Invalidate();
        }

        /// <summary>
        /// Timer para desvanecer puntos de click antiguos
        /// </summary>
        private void TimerPuntos_Tick(object sender, EventArgs e)
        {
            if (puntosClicados.Count == 0) return;

            List<Point> puntosAEliminar = new List<Point>();
            DateTime ahora = DateTime.Now;

            foreach (var kvp in puntosClicados)
            {
                // Eliminar puntos después de 3 segundos
                if ((ahora - kvp.Value).TotalSeconds > 3)
                {
                    puntosAEliminar.Add(kvp.Key);
                }
            }

            // Eliminar puntos caducados
            foreach (var punto in puntosAEliminar)
            {
                puntosClicados.Remove(punto);
            }

            // Redibujar si es necesario
            if (puntosAEliminar.Count > 0)
            {
                RefrescarVisualizacion();
            }
        }

        /// <summary>
        /// Refresca la visualización completa
        /// </summary>
        public void RefrescarVisualizacion()
        {
            if (!string.IsNullOrEmpty(categoriaActual))
            {
                ConfigurarCategoria(categoriaActual);
                
                // Redibujar puntos activos
                foreach (var punto in puntosClicados.Keys)
                {
                    DibujarPuntoClick(punto);
                }
            }
        }

        /// <summary>
        /// Limpia toda la visualización
        /// </summary>
        public void LimpiarVisualizacion()
        {
            puntosClicados.Clear();
            mostrarPlano = false;
            mostrarAreaRecorte = false;
            categoriaActual = "";
            
            if (pictureBox.Image != null)
            {
                using (Graphics g = Graphics.FromImage(pictureBox.Image))
                {
                    g.Clear(Color.White);
                }
                pictureBox.Invalidate();
            }
        }

        /// <summary>
        /// Obtiene el área de recorte actual
        /// </summary>
        public RectangleF ObtenerAreaRecorte()
        {
            return areaRecorte;
        }

        /// <summary>
        /// Verifica si un punto está dentro del área de recorte
        /// </summary>
        public bool EstaDentroAreaRecorte(Point punto)
        {
            return areaRecorte.Contains(punto);
        }

        /// <summary>
        /// Libera recursos
        /// </summary>
        public void Dispose()
        {
            timerPuntos?.Stop();
            timerPuntos?.Dispose();
        }
    }
}
