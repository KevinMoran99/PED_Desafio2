using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace PED_Desafio2_MA171622_MG171623
{
    class ALV
    {
        public char valor;
        public ALV NodoIzquierdo;
        public ALV NodoDerecho;
        public ALV NodoPadre;
        public int altura;
        public Rectangle prueba;
        private DibujaALV arbol;

        public ALV()
        {

        }

        public DibujaALV Arbol
        {
            get { return arbol; }
            set { arbol = value; }
        }

        public ALV(char valorNuevo, ALV izquierdo, ALV derecho, ALV padre)
        {
            valor = valorNuevo;
            NodoIzquierdo = izquierdo;
            NodoDerecho = derecho;
            NodoPadre = padre;
            altura = 0;
        }

        public ALV Insertar(char valorNuevo, ALV Raiz)
        {
            if (Raiz == null)
            {
                Raiz = new ALV(valorNuevo, null, null, null);
            }
            else if (valorNuevo < Raiz.valor)
            {
                Raiz.NodoIzquierdo = Insertar(valorNuevo, Raiz.NodoIzquierdo);
            }
            else if (valorNuevo > Raiz.valor)
            {
                Raiz.NodoDerecho = Insertar(valorNuevo, Raiz.NodoDerecho);
            }
            else
            {
                MessageBox.Show("Valor existente en el árbol", "Error", MessageBoxButtons.OK);
            }
            
            //Rotaciones
            if (Alturas(Raiz.NodoIzquierdo) - Alturas(Raiz.NodoDerecho) == 2)
            {
                if (valorNuevo < Raiz.NodoIzquierdo.valor)
                {
                    //MessageBox.Show("Se realizará una rotación izquierda simple");
                    Raiz = RotacionIzquierdaSimple(Raiz);
                }
                else
                {
                    //MessageBox.Show("Se realizará una rotación izquierda doble");
                    Raiz = RotacionIzquierdaDoble(Raiz);
                }
            }

            if (Alturas(Raiz.NodoDerecho) - Alturas(Raiz.NodoIzquierdo) == 2)
            {
                if (valorNuevo > Raiz.NodoDerecho.valor)
                {
                    //MessageBox.Show("Se realizará una rotación derecha simple");
                    Raiz = RotacionDerechaSimple(Raiz);
                }
                else
                {
                    //MessageBox.Show("Se realizará una rotación derecha doble");
                    Raiz = RotacionDerechaDoble(Raiz);
                }
            }

            Raiz.altura = max(Alturas(Raiz.NodoIzquierdo), Alturas(Raiz.NodoDerecho)) + 1;

            return Raiz;
        }

        private static int max(int lhs, int rhs)
        {
            return lhs > rhs ? lhs : rhs;
        }
        private static int Alturas(ALV Raiz)
        {
            return Raiz == null ? -1 : Raiz.altura;
        }

        ALV nodoE, nodoP;
        public ALV Eliminar(char valorEliminar, ref ALV Raiz)
        {
            if(Raiz != null)
            {
                if(valorEliminar < Raiz.valor)
                {
                    nodoE = Raiz;
                    Eliminar(valorEliminar, ref Raiz.NodoIzquierdo);
                }
                else
                {
                    if(valorEliminar > Raiz.valor)
                    {
                        nodoE = Raiz;
                        Eliminar(valorEliminar, ref Raiz.NodoDerecho);
                    }
                    else
                    {
                        nodoE = Raiz;
                        if(nodoE.NodoDerecho == null)
                        {
                            Raiz = nodoE.NodoIzquierdo;

                            
                            if(Alturas(nodoE.NodoIzquierdo) - Alturas(nodoE.NodoDerecho) == 2)
                            {
                                if(valorEliminar < nodoE.valor)
                                {
                                    //MessageBox.Show("Se realizará una rotación izquierda simple");
                                    nodoP = RotacionIzquierdaSimple(nodoE);
                                }
                                else
                                {
                                    //MessageBox.Show("Se realizará una rotación derecha simple");
                                    nodoE = RotacionDerechaSimple(nodoE);
                                }
                            }
                            if(Alturas(nodoE.NodoDerecho) - Alturas(nodoE.NodoIzquierdo) == 2)
                            {
                                if(valorEliminar > nodoE.NodoDerecho.valor)
                                {
                                    //MessageBox.Show("Se realizarán dos rotaciones derechas simples");
                                    nodoE = RotacionDerechaSimple(nodoE);
                                }
                                else
                                {
                                    //MessageBox.Show("Se realizará una rotación derecha doble y luego una rotación derecha simple");
                                    nodoE = RotacionDerechaDoble(nodoE);
                                }
                                nodoP = RotacionDerechaSimple(nodoE);
                            }
                        }
                        else
                        {
                            if(nodoE.NodoIzquierdo == null)
                            {
                                Raiz = nodoE.NodoDerecho;
                            }
                            else
                            {
                                if(Alturas(Raiz.NodoIzquierdo) - Alturas(Raiz.NodoDerecho) > 0)
                                {
                                    ALV AuxiliarNodo = null;
                                    ALV Auxiliar = Raiz.NodoIzquierdo;
                                    bool Bandera = false;
                                    while (Auxiliar.NodoDerecho != null)
                                    {
                                        AuxiliarNodo = Auxiliar;
                                        Auxiliar = Auxiliar.NodoDerecho;
                                        Bandera = true;
                                    }
                                    Raiz.valor = Auxiliar.valor;
                                    nodoE = Auxiliar;
                                    if(Bandera)
                                    {
                                        AuxiliarNodo.NodoDerecho = Auxiliar.NodoIzquierdo;
                                    }
                                    else
                                    {
                                        Raiz.NodoIzquierdo = Auxiliar.NodoIzquierdo;
                                    }
                                }
                                else
                                {
                                    if(Alturas(Raiz.NodoDerecho) - Alturas(Raiz.NodoIzquierdo) > 0)
                                    {
                                        ALV AuxiliarNodo = null;
                                        ALV Auxiliar = Raiz.NodoDerecho;
                                        bool Bandera = false;
                                        while(Auxiliar.NodoIzquierdo != null)
                                        {
                                            AuxiliarNodo = Auxiliar;
                                            Auxiliar = Auxiliar.NodoIzquierdo;
                                            Bandera = true;
                                        }
                                        Raiz.valor = Auxiliar.valor;
                                        nodoE = Auxiliar;
                                        if (Bandera)
                                        {
                                            AuxiliarNodo.NodoIzquierdo = Auxiliar.NodoDerecho;
                                        }
                                        else
                                        {
                                            Raiz.NodoDerecho = Auxiliar.NodoDerecho;
                                        }
                                    }

                                    else
                                    {
                                        if(Alturas(Raiz.NodoDerecho) - Alturas(Raiz.NodoIzquierdo) == 0)
                                        {
                                            ALV AuxiliarNodo = null;
                                            ALV Auxiliar = Raiz.NodoIzquierdo;
                                            bool Bandera = false;
                                            while (Auxiliar.NodoDerecho != null)
                                            {
                                                AuxiliarNodo = Auxiliar;
                                                Auxiliar = Auxiliar.NodoDerecho;
                                                Bandera = true;
                                            }
                                            Raiz.valor = Auxiliar.valor;
                                            nodoE = Auxiliar;
                                            if (Bandera)
                                            {
                                                AuxiliarNodo.NodoDerecho = Auxiliar.NodoIzquierdo;
                                            }
                                            else
                                            {
                                                Raiz.NodoIzquierdo = Auxiliar.NodoIzquierdo;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Nodo inexistente en el árbol", "Error", MessageBoxButtons.OK);
            }

            return nodoP;
        }

        private static ALV RotacionIzquierdaSimple(ALV k2)
        {
            ALV k1 = k2.NodoIzquierdo;
            k2.NodoIzquierdo = k1.NodoDerecho;
            k1.NodoDerecho = k2;
            k2.altura = max(Alturas(k2.NodoIzquierdo), Alturas(k2.NodoDerecho)) + 1;
            k1.altura = max(Alturas(k1.NodoIzquierdo), k2.altura) + 1;
            return k1;
        }
        private static ALV RotacionDerechaSimple(ALV k1)
        {
            ALV k2 = k1.NodoDerecho;
            k1.NodoDerecho = k2.NodoIzquierdo;
            k2.NodoIzquierdo = k1;
            k1.altura = max(Alturas(k1.NodoIzquierdo), Alturas(k1.NodoDerecho)) + 1;
            k2.altura = max(Alturas(k2.NodoDerecho), k1.altura) + 1;
            return k2;
        }
        private static ALV RotacionIzquierdaDoble(ALV k3)
        {
            k3.NodoIzquierdo = RotacionDerechaSimple(k3.NodoIzquierdo);
            return RotacionIzquierdaSimple(k3);
        }
        private static ALV RotacionDerechaDoble(ALV k1)
        {
            k1.NodoDerecho = RotacionIzquierdaSimple(k1.NodoDerecho);
            return RotacionDerechaSimple(k1);
        }
        public static int getAltura(ALV nodoActual)
        {
            if (nodoActual == null)
            {
                return 0;
            }
            else
            {
                return 1 + Math.Max(getAltura(nodoActual.NodoIzquierdo), getAltura(nodoActual.NodoDerecho));
            }
        }

        public void buscar(int valorBuscar, ALV Raiz)
        {
            if (Raiz != null)
            {
                if (valorBuscar < Raiz.valor)
                {
                    buscar(valorBuscar, Raiz.NodoIzquierdo);
                }
                else
                {
                    if (valorBuscar > Raiz.valor)
                    {
                        buscar(valorBuscar, Raiz.NodoDerecho);
                    }
                }
            }
            else
            {
                MessageBox.Show("Valor no encontrado", "Error", MessageBoxButtons.OK);
            }
        }


        //FUNCIONES PARA DIBUJAR EL ARBOL

        private const int Radio = 30;
        private const int DistanciaH = 40;
        private const int DistanciaV = 10;

        private int CoordenadaX;
        private int CoordenadaY;

        public void PosicionNodo(ref int xmin, int ymin)
        {
            int aux1, aux2;

            CoordenadaY = (int)(ymin + Radio / 2);

            if (NodoIzquierdo != null)
            {
                NodoIzquierdo.PosicionNodo(ref xmin, ymin + Radio + DistanciaV);
            }
            if ((NodoIzquierdo != null) && (NodoDerecho != null))
            {
                xmin += DistanciaH;
            }

            if (NodoDerecho != null)
            {
                NodoDerecho.PosicionNodo(ref xmin, ymin + Radio + DistanciaV);
            }

            if (NodoIzquierdo != null)
            {
                if (NodoDerecho != null)
                {
                    CoordenadaX = (int)((NodoIzquierdo.CoordenadaX + NodoDerecho.CoordenadaX) / 2);
                }
                else
                {
                    aux1 = NodoIzquierdo.CoordenadaX;
                    NodoIzquierdo.CoordenadaX = CoordenadaX - 40;
                    CoordenadaX = aux1;
                }
            }
            else if (NodoDerecho != null)
            {
                aux2 = NodoDerecho.CoordenadaX;
                NodoDerecho.CoordenadaX = CoordenadaX + 40;
                CoordenadaX = aux2;
            }
            else
            {
                CoordenadaX = (int)(xmin + Radio / 2);
                xmin += Radio;
            }
        }

        public void DibujarRamas(Graphics grafo, Pen Lapiz)
        {
            if (NodoIzquierdo != null)
            {
                grafo.DrawLine(Lapiz, CoordenadaX, CoordenadaY, NodoIzquierdo.CoordenadaX, NodoIzquierdo.CoordenadaY);
                NodoIzquierdo.DibujarRamas(grafo, Lapiz);
            }
            if (NodoDerecho != null)
            {
                grafo.DrawLine(Lapiz, CoordenadaX, CoordenadaY, NodoDerecho.CoordenadaX, NodoDerecho.CoordenadaY);
                NodoDerecho.DibujarRamas(grafo, Lapiz);
            }
        }

        public void DibujarNodo(Graphics grafo, Font fuente, Brush Relleno, Brush RellenoFuente, Pen Lapiz, char dato, Brush encuentro)
        {
            Rectangle rect = new Rectangle((int)(CoordenadaX - Radio / 2),
                (int)(CoordenadaY - Radio / 2),
                Radio, Radio);
            
            if(valor == dato)
            {
                grafo.FillEllipse(encuentro, rect);
            }
            else
            {
                grafo.FillEllipse(encuentro, rect);
                grafo.FillEllipse(Relleno, rect);
            }
            grafo.DrawEllipse(Lapiz, rect);

            StringFormat formato = new StringFormat();

            formato.Alignment = StringAlignment.Center;
            formato.LineAlignment = StringAlignment.Center;
            grafo.DrawString(valor.ToString(), fuente,Brushes.Black, CoordenadaX,CoordenadaY,formato);

            if (NodoIzquierdo != null)
            {
                NodoIzquierdo.DibujarNodo(grafo, fuente, Brushes.YellowGreen, RellenoFuente, Lapiz, dato, encuentro);
            }
            if (NodoDerecho != null)
            {
                NodoDerecho.DibujarNodo(grafo, fuente, Brushes.Yellow, RellenoFuente, Lapiz, dato, encuentro);
            }
        }

        public void colorear(Graphics grafo, Font fuente, Brush Relleno, Brush RellenoFuente, Pen Lapiz)
        {
            Rectangle rect = new Rectangle((int)(CoordenadaX - Radio / 2),
                (int)(CoordenadaY - Radio / 2),
                Radio, Radio);
            prueba = new Rectangle((int)(CoordenadaX - Radio / 2),
                (int)(CoordenadaY - Radio / 2),
                Radio, Radio);

            StringFormat formato = new StringFormat();
            formato.Alignment = StringAlignment.Center;
            formato.LineAlignment = StringAlignment.Center;

            grafo.DrawEllipse(Lapiz, rect);
            grafo.FillEllipse(Brushes.PaleVioletRed, rect);
            grafo.DrawString(valor.ToString(), fuente, Brushes.Black, CoordenadaX, CoordenadaY, formato);
        }


        public static string preOrder(ALV t, ref string output)
        {
            if (t != null)
            {
                output += t.valor + " ";
                preOrder(t.NodoIzquierdo, ref output);
                preOrder(t.NodoDerecho, ref output);
            }

            return output;
        }

        public static string inOrder(ALV t, ref string output)
        {
            if (t != null)
            {
                inOrder(t.NodoIzquierdo, ref output);
                output += t.valor + " ";
                inOrder(t.NodoDerecho, ref output);
            }

            return output;
        }

        public static string postOrder(ALV t, ref string output)
        {
            if (t != null)
            {
                postOrder(t.NodoIzquierdo, ref output);
                postOrder(t.NodoDerecho, ref output);
                output += t.valor + " ";
            }

            return output;
        }
    }
}
