using System.Threading.Channels;

namespace Gastos_registro_clase_9
{
    internal class Program
    {
        public static void Main(string[] args)


        {
            string ruta = "gastos.csv";
            int siguienteId = 1;
            string opcion;
            List<Gasto> registro = cargar(ruta);
            foreach (Gasto p in registro)
            {
                if (p.Id >= siguienteId)
                {
                    siguienteId = p.Id + 1;
                }
            }

            do
            {
                opcion = OpcionMenu();
                switch (opcion)
                {
                    case "1":
                        Agregar(registro, ref siguienteId);
                        break;
                    case "2":
                        Listar(registro);
                        break;
                    case "3":
                        Buscartexto(registro);
                        break;
                    case "4":
                        guardar(registro, ruta);
                        Console.WriteLine("Saliendo del sistema...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida, intenta de nuevo.");
                        break;
                }
            }
            while (opcion != "4");
        }



        private static string OpcionMenu()
        {
            Console.WriteLine();
            Console.WriteLine("=== REGISTRO DE GASTOS ===");
            Console.WriteLine("1. Agregar gasto");
            Console.WriteLine("2. Listar gastos");
            Console.WriteLine("3. Buscar por categoria");
            Console.WriteLine("4. Salir");
            Console.Write("Elige una opción:");
            return Console.ReadLine();
        }

        private static void Agregar(List<Gasto> gastos, ref int id)
        {

            Console.Write("Descripcion:");
            string Descripcion = Console.ReadLine().Trim();
            Console.Write("Monto:");
            bool montoOk = decimal.TryParse(Console.ReadLine(), out decimal monto);
            Console.Write("Categoria:");
            string Categoria = Console.ReadLine().Trim();

            if (!montoOk || monto <= 0 || string.IsNullOrWhiteSpace(Descripcion) || string.IsNullOrWhiteSpace(Categoria))
            {
                Console.WriteLine("Datos invalidos.");
                return;

            }
            else
            {
                gastos.Add(new Gasto
                {
                    Id = id,
                    descripcion = Descripcion,
                    monto = monto,
                    categoria = Categoria,


                });
                id++;
                Console.WriteLine("Gasto agregado.");
            }


        }

        private static void Listar(List<Gasto> registro)
        {
            if (registro.Count == 0)
            {
                Console.WriteLine("Registro vacio");
                return;

            }
            Console.WriteLine($"{"Id",-3} {"Descripcion",-20} Q {"Monto",8:N2} {"Categoria",10}");

            decimal totalgastos = 0;
            foreach (Gasto p in registro)
            {
                Console.WriteLine(p);
                totalgastos += p.monto;
            }
            Console.WriteLine($"TOTAL GASTADO: Q {totalgastos:N2}");

            

        }

        private static void Buscartexto(List<Gasto> gastos)
        {
            Console.Write("Buscar por categoria:");
            string t = Console.ReadLine().Trim().ToUpper();
            bool encontrado = false;

            Console.WriteLine($"{"Id",-3} {"Descripcion",-20} Q {"Monto",8:N2} {"Categoria",10}");

            foreach (Gasto p in gastos)
            {

                if (p.categoria.ToUpper().Contains(t))
                {

                    Console.WriteLine($"{p}");
                    encontrado = true;

                }

            }

                if (!encontrado)
                {

                    Console.WriteLine("Sin coicidencias");

                }

   


        }

        private static void guardar(List<Gasto> gastos, string ruta)
        {
            var lineas = new List<string>();
            foreach (Gasto p in gastos)
            {
                lineas.Add($"{p.Id};{p.descripcion};{p.monto};{p.categoria}");

            }
            File.WriteAllLines(ruta, lineas);
            Console.WriteLine($"gastos guardados en {ruta} ({gastos.Count} registros)");

        }

        private static List<Gasto> cargar(string ruta)
        {
            var gastos = new List<Gasto>();
            if (!File.Exists(ruta)) return gastos;
            
                foreach (string linea in File.ReadAllLines(ruta))
                {
                    string[] campos = linea.Split(";");

                    if (campos.Length != 4) continue;
                    
                        gastos.Add(new Gasto
                        {
                            Id = int.Parse(campos[0]),
                            descripcion = campos[1],
                            categoria = campos[3],
                            monto = decimal.Parse(campos[2]),
                          


                        });

                    


                }

                Console.WriteLine("--- Vuelve a ejecutar el programa (F5) ---");

            Console.WriteLine($"Cargados {gastos.Count} gastos desde {ruta}.");


            return gastos;

            }
        }
    }
    

