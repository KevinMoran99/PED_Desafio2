using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PED_Desafio2_MA171622_MG171623
{
    class DibujaALV
    {
        public ALV Raiz;
        public ALV aux;

        public DibujaALV()
        {
            aux = new ALV();
        }
        public DibujaALV(ALV RaizNueva)
        {
            Raiz = RaizNueva;
        }

        public void Insertar(char dato)
        {
            if (Raiz == null)
            {
                Raiz = new ALV(dato, null, null, null);
            }
            else
            {
                Raiz = Raiz.Insertar(dato, Raiz);
            }
        }

        public void  Eliminar(char dato)
        {
            if (Raiz == null)
            {
               Raiz = new ALV(dato, null, null, null);
            }
            else
            {
                Raiz.Eliminar(dato, ref Raiz);
            }
        }

        public string preOrder()
        {
            string output = "";
            ALV.preOrder(Raiz, ref output);
            return output;
        }
        public string inOrder()
        {
            string output = "";
            ALV.inOrder(Raiz, ref output);
            return output;
        }
        public string postOrder()
        {
            string output = "";
            ALV.postOrder(Raiz, ref output);
            return output;
        }

        private const int Radio = 30;
        private const int DistanciaH = 40;
        private const int DistanciaV = 10;

        private int CoordenadaX;
        private int CoordenadaY;

        public void PosicionNodoRecorrido(ref int xmin, ref int ymin)
        {
            CoordenadaX = (int)(ymin + Radio / 2);
            CoordenadaY = (int)(xmin + Radio / 2);
            xmin += Radio;
        }

        public void colorear(Graphics grafo, Font fuente, Brush Relleno, Brush RellenoFuente, Pen Lapiz, ALV Raiz, bool post, bool inor, bool preor)
        {
            Brush entorno = Brushes.Red;
            if (inor)
            {
                if (Raiz != null)
                {
                    colorear(grafo, fuente, Brushes.Blue, RellenoFuente, Lapiz, Raiz.NodoIzquierdo, post, inor, preor);
                    Raiz.colorear(grafo, fuente, entorno, RellenoFuente, Lapiz);
                    Thread.Sleep(500);
                    Raiz.colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz);
                    colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz, Raiz.NodoDerecho, post, inor, preor);
                }
            }
            else if (preor)
            {
                if (Raiz != null)
                {
                    Raiz.colorear(grafo, fuente, Brushes.Yellow, Brushes.Blue, Pens.Black);
                    Thread.Sleep(500);
                    Raiz.colorear(grafo, fuente, Brushes.White, Brushes.Blue, Pens.Black);
                    colorear(grafo, fuente, Brushes.Blue, RellenoFuente, Lapiz, Raiz.NodoIzquierdo, post, inor, preor);
                    colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz, Raiz.NodoDerecho, post, inor, preor);
                }
            }
            else if (post)
            {
                if (Raiz != null)
                {
                    colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz, Raiz.NodoIzquierdo, post, inor, preor);
                    colorear(grafo, fuente, Relleno, RellenoFuente, Lapiz, Raiz.NodoDerecho, post, inor, preor);
                    Raiz.colorear(grafo, fuente, entorno, RellenoFuente, Lapiz);
                    Thread.Sleep(500);
                    Raiz.colorear(grafo, fuente, entorno, RellenoFuente, Lapiz);
                }
            }
        }

        public void colorearB(Graphics grafo,Font fuente,Brush Relleno, Brush RellenoFuente, Pen Lapiz, ALV Raiz, int busqueda)
        {
            Brush entorno = Brushes.Red;
            if(Raiz != null)
            {
                Raiz.colorear(grafo, fuente, entorno, RellenoFuente, Lapiz);

                if (busqueda < Raiz.valor)
                {
                    Thread.Sleep(500);
                    Raiz.colorear(grafo, fuente, entorno, Brushes.Black, Lapiz);
                    colorearB(grafo, fuente, Relleno, RellenoFuente, Lapiz, Raiz.NodoIzquierdo, busqueda);
                }
                else
                {
                    if (busqueda > Raiz.valor)
                    {
                        Thread.Sleep(500);
                        Raiz.colorear(grafo, fuente, entorno, RellenoFuente, Lapiz);
                        colorearB(grafo, fuente, Relleno, RellenoFuente, Lapiz, Raiz.NodoDerecho, busqueda);
                    }
                    else
                    {
                        Raiz.colorear(grafo, fuente, entorno, RellenoFuente, Lapiz);
                        Thread.Sleep(500);
                    }
                }
            }
        }

        public void DibujarArbol(Graphics grafo, Font fuente, Brush Relleno, Brush RellenoFuente,Pen Lapiz,char dato, Brush encuentro)
        {
            int x = 100;
            int y = 75;
            if (Raiz == null) return;

            Raiz.PosicionNodo(ref x, y);
            Raiz.DibujarRamas(grafo, Lapiz);
            Raiz.DibujarNodo(grafo, fuente, Relleno, RellenoFuente, Lapiz, dato, encuentro);
        }

        public int x1 = 100;
        public int y2 = 75;
        public void restablecerValores()
        {
            x1 = 100;
            y2 = 75;
        }

        public void buscar(int x)
        {
            if (Raiz == null)
            {
                MessageBox.Show("Árbol AVL Vacío", "Error", MessageBoxButtons.OK);
            }
            else
            {
                Raiz.buscar(x, Raiz);
            }
        }
    }
}
