using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SpaceConsole.Domain;
using System.Collections;

namespace SpaceConsole.Implementacion
{
    public class Space : InterfaceSpace

    {
        /// <summary>
        /// Metodo de que devuelve la posicion x e y con el emisor 
        /// </summary>
        /// <param name="d1">Distancia a Kenobi </param>
        /// <param name="d2">Distancia a Skywlaker </param>
        /// <param name="d3">Distancia a Sato  </param>
        /// <returns></returns>
        /// 


        public Tuple<float, float> GetLocation(float d1, float d2, float d3)
        {
            // defino los puntos a devolver 

            Satelite Kenobi = new Satelite { Nombre = "Kenobi", CoordenadaX = -500, CoordenadaY = -200 };
            Satelite Skywlaker = new Satelite { Nombre = "Skywlaker", CoordenadaX = 100, CoordenadaY = -100 };
            Satelite Sato = new Satelite { Nombre = "Sato", CoordenadaX = 500, CoordenadaY = 100 };

            // obtengo puntos de interseccion entre  Kenobi y Skywlaker
            var lista = Interseccion(Kenobi, Skywlaker, d1, d2);

            // obtengo puntos de interseccion entre  Sato y Skywlaker
            var lista2 = Interseccion(Skywlaker, Sato, d2, d3);

            // busco punto comun entre las dos lista de puntos 
            var result = lista.Where(w => lista2.Any(z => z.x == w.x && z.y == w.y)).ToList();

            Tuple<float, float> T ; 
            if (result.Count == 1)
            //    T = new Tuple<float, float>() { T.Item1 = result.FirstOrDefault().x, T.Item2 = result.FirstOrDefault().y };
                T=(Tuple.Create(result.FirstOrDefault().x, result.FirstOrDefault().y));
            else
            {
                // verifico si redondeando a una cifra llego a algun valor 
                var result1 = lista.Where(w => lista2.Any(z => Math.Round(z.x, 1) == Math.Round(w.x, 2) && Math.Round(z.y, 1) == Math.Round(w.y, 1))).ToList();
                if (result1.Count == 1)
                    T=(Tuple.Create(result1.FirstOrDefault().x, result1.FirstOrDefault().y));
                

                else
                    throw new Exception("No se pudo determinar un punto de ubicacion exacto con los datos de distancia proporcionado de los tres satelites . Verifique distancias ");
            }

            return T;










        }

        private List<Punto> Interseccion(Satelite SateliteA, Satelite SateliteB, float r1, float r2)
        
        {

            float a;
            float X0, Y0, X1, Y1;
            X0 = SateliteA.CoordenadaX;
            Y0 = SateliteA.CoordenadaY;
            X1 = SateliteB.CoordenadaX;
            Y1 = SateliteB.CoordenadaY;

            List<Punto> Puntos = new List<Punto>();
            float d = (float)Math.Sqrt(Math.Pow(Math.Abs(X1 - X0), 2) + Math.Pow(Math.Abs(Y1 - Y0), 2));
            float h, x3, x4, y3, y4, x2, y2;

            // verifico si son cirulos que se tocan en algun punto y si no no son circulos concentricos 
            if (!(d > (r1 + r2) || d < (Math.Abs(r1 - r2))) && (d != 0))
            {


                a = (float)((Math.Pow(r1, 2) - Math.Pow(r2, 2) + Math.Pow(d, 2)) / (2 * d));
                h = (float)Math.Sqrt(Math.Pow(r1, 2) - Math.Pow(a, 2));

                x2 = X0 + a * (X1 - X0) / d;
                y2 = Y0 + a * (Y1 - Y0) / d;

                x3 = x2 + h * (Y1 - Y0) / d;
                y3 = y2 - h * (X1 - X0) / d;

                x4 = x2 - h * (Y1 - Y0) / d;
                y4 = y2 + h * (X1 - X0) / d;

                var Punto1 = new Punto { x = (float)Math.Round(x3, 2), y = (float)Math.Round(y3, 2) };
                var Punto2 = new Punto { x = (float)Math.Round(x4, 2), y = (float)Math.Round(y4, 2) };

                Puntos.Add(Punto1);
                Puntos.Add(Punto2);

            }

            return Puntos;
        }


        private class desfasajes
        {
            public string descripcion { get; set; }
            public int? desfasaje { get; set; }


        }

        public string GetMessage(string[] Message1, string[] Message2, string[] Message3)
        {

            #region Determino la maxima longitud de los array 
            int longitudArrayMax = Message1.Length;

            if (longitudArrayMax < Message2.Length)
                longitudArrayMax = Message2.Length;
            if (longitudArrayMax < Message3.Length)
                longitudArrayMax = Message3.Length;

            #endregion

            #region definicion de variables 
            string mensaje = string.Empty;
            int? DesfasajeMensaje1EnMensaje2 = null;
            int? DesfasajeMensaje1EnMensaje3 = null;
            int? DesfasajeMensaje2EnMensaje1 = null;
            int? DesfasajeMensaje2EnMensaje3 = null;
            int? DesfasajeMensaje3EnMensaje1 = null;
            int? DesfasajeMensaje3EnMensaje2 = null;
            List<desfasajes> desfasajes = new List<desfasajes>();

            #endregion

            #region trato de determinar los desfasajes en diferencias de indicesde los 3 array entre si
            /*
              Sirve buscar los desfasajes entre los tres array ya que permite que si un array no llega a tener alguna palabra
            en comun utilizo el otro arrar como puente para llegar al array 
             */
            for (int i = 0; i < longitudArrayMax; i++)
            {
                if (i < Message1.Length)
                {
                    DesfasajeMensaje1EnMensaje2 = DesfasajeMensaje1EnMensaje2 == null ? DeterminarPosicionEnArray(Message1[i], Message2, 0) - i : DesfasajeMensaje1EnMensaje2;
                    DesfasajeMensaje1EnMensaje3 = DesfasajeMensaje1EnMensaje3 == null ? DeterminarPosicionEnArray(Message1[i], Message3, 0) - i : DesfasajeMensaje1EnMensaje3;
                }
                if (i < Message2.Length)
                {
                    DesfasajeMensaje2EnMensaje1 = DesfasajeMensaje2EnMensaje1 == null ? DeterminarPosicionEnArray(Message2[i], Message1, 0) - i : DesfasajeMensaje2EnMensaje1;
                    DesfasajeMensaje2EnMensaje3 = DesfasajeMensaje2EnMensaje3 == null ? DeterminarPosicionEnArray(Message2[i], Message3, 0) - i : DesfasajeMensaje2EnMensaje3;
                }
                if (i < Message3.Length)
                {

                    DesfasajeMensaje3EnMensaje1 = DesfasajeMensaje3EnMensaje1 == null ? DeterminarPosicionEnArray(Message3[i], Message1, 0) - i : DesfasajeMensaje3EnMensaje1;
                    DesfasajeMensaje3EnMensaje2 = DesfasajeMensaje3EnMensaje2 == null ? DeterminarPosicionEnArray(Message3[i], Message2, 0) - i : DesfasajeMensaje3EnMensaje2;
                }
                if (DesfasajeMensaje1EnMensaje2 != null &&
                    DesfasajeMensaje1EnMensaje3 != null &&
                    DesfasajeMensaje2EnMensaje1 != null &&
                    DesfasajeMensaje2EnMensaje3 != null &&
                    DesfasajeMensaje3EnMensaje1 != null &&
                    DesfasajeMensaje3EnMensaje2 != null
                    )
                {


                    break;

                }
            }


            #endregion

            #region Busco cual es array directiz para armar el mensaje (que es el que mas lejos tiene la palabra inicial 

            desfasajes.Add(new desfasajes { descripcion = "DesfasajeMensaje1", desfasaje = DesfasajeMensaje1EnMensaje2 });
            desfasajes.Add(new desfasajes { descripcion = "DesfasajeMensaje1", desfasaje = DesfasajeMensaje1EnMensaje3 });
            desfasajes.Add(new desfasajes { descripcion = "DesfasajeMensaje2", desfasaje = DesfasajeMensaje2EnMensaje1 });
            desfasajes.Add(new desfasajes { descripcion = "DesfasajeMensaje2", desfasaje = DesfasajeMensaje2EnMensaje3 });
            desfasajes.Add(new desfasajes { descripcion = "DesfasajeMensaje3", desfasaje = DesfasajeMensaje3EnMensaje1 });
            desfasajes.Add(new desfasajes { descripcion = "DesfasajeMensaje3", desfasaje = DesfasajeMensaje3EnMensaje2 });


            desfasajes = desfasajes.Where(w => w.desfasaje != null).OrderBy(w => w.desfasaje).ToList();
            var obj = desfasajes.FirstOrDefault();


            if (obj != null)
            {
                switch (obj.descripcion)
                {
                    case "DesfasajeMensaje1":

                        // si no tengo algun desfasaje verifico si no puedo armar el desfasaje relativo respecto a los otros desfasajes 
                        if (DesfasajeMensaje1EnMensaje2 == null)
                            DesfasajeMensaje1EnMensaje2 = DesfasajeMensaje1EnMensaje3 + DesfasajeMensaje3EnMensaje2;
                        if (DesfasajeMensaje1EnMensaje3 == null)
                            DesfasajeMensaje1EnMensaje3 = DesfasajeMensaje1EnMensaje2 + DesfasajeMensaje2EnMensaje3;

                        mensaje = armarMensaje(Message1, Message2, Message3, DesfasajeMensaje1EnMensaje2, DesfasajeMensaje1EnMensaje3, longitudArrayMax);
                        break;
                    case "DesfasajeMensaje2":

                        // si no tengo algun desfasaje verifico si no puedo armar el desfasaje relativo respecto a los otros desfasajes 
                        if (DesfasajeMensaje2EnMensaje1 == null)
                            DesfasajeMensaje2EnMensaje1 = DesfasajeMensaje2EnMensaje3 + DesfasajeMensaje3EnMensaje1;

                        if (DesfasajeMensaje2EnMensaje3 == null)
                            DesfasajeMensaje2EnMensaje3 = DesfasajeMensaje2EnMensaje1 + DesfasajeMensaje1EnMensaje3;

                        mensaje = armarMensaje(Message2, Message1, Message3, DesfasajeMensaje2EnMensaje1, DesfasajeMensaje2EnMensaje3, longitudArrayMax);


                        break;

                    case "DesfasajeMensaje3":
                        // si no tengo algun desfasaje verifico si no puedo armar el desfasaje relativo respecto a los otros desfasajes 
                        if (DesfasajeMensaje3EnMensaje1 == null)
                            DesfasajeMensaje3EnMensaje1 = DesfasajeMensaje3EnMensaje2 + DesfasajeMensaje2EnMensaje1;

                        if (DesfasajeMensaje3EnMensaje2 == null)
                            DesfasajeMensaje3EnMensaje2 = DesfasajeMensaje3EnMensaje1 + DesfasajeMensaje1EnMensaje2;

                        mensaje = armarMensaje(Message3, Message1, Message2, DesfasajeMensaje3EnMensaje1, DesfasajeMensaje3EnMensaje2, longitudArrayMax);



                        break;

                }
            }


            #endregion 








            return mensaje;



        }

        /// <summary>
        /// Funcion que busca armar el mensaje desde un array principal y que se vale de dos array secundarios en el caso de que el principal no tenga palabra alguna 
        /// 
        /// </summary>
        /// <param name="arrayprincipal">Array que posee la palabra inicial mas cerca del principio </param>
        /// <param name="array1"> array secundario 1 </param>
        /// <param name="array2">array secundario 2</param>
        /// <param name="desfasaje1">desfasaje en cantidad indices del array 1 con respecto al array principal  </param>
        /// <param name="desfasaje2">desfasaje en cantidad indices del array 2 con respecto al array principal </param>
        /// <param name="longitudArrayMax"> Maxima longitud de los tres arrays</param>
        /// <returns></returns>
        private string armarMensaje(string[] arrayprincipal, string[] array1, string[] array2, int? desfasaje1, int? desfasaje2, int longitudArrayMax)

        {

            string msg = string.Empty;

            //valido que tenga referencia desde el array principal a los dos array secundarios sino arrojo error solo si el array tiene mas de 1 elemento 
            if (desfasaje1 == null && array1.Length > 1)
                throw new Exception("No exite  forma de relacionar los mensajes porque no tienen palabras en comun en alguno de ellos ");
            if (desfasaje2 == null && array2.Length > 1)
                throw new Exception("No exite forma de relacionar los mensajes porque no tienen palabras en comun en alguno de ellos ");


            for (int i = 0; i < longitudArrayMax; i++)
            {
                string palabra = string.Empty;

                if (i < arrayprincipal.Length && arrayprincipal[i] != "")
                {
                    palabra = arrayprincipal[i];

                }
                else if (desfasaje1 != null && i + desfasaje1 >= 0 && i + desfasaje1 < array1.Length && array1[i + desfasaje1.GetValueOrDefault()] != "")
                {
                    palabra = array1[i + desfasaje1.GetValueOrDefault()];

                }
                else if (desfasaje2 != null && i + desfasaje2 >= 0 && i + desfasaje2 < array2.Length && array2[i + desfasaje2.GetValueOrDefault()] != "")
                {
                    palabra = array2[i + desfasaje2.GetValueOrDefault()];
                }

                if (palabra != string.Empty)
                    msg += palabra + " ";

            }


            return msg;
        }

        /// <summary>
        /// Funcion recursiva que permite determinar el indice donde se encuentra una palabra dentro de un array 
        /// </summary>
        /// <param name="palabra"></param>
        /// <param name="arrayMensaje"></param>
        /// <param name="indice"></param>
        /// <returns></returns>
        public int? DeterminarPosicionEnArray(string palabra, string[] arrayMensaje, int indice)
        {

            if (indice >= arrayMensaje.Length)
            {
                return null;
            }
            else
            {
                if (arrayMensaje[indice].ToUpper() == palabra.ToUpper() && palabra != string.Empty)
                {
                    return indice;

                }
                else
                {
                    return DeterminarPosicionEnArray(palabra, arrayMensaje, indice + 1);
                }

            }


        }


    }
}

