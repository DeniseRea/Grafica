using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CurvasParametricas
{
    /// <summary>
    /// Enumeración de tipos de curvas disponibles.
    /// </summary>
    public enum TipoCurva
    {
        Bezier,
        BSpline
    }

    /// <summary>
    /// Argumentos del evento cuando se completa una curva.
    /// </summary>
    public class CurvaCompletadaEventArgs : EventArgs
    {
        public ICurva Curva { get; }
        public TipoCurva Tipo { get; }

        public CurvaCompletadaEventArgs(ICurva curva, TipoCurva tipo)
        {
            Curva = curva;
            Tipo = tipo;
        }
    }

    /// <summary>
    /// Administrador para el manejo de curvas paramétricas en la aplicación.
    /// Proporciona funcionalidad para crear, dibujar y gestionar curvas.
    /// </summary>
    public class AdministradorCurvas
    {
        #region Campos Privados

        private readonly PictureBox pictureBox;
        private ICurva curvaActual;
        private readonly List<ICurva> curvasCreadas;
        private TipoCurva tipoCurvaSeleccionado;
        private bool mostrandoPuntosControl;
        private bool modoEdicion;
        private int indicePuntoSeleccionado = -1;

        #endregion

        #region Eventos

        /// <summary>
        /// Evento que se dispara cuando se completa una curva.
        /// </summary>
        public event EventHandler<CurvaCompletadaEventArgs> CurvaCompletada;

        /// <summary>
        /// Evento que se dispara cuando cambia el estado del administrador.
        /// </summary>
        public event EventHandler<string> EstadoCambiado;

        #endregion

        #region Propiedades

        /// <summary>
        /// Número total de curvas creadas.
        /// </summary>
        public int NumeroCurvas => curvasCreadas.Count;

        /// <summary>
        /// Indica si hay una curva actualmente en creación.
        /// </summary>
        public bool HayCurvaEnCreacion => curvaActual != null;

        /// <summary>
        /// Tipo de curva actualmente seleccionado.
        /// </summary>
        public TipoCurva TipoCurvaActual => tipoCurvaSeleccionado;

        /// <summary>
        /// Indica si se están mostrando los puntos de control.
        /// </summary>
        public bool MostrandoPuntosControl => mostrandoPuntosControl;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor del administrador de curvas.
        /// </summary>
        /// <param name="pictureBox">PictureBox donde se dibujarán las curvas.</param>
        public AdministradorCurvas(PictureBox pictureBox)
        {
            this.pictureBox = pictureBox ?? throw new ArgumentNullException(nameof(pictureBox));
            curvasCreadas = new List<ICurva>();
            mostrandoPuntosControl = true;
            modoEdicion = false;
            tipoCurvaSeleccionado = TipoCurva.Bezier;
        }

        #endregion

        #region Métodos Públicos

        /// <summary>
        /// Inicia la creación de una nueva curva del tipo especificado.
        /// </summary>
        /// <param name="tipoCurva">Tipo de curva a crear.</param>
        public void IniciarCreacionCurva(string tipoCurva)
        {
            // Completar curva actual si existe
            if (curvaActual != null)
            {
                CompletarCurva();
            }

            // Determinar tipo de curva
            switch (tipoCurva.ToLower())
            {
                case "bézier":
                case "bezier":
                    tipoCurvaSeleccionado = TipoCurva.Bezier;
                    curvaActual = new CurvaBezier();
                    break;
                case "b-spline":
                case "bspline":
                    tipoCurvaSeleccionado = TipoCurva.BSpline;
                    curvaActual = new CurvaBSpline();
                    break;
                default:
                    throw new ArgumentException($"Tipo de curva no soportado: {tipoCurva}");
            }

            // Configurar propiedades de la curva
            if (curvaActual is CurvaBase curvaBase)
            {
                curvaBase.MostrarPuntosControl = mostrandoPuntosControl;
            }

            NotificarEstadoCambiado($"Iniciando creación de curva {tipoCurva}");
        }

        /// <summary>
        /// Agrega un punto de control a la curva actual.
        /// </summary>
        /// <param name="punto">Punto de control a agregar.</param>
        /// <returns>True si se pudo agregar el punto.</returns>
        public bool AgregarPuntoControl(Point punto)
        {
            return AgregarPuntoControl(new PointF(punto.X, punto.Y));
        }

        /// <summary>
        /// Agrega un punto de control a la curva actual.
        /// </summary>
        /// <param name="punto">Punto de control a agregar.</param>
        /// <returns>True si se pudo agregar el punto.</returns>
        public bool AgregarPuntoControl(PointF punto)
        {
            if (curvaActual == null)
                return false;

            try
            {
                // Agregar punto de control
                curvaActual.AgregarPuntoControl(punto);

                // Verificar si la curva se completa automáticamente
                bool curvaCompleta = false;
                if (tipoCurvaSeleccionado == TipoCurva.Bezier && curvaActual.PuntosControl != null && curvaActual.PuntosControl.Count == 4)
                {
                    curvaCompleta = true;
                }

                // Redibujar
                DibujarTodasLasCurvas();

                if (curvaCompleta)
                {
                    CompletarCurva();
                }

                NotificarEstadoCambiado($"Punto agregado. Total: {curvaActual.PuntosControl?.Count ?? 0}");
                return true;
            }
            catch (Exception ex)
            {
                NotificarEstadoCambiado($"Error al agregar punto: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Completa la curva actual y la agrega a la lista de curvas creadas.
        /// </summary>
        public void CompletarCurva()
        {
            if (curvaActual == null || !curvaActual.PuedeDibujar())
                return;

            // Agregar a la lista de curvas completadas
            curvasCreadas.Add(curvaActual);

            // Disparar evento
            CurvaCompletada?.Invoke(this, new CurvaCompletadaEventArgs(curvaActual, tipoCurvaSeleccionado));

            // Limpiar curva actual
            curvaActual = null;

            NotificarEstadoCambiado($"Curva completada. Total de curvas: {curvasCreadas.Count}");
        }

        /// <summary>
        /// Alterna la visibilidad de los puntos de control.
        /// </summary>
        public void AlternarPuntosControl()
        {
            mostrandoPuntosControl = !mostrandoPuntosControl;

            // Actualizar todas las curvas
            foreach (var curva in curvasCreadas)
            {
                if (curva is CurvaBase curvaBase)
                {
                    curvaBase.MostrarPuntosControl = mostrandoPuntosControl;
                }
            }

            if (curvaActual is CurvaBase curvaActualBase)
            {
                curvaActualBase.MostrarPuntosControl = mostrandoPuntosControl;
            }

            DibujarTodasLasCurvas();
            NotificarEstadoCambiado($"Puntos de control: {(mostrandoPuntosControl ? "Visibles" : "Ocultos")}");
        }

        /// <summary>
        /// Limpia todas las curvas creadas.
        /// </summary>
        public void LimpiarCurvas()
        {
            curvasCreadas.Clear();
            curvaActual = null;
            indicePuntoSeleccionado = -1;
            modoEdicion = false;

            if (pictureBox.Image != null)
            {
                using (var g = Graphics.FromImage(pictureBox.Image))
                {
                    g.Clear(Color.White);
                }
                pictureBox.Invalidate();
            }

            NotificarEstadoCambiado("Todas las curvas eliminadas");
        }

        /// <summary>
        /// Dibuja todas las curvas en el PictureBox.
        /// </summary>
        public void DibujarTodasLasCurvas()
        {
            if (pictureBox == null)
                return;

            if (pictureBox.Image == null)
            {
                pictureBox.Image = new Bitmap(Math.Max(1, pictureBox.Width), Math.Max(1, pictureBox.Height));
            }

            using (var g = Graphics.FromImage(pictureBox.Image))
            {
                g.Clear(Color.White);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                // Dibujar curvas completadas
                if (curvasCreadas != null)
                {
                    foreach (var curva in curvasCreadas)
                    {
                        try
                        {
                            curva?.Dibujar(g);
                        }
                        catch
                        {
                            // Continuar si hay error al dibujar una curva
                            continue;
                        }
                    }
                }

                // Dibujar curva en creación
                if (curvaActual != null && curvaActual.PuntosControl != null && curvaActual.PuntosControl.Count > 0)
                {
                    try
                    {
                        // Cambiar color para indicar que está en creación
                        var colorOriginal = Color.Blue;
                        if (curvaActual is CurvaBase curvaBase)
                        {
                            colorOriginal = curvaBase.Color;
                            curvaBase.Color = Color.Orange;
                        }

                        curvaActual.Dibujar(g);

                        // Restaurar color original
                        if (curvaActual is CurvaBase curvaBaseRestore)
                        {
                            curvaBaseRestore.Color = colorOriginal;
                        }
                    }
                    catch
                    {
                        // Si hay error al dibujar la curva actual, continuar
                    }
                }
            }

            pictureBox.Invalidate();
        }

        /// <summary>
        /// Obtiene información del estado actual del administrador.
        /// </summary>
        /// <returns>Cadena con información del estado.</returns>
        public string ObtenerInformacionEstado()
        {
            var info = $"Curvas completadas: {curvasCreadas.Count}";
            
            if (curvaActual != null)
            {
                info += $" - En creación: {tipoCurvaSeleccionado} ({curvaActual.PuntosControl.Count} puntos)";
            }

            return info;
        }

        /// <summary>
        /// Obtiene el número de puntos restantes para completar la curva actual.
        /// </summary>
        /// <returns>Número de puntos restantes, o -1 si no hay curva en creación.</returns>
        public int ObtenerPuntosRestantes()
        {
            if (curvaActual == null)
                return -1;

            switch (tipoCurvaSeleccionado)
            {
                case TipoCurva.Bezier:
                    return Math.Max(0, 4 - curvaActual.PuntosControl.Count);
                case TipoCurva.BSpline:
                    return curvaActual.PuntosControl.Count < 4 ? 4 - curvaActual.PuntosControl.Count : 0;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Encuentra la curva más cercana a un punto dado.
        /// </summary>
        /// <param name="punto">Punto de referencia.</param>
        /// <param name="tolerancia">Tolerancia de distancia.</param>
        /// <returns>Curva más cercana o null si ninguna está dentro de la tolerancia.</returns>
        public ICurva EncontrarCurvaCercana(PointF punto, float tolerancia = 10.0f)
        {
            ICurva curvaCercana = null;
            float distanciaMinima = float.MaxValue;

            foreach (var curva in curvasCreadas)
            {
                var puntoCurva = curva.PuntoMasCercano(punto);
                var distancia = CalcularDistancia(punto, puntoCurva);

                if (distancia < tolerancia && distancia < distanciaMinima)
                {
                    distanciaMinima = distancia;
                    curvaCercana = curva;
                }
            }

            return curvaCercana;
        }

        /// <summary>
        /// Exporta todas las curvas a una lista de puntos.
        /// </summary>
        /// <returns>Diccionario con las curvas y sus puntos calculados.</returns>
        public Dictionary<string, List<PointF>> ExportarCurvas()
        {
            var resultado = new Dictionary<string, List<PointF>>();

            for (int i = 0; i < curvasCreadas.Count; i++)
            {
                var curva = curvasCreadas[i];
                var nombreCurva = $"{curva.GetType().Name}_{i + 1}";
                resultado[nombreCurva] = curva.CalcularPuntosCurva();
            }

            return resultado;
        }

        #endregion

        #region Métodos Privados

        /// <summary>
        /// Notifica un cambio de estado a los suscriptores.
        /// </summary>
        /// <param name="mensaje">Mensaje del cambio de estado.</param>
        private void NotificarEstadoCambiado(string mensaje)
        {
            EstadoCambiado?.Invoke(this, mensaje);
        }

        /// <summary>
        /// Calcula la distancia entre dos puntos.
        /// </summary>
        /// <param name="p1">Primer punto.</param>
        /// <param name="p2">Segundo punto.</param>
        /// <returns>Distancia entre los puntos.</returns>
        private float CalcularDistancia(PointF p1, PointF p2)
        {
            var dx = p2.X - p1.X;
            var dy = p2.Y - p1.Y;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }

        #endregion
    }
}
