using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CurvasParametricas
{
    /// <summary>
    /// Implementación de curva B-Spline cúbica uniforme.
    /// Permite cualquier número de puntos de control (mínimo 4).
    /// </summary>
    public class CurvaBSpline : CurvaBase
    {
        /// <summary>
        /// Grado de la curva B-Spline (por defecto 3 para cúbica).
        /// </summary>
        public int Grado { get; set; }

        /// <summary>
        /// Vector de nudos para la curva B-Spline.
        /// </summary>
        public List<float> VectorNudos { get; private set; }

        /// <summary>
        /// Constructor para curva B-Spline.
        /// </summary>
        /// <param name="grado">Grado de la curva (por defecto 3).</param>
        /// <param name="resolucion">Resolución de la curva (por defecto 100).</param>
        public CurvaBSpline(int grado = 3, int resolucion = 100) : base(resolucion)
        {
            Grado = grado;
            VectorNudos = new List<float>();
            Color = Color.Green;
        }

        /// <summary>
        /// Agrega un punto de control y actualiza el vector de nudos.
        /// </summary>
        /// <param name="punto">Punto de control a agregar.</param>
        public override void AgregarPuntoControl(PointF punto)
        {
            base.AgregarPuntoControl(punto);
            ActualizarVectorNudos();
        }

        /// <summary>
        /// Limpia los puntos de control y el vector de nudos.
        /// </summary>
        public override void LimpiarPuntosControl()
        {
            base.LimpiarPuntosControl();
            VectorNudos.Clear();
        }

        /// <summary>
        /// Actualiza el vector de nudos basado en el número de puntos de control.
        /// Utiliza nudos uniformes con multiplicidad en los extremos.
        /// </summary>
        private void ActualizarVectorNudos()
        {
            VectorNudos.Clear();

            if (PuntosControl.Count < Grado + 1)
                return;

            int n = PuntosControl.Count - 1; // Índice del último punto de control
            int m = n + Grado + 1;           // Índice del último nudo

            // Crear vector de nudos uniforme con multiplicidad en extremos
            for (int i = 0; i <= m; i++)
            {
                if (i <= Grado)
                    VectorNudos.Add(0.0f);
                else if (i >= n)
                    VectorNudos.Add(1.0f);
                else
                    VectorNudos.Add((float)(i - Grado) / (n - Grado));
            }
        }

        /// <summary>
        /// Calcula un punto específico de la curva B-Spline para un parámetro t.
        /// </summary>
        /// <param name="t">Parámetro entre 0 y 1.</param>
        /// <returns>Punto calculado en la curva.</returns>
        public override PointF CalcularPunto(float t)
        {
            if (!PuedeDibujar())
                throw new InvalidOperationException($"Se necesitan al menos {Grado + 1} puntos de control.");

            if (VectorNudos == null || VectorNudos.Count == 0)
                throw new InvalidOperationException("Vector de nudos no inicializado.");

            // Asegurar que t esté en el rango válido
            t = Math.Max(0.0f, Math.Min(1.0f, t));

            float x = 0.0f;
            float y = 0.0f;

            // Calcular el punto usando las funciones base de B-Spline
            for (int i = 0; i < PuntosControl.Count; i++)
            {
                try
                {
                    float basis = CalcularFuncionBase(i, Grado, t);
                    x += basis * PuntosControl[i].X;
                    y += basis * PuntosControl[i].Y;
                }
                catch
                {
                    // Si hay error en el cálculo de la función base, continuar
                    continue;
                }
            }

            return new PointF(x, y);
        }

        /// <summary>
        /// Calcula la función base de B-Spline usando el algoritmo de De Boor.
        /// </summary>
        /// <param name="i">Índice del punto de control.</param>
        /// <param name="k">Grado de la función base.</param>
        /// <param name="t">Parámetro.</param>
        /// <returns>Valor de la función base.</returns>
        private float CalcularFuncionBase(int i, int k, float t)
        {
            if (VectorNudos == null || VectorNudos.Count == 0)
                return 0.0f;

            // Verificar límites de índices
            if (i < 0 || i >= VectorNudos.Count - 1)
                return 0.0f;

            // Caso base: k = 0
            if (k == 0)
            {
                if (i + 1 < VectorNudos.Count)
                    return (t >= VectorNudos[i] && t < VectorNudos[i + 1]) ? 1.0f : 0.0f;
                else
                    return 0.0f;
            }

            // Caso recursivo
            float resultado = 0.0f;

            // Primer término - verificar límites
            if (i + k < VectorNudos.Count)
            {
                float denominador1 = VectorNudos[i + k] - VectorNudos[i];
                if (Math.Abs(denominador1) > float.Epsilon)
                {
                    resultado += (t - VectorNudos[i]) / denominador1 * CalcularFuncionBase(i, k - 1, t);
                }
            }

            // Segundo término - verificar límites
            if (i + k + 1 < VectorNudos.Count && i + 1 < VectorNudos.Count)
            {
                float denominador2 = VectorNudos[i + k + 1] - VectorNudos[i + 1];
                if (Math.Abs(denominador2) > float.Epsilon)
                {
                    resultado += (VectorNudos[i + k + 1] - t) / denominador2 * CalcularFuncionBase(i + 1, k - 1, t);
                }
            }

            return resultado;
        }

        /// <summary>
        /// Verifica si la curva tiene suficientes puntos de control para ser dibujada.
        /// </summary>
        /// <returns>True si tiene al menos grado+1 puntos de control.</returns>
        public override bool PuedeDibujar()
        {
            return PuntosControl.Count >= Grado + 1;
        }

        /// <summary>
        /// Calcula todos los puntos de la curva, ajustando el rango de t.
        /// </summary>
        /// <returns>Lista de puntos que forman la curva.</returns>
        public override List<PointF> CalcularPuntosCurva()
        {
            if (!PuedeDibujar() || VectorNudos == null || VectorNudos.Count == 0)
                return new List<PointF>();

            var puntos = new List<PointF>();

            // Para B-Splines, el rango efectivo está entre los nudos internos
            if (VectorNudos.Count > 2 * Grado)
            {
                float tMin = VectorNudos[Grado];
                float tMax = VectorNudos[VectorNudos.Count - Grado - 1];

                for (int i = 0; i <= Resolucion; i++)
                {
                    float t = tMin + (tMax - tMin) * i / Resolucion;
                    try
                    {
                        var punto = CalcularPunto(t);
                        puntos.Add(punto);
                    }
                    catch
                    {
                        // Si hay error en el cálculo, continuar con el siguiente punto
                        continue;
                    }
                }
            }

            return puntos;
        }

        /// <summary>
        /// Obtiene información específica de la curva B-Spline.
        /// </summary>
        /// <returns>Información descriptiva de la curva B-Spline.</returns>
        public override string ObtenerInformacion()
        {
            return $"Curva B-Spline (Grado {Grado}) - {base.ObtenerInformacion()}, Nudos: {VectorNudos.Count}";
        }

        /// <summary>
        /// Dibuja el polígono de control (líneas entre puntos de control consecutivos).
        /// </summary>
        /// <param name="graphics">Contexto gráfico donde dibujar.</param>
        /// <param name="colorPoligono">Color para el polígono de control.</param>
        public void DibujarPoligonoControl(Graphics graphics, Color colorPoligono)
        {
            if (PuntosControl.Count < 2)
                return;

            using (var pen = new Pen(colorPoligono, 1.0f))
            {
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

                for (int i = 0; i < PuntosControl.Count - 1; i++)
                {
                    graphics.DrawLine(pen, PuntosControl[i], PuntosControl[i + 1]);
                }
            }
        }

        /// <summary>
        /// Crea una curva B-Spline con puntos de control específicos.
        /// </summary>
        /// <param name="puntos">Lista de puntos de control.</param>
        /// <param name="grado">Grado de la curva.</param>
        /// <param name="resolucion">Resolución de la curva.</param>
        /// <returns>Nueva instancia de CurvaBSpline.</returns>
        public static CurvaBSpline CrearCurva(List<PointF> puntos, int grado = 3, int resolucion = 100)
        {
            var curva = new CurvaBSpline(grado, resolucion);
            foreach (var punto in puntos)
            {
                curva.AgregarPuntoControl(punto);
            }
            return curva;
        }

        /// <summary>
        /// Crea una copia de la curva B-Spline actual.
        /// </summary>
        /// <returns>Nueva instancia de CurvaBSpline.</returns>
        public override ICurva Clonar()
        {
            var nuevaCurva = new CurvaBSpline(Grado, Resolucion)
            {
                Color = this.Color,
                GrosorLinea = this.GrosorLinea,
                MostrarPuntosControl = this.MostrarPuntosControl
            };

            foreach (var punto in PuntosControl)
            {
                nuevaCurva.AgregarPuntoControl(punto);
            }

            return nuevaCurva;
        }

        /// <summary>
        /// Inserta un nuevo nudo en el vector de nudos y ajusta los puntos de control.
        /// </summary>
        /// <param name="nuevoNudo">Valor del nuevo nudo a insertar.</param>
        public void InsertarNudo(float nuevoNudo)
        {
            if (PuntosControl.Count < Grado + 1)
                return;

            // Encontrar posición para insertar el nudo
            int posicion = VectorNudos.Count;
            for (int i = 0; i < VectorNudos.Count; i++)
            {
                if (VectorNudos[i] > nuevoNudo)
                {
                    posicion = i;
                    break;
                }
            }

            // Insertar el nudo
            VectorNudos.Insert(posicion, nuevoNudo);

            // Actualizar puntos de control usando algoritmo de inserción de nudos
            ActualizarPuntosControlPorNudo(posicion, nuevoNudo);
        }

        /// <summary>
        /// Actualiza los puntos de control después de la inserción de un nudo.
        /// </summary>
        /// <param name="posicion">Posición del nudo insertado.</param>
        /// <param name="nuevoNudo">Valor del nudo insertado.</param>
        private void ActualizarPuntosControlPorNudo(int posicion, float nuevoNudo)
        {
            var nuevosPuntos = new List<PointF>(PuntosControl);

            // Aplicar algoritmo de inserción de nudos
            for (int nivel = 1; nivel <= Grado; nivel++)
            {
                for (int i = posicion - Grado; i <= posicion - nivel; i++)
                {
                    if (i >= 0 && i < nuevosPuntos.Count)
                    {
                        float alpha = 0.5f; // Simplificación para nudos uniformes
                        if (i + Grado < VectorNudos.Count && VectorNudos[i + Grado] != VectorNudos[i])
                        {
                            alpha = (nuevoNudo - VectorNudos[i]) / (VectorNudos[i + Grado] - VectorNudos[i]);
                        }

                        var puntoAnterior = i > 0 ? nuevosPuntos[i - 1] : nuevosPuntos[i];
                        nuevosPuntos[i] = new PointF(
                            (1 - alpha) * puntoAnterior.X + alpha * nuevosPuntos[i].X,
                            (1 - alpha) * puntoAnterior.Y + alpha * nuevosPuntos[i].Y
                        );
                    }
                }
            }

            PuntosControl = nuevosPuntos;
        }

        /// <summary>
        /// Eleva el grado de la curva B-Spline.
        /// </summary>
        /// <param name="incremento">Incremento en el grado (por defecto 1).</param>
        public void ElevarGrado(int incremento = 1)
        {
            if (incremento <= 0)
                return;

            for (int i = 0; i < incremento; i++)
            {
                ElevarGradoUnaVez();
            }
        }

        /// <summary>
        /// Eleva el grado de la curva en una unidad.
        /// </summary>
        private void ElevarGradoUnaVez()
        {
            var nuevosPuntos = new List<PointF>();
            int n = PuntosControl.Count - 1;

            // Primer punto permanece igual
            nuevosPuntos.Add(PuntosControl[0]);

            // Puntos intermedios
            for (int i = 1; i <= n; i++)
            {
                float alpha = (float)i / (Grado + 1);
                var nuevoPunto = new PointF(
                    alpha * PuntosControl[i - 1].X + (1 - alpha) * PuntosControl[i].X,
                    alpha * PuntosControl[i - 1].Y + (1 - alpha) * PuntosControl[i].Y
                );
                nuevosPuntos.Add(nuevoPunto);
            }

            // Último punto permanece igual
            nuevosPuntos.Add(PuntosControl[n]);

            PuntosControl = nuevosPuntos;
            Grado++;
            ActualizarVectorNudos();
        }

        /// <summary>
        /// Obtiene los segmentos activos de la curva en un parámetro dado.
        /// </summary>
        /// <param name="t">Parámetro entre 0 y 1.</param>
        /// <returns>Lista de índices de puntos de control activos.</returns>
        public List<int> ObtenerSegmentosActivos(float t)
        {
            var segmentos = new List<int>();
            
            if (VectorNudos.Count == 0)
                return segmentos;

            // Encontrar el intervalo de nudos que contiene t
            int intervalo = -1;
            for (int i = Grado; i < VectorNudos.Count - Grado - 1; i++)
            {
                if (t >= VectorNudos[i] && t < VectorNudos[i + 1])
                {
                    intervalo = i;
                    break;
                }
            }

            if (intervalo >= 0)
            {
                // Los puntos de control activos son los que afectan este intervalo
                for (int i = intervalo - Grado; i <= intervalo; i++)
                {
                    if (i >= 0 && i < PuntosControl.Count)
                    {
                        segmentos.Add(i);
                    }
                }
            }

            return segmentos;
        }
    }
}
