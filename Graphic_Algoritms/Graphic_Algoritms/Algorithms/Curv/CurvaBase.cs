using System;
using System.Collections.Generic;
using System.Drawing;

namespace CurvasParametricas
{
    /// <summary>
    /// Clase base abstracta para curvas paramétricas.
    /// Implementa funcionalidad común para todas las curvas.
    /// </summary>
    public abstract class CurvaBase : ICurva
    {
        /// <summary>
        /// Lista de puntos de control que definen la curva.
        /// </summary>
        public List<PointF> PuntosControl { get; set; }

        /// <summary>
        /// Resolución de la curva (número de puntos calculados).
        /// </summary>
        public int Resolucion { get; set; }

        /// <summary>
        /// Color de la curva.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Grosor de la línea de la curva.
        /// </summary>
        public float GrosorLinea { get; set; }

        /// <summary>
        /// Indica si se deben mostrar los puntos de control.
        /// </summary>
        public bool MostrarPuntosControl { get; set; }

        /// <summary>
        /// Constructor base para curvas paramétricas.
        /// </summary>
        /// <param name="resolucion">Resolución de la curva (por defecto 100).</param>
        protected CurvaBase(int resolucion = 100)
        {
            PuntosControl = new List<PointF>();
            Resolucion = Math.Max(10, resolucion); // Mínimo 10 puntos
            Color = Color.Blue;
            GrosorLinea = 2.0f;
            MostrarPuntosControl = true;
        }

        /// <summary>
        /// Agrega un punto de control a la curva.
        /// </summary>
        /// <param name="punto">Punto de control a agregar.</param>
        public virtual void AgregarPuntoControl(PointF punto)
        {
            PuntosControl.Add(punto);
        }

        /// <summary>
        /// Elimina todos los puntos de control.
        /// </summary>
        public virtual void LimpiarPuntosControl()
        {
            PuntosControl.Clear();
        }

        /// <summary>
        /// Calcula todos los puntos de la curva basados en los puntos de control.
        /// </summary>
        /// <returns>Lista de puntos que forman la curva.</returns>
        public virtual List<PointF> CalcularPuntosCurva()
        {
            if (!PuedeDibujar())
                return new List<PointF>();

            var puntos = new List<PointF>();

            for (int i = 0; i <= Resolucion; i++)
            {
                float t = (float)i / Resolucion;
                var punto = CalcularPunto(t);
                puntos.Add(punto);
            }

            return puntos;
        }

        /// <summary>
        /// Calcula un punto específico de la curva para un parámetro t.
        /// Debe ser implementado por las clases derivadas.
        /// </summary>
        /// <param name="t">Parámetro entre 0 y 1.</param>
        /// <returns>Punto calculado en la curva.</returns>
        public abstract PointF CalcularPunto(float t);

        /// <summary>
        /// Verifica si la curva tiene suficientes puntos de control para ser dibujada.
        /// Puede ser sobrescrito por las clases derivadas.
        /// </summary>
        /// <returns>True si se puede dibujar la curva.</returns>
        public virtual bool PuedeDibujar()
        {
            return PuntosControl.Count >= 2;
        }

        /// <summary>
        /// Obtiene información de la curva como string.
        /// </summary>
        /// <returns>Información descriptiva de la curva.</returns>
        public virtual string ObtenerInformacion()
        {
            return $"Puntos de control: {PuntosControl.Count}, Resolución: {Resolucion}";
        }

        /// <summary>
        /// Dibuja los puntos de control como pequeños círculos con líneas de conexión.
        /// </summary>
        /// <param name="graphics">Contexto gráfico donde dibujar.</param>
        /// <param name="colorPuntos">Color para los puntos de control.</param>
        /// <param name="tamanoPunto">Tamaño de los puntos de control.</param>
        public virtual void DibujarPuntosControl(Graphics graphics, Color colorPuntos, float tamanoPunto = 6.0f)
        {
            if (graphics == null || PuntosControl == null || PuntosControl.Count == 0) 
                return;

            try
            {
                // Dibujar líneas de conexión entre puntos de control (polígono de control)
                using (var penLinea = new Pen(Color.Gray, 1.0f))
                {
                    penLinea.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

                    for (int i = 0; i < PuntosControl.Count - 1; i++)
                    {
                        graphics.DrawLine(penLinea, PuntosControl[i], PuntosControl[i + 1]);
                    }
                }

                // Dibujar puntos de control
                using (var brush = new SolidBrush(colorPuntos))
                using (var penBorde = new Pen(Color.Black, 1.0f))
                {
                    foreach (var punto in PuntosControl)
                    {
                        graphics.FillEllipse(brush,
                            punto.X - tamanoPunto / 2,
                            punto.Y - tamanoPunto / 2,
                            tamanoPunto,
                            tamanoPunto);

                        graphics.DrawEllipse(penBorde,
                            punto.X - tamanoPunto / 2,
                            punto.Y - tamanoPunto / 2,
                            tamanoPunto,
                            tamanoPunto);
                    }
                }
            }
            catch
            {
                // Si hay error al dibujar puntos de control, no hacer nada
            }
        }

        /// <summary>
        /// Calcula el coeficiente binomial (n sobre k) para cálculos de Bézier.
        /// </summary>
        /// <param name="n">Número total.</param>
        /// <param name="k">Número de combinaciones.</param>
        /// <returns>Coeficiente binomial.</returns>
        protected static double CoeficienteBinomial(int n, int k)
        {
            if (k > n || k < 0)
                return 0;

            if (k == 0 || k == n)
                return 1;

            double resultado = 1;
            for (int i = 0; i < k; i++)
            {
                resultado *= (n - i);
                resultado /= (i + 1);
            }

            return resultado;
        }

        /// <summary>
        /// Valida que el parámetro t esté en el rango [0, 1].
        /// </summary>
        /// <param name="t">Parámetro a validar.</param>
        /// <returns>Parámetro t limitado al rango [0, 1].</returns>
        protected float ValidarParametroT(float t)
        {
            return Math.Max(0f, Math.Min(1f, t));
        }

        /// <summary>
        /// Dibuja la curva en el contexto gráfico especificado.
        /// </summary>
        /// <param name="graphics">Contexto gráfico donde dibujar.</param>
        public virtual void Dibujar(Graphics graphics)
        {
            if (graphics == null || !PuedeDibujar() || PuntosControl == null)
                return;

            try
            {
                // Dibujar la curva
                var puntosCurva = CalcularPuntosCurva();
                if (puntosCurva != null && puntosCurva.Count > 1)
                {
                    using (var pen = new Pen(Color, GrosorLinea))
                    {
                        pen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
                        pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                        pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
                        
                        for (int i = 0; i < puntosCurva.Count - 1; i++)
                        {
                            graphics.DrawLine(pen, puntosCurva[i], puntosCurva[i + 1]);
                        }
                    }
                }

                // Dibujar puntos de control si está habilitado
                if (MostrarPuntosControl && PuntosControl.Count > 0)
                {
                    DibujarPuntosControl(graphics, Color.Red);
                }
            }
            catch
            {
                // Si hay error al dibujar, no hacer nada
            }
        }

        /// <summary>
        /// Crea una copia de la curva actual.
        /// </summary>
        /// <returns>Nueva instancia de la curva.</returns>
        public abstract ICurva Clonar();

        /// <summary>
        /// Calcula la longitud aproximada de la curva.
        /// </summary>
        /// <returns>Longitud de la curva.</returns>
        public virtual float CalcularLongitud()
        {
            if (!PuedeDibujar())
                return 0f;

            var puntosCurva = CalcularPuntosCurva();
            float longitud = 0f;

            for (int i = 0; i < puntosCurva.Count - 1; i++)
            {
                float dx = puntosCurva[i + 1].X - puntosCurva[i].X;
                float dy = puntosCurva[i + 1].Y - puntosCurva[i].Y;
                longitud += (float)Math.Sqrt(dx * dx + dy * dy);
            }

            return longitud;
        }

        /// <summary>
        /// Encuentra el punto más cercano en la curva a un punto dado.
        /// </summary>
        /// <param name="punto">Punto de referencia.</param>
        /// <returns>Punto más cercano en la curva.</returns>
        public virtual PointF PuntoMasCercano(PointF punto)
        {
            if (!PuedeDibujar())
                return PointF.Empty;

            var puntosCurva = CalcularPuntosCurva();
            if (puntosCurva.Count == 0)
                return PointF.Empty;

            PointF puntoMasCercano = puntosCurva[0];
            float distanciaMinima = CalcularDistancia(punto, puntoMasCercano);

            foreach (var puntoCurva in puntosCurva)
            {
                float distancia = CalcularDistancia(punto, puntoCurva);
                if (distancia < distanciaMinima)
                {
                    distanciaMinima = distancia;
                    puntoMasCercano = puntoCurva;
                }
            }

            return puntoMasCercano;
        }

        /// <summary>
        /// Calcula la distancia entre dos puntos.
        /// </summary>
        /// <param name="p1">Primer punto.</param>
        /// <param name="p2">Segundo punto.</param>
        /// <returns>Distancia entre los puntos.</returns>
        protected float CalcularDistancia(PointF p1, PointF p2)
        {
            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }

        /// <summary>
        /// Transforma todos los puntos de control aplicando una función.
        /// </summary>
        /// <param name="transformacion">Función de transformación.</param>
        public virtual void TransformarPuntosControl(Func<PointF, PointF> transformacion)
        {
            if (transformacion == null)
                return;

            for (int i = 0; i < PuntosControl.Count; i++)
            {
                PuntosControl[i] = transformacion(PuntosControl[i]);
            }
        }

        /// <summary>
        /// Traslada la curva por un vector dado.
        /// </summary>
        /// <param name="dx">Desplazamiento en X.</param>
        /// <param name="dy">Desplazamiento en Y.</param>
        public virtual void Trasladar(float dx, float dy)
        {
            TransformarPuntosControl(p => new PointF(p.X + dx, p.Y + dy));
        }

        /// <summary>
        /// Escala la curva por un factor dado.
        /// </summary>
        /// <param name="escala">Factor de escala.</param>
        /// <param name="centro">Centro de escalado (opcional).</param>
        public virtual void Escalar(float escala, PointF? centro = null)
        {
            PointF centroEscala = centro ?? new PointF(0, 0);
            TransformarPuntosControl(p => new PointF(
                centroEscala.X + (p.X - centroEscala.X) * escala,
                centroEscala.Y + (p.Y - centroEscala.Y) * escala
            ));
        }
    }
}
