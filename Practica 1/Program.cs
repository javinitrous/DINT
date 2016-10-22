using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Console;
using static System.Convert;


namespace practica1
{
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

        private static void Main(string[] args)
        {
            // Variables que almacenan la opción seleccionada y los posibles valores requeridos por esa opción
            int opc, valor, valor2;

            Array objeto = new Array();

            do
            {
                Console.WriteLine(" ---------------------------------- ");
                Console.WriteLine("|\t\tMENU               |");
                Console.WriteLine("|----------------------------------|");
                Console.WriteLine("|1.Introducir valor al final       |");
                Console.WriteLine("|2.Introducir valor en una posición|");
                Console.WriteLine("|3.Borrar posición                 |");
                Console.WriteLine("|4.Ordenar                         |");
                Console.WriteLine("|5.Buscar                          |");
                Console.WriteLine("|6.Buscar Mayor                    |");
                Console.WriteLine("|7.Buscar Menor                    |");
                Console.WriteLine("|8.Primero                         |");
                Console.WriteLine("|9.Siguiente                       |");
                Console.WriteLine("|10.Invertir                       |");
                Console.WriteLine("|11.Rotar                          |");
                Console.WriteLine("|12.Mostrar                        |");
                Console.WriteLine("|0.Salir                           |");
                Console.WriteLine(" ---------------------------------- ");
                Console.Write("\tElige una opción: ");
                Validar(out opc);
                Console.WriteLine(" ---------------------------------- ");

                switch (opc)
                {
                    case 1:
                        Console.WriteLine("Introduce el valor que deseas almacenar en el array");
                        Validar(out valor);
                        objeto.Insertar(valor);
                        if (objeto.Error)
                        {
                            objeto.MostrarError(objeto.Error);
                            break;
                        }
                        goto case 12;
                    case 2:
                        Console.WriteLine("Introduce el valor que deseas almacenar en el array");
                        Validar(out valor);
                        Console.WriteLine("Introduce la posición en que lo deseas almacenar");
                        Validar(out valor2);
                        objeto.Insertar(valor, valor2);
                        if (objeto.Error)
                        {
                            objeto.MostrarError(objeto.Error);
                            objeto.Insertar(valor);
                            if (objeto.Error)
                            {
                                objeto.MostrarError(objeto.Error);
                                break;
                            }
                        }
                        goto case 12;
                    case 3:
                        Console.WriteLine("Introduce la posición en que lo deseas eliminar");
                        Validar(out valor2);
                        objeto.Borrar(valor2);
                        if (objeto.Error)
                        {
                            objeto.MostrarError(objeto.Error);
                            break;
                        }
                        goto case 12;
                    case 4:
                        Console.WriteLine("1.Ordenar creciente");
                        Console.WriteLine("2.Ordenar decreciente");
                        Console.Write("Elige un opción: ");
                        Validar(out valor);
                        objeto.OrdenarBurbuja(valor);
                        if (objeto.Error)
                        {
                            objeto.MostrarError(objeto.Error);
                            break;
                        }
                        goto case 12;
                    case 5:
                        Console.WriteLine("Introduzca el valor a buscar");
                        Validar(out valor);
                        valor2 = objeto.Posicion(valor);
                        if (objeto.Error == false)
                            Console.WriteLine("El valor se encuentra en la posición: " + valor2);
                        else
                        {
                            objeto.MostrarError(objeto.Error);
                            break;
                        }
                        goto case 12;
                    case 6:
                        valor = objeto.Mayor();
                        if (objeto.Error == false)
                            Console.WriteLine("El mayor valor del array es: " + valor);
                        else
                        {
                            objeto.MostrarError(objeto.Error);
                            break;
                        }
                        goto case 12;
                    case 7:
                        valor = objeto.Menor();
                        if (objeto.Error == false)
                            Console.WriteLine("El menor valor del array es: " + valor);
                        else
                        {
                            objeto.MostrarError(objeto.Error);
                            break;
                        }
                        goto case 12;
                    case 8:
                        valor = objeto.Primero();
                        if (objeto.Error == false)
                            Console.WriteLine("El primer valor del array es: " + valor);
                        else
                        {
                            objeto.MostrarError(objeto.Error);
                            break;
                        }
                        goto case 12;
                    case 9:
                        valor = objeto.Siguiente();
                        if (objeto.Error == false)
                            Console.WriteLine("El siguiente valor del array es: " + valor);
                        else
                        {
                            objeto.MostrarError(objeto.Error);
                            break;
                        }
                        goto case 12;
                    case 10:
                        objeto.Invertir();
                        if (objeto.Error)
                        {
                            objeto.MostrarError(objeto.Error);
                            break;
                        }
                        goto case 12;
                    case 11:
                        Console.WriteLine("Introduce las posiciones que quieres rotar");
                        Validar(out valor);
                        Console.WriteLine("1.Rotar derecha");
                        Console.WriteLine("2.Rotar izquierda");
                        Console.Write("Elige un opción: ");
                        Validar(out valor2);
                        objeto.Rotar(valor, valor2);
                        if (objeto.Error)
                        {
                            objeto.MostrarError(objeto.Error);
                            break;
                        }
                        goto case 12;
                    case 12:
                        objeto.Mostrar();
                        Console.WriteLine();
                        if (objeto.Error)
                            objeto.MostrarError(objeto.Error);
                        break;
                }
                Console.WriteLine("Pulsa Intro para continuar...");
                Console.ReadLine();
                Console.Clear();
            } while (opc != 0);
        }

        class Array
        {
            private readonly string[] MENSAJES = {
                "Error al introducir datos en una posición que no existe, el valor se introdujo al final del array",
                "Error al eliminar, la posición especificada no existe",
                "Error al ordenar el array, array vacio",
                "Error al buscar valor, el valor especificado no existe en el array",
                "Error al buscar mayor valor, el array está vacío",
                "Error al buscar menor valor, el array está vacío",
                "Error al mostrar primer valor, el array está vacío",
                "Error al mostrar siguiente valor, la posición no existe",
                "Error al invertir array, array vacío",
                "Error al rotar array, array vacío",
                "Error al rotar array, el array solo tiene un elemento",
                "Error al mostrar datos, array vacío"
            };

            private int[] array;
            private int tam, sig;

            public bool Error { get; set; }
            public int CodError { get; set; }


            public Array()
            {
                array = new int[0];
                tam = 0;
                Error = false;
                CodError = 0;
                sig = -1;
            }

            // Muestra el mensaje privado
            private void MsgError(int valor)
            {
                if ((valor <= MENSAJES.Length) && (valor > 0))
                    WriteLine($"{MENSAJES[valor - 1]}");
            }

            /*
             
            // Implementación de la función Insertar sin utilizar métodos de la clase Array
            
            public void Insertar(int valor)                
            {
                   // Como alternativa se podría llamar a la función Insertar de esta forma
                   // Insertar(valor, tam);

                   int []a = new int[++tam];               
                   if (tam > 1)
                   {
                       for (int i = 0; i < tam-1; i++)
                           a[i] = array[i];                
                   }
                   array = a;                              
                   array[tam - 1] = valor;                 

                   // Esta función nunca puede proporcionar un error
                   Error = false;                                           
            }

            public void Insertar(int valor, int posicion)  
            {
                // Si se indica que se quiere insertar en la posición tam, se entiende que lo que se quiere es
                // insertar en la última posición del nuevo array
                if ( (posicion <= tam) && (posicion > 0) )
                {
                    int[] a = new int[++tam];
                    a[posicion] = valor;

                    for (int i = 0; i <= posicion - 1; i++)
                        a[i] = array[i];
    
                    for (int i = posicion + 1; i <= tam - 1; i++)
                        a[i] = array[i-1];

                    array = a;
                    Error = false;                      
                }
                else
                {
                    Insertar(valor);                        
                    Error = true;                           
                    CodError = 1;                          
                }
            }
            */

            // /*
            // Implementación de la función Insertar utilizando métodos de la clase Array
            // Además se utiliza el parametro params para tener una única función

            public void Insertar(int valor, params int[] pos)
            {
                int posicion;

                posicion = (pos.Length != 0) ? pos[0] : tam;

                if ((posicion > tam) || (posicion < 0))
                {
                    Error = true;
                    CodError = 1;
                }
                else
                {
                    tam++;
                    System.Array.Resize(ref array, tam);

                    for (int i = tam - 1; i > posicion; i--)
                        array[i] = array[i - 1];

                    array.SetValue(valor, posicion);
                    Error = false;
                }
            }
            // */


            public bool Borrar(int posicion)
            {
                if (posicion < tam)
                {
                    if (posicion != tam - 1)
                    {
                        for (int i = posicion; i < tam - 1; i++)
                        {
                            array[i] = array[i + 1];
                        }
                    }

                    int[] a = new int[--tam];
                    for (int i = 0; i < tam; i++)
                        a[i] = array[i];

                    array = a;
                    Error = false;
                }
                else
                {
                    Error = true;
                    CodError = 2;
                }

                return (!Error);
            }

            // Implementación de la función OrdenarBurbuja utilizando métodos de la clase Array
            // Además se utiliza el parametro params para tener una única función
            public void OrdenarBurbuja(int orientacion)
            {
                if (tam > 0)
                {
                    Error = false;
                    System.Array.Sort(array);
                    if (orientacion == 2)
                        System.Array.Reverse(array);
                }
                else
                {
                    Error = true;
                    CodError = 9;
                }
            }

            /*
            // Implementación de la función Insertar sin utilizar métodos de la clase Array
            public void OrdenarBurbuja(int orientacion)  
            {
                int i, j, aux;
                if (tam != 0)
                {
                    if (orientacion == 1)  
                    {
                        for (i = tam - 1; i >= 0; i--)      
                            for (j = 0; j < i; j++)         
                                if (array[j] > array[j + 1])
                                {
                                    aux = array[j];
                                    array[j] = array[j + 1];
                                    array[j + 1] = aux;
                                }
                    }
                    else                          
                    {
                        for (i = tam - 1; i >= 0; i--)
                            for (j = 0; j < i; j++)
                                if (array[j] < array[j + 1])  
                                {
                                    aux = array[j];
                                    array[j] = array[j + 1];
                                    array[j + 1] = aux;
                                }
                    }

                    Console.WriteLine();
                    Error = false;
                }
                else
                {
                    Error = true;
                    CodError = 3;   
                }
            }
            */

            // /*
            // Implementación de la función Posicion utilizando métodos de la clase Array
            public int Posicion(int valor)
            {
                int posicion = System.Array.IndexOf(array, valor);

                if (posicion == -1)
                {
                    Error = true;
                    CodError = 4;
                }
                else
                {
                    Error = false;
                }

                return posicion;
            }
            // */

            /*
              
            // Implementación de la función Posicion sin utilizar métodos de la clase Array             
            public int Posicion(int valor)
            {
                bool encontrado=false;
                int i=0;

                for (i = 0; i < tam; i++)
                {
                    if (array[i] == valor)
                    {
                        encontrado = true;
                        Error = false;
                        break;    
                    }
                }

                if (encontrado == false)
                {
                    Error = true;
                    CodError = 4;
                    i = -1;
                }

                return i;         
            }
            */

            // /*

            // Implementación de la función Mayor utilizando métodos de la clase Array
            public int Mayor()
            {
                if (tam > 0)
                {
                    Error = false;
                    return array.Max();
                }
                else
                {
                    Error = true;
                    CodError = 5;
                    return -1;
                }
            }

            // Implementación de la función Menor utilizando métodos de la clase Array
            public int Menor()
            {
                if (tam > 0)
                {
                    Error = false;
                    return array.Min();
                }
                else
                {
                    Error = true;
                    CodError = 6;
                    return -1;
                }

            }

            // */


            /*
            // Implementación de la función Mayor sin utilizar métodos de la clase Array             
            public int Mayor()
            {
                int may = 0;
                int posicion = 0;
                if (tam > 0)
                {
                    may = array[0];
                    for (int i = 1; i < tam; i++)
                        if (array[i] > may)
                            may = array[i];

                    posicion = Posicion(may);
                    Error = false;
                }
                else
                {
                    Error = true;
                    CodError = 5;
                }
                return may;
            }

            // Implementación de la función Menor sin utilizar métodos de la clase Array             
            public int Menor()
            {
                int men = 0;
                int posicion = 0;

                if (tam > 0)
                {
                    men = array[0];
                    for (int i = 1; i < tam; i++)
                        if (array[i] < men)
                            men = array[i];

                    posicion = Posicion(men);
                    Error = false;
                }
                else
                {
                    Error = true;
                    CodError = 6;
                }
                return men;
            }

            */

            // Implementación de la función Primero utilizando métodos de la clase Array
            public int Primero()
            {
                int posicion = -1;

                if (tam > 0)
                {
                    Error = false;
                    posicion = (int)array.GetValue(0);
                    sig = 0;
                }
                else
                {
                    Error = true;
                    CodError = 7;
                }

                return posicion;
            }

            // Implementación de la función Siguiente utilizando métodos de la clase Array
            public int Siguiente()
            {
                int posicion = -1;

                if (sig < tam - 1)
                {
                    Error = false;
                    posicion = (int)array.GetValue(0);
                    sig++;
                }
                else
                {
                    Error = true;
                    CodError = 8;
                }

                return posicion;

            }

            /*
            // Implementación de la función Primero utilizando métodos de la clase Array
            public int Primero()
            {
                int valor=-1;
                
                if (tam > 0)
                {
                    valor = array[0];
                    sig = 0;      
                    Error = false;
                }
                else
                {
                    Error = true;
                    CodError = 7; 
                }
                return valor;
            }

            // Implementación de la función Siguiente utilizando métodos de la clase Array
            public int Siguiente()
            {
                int valor = -1;                
                if (sig < tam - 1)
                {
                    sig++;
                    valor = array[sig];
                    Error = false;
                }
                else
                {
                    Error = true;
                    CodError = 8; 
                }
                return valor;
            }
            */

            // Implementación de la función Invertir utilizando métodos de la clase Array
            public void Invertir()
            {
                if (tam > 0)
                {
                    Error = false;
                    System.Array.Reverse(array);
                }
                else
                {
                    Error = true;
                    CodError = 9;
                }
            }

            /*
            // Implementación de la función Primero utilizando métodos de la clase Array
            public void Invertir()
            {
                int[] a;
                a = new int[tam];  

                for (int i = 0; i < tam; i++) 
                    a[i] = array[i]; 

                if (tam > 0)
                {
                    for (int i = 0; i < tam; i++)
                    {
                        array[i] = a[tam - (i+1)];    
                     }
                    Error = false;
                }
                else
                {
                    Error = true;
                    CodError = 9;   
                }
            }
            */

            public void Rotar(int pos, int orientacion)
            {
                int[] a = new int[tam];

                // Se podria implementar también almacenando el primer o último valor en una variable
                // y desplazar el resto una posición. Luego poner el valor en la posición adecuada

                if (tam > 1)
                {
                    if (orientacion == 1)
                    {

                        for (int y = 0; y < pos; y++)
                        {
                            for (int i = 0; i < tam; i++)
                                a[i] = array[i];


                            for (int j = 0; j < tam; j++)
                            {
                                if (j == 0)
                                    array[j] = a[tam - 1];
                                else
                                    array[j] = a[j - 1];
                            }
                        }
                    }
                    else
                    {

                        for (int y = 0; y < pos; y++)
                        {
                            for (int i = 0; i < tam; i++)
                                a[i] = array[i];

                            for (int j = 0; j < tam; j++)
                            {
                                if (j == tam - 1)
                                    array[j] = a[0];
                                else
                                    array[j] = a[j + 1];
                            }
                        }
                    }
                    Error = false;
                }
                else
                {
                    if (tam == 0)
                    {
                        Error = true;
                        CodError = 10;
                    }
                    else
                    {
                        Error = true;
                        CodError = 11;
                    }
                }

            }

            // Muestra en una línea el contenido del array
            public void Mostrar()
            {
                if (tam != 0)
                {
                    Write("CONTENIDO DEL ARRAY : ");
                    foreach (int valor in array)
                    {
                        Write($"{valor}  ");
                    }
                    Error = false;
                }
                else
                {
                    Error = true;
                    CodError = 12;
                }
            }

            // Muestra el mensaje de error de acuerdo el código de error generado
            // si existe tal error
            public void MostrarError(bool val)
            {
                if (val == true)
                    MsgError(CodError);
            }
        }
    }
}
