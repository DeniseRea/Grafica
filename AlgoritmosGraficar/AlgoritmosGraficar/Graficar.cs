/**
 * Clase FrmGraficar
 * 
 * Formulario principal que implementa la interfaz gráfica para los algoritmos de dibujo.
 * Permite seleccionar entre diferentes algoritmos (DDA, Bresenham, Círculo) y
 * visualizar su ejecución en un lienzo gráfico.
 * 
 * Implementa el patrón Singleton para asegurar una única instancia.
 *
 * @author Denise Rea
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgoritmosGraficar
{
    public partial class FrmGraficar : Form
    {
        // Singleton: Campo estático para almacenar la única instancia
        private static FrmGraficar instancia;

        // Singleton: Propiedad para obtener la instancia única
        public static FrmGraficar Instancia
        {
            get
            {
                if (instancia == null || instancia.IsDisposed)
                {
                    instancia = new FrmGraficar();
                }
                return instancia;
            }
        }
        
        private Algoritmo algoritmoSeleccionado;
        private Animation animator;
        
        // Singleton: Constructor privado para evitar instanciación externa
        private FrmGraficar()
        {
            InitializeComponent();

            // Conectar eventos de los RadioButtons
            radBtn_DDA.CheckedChanged += RadioButton_CheckedChanged;
            rdBtn_Bresenham.CheckedChanged += RadioButton_CheckedChanged;
            rdBtn_Circulo.CheckedChanged += RadioButton_CheckedChanged;

            // Conectar eventos para los botones de opciones
            btn_reset.Click += Btn_reset_Click;
            btn_limpiar.Click += Btn_limpiar_Click;

            // Agregar eventos para limpiar TextBoxes al recibir foco
            txt_Xstart.Enter += TextBox_Enter;
            txt_Ystart.Enter += TextBox_Enter;
            txt_Xfinal.Enter += TextBox_Enter;
            txt_Yfinal.Enter += TextBox_Enter;
            txt_Radius.Enter += TextBox_Enter;

            // Seleccionar DDA por defecto
            radBtn_DDA.Checked = true;
        }

        private void Graficar_Load(object sender, EventArgs e)
        {
            // Inicializar el PictureBox
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                g.Clear(Color.White);
            }
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;

            if (radioButton.Checked)
            {
                // Habilitar/deshabilitar campos según el algoritmo seleccionado
                if (radioButton == rdBtn_Circulo)
                {
                    // Configuración para el algoritmo del Círculo
                    txt_Radius.Enabled = true;
                    txt_Radius.Visible = true;
                    lbl_radius.Visible = true;
                    lbl_inicio.Text = "Centro:";
                    txt_Xstart.Text = "0";
                    txt_Ystart.Text = "0";
                    txt_Xfinal.Text = "";
                    txt_Yfinal.Text = "";
                    txt_Xstart.Enabled = false;
                    txt_Ystart.Enabled = false;
                    lbl_final.Visible = false;
                    txt_Xfinal.Visible = false;
                    txt_Yfinal.Visible = false;

                }
                else
                {
                    // Configuración para algoritmos DDA y Bresenham
                    lbl_inicio.Text = "Inicial:";
                    txt_Xstart.Enabled = true;
                    txt_Ystart.Enabled = true;
                    lbl_final.Visible = true;
                    txt_Xfinal.Visible = true;
                    txt_Yfinal.Visible = true;
                    

                    // Colocar el radio abajo o en otra posición
                    lbl_radius.Visible = false;
                    txt_Radius.Visible = false;
                }

                // Mostrar información del algoritmo seleccionado
                txt_output.Text = $"Algoritmo seleccionado: {radioButton.Text}";
            }
        }

        private void Btn_reset_Click(object sender, EventArgs e)
        {
            // Reiniciar todos los campos
            algoritmoSeleccionado.clean(txt_output,txt_Radius,txt_Xfinal,txt_Xstart,txt_Yfinal,txt_Ystart);
        }

        private void Btn_limpiar_Click(object sender, EventArgs e)
        {
            algoritmoSeleccionado.clear(pictureBox1);
            txt_output.Text = "Lienzo limpiado";
        }

        private void check_oti(object sender, EventArgs e)
        {
            
        }

        private void btn_Draw_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que se hayan ingresado los datos necesarios
                if (string.IsNullOrEmpty(txt_Xstart.Text) || string.IsNullOrEmpty(txt_Ystart.Text))
                {
                    MessageBox.Show("Debe ingresar las coordenadas del punto inicial.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Point puntoInicial = new Point(
                    int.Parse(txt_Xstart.Text),
                    int.Parse(txt_Ystart.Text));

                // Crear el algoritmo según el RadioButton seleccionado
                if (radBtn_DDA.Checked)
                {
                    if (string.IsNullOrEmpty(txt_Xfinal.Text) || string.IsNullOrEmpty(txt_Yfinal.Text))
                    {
                        MessageBox.Show("Debe ingresar las coordenadas del punto final.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    Point puntoFinal = new Point(
                        int.Parse(txt_Xfinal.Text),
                        int.Parse(txt_Yfinal.Text));

                    algoritmoSeleccionado = new DDA
                    {
                        PuntoInicial = puntoInicial,
                        PuntoFinal = puntoFinal
                    };
                }
                else if (rdBtn_Bresenham.Checked)
                {
                    if (string.IsNullOrEmpty(txt_Xfinal.Text) || string.IsNullOrEmpty(txt_Yfinal.Text))
                    {
                        MessageBox.Show("Debe ingresar las coordenadas del punto final.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    Point puntoFinal = new Point(
                        int.Parse(txt_Xfinal.Text),
                        int.Parse(txt_Yfinal.Text));

                    algoritmoSeleccionado = new Bresenham
                    {
                        PuntoInicial = puntoInicial,
                        PuntoFinal = puntoFinal
                    };
                }
                else if (rdBtn_Circulo.Checked)
                {
                    if (string.IsNullOrEmpty(txt_Radius.Text))
                    {
                        MessageBox.Show("Debe ingresar el radio del círculo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    algoritmoSeleccionado = new Circulo
                    {
                        PuntoInicial = puntoInicial, // Centro del círculo
                        Radio = int.Parse(txt_Radius.Text)
                    };
                }

                // Calcular los puntos y dibujar
                algoritmoSeleccionado.CalcularPuntos();
                animator = new Animation(algoritmoSeleccionado.Puntos, pictureBox1, Color.Black, escala: 10, intervalo: 100);
                animator.Iniciar();
                //algoritmoSeleccionado.Dibujar(pictureBox1, Color.Black);

                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"Se dibujó {algoritmoSeleccionado.GetType().Name}");
                sb.AppendLine($"Número de pasos: {algoritmoSeleccionado.Puntos.Length}");

                int maxPuntosAMostrar = 100;
                int puntosCount = algoritmoSeleccionado.Puntos.Length;

                for (int i = 0; i < Math.Min(puntosCount, maxPuntosAMostrar); i++)
                {
                    sb.AppendLine($"{i + 1}. ({algoritmoSeleccionado.Puntos[i].X}, {algoritmoSeleccionado.Puntos[i].Y})");
                }

                // Si hay más puntos de los que mostramos, indicarlo
                if (puntosCount > maxPuntosAMostrar)
                {
                    sb.AppendLine($"... y {puntosCount - maxPuntosAMostrar} puntos más");
                }

                txt_output.Text = sb.ToString();
                txt_output.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void TextBox_Enter(object sender, EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // Guardar el texto actual (opcional, si quieres restaurarlo al perder el foco)
                // textBox.Tag = textBox.Text;

                // Limpiar el TextBox
                textBox.Clear();
            }
        }


    }
}
