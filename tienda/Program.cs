using System;

namespace tienda
{
    class MainClass
    {

        /*Función Menú muestra las opciones del menú de la aplicación
        y permite escoger una*/

        static int Menu()
        {
            Console.WriteLine("1. Nuevo producto.");
            Console.WriteLine("2. Precio medio.");
            Console.WriteLine("3. Listar productos.");
            Console.WriteLine("4. Modificar producto.");
            Console.WriteLine("5. Eliminar producto.");
            Console.WriteLine("6. Buscar producto.");
            Console.WriteLine("7. Salir");
            int op = int.Parse(Console.ReadLine());

            return op;
        }

        /*Función InstertarProducto inserta un nuevo producto si no
        esta la tienda llena*/

        static void InsertarProducto(string[] nom, double[] p, ref int totalProductos)
        {
            if (totalProductos < MAX_ELEMENTOS)
            {
                Console.WriteLine("Introduce el nombre del nuevo producto");
                nom[totalProductos] = Console.ReadLine();
                Console.WriteLine("Introduce el precio del producto");
                p[totalProductos] = double.Parse(Console.ReadLine());
                totalProductos++;
            }
            else
            {
                Console.WriteLine("Inventario lleno");
            }
        }

        /*Función Promedio realiza la media aritmética de los precios de los
        productos de la tienda */

        static double Promedio(double[] p, int totalProductos)
        {
            double media = 0.0;
            if (totalProductos > 0)
            {
                for (int i = 0; i < totalProductos; i++)
                {
                    media = media + p[i];
                }
                media = media / totalProductos;
            }

            return media;
        }

        /*Función ListarProductos muestra los nombres y precios de los productos*/

        static void ListarProductos(string[] nom, double[] p, int totalProductos)
        {
            if (totalProductos > 0)
            {
                Console.WriteLine("\tPRECIO\t\t\tPRODUCTOS");
                Console.WriteLine("-----------------------------------------");
                for (int i = 0; i < totalProductos; i++)
                {
                    Console.WriteLine("\t{0}\t\t\t{1}", nom[i], p[i]);
                }
            }
            else
            {
                Console.WriteLine("No se ha almacenado aun ningun producto");
            }
        }

        /*Función ModificarNombre se encarga de modificar el nombre del producto
        almacenado en la tabla nom[] */

        static void ModificarNombre(ref string nom)
        {
            Console.WriteLine("Introduce el nuevo nombre");
            nom = Console.ReadLine();
        }

        /*Función ModificarPrecio modificara el precio de la tabla p[]*/

        static void ModificarPrecio(ref double p)
        {
            Console.WriteLine("Introduce el nuevo precio");
            p = double.Parse(Console.ReadLine());
        }

        /*Función EliminarProducto eliminara el nombre y el precio almacenados en
        las tablas nom[] y p[] */

        static void EliminarProducto(string[] nom, double[] p, ref int numElementos, int posEliminar)
        {
            for (int i = posEliminar; i < numElementos; i++)
            {
                nom[i] = nom[i + 1];
                p[i] = p[i + 1];
            }

            numElementos--;
        }

        /*Función BuscarProducto se encargara de buscar el producto almacenado
        en la tabla nom[] */

        static int BuscarProducto(string[] nom, int numElementos, string nombreBuscado)
        {
            int buscado = -1;
            int i = 0;
            while (buscado == -1 && i < numElementos)
            {
                if (nom[i].CompareTo(nombreBuscado) == 0)
                {
                    buscado = i;
                }

                i++;
            }

            return buscado;
        }

        const int MAX_ELEMENTOS = 100;

        public static void Main(string[] args)
        {
            int opcion = 0, subOpcion;
            string nomAux;
            int pos;
            string[] productos = new string[MAX_ELEMENTOS];
            double[] precios = new double[MAX_ELEMENTOS];
            int numElementos = 0;

            do
            {
                Console.Clear();
                opcion = Menu();
                switch (opcion)
                {
                    case 1:
                        InsertarProducto(productos, precios, ref numElementos);
                        break;

                    case 2:
                        Console.WriteLine("El precio medio de todos los productos alamacenados" +
                                           " es: {0}", Promedio(precios, numElementos));
                        break;

                    case 3:
                        ListarProductos(productos, precios, numElementos);
                        break;

                    case 4:
                        Console.WriteLine("¿Que producto desea modificar?");
                        nomAux = Console.ReadLine();
                        pos = BuscarProducto(productos, numElementos, nomAux);
                        if (pos < 0)
                        {
                            Console.WriteLine("Producto no encontrado");
                        }
                        else
                        {
                            Console.WriteLine("¿Que desea modificar? (1-Nombre/2-Precio/3-Ambos)");
                            subOpcion = int.Parse(Console.ReadLine());

                            switch (subOpcion)
                            {
                                case 1: 
                                    ModificarNombre(ref productos[pos]);
                                    break;

                                case 2: 
                                    ModificarPrecio(ref precios[pos]);
                                    break;

                                case 3:
                                    ModificarNombre(ref productos[pos]);
                                    ModificarPrecio(ref precios[pos]);
                                    break;

                                default:
                                    Console.WriteLine("Opcion elegida inexistente");
                                    break;
                            }
                        }
                        break;

                    case 5:
                        if (numElementos > 0)
                        {
                            Console.WriteLine("Indica el nombre del producto a eliminar");
                            nomAux = Console.ReadLine();
                            pos = BuscarProducto(productos, numElementos, nomAux);

                            if (pos >= 0)
                            {
                                EliminarProducto(productos, precios, ref numElementos, pos);
                            }
                            else
                            {
                                Console.WriteLine("El producto que desea eliminar no existe.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No hay productos almacenados");
                        }
                        break;

                    case 6:
                        Console.WriteLine("Indica el nombre del producto que buscas");
                        nomAux = Console.ReadLine();
                        pos = BuscarProducto(productos, numElementos, nomAux);
                        if (pos >= 0)
                        {
                            Console.WriteLine("Producto encontado");
                            Console.WriteLine("{0} - {1}", productos[pos], precios[pos]);
                        }
                        else
                        {
                            Console.WriteLine("No se ha encontrado ningún producto");
                        }
                        break;

                    case 7:
                        Console.WriteLine("El programa ha finalizado, gracias por usar este software.");
                        break;

                    default:
                        Console.WriteLine("Opción escogida no valida");
                        break;
                }
                Console.ReadLine();
            } while (opcion != 7);
        }
    }
}
