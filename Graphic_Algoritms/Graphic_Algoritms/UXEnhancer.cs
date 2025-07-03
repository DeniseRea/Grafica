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
        private Graphics graphics;
        private Dictionary<Point, DateTime> puntosClicados;
        private Timer timerPuntos;
        private bool mostrarPlano;
        private bool mostrarAreaRecorte;
        private RectangleF areaRecorte;
        private string categoriaActual;

        // Colores para diferentes elementos visuales
        private readonly Color colorPlano = Color.FromArgb(30, 200, 200, 200);
        private readonly Color colorEjes = Color.FromArgb(100, 100, 100, 100);
        private readonly Color colorPuntoClick = Color.FromArgb(200, 255, 0, 0);
        private readonly Color colorAreaRecorte = Color.FromArgb(60, 0, 255, 0);
        private readonly Color colorBordeRecorte = Color.FromArgb(150, 0, 200, 0);

        public UXEnhancer(PictureBox pictureBox)
        {
            this.pictureBox = pictureBox;
            this.puntosClicados = new Dictionary<Point, DateTime>();
            
            // Timer para desvanecer puntos de click
            timerPuntos = new Timer();
            timerPuntos.Interval = 100; // Actualizar cada 100ms
            timerPuntos.Tick += TimerPuntos_Tick;
            timerPuntos.Start();
        }

        /// <summary>
        /// Configura la visualización según la categoría de algoritmo seleccionada
        /// </summary>
        /// <param name="categoria">Categoría del algoritmo (Rasterizado, Relleno, Recorte, Curvas)</param>
        public void ConfigurarCategoria(string categoria)
        {
            categoriaActual = categoria;
            
            // Asegurar que tenemos una imagen en el PictureBox
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
