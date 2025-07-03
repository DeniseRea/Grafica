using System;
using System.Collections.Generic;
using System.Drawing;

namespace CurvasParametricas
{
    /// <summary>
    /// Interfaz base para todas las curvas paramétricas.
    /// </summary>
    public interface ICurva
    {
        /// <summary>
        /// Lista de puntos de control que definen la curva.
        /// </summary>
        List<PointF> PuntosControl { get; set; }

        /// <summary>
        /// Resolución de la curva (número de puntos calculados).
        /// </summary>
        int Resolucion { get; set; }

        /// <summary>
        /// Calcula todos los puntos de la curva basados en los puntos de control.
        /// </summary>
        /// <returns>Lista de puntos que forman la curva.</returns>
        List<PointF> CalcularPuntosCurva();

        /// <summary>
        /// Calcula un punto específico de la curva para un parámetro t.
        /// </summary>
        /// <param name="t">Parámetro entre 0 y 1.</param>
        /// <returns>Punto calculado en la curva.</returns>
        PointF CalcularPunto(float t);

        /// <summary>
        /// Agrega un punto de control a la curva.
        /// </summary>
        /// <param name="punto">Punto de control a agregar.</param>
        void AgregarPuntoControl(PointF punto);

        /// <summary>
        /// Elimina todos los puntos de control.
        /// </summary>
        void LimpiarPuntosControl();

        /// <summary>
        /// Verifica si la curva tiene suficientes puntos de control para ser dibujada.
        /// </summary>
        /// <returns>True si se puede dibujar la curva.</returns>
        bool PuedeDibujar();

        /// <summary>
        /// Dibuja la curva en el contexto gráfico especificado.
        /// </summary>
        /// <param name="graphics">Contexto gráfico donde dibujar.</param>
        void Dibujar(Graphics graphics);

        /// <summary>
        /// Obtiene información descriptiva de la curva.
        /// </summary>
        /// <returns>Información sobre la curva como string.</returns>
        string ObtenerInformacion();

        /// <summary>
        /// Crea una copia de la curva actual.
        /// </summary>
        /// <returns>Nueva instancia de la curva.</returns>
        ICurva Clonar();

        /// <summary>
        /// Calcula la longitud aproximada de la curva.
        /// </summary>
        /// <returns>Longitud de la curva.</returns>
        float CalcularLongitud();

        /// <summary>
        /// Encuentra el punto más cercano en la curva a un punto dado.
        /// </summary>
        /// <param name="punto">Punto de referencia.</param>
        /// <returns>Punto más cercano en la curva.</returns>
        PointF PuntoMasCercano(PointF punto);
    }
}
