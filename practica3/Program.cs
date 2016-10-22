/*
Desarrolla una clase de sinónimos.
La clase se denominará SinonimoArray()
La clase incorporará un jagged array private, que incorporará en cada
posición un conjunto de palabras que sean sinónimas entre sí.
Los métodos, que como mínimo han de desarrollarse son :
- Para añadir un sinónimo se creará un método public add que recibirá
las palabras que sea sinónimas entre sí
- Para consultar los sinónimos de una palabra se creará un método
public search que recibirá la palabra
Debe permitirse la eliminación de alguno de los sinónimos
mostrados.
Comportamiento de la clase
- Si se consulta una palabra que no existe devolverá “No existe la
palabra” e insertará la palabra en el array sin sinónimos.
- Si la palabra existe, pero no tiene sinónimos, devolverá “No existe
sinónimo”.
- Para poder añadir sinónimos deben pasarse como mínimo dos
parámetros y se mirará cuales de los parámetros ya existen. Si todos
los parámetros que existen son sinónimos entre sí, se añade el resto.
Si no son sinónimos entre sí mostramos lo que ha ocurrido.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


namespace practica3
{
    public class SinonimoDiccionario : DictionaryBase
    {
        private string[][] sinonimos = new string[0][];

        public void add(string[] sinonim)
        {
            if (this.sinonimos.Length == 0)
            {
                Array.Resize(ref this.sinonimos, 1);
                Array.Resize(ref this.sinonimos[0], sinonim.Length);

                this.sinonimos[0] = sinonim;
            }
            else
            {
                Array.Resize(ref this.sinonimos, this.sinonimos.Length + 1);
                this.sinonimos[this.sinonimos.Length - 1] = sinonim;
            }
        }

        public string search(string word)
        {
            string result = null;

            for (int i = 0; i < this.sinonimos.Length; i++)
            {
                for (int k = 0; k < this.sinonimos[i].Length; k++)
                {
                    if (word == this.sinonimos[i][k])
                    {
                        if (this.sinonimos[i].Length == 1)
                        {
                            result = "La palabra no tiene sinonimos";
                            
                        }
                        else
                        {
                            result = String.Join(", ", this.sinonimos[i]);
                        }

                        break;
                    }
                }
            }

            if (result == null)
            {
                result = "No existe la palabra";

                Array.Resize(ref this.sinonimos, this.sinonimos.Length + 1);
                this.sinonimos[this.sinonimos.Length - 1] = new string[] { word };
            }

            return result;
        }

        public void delete(string word)
        {
            for (int i = 0; i < this.sinonimos.Length; i++)
            {
                for (int k = 0; k < this.sinonimos[i].Length; k++)
                {
                    if (word == this.sinonimos[i][k])
                    {
                        List<string> tmp = new List<string>(this.sinonimos[i]);
                        tmp.RemoveAt(k);
                        this.sinonimos[i] = tmp.ToArray();

                        break;
                    }
                }
            }
        }
    }

    public class Program
    {
        private static void Validar(out int valor)
        {
            while (!Int32.TryParse(Console.ReadLine(), out valor))
            {
                Console.WriteLine("El valor introducido debe ser un entero");
                Console.Write("Vuelve a introducirlo: ");
            }
        }
        
        public static void Main(string[] args)
        {
            SinonimoDiccionario sd = new SinonimoDiccionario();
            /*sd.add(new string[] { "prueba", "test" });
            sd.add(new string[] { "cosa", "thing", "ñe" });
            sd.add(new string[] { "solo" });*/

            /*Console.WriteLine(sd.search("inventada"));
            Console.WriteLine(sd.search("solo"));
            Console.WriteLine(sd.search("cosa"));

            sd.delete("ñe");
            Console.WriteLine(sd.search("cosa"));*/

            int opcion;
            string palabra, sinonim;

            do
            {
                Console.WriteLine("--- Menú del diccionario de sinónimos ---");
                Console.WriteLine("1. Consultar sinónimos.");
                Console.WriteLine("2. Añadir sinónimo.");
                Console.WriteLine("3. Borrar sinónimos.");
                Console.WriteLine("0. Salir.");
                Validar(out opcion);

                switch(opcion)
                {
                    case 1:
                        {
                            Console.WriteLine("Introduce la palabra cuyo sinónimo quieres consultar: ");
                            palabra = Console.ReadLine();
                            Console.WriteLine("Introduce el sinónimo quieres consultar: ");
                            sinonim = Console.ReadLine();
                            sd.search( sinonim );
                            Console.WriteLine(sd.search(sinonim));
                            
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("Introduce la palabra a la que le quieres añadir un sinónimo: ");
                            palabra = Console.ReadLine();
                            Console.WriteLine("Introduce el sinónimo que quieres añadir: ");
                            sinonim = Console.ReadLine();
                            sd.add(new string[] { sinonim });

                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Introduce el sinónimo que quieres borrar: ");
                            sinonim = Console.ReadLine();
                            sd.delete(sinonim);

                            break;
                        }
                    case 0:
                        {
                            Console.WriteLine("Saliendo del programa");
                            break;
                        }
                }

            } while (opcion != 0);

            Console.WriteLine("Pulsa cualquier tecla para salir.");
            Console.ReadKey();
        }
    }
}