using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practica5
{
    class EnteroLargo
    {
        private string numero;
        private int longitud;

        public static EnteroLargo operator +(EnteroLargo num1, EnteroLargo num2)
        {

            /*primero tenemos que saber si los dos números pasados como parámetro tienen la misma longitud
              y añadirles tantos ceros a la izquierda como sea necesario para que lo sean*/
            if(num1.longitud != num2.longitud)
            {
                if(num1.longitud > num2.longitud)
                {
                    for(int i = 0; i < num1.longitud - num2.longitud; i++)
                    {
                        num2.numero = "0" + num2.numero;
                    }
                }
                else
                {
                    for(int i = 0; i < num2.longitud - num1.longitud; i++)
                    {
                        num1.numero = "0" + num1.numero;
                    }
                }
            }

            int suma = 0;
            EnteroLargo resultado;
            for(int i = num1.longitud; i > 0; i--)
            {
                suma = Convert.ToInt32 (num1.numero[i].ToString()) + Convert.ToInt32(num2.numero[i].ToString());
                if(suma > 9)
                {

                }
                else
                {

                }
            }
        }
        
    }

    class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
}
