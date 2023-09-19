using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Controle
{
   public class Aleatorios
    {
        private Random _random;
        public Aleatorios()
        {
            this._random = new Random();
        }
        //generates random numbers
        public int GeradorAleatorio(int min, int max)
        {
            if (min > max)
            {
                int aux = min;
                min = max;
                max = aux;
            }
            return this._random.Next(min, max + 1);
        }
        public int[] GeradorAleatorios(int longetud, int min, int max)
        {
            if (longetud <= 0)
            {
                return null;
            }
            int[] numeros = new int[longetud];

            for (int i = 0; i < numeros.Length; i++)
            {
                numeros[i] = GeradorAleatorio(min, max);
            }
            return numeros;
        }
        public int[] GeradorAleatoriosExcluidos(int longetud, int min, int max)
        {
            if (min > max)
            {
                int aux = min;
                min = max;
                max = aux;
            }


            if (longetud <= 0 || (max - min) < longetud - 1)
            {
                return null;
            }
            int[] numeros = new int[longetud];

            bool repetido;
            int numero;
            int indice = 0;
            while (indice < numeros.Length)
            {
                repetido = false;
                numero = GeradorAleatorio(min, max);

                for (int i = 0; i < indice; i++)
                {
                    if (numeros[i] == numero)
                    {
                        repetido = true;
                    }
                }

                if (!repetido)
                {
                    numeros[indice] = numero;
                    indice++;
                }
            }

            return numeros;
        }
        public int[] GeradorAleatoriosFixos(int longetud, int min, int max, Label[] removido)
        {
            if (min > max)
            {
                int aux = min;
                min = max;
                max = aux;
            }


            if (longetud <= 0 || (max - min) < longetud - 1)
            {
                return null;
            }
            int[] numeros = new int[longetud];

            bool repetido;
            int numero;
            int indice = 0;
            int indicerem = removido.Length;
            while (indice < numeros.Length)
            {
                repetido = false;
                numero = GeradorAleatorio(min, max);
                for (int i = 0; i < indice; i++)
                {
                    if (numeros[i] == numero)
                    {
                        repetido = true;
                    }
                }

                for (int x = 0; x < indicerem; x++)
                {

                    if (Convert.ToInt32(removido[x].Text) == numero)
                    {
                        repetido = true;
                        break;
                    }

                }
                if (!repetido)
                {
                    numeros[indice] = numero;
                    indice++;
                }
            }

            return numeros;
        }
        public int[] GeradorAleatoriosFixos2(int longetud, int min, int max)
        {
            if (min > max)
            {
                int aux = min;
                min = max;
                max = aux;
            }

            if (longetud <= 0 || (max - min) < longetud - 1)
            {
                return null;
            }
            int[] numeros = new int[longetud];

            bool repetido;
            int numero;
            int indice = 0;
            while (indice < numeros.Length)
            {
                repetido = false;
                numero = GeradorAleatorio(min, max);
                for (int i = 0; i < indice; i++)
                {
                    if (numeros[i] == numero)
                    {
                        repetido = true;
                    }
                }
                if (!repetido)
                {
                    numeros[indice] = numero;
                    indice++;
                }
            }

            return numeros;
        }
        public int[] GeradorNaoRepetidos(int longetud, int min, int max, Label[] NumJaEscolhidos, Label[] removido)
        {
            if (min > max)
            {
                int aux = min;
                min = max;
                max = aux;
            }


            if (longetud <= 0 || (max - min) < longetud - 1)
            {
                return null;
            }
            int[] numeros = new int[longetud];

            bool repetido;
            int numero;
            int indice = 0;
            int indiceX = NumJaEscolhidos.Length;
            int indicerem = removido.Length;
            while (indice < numeros.Length)
            {
                repetido = false;
                numero = GeradorAleatorio(min, max);

                for (int i = 0; i < indice; i++)
                {
                    if (numeros[i] == numero)
                    {
                        repetido = true;
                    }
                }
                for (int x = 0; x < indiceX; x++)
                {

                    if (Convert.ToInt32(NumJaEscolhidos[x].Text) == numero)
                    {
                        repetido = true;
                        break;
                    }
                }
                for (int x = 0; x < indicerem; x++)
                {

                    if (Convert.ToInt32(removido[x].Text) == numero)
                    {
                        repetido = true;
                        break;
                    }
                }
                if (!repetido)
                {
                    numeros[indice] = numero;
                    indice++;
                }
            }

            return numeros;
        }
        public int[] GeradorNaoRepetidos2(int longetud, int min, int max, Label[] NumJaEscolhidos)
        {
            if (min > max)
            {
                int aux = min;
                min = max;
                max = aux;
            }

            if (longetud <= 0 || (max - min) < longetud - 1)
            {
                return null;
            }
            int[] numeros = new int[longetud];

            bool repetido;
            int numero;
            int indice = 0;
            int indiceX = NumJaEscolhidos.Length;
            while (indice < numeros.Length)
            {
                repetido = false;
                numero = GeradorAleatorio(min, max);

                for (int i = 0; i < indice; i++)
                {
                    if (numeros[i] == numero)
                    {
                        repetido = true;
                    }
                }
                for (int x = 0; x < indiceX; x++)
                {

                    if (Convert.ToInt32(NumJaEscolhidos[x].Text) == numero)
                    {
                        repetido = true;
                        break;
                    }
                }
                if (!repetido)
                {
                    numeros[indice] = numero;
                    indice++;
                }
            }

            return numeros;
        }
    }
}
