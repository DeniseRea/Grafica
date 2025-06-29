using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReaDenise_Leccion2
{
    public class AnalisisEuleriano
    {
        public static bool TieneCicloEuleriano(List<Point[]> lineas)
        {
            var gradoVertices = new Dictionary<Point, int>();

            foreach (var linea in lineas)
            {
                if (!gradoVertices.ContainsKey(linea[0])) gradoVertices[linea[0]] = 0;
                if (!gradoVertices.ContainsKey(linea[1])) gradoVertices[linea[1]] = 0;

                gradoVertices[linea[0]]++;
                gradoVertices[linea[1]]++;
            }

            return gradoVertices.Values.All(grado => grado % 2 == 0);
        }

        public static bool TieneSenderoEuleriano(List<Point[]> lineas)
        {
            var gradoVertices = new Dictionary<Point, int>();

            foreach (var linea in lineas)
            {
                if (!gradoVertices.ContainsKey(linea[0])) gradoVertices[linea[0]] = 0;
                if (!gradoVertices.ContainsKey(linea[1])) gradoVertices[linea[1]] = 0;

                gradoVertices[linea[0]]++;
                gradoVertices[linea[1]]++;
            } 

            int verticesGradoImpar = gradoVertices.Values.Count(grado => grado % 2 == 1);

            return verticesGradoImpar == 0 || verticesGradoImpar == 2;
        }
    }
}
