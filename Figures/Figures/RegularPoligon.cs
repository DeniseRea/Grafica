using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Figures
{
    public abstract class RegularPolygon
    {
        protected int NumberOfSides;
        protected double Base;
        protected double height;  // Altura del polígono regular
        protected double Area;
        protected double Perimeter;
        protected double Radius;

        public double getArea()
        {
            return Area;
        }

        public double getPerimeter()
        {
            return Perimeter;
        }

        public double getHeight()
        {
            return height;
        }

        public void setArea(double area)
        {
            this.Area = area;
        }

        public void setHeight(double height)
        {
            this.height = height;
        }

        public double getRadius()
        {
            return Radius;
        }

        public double getBase()
        {
            return Base;
        }

        public RegularPolygon(int numberOfSides, double base_, double radious)
        {
            this.NumberOfSides = numberOfSides;
            this.Base = base_;
            this.Radius = radious;
            //this.height = radious* (Math.PI / 180) * (180.0 / numberOfSides);
        }

        public RegularPolygon()
        {
        }

        public void CalculatePerimeter()
        {
            Perimeter= NumberOfSides * Base;  // Método abstracto
        }

        public void clean(TextBox Base, TextBox height, TextBox area, TextBox perimeter, TextBox apotema)
        {
            Base.Text = "";
            height.Text = "";
            area.Text = "";
            perimeter.Text = "";
            apotema.Text = "";

        }


        public void close(Form form)
        {
            form.Close();
        }

        public abstract void calculateApothema();
        public abstract void CalculateArea();  // Método abstracto
    }
}
