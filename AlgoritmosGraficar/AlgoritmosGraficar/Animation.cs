﻿/**
 * Clase Animation
 * 
 * Proporciona funcionalidad para animar la ejecución de algoritmos de dibujo.
 * Muestra progresivamente los puntos calculados utilizando un Timer para
 * visualizar el proceso paso a paso.
 *
 * @author Denise Rea
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

public class Animation
{
    private Timer timer;         // Controla el intervalo entre cada paso de la animación
    private int index;           // Índice del punto actual en la animación
    private Bitmap bmp;          // Superficie de dibujo
    private List<Point> puntos;  // Lista de puntos a animar
    private PictureBox pictureBox; // Control donde se muestra la animación
    private Color color;         // Color de los puntos
    private int escala;          // Tamaño de cada punto dibujado

    /**
     * Constructor que configura la animación
     * @param puntos Array de puntos a animar
     * @param pictureBox Control donde se dibujará la animación
     * @param color Color de los puntos
     * @param escala Tamaño de cada punto dibujado
     * @param intervalo Tiempo en milisegundos entre cada punto
     */
    public Animation(Point[] puntos, PictureBox pictureBox, Color color, int escala = 10, int intervalo = 1000)
    {
        this.puntos = new List<Point>(puntos);
        this.pictureBox = pictureBox;
        this.color = color;
        this.escala = escala;
        this.index = 0;

        bmp = new Bitmap(pictureBox.Width, pictureBox.Height);

        // Dibuja ejes y fondo
        using (Graphics g = Graphics.FromImage(bmp))
        {
            g.Clear(Color.White);
            int centroX = bmp.Width / 2;
            int centroY = bmp.Height / 2;
            using (Pen ejesPen = new Pen(Color.LightGray, 1))
            {
                g.DrawLine(ejesPen, 0, centroY, bmp.Width, centroY);
                g.DrawLine(ejesPen, centroX, 0, centroX, bmp.Height);
            }
        }

        pictureBox.Image = bmp;
        pictureBox.Refresh();

        timer = new Timer();
        timer.Interval = intervalo;
        timer.Tick += Timer_Tick;
    }

    /**
     * Inicia la animación
     */
    public void Iniciar()
    {
        timer.Start();
    }

    /**
     * Detiene la animación y libera recursos
     */
    public void Detener()
    {
        timer.Stop();
        timer.Dispose();
    }

    /**
     * Evento que se ejecuta en cada intervalo del Timer
     * Dibuja el siguiente punto de la animación
     */
    private void Timer_Tick(object sender, EventArgs e)
    {
        if (index >= puntos.Count)
        {
            Detener();
            return;
        }

        int centroX = bmp.Width / 2;
        int centroY = bmp.Height / 2;
        using (Graphics g = Graphics.FromImage(bmp))
        using (SolidBrush brush = new SolidBrush(color))
        {
            var punto = puntos[index];
            int escaladoX = punto.X * escala;
            int escaladoY = punto.Y * escala;
            int pixelX = centroX + escaladoX;
            int pixelY = centroY - escaladoY;
            int x = pixelX - escala / 2;
            int y = pixelY - escala / 2;

            if (x >= 0 && x + escala < bmp.Width &&
                y >= 0 && y + escala < bmp.Height)
            {
                g.FillRectangle(brush, x, y, escala, escala);
            }
        }

        pictureBox.Image = bmp;
        pictureBox.Refresh();
        index++;
    }
}
