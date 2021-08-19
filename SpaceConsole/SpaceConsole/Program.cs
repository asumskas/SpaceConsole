using System;
using System.Linq;
namespace SpaceConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            
            SpaceConsole.Implementacion.Space ospace = new Implementacion.Space();

            //ospcace.GetLocation(640.31242374328486864882176746218F, 447.21359549995793928183473374626F, 632.45553203367586639977870888654F);
            //ospcace.GetLocation(640.31F, 447.21F, 632.45F);

            string opcion = string.Empty;
            while ( opcion !="0")
            {
                Console.Clear();
                Console.WriteLine("Seleccione que operacion desea hacer (1) Mensaje (2)  Ubicación (0) Salir :");
                opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "1":
                        Console.WriteLine("Ingrese Primer mensaje separado por | (para terminar presione enter):");
                        var mensaje1  = Console.ReadLine();
                        string[] a = mensaje1.Split("|");
                        Console.WriteLine("Ingrese Segundo mensaje separado por | (para terminar presione enter):");
                        var mensaje2 = Console.ReadLine();
                        string[] b = mensaje2.Split("|");
                        Console.WriteLine("Ingrese Tercer mensaje separado por | (para terminar presione enter):");
                        var mensaje3 = Console.ReadLine();
                        string[] c = mensaje3.Split("|");
                        
                        try
                        {
                            Console.WriteLine("***********************************************");
                            Console.WriteLine("El mensaje del emisor interpretado es :");
                            Console.WriteLine(ospace.GetMessage(a, b, c).ToUpper());
                            Console.WriteLine("***********************************************");
                            Console.WriteLine("Presione Enter continuar");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("***********************************************");
                            Console.WriteLine("ATENCION SE HA DETECTADO UN INCONVENIENTE :");
                            Console.WriteLine(ex.Message);
                            Console.WriteLine("***********************************************");
                            Console.WriteLine("Presione Enter continuar");
                        }
                        
                        Console.WriteLine("Presione Enter para volver al menu :");
                         Console.ReadLine();



                        break;
                    case "2":
                        bool parseok =false ; 
                        Console.WriteLine("Ingrese Distancia al Satelite Kenobi:");
                        float D1=0, D2=0, D3=0;
                        while (!parseok )
                        {
                            var n1 = Console.ReadLine();
                        parseok = float.TryParse(n1.Replace(".",","), out D1);
                        if (!parseok)
                                Console.WriteLine("Ingrese Distancia al Satelite Kenobi Numérica Valida:");
                        }

                        parseok = false;
                        Console.WriteLine("Ingrese Distancia al Satelite Skywlaker:");
                        while (!parseok)
                        {
                            var n2 = Console.ReadLine();
                            parseok = float.TryParse(n2.Replace(".", ","), out D2);
                            if (!parseok)
                                Console.WriteLine("Ingrese Distancia al Satelite Skywlaker Numérica Valida:");
                        }


                        
                        parseok = false;
                        Console.WriteLine("Ingrese Distancia al Satelite Sato:");
                        while (!parseok)
                        {
                            var n3 = Console.ReadLine();
                            parseok = float.TryParse(n3.Replace(".", ","), out D3);
                            if (!parseok)
                                Console.WriteLine("Ingrese Distancia al Satelite Sato Numérica Valida:");
                        }

                        
                        try
                        {
                            
                            var T = ospace.GetLocation(D1, D2, D3);
                            Console.WriteLine("***********************************************");
                            Console.WriteLine("DADAS LAS SIGUIENTES ENTRADAS :");
                            Console.WriteLine(string.Format( "Distancia a Kenoby de {0}",D1));
                            Console.WriteLine(string.Format("Distancia a Skywlaker de {0}", D2));
                            Console.WriteLine(string.Format("Distancia a Sato de {0}", D3));

                            Console.WriteLine("LA POSICION DEL EMISOR ES [x;y]:");
                            Console.WriteLine(string.Format("[{0};{1}]",T.Item1,T.Item2));

                            Console.WriteLine("***********************************************");
                            Console.WriteLine("Presione Enter continuar");
                            Console.ReadLine();
                            

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("***********************************************");
                            Console.WriteLine("ATENCION SE HA DETECTADO UN INCONVENIENTE :");
                            Console.WriteLine(ex.Message);
                            Console.WriteLine("***********************************************");
                            Console.WriteLine("Presione Enter continuar");
                            
                            Console.ReadLine();

                        }
                        

                        break;
                    case "0":
                        return;
                        
                        
                }

                ospace = null; 

            }


            

        }
    }
}
