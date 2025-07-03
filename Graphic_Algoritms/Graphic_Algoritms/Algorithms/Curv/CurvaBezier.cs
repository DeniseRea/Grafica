using System;
using System.Collections.Generic;
using System.Drawing;

namespace CurvasParametricas
{
    /// <summary>
    /// Implementación de curva de Bézier cúbica.
    /// Utiliza 4 puntos de control para generar una curva suave.
    /// </summary>
    public class CurvaBezier : CurvaBase
    {
        /// <summary>
        /// Constructor para curva de Bézier.
        /// </summary>
        /// <param name="resolucion">Resolución de la curva (por defecto 100).</param>
        public CurvaBezier(int resolucion = 100) : base(resolucion)
        {
            Color = Color.Red;
        }

        /// <summary>
        /// Calcula un punto específico de la curva de Bézier para un parámetro t.
        /// Utiliza la fórmula de Bézier cúbica: B(t) = (1-t)³P₀ + 3(1-t)²tP₁ + 3(1-t)t²P₂ + t³P₃
        /// </summary>
        /// <param name="t">Parámetro entre 0 y 1.</param>
        /// <returns>Punto calculado en la curva.</returns>
        public override PointF CalcularPunto(float t)
        {
            if (PuntosControl.Count < 4)
                throw new InvalidOperationException("Se necesitan exactamente 4 puntos de control para una curva de Bézier cúbica.");

            // Coeficientes de Bernstein para Bézier cúbica
            float t2 = t * t;
            float t3 = t2 * t;
            float mt = 1 - t;
            float mt2 = mt * mt;
            float mt3 = mt2 * mt;

            float b0 = mt3;           // (1-t)³
            float b1 = 3 * mt2 * t;   // 3(1-t)²t
            float b2 = 3 * mt * t2;   // 3(1-t)t²
            float b3 = t3;            // t³

            // Calcular coordenadas del punto
            float x = b0 * PuntosControl[0].X +
                      b1 * PuntosControl[1].X +
                      b2 * PuntosControl[2].X +
                      b3 * PuntosControl[3].X;

            float y = b0 * PuntosControl[0].Y +
                      b1 * PuntosControl[1].Y +
                      b2 * PuntosControl[2].Y +
                      b3 * PuntosControl[3].Y;

            return new PointF(x, y);
        }

        /// <summary>
        /// Verifica si la curva tiene exactamente 4 puntos de control para ser dibujada.
        /// </summary>
        /// <returns>True si tiene exactamente 4 puntos de control.</returns>
        public override bool PuedeDibujar()
        {
            return PuntosControl.Count == 4;
        }

        /// <summary>
        /// Agrega un punto de control, máximo 4 puntos.
        /// </summary>
        /// <param name="punto">Punto de control a agregar.</param>
        public override void AgregarPuntoControl(PointF punto)
        {
            if (PuntosControl.Count < 4)
            {
                base.AgregarPuntoControl(punto);
            }
        }

        /// <summary>
        /// Calcula la tangente en un punto específico de la curva.
        /// </summary>
        /// <param name="t">Parámetro entre 0 y 1.</param>
        /// <returns>Vector tangente en el punto.</returns>
        public PointF CalcularTangente(float t)
        {
            if (PuntosControl.Count < 4)
                throw new InvalidOperationException("Se necesitan exactamente 4 puntos de control.");

            // Derivada de la curva de Bézier
            float mt = 1 - t;
            float mt2 = mt * mt;
            float t2 = t * t;

            float dx = 3 * mt2 * (PuntosControl[1].X - PuntosControl[0].X) +
                       6 * mt * t * (PuntosControl[2].X - PuntosControl[1].X) +
                       3 * t2 * (PuntosControl[3].X - PuntosControl[2].X);

            float dy = 3 * mt2 * (PuntosControl[1].Y - PuntosControl[0].Y) +
                       6 * mt * t * (PuntosControl[2].Y - PuntosControl[1].Y) +
                       3 * t2 * (PuntosControl[3].Y - PuntosControl[2].Y);

            return new PointF(dx, dy);
        }

        /// <summary>
        /// Obtiene información específica de la curva de Bézier.
        /// </summary>
        /// <returns>Información descriptiva de la curva de Bézier.</returns>
        public override string ObtenerInformacion()
        {
            return $"Curva de Bézier Cúbica - {base.ObtenerInformacion()}";
        }

        /// <summary>
        /// Crea una curva de Bézier con puntos de control específicos.
        /// </summary>
        /// <param name="p0">Punto de inicio.</param>
        /// <param name="p1">Primer punto de control.</param>
        /// <param name="p2">Segundo punto de control.</param>
        /// <param name="p3">Punto final.</param>
        /// <param name="resolucion">Resolución de la curva.</param>
        /// <returns>Nueva instancia de CurvaBezier.</returns>
        public static CurvaBezier CrearCurva(PointF p0, PointF p1, PointF p2, PointF p3, int resolucion = 100)
        {
            var curva = new CurvaBezier(resolucion);
            curva.AgregarPuntoControl(p0);
            curva.AgregarPuntoControl(p1);
            curva.AgregarPuntoControl(p2);
            curva.AgregarPuntoControl(p3);
            return curva;
        }

        /// <summary>
        /// Crea una copia de la curva de Bézier actual.
        /// </summary>
        /// <returns>Nueva instancia de CurvaBezier.</returns>
        public override ICurva Clonar()
        {
            var nuevaCurva = new CurvaBezier(Resolucion)
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
        /// Calcula la curvatura en un punto específico de la curva.
        /// </summary>
        /// <param name="t">Parámetro entre 0 y 1.</param>
        /// <returns>Valor de curvatura en el punto.</returns>
        public float CalcularCurvatura(float t)
        {
            if (PuntosControl.Count < 4)
                throw new InvalidOperationException("Se necesitan exactamente 4 puntos de control.");

            // Calcular primera y segunda derivada
            var primeraDeriv = CalcularTangente(t);
            var segundaDeriv = CalcularSegundaDerivada(t);

            // Curvatura = |r'(t) × r''(t)| / |r'(t)|³
            float crossProduct = Math.Abs(primeraDeriv.X * segundaDeriv.Y - primeraDeriv.Y * segundaDeriv.X);
            float magnitudPrimera = (float)Math.Sqrt(primeraDeriv.X * primeraDeriv.X + primeraDeriv.Y * primeraDeriv.Y);
            
            if (magnitudPrimera < 1e-6f)
                return 0f;

            return crossProduct / (magnitudPrimera * magnitudPrimera * magnitudPrimera);
        }

        /// <summary>
        /// Calcula la segunda derivada en un punto específico de la curva.
        /// </summary>
        /// <param name="t">Parámetro entre 0 y 1.</param>
        /// <returns>Vector de segunda derivada en el punto.</returns>
        public PointF CalcularSegundaDerivada(float t)
        {
            if (PuntosControl.Count < 4)
                throw new InvalidOperationException("Se necesitan exactamente 4 puntos de control.");

            // Segunda derivada de la curva de Bézier
            float mt = 1 - t;

            float d2x = 6 * mt * (PuntosControl[2].X - 2 * PuntosControl[1].X + PuntosControl[0].X) +
                        6 * t * (PuntosControl[3].X - 2 * PuntosControl[2].X + PuntosControl[1].X);

            float d2y = 6 * mt * (PuntosControl[2].Y - 2 * PuntosControl[1].Y + PuntosControl[0].Y) +
                        6 * t * (PuntosControl[3].Y - 2 * PuntosControl[2].Y + PuntosControl[1].Y);

            return new PointF(d2x, d2y);
        }

        /// <summary>
        /// Subdivide la curva de Bézier en dos curvas en el parámetro t.
        /// </summary>
        /// <param name="t">Parámetro de subdivisión (entre 0 y 1).</param>
        /// <returns>Tupla con las dos curvas resultantes.</returns>
        public (CurvaBezier Primera, CurvaBezier Segunda) Subdividir(float t)
        {
            if (PuntosControl.Count < 4)
                throw new InvalidOperationException("Se necesitan exactamente 4 puntos de control.");

            t = ValidarParametroT(t);

            // Algoritmo de De Casteljau para subdivisión
            var p0 = PuntosControl[0];
            var p1 = PuntosControl[1];
            var p2 = PuntosControl[2];
            var p3 = PuntosControl[3];

            // Primera iteración
            var p01 = new PointF(p0.X + t * (p1.X - p0.X), p0.Y + t * (p1.Y - p0.Y));
            var p12 = new PointF(p1.X + t * (p2.X - p1.X), p1.Y + t * (p2.Y - p1.Y));
            var p23 = new PointF(p2.X + t * (p3.X - p2.X), p2.Y + t * (p3.Y - p2.Y));

            // Segunda iteración
            var p012 = new PointF(p01.X + t * (p12.X - p01.X), p01.Y + t * (p12.Y - p01.Y));
            var p123 = new PointF(p12.X + t * (p23.X - p12.X), p12.Y + t * (p23.Y - p12.Y));

            // Punto de subdivisión
            var p0123 = new PointF(p012.X + t * (p123.X - p012.X), p012.Y + t * (p123.Y - p012.Y));

            // Crear las dos curvas
            var primera = new CurvaBezier(Resolucion);
            primera.AgregarPuntoControl(p0);
            primera.AgregarPuntoControl(p01);
            primera.AgregarPuntoControl(p012);
            primera.AgregarPuntoControl(p0123);

            var segunda = new CurvaBezier(Resolucion);
            segunda.AgregarPuntoControl(p0123);
            segunda.AgregarPuntoControl(p123);
            segunda.AgregarPuntoControl(p23);
            segunda.AgregarPuntoControl(p3);

            return (primera, segunda);
        }
    }
}
