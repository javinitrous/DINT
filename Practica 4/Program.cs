using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Console;

namespace Edu.IesPacoMolla.Practica4
{
    public class SinonimoDic : DictionaryBase
    {
        private ArrayList Posicion = new ArrayList();

        // Esta función privada nos devuelve la posición de la clave ID dentro del diccionario
        private int KeyPosition(string ID)
        {
            int i = 0;
            foreach (string cadena in Dictionary.Keys)
            {
                if (!(cadena.Equals(ID)))
                    i++;
                else
                    break;
            }
            return i;
        }

        // Esta función añade un objeto ArrayList al diccionario
        private void Add(string newID, ArrayList nuevoArrayList)
        {
            Dictionary.Add(newID, nuevoArrayList);
            Posicion.Add(0);
        }

        public String this[string ID]
        {
            get
            {

                ArrayList MiArray = new ArrayList();

                // EXISTE EL INDICE...
                if (Dictionary[ID] != null)
                {
                    // NO HAY SINONIMOS
                    if (((ArrayList)Dictionary[ID]).Count == 0)
                    {
                        return "La palabra " + ID + " no tiene sinónimo alguno.";
                    }
                    else
                    {
                        // Obtengo la posición de la clave para saber que sinónimo debo devolver
                        int i = KeyPosition(ID);

                        // Especifico qué posicion será la siguiente
                        Posicion[i] = (Int32)Posicion[i] + 1;  // el casting se hace porque el arraylist almacena objetos
                        MiArray = (ArrayList)Dictionary[ID];    // el casting se hace porque el arraylist almacena objetos

                        // Comprobar si la posición está fuera de rango
                        if ((Int32)Posicion[i] <= MiArray.Count)
                            return "El Siguiente SINÓNIMO de la palabra " + ID + " ocupa la posición  " + (Int32)Posicion[i] + " con el valor : " + (String)MiArray[(Int32)Posicion[i] - 1];
                        else
                        {
                            // Volvemos a empezar por el primer sinónimo
                            Posicion[i] = 0;
                            return "Ya se ha acabado los sinonimos para la palabra " + ID + ". Reseteamos el contador de sinonimos";
                        }
                    }
                }
                else
                {
                    // NO EXISTE EL INDICE...
                    // Se crea una entrada sin sinónimos.
                    Add(ID, MiArray);
                    return "No existe la palabra " + ID + ". Se añade al diccionario sin ningún sinónimo";
                }
            }
            set
            {
                ArrayList MiArray = new ArrayList();

                // El ArrayList no existe por lo que se crea
                if (Dictionary[ID] == null)
                {
                    WriteLine($"Añadiendo el primer valor de {ID}");
                    this.Add(ID, MiArray);
                }

                // Añadimos un nuevo valor al ArrayList
                MiArray = (ArrayList)Dictionary[ID];
                MiArray.Add(value);
                Dictionary[ID] = MiArray;
                WriteLine($"Añadido el sinónimo {value} de {ID}");
            }
        }
    }

    class Program
    {

        // Función que solicita un valor hasta que el valor introducido sea un entero
        private static void Validar(out int valor)
        {
            while (!Int32.TryParse(Console.ReadLine(), out valor))
            {
                Console.WriteLine("El valor introducido debe ser un entero");
                Console.Write("Vuelve a introducirlo: ");
            }
        }


        static void Main(string[] args)
        {
            SinonimoDic MiDiccionario = new SinonimoDic();


            int opc;
            string palabra, sinonimo;

            do
            {
                Console.WriteLine(" ---------------------------------- ");
                Console.WriteLine("|\t\tMENU               |");
                Console.WriteLine("|----------------------------------|");
                Console.WriteLine("|1.Introducir un sinonimo          |");
                Console.WriteLine("|2.Obtener un sinónimo             |");
                Console.WriteLine("|0.Salir                           |");
                Console.WriteLine(" ---------------------------------- ");
                Console.Write("\tElige una opción: ");
                Validar(out opc);
                Console.WriteLine(" ---------------------------------- ");

                switch (opc)
                {
                    case 1:
                        Console.WriteLine("Introduce la palabra para introducir un sinónimo");
                        palabra = Console.ReadLine();
                        Console.WriteLine("Introduce el sinónimo de la palabra");
                        sinonimo = Console.ReadLine();
                        MiDiccionario[palabra] = sinonimo;
                        break;
                    case 2:
                        Console.WriteLine("Introduce la palabra para obtener un sinónimo");
                        palabra = Console.ReadLine();
                        WriteLine(MiDiccionario[palabra]);
                        break;
                }

                Console.WriteLine("Pulsa Intro para continuar...");
                Console.ReadLine();
                Console.Clear();
            } while (opc != 0);

        }
    }
}