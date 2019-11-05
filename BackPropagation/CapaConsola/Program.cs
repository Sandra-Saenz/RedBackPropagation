using System;
using System.IO;

namespace CapaConsola
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }

        private static void Entrenamiento(int iteraciones, string direccionArchivo)
        {
            int numEntradas = 0, numSalidas = 0;
            string direccion = direccionArchivo;

            //normalizar el archivo de datos categoricos a numericos


            //entrada y salida
            StreamReader sr = new StreamReader(direccion);
            int numeroFilas = File.ReadAllLines(direccion).Length;
            int Patrones = numeroFilas - 1;

            for (int f = 0; f < 1; f++)
            {
                string linea = sr.ReadLine();
                for (int c = 0; c < linea.Length; c++)
                {
                    if (Convert.ToString(linea[c]) == "x" || Convert.ToString(linea[c]) == "X")
                    {
                        numEntradas++;
                    }
                    else if (Convert.ToString(linea[c]) == "y" || Convert.ToString(linea[c]) == "Y")
                    {
                        numSalidas++;
                    }
                }
            }
            sr.Close();


            int num = 1, neuronasCapaOculta1 = 6, neuronasCapaOculta2 = 5; double emp = 0.001, erms;
            double rataAprendizaje = 1;

            double[] vectorUmbralUno = new double[neuronasCapaOculta1];
            double[] vectorUmbralDos = new double[neuronasCapaOculta2];
            double[] vectorUmbralTres = new double[numSalidas];

            double[,] matrizPesoUno = new double[numEntradas, neuronasCapaOculta1];
            double[,] matrizPesoDos = new double[neuronasCapaOculta1, neuronasCapaOculta2];
            double[,] matrizPesoTres = new double[neuronasCapaOculta2, numSalidas];

            int[,] matrizProblema = new int[Patrones, numEntradas + numSalidas];

            double[] vectorEntrada = new double[numEntradas];
            double[] vectorSalida = new double[numSalidas];

            double[] SalidaRed1 = new double[neuronasCapaOculta1];
            double[] SalidaRed2 = new double[neuronasCapaOculta2];
            double[] SalidaRed3 = new double[numSalidas];

            double[] erroresLineales = new double[numSalidas];
            double[] errorPatron = new double[Patrones];
            _ = new Random();

            // guardar los valores del archivo en matrizProblema
            StreamReader sreader = new StreamReader(direccion);
            _ = sreader.ReadLine();
            for (int f = 0; f < Patrones; f++)
            {
                string lineas = sreader.ReadLine();
                string numeros = lineas.Replace(",", "");
                for (int c = 0; c < numeros.Length; c++)
                {
                    if (Convert.ToString(numeros[c]) != ",")
                    {
                        matrizProblema[f, c] = (int)char.GetNumericValue(numeros[c]);
                    }
                }
            }
            sreader.Close();

            //crear matriz y vector con nummeros aleatorios
            for (int i = 0; i < numEntradas; i++)
            {
                for (int j = 0; j < neuronasCapaOculta1; j++)
                {
                    matrizPesoUno[i, j] = GenerarNumeroAleatorio();
                }
            }

            for (int i = 0; i < neuronasCapaOculta1; i++)
            {
                for (int j = 0; j < neuronasCapaOculta2; j++)
                {
                    matrizPesoDos[i, j] = GenerarNumeroAleatorio();
                }
            }

            for (int i = 0; i < neuronasCapaOculta2; i++)
            {
                for (int j = 0; j < numSalidas; j++)
                {
                    matrizPesoTres[i, j] = GenerarNumeroAleatorio();
                }
            }

            for (int i = 0; i < neuronasCapaOculta1; i++)
            {
                vectorUmbralUno[i] = GenerarNumeroAleatorio();
            }

            for (int i = 0; i < neuronasCapaOculta2; i++)
            {
                vectorUmbralDos[i] = GenerarNumeroAleatorio();
            }

            for (int i = 0; i < numSalidas; i++)
            {
                vectorUmbralTres[i] = GenerarNumeroAleatorio();
            }

            //mostrar pesos
            Console.WriteLine("matriz peso uno");
            for (int z = 0; z < numEntradas; z++)
            {
                for (int x = 0; x < neuronasCapaOculta1; x++)
                {
                    Console.Write(matrizPesoUno[z, x] + " ");
                }
                Console.Write("\n");
            }
            //mostrar umbrales 
            Console.WriteLine("umbral capa oculta 1");
            for (int x = 0; x < neuronasCapaOculta1; x++)
            {
                Console.Write(vectorUmbralUno[x] + " ");
            }
            Console.WriteLine("\n");

            Console.WriteLine("matriz peso dos");
            for (int z = 0; z < neuronasCapaOculta1; z++)
            {
                for (int x = 0; x < neuronasCapaOculta2; x++)
                {
                    Console.Write(matrizPesoDos[z, x] + " ");
                }
                Console.Write("\n");
            }
            //mostrar umbrales 
            Console.WriteLine("umbral capa oculta 2");
            for (int x = 0; x < neuronasCapaOculta2; x++)
            {
                Console.Write(vectorUmbralDos[x] + " ");
            }
            Console.WriteLine("\n");

            Console.WriteLine("matriz peso tres");
            for (int z = 0; z < neuronasCapaOculta2; z++)
            {
                for (int x = 0; x < numSalidas; x++)
                {
                    Console.Write(matrizPesoTres[z, x] + " ");
                }
                Console.Write("\n");
            }
            //mostrar umbrales 
            Console.WriteLine("umbral salida");
            for (int x = 0; x < numSalidas; x++)
            {
                Console.Write(vectorUmbralTres[x] + " ");
            }
            Console.WriteLine("\n");

            bool detener = false;
            while (num <= iteraciones && detener == false)
            {
                Console.WriteLine("iteracion n°: " + num);
                //una iteracion es pasar por todos los patrones
                for (int i = 0; i < Patrones; i++)
                {
                    Console.WriteLine("\n");
                    Console.WriteLine("Patron n°: " + i);

                    //presentar el vector de entrada y el vector de salida
                    for (int w = 0; w < numSalidas; w++)
                    {
                        if (w < numEntradas)
                        {
                            vectorEntrada[w] = matrizProblema[i, w];
                        }
                        else
                        {
                            vectorSalida[w] = matrizProblema[i, w];
                        }
                    }

                    //calcular funcion de activacio  por capas 
                    //capa 1
                    double funcionSoma = 0;
                    for (int q = 0; q < neuronasCapaOculta1; q++)
                    {
                        for (int j = 0; j < numEntradas; j++)
                        {
                            funcionSoma = (vectorEntrada[j] * matrizPesoUno[j, q]) + funcionSoma;
                        }

                        double calcularSalida = (Math.Truncate((funcionSoma - vectorUmbralUno[q]) * 10000) / 10000);
                        Console.WriteLine("salida de la red: " + calcularSalida);

                        double h = Math.Tanh(calcularSalida);
                        
                            SalidaRed3[q] = 0;
                       
                        Console.WriteLine("salida de la red con la funcion activacion: " + SalidaRed3[q]);
                    }

                    //capa 2
                    double funcionSoma2 = 0;
                    for (int q = 0; q < neuronasCapaOculta1; q++)
                    {
                        for (int j = 0; j < neuronasCapaOculta2; j++)
                        {
                            funcionSoma2 = (vectorEntrada[j] * matrizPesoUno[q, j]) + funcionSoma2;
                        }

                        double calcularSalida = (Math.Truncate((funcionSoma2 - vectorUmbralUno[q]) * 10000) / 10000);
                        Console.WriteLine("salida de la red: " + calcularSalida);

                        if (calcularSalida < 0)
                        {
                            SalidaRed3[q] = 0;
                        }
                        else if (true)
                        {
                            SalidaRed3[q] = 1;
                        }
                        Console.WriteLine("salida de la red con la funcion activacion: " + SalidaRed3[q]);
                    }

                    //salida 
                    double funcionSoma3 = 0;
                    for (int q = 0; q < neuronasCapaOculta2; q++)
                    {
                        for (int j = 0; j < numEntradas; j++)
                        {
                            funcionSoma3 = (vectorEntrada[j] * matrizPesoUno[q, j]) + funcionSoma3;
                        }

                        double calcularSalida = (Math.Truncate((funcionSoma3 - vectorUmbralUno[q]) * 10000) / 10000);
                        Console.WriteLine("salida de la red: " + calcularSalida);

                        if (calcularSalida < 0)
                        {
                            SalidaRed3[q] = 0;
                        }
                        else if (true)
                        {
                            SalidaRed3[q] = 1;
                        }
                        Console.WriteLine("salida de la red con la funcion activacion: " + SalidaRed3[q]);
                    }

                    //calcular los errores lineales producidos a la salida
                    for (int ii = 0; ii < numSalidas; ii++)
                    {
                        erroresLineales[ii] = (Math.Truncate((vectorSalida[ii] - SalidaRed3[ii]) * 10000) / 10000);
                        Console.WriteLine("error lineal " + ii + ": " + erroresLineales[ii]);
                    }

                    //calcular el error del patron
                    double sumaErrores = 0;
                    for (int a = 0; a < numSalidas; a++)
                    {
                        sumaErrores = erroresLineales[a] + sumaErrores;
                    }

                    errorPatron[i] = (Math.Truncate((sumaErrores / numSalidas) * 10000) / 10000);
                    Console.WriteLine("error del patron: " + errorPatron[i]);

                    //modificar pesos
                    Console.Write("\n");
                    Console.WriteLine("nueva matriz peso 1");

                    for (int z = 0; z < numEntradas; z++)
                    {
                        for (int x = 0; x < neuronasCapaOculta1; x++)
                        {
                            matrizPesoUno[z, x] = (Math.Truncate((matrizPesoUno[z, x] + rataAprendizaje * erroresLineales[x] * vectorEntrada[z]) * 10000) / 10000);
                            Console.Write(matrizPesoUno[z, x] + " ");
                        }
                        Console.Write("\n");
                    }

                    Console.Write("\n");
                    Console.WriteLine("nuevo umbral capa 1");
                    for (int x = 0; x < neuronasCapaOculta1; x++)
                    {
                        vectorUmbralUno[x] = (Math.Truncate((vectorUmbralUno[x] + rataAprendizaje * erroresLineales[x] * 1) * 10000) / 10000);
                        Console.Write(vectorUmbralUno[x] + " ");
                    }

                    Console.Write("\n");
                    Console.WriteLine("nueva matriz peso 2");

                    for (int z = 0; z < neuronasCapaOculta1; z++)
                    {
                        for (int x = 0; x < neuronasCapaOculta2; x++)
                        {
                            matrizPesoUno[z, x] = (Math.Truncate((matrizPesoUno[z, x] + rataAprendizaje * erroresLineales[x] * vectorEntrada[z]) * 10000) / 10000);
                            Console.Write(matrizPesoUno[z, x] + " ");
                        }
                        Console.Write("\n");
                    }

                    Console.Write("\n");
                    Console.WriteLine("nuevo umbral capa 2");
                    for (int x = 0; x < numSalidas; x++)
                    {
                        vectorUmbralUno[x] = (Math.Truncate((vectorUmbralUno[x] + rataAprendizaje * erroresLineales[x] * 1) * 10000) / 10000);
                        Console.Write(vectorUmbralUno[x] + " ");
                    }

                    Console.Write("\n");
                    Console.WriteLine("nueva matriz peso 3");

                    for (int z = 0; z < neuronasCapaOculta2; z++)
                    {
                        for (int x = 0; x < numSalidas; x++)
                        {
                            matrizPesoUno[z, x] = (Math.Truncate((matrizPesoUno[z, x] + rataAprendizaje * erroresLineales[x] * vectorEntrada[z]) * 10000) / 10000);
                            Console.Write(matrizPesoUno[z, x] + " ");
                        }
                        Console.Write("\n");
                    }

                    Console.Write("\n");
                    Console.WriteLine("nuevo umbral salida");
                    for (int x = 0; x < numSalidas; x++)
                    {
                        vectorUmbralUno[x] = (Math.Truncate((vectorUmbralUno[x] + rataAprendizaje * erroresLineales[x] * 1) * 10000) / 10000);
                        Console.Write(vectorUmbralUno[x] + " ");
                    }
                }
                Console.Write("\n");

                //calcular el error del entrenamiento (con este se hace la grafica)
                double sumaErrorPatron = 0;
                for (int l = 0; l < Patrones; l++)
                {
                    sumaErrorPatron = Math.Abs(errorPatron[l] + sumaErrorPatron);
                }

                erms = (Math.Truncate((sumaErrorPatron / Patrones) * 10000) / 10000);
                Console.Write("\n");
                Console.WriteLine("error entrenamiento " + erms);

                if (erms <= emp)
                {
                    detener = true;
                }
                num++;
            }
            Console.Write("\n");
            Console.WriteLine("Entrenamiento finalizado.");

            using (StreamWriter writer = new StreamWriter("C:/Users/55YV/Downloads/redes/ArchivosBackPropagation/pesosEntrenamiento.txt", false))
            {
                for (int i = 0; i < numEntradas; i++)
                {
                    for (int j = 0; j < numSalidas; j++)
                    {
                        writer.Write(matrizPesoUno[i, j].ToString() + ";");
                    }
                    writer.Write("\n");
                }
            }

            using (StreamWriter writer = new StreamWriter("C:/Users/55YV/Downloads/redes/ArchivosBackPropagation/umbralesEntrenamiento.txt", false))
            {
                for (int i = 0; i < vectorUmbralUno.Length; i++)
                {
                    writer.Write(vectorUmbralUno[i].ToString() + ";");
                }
            }

            Console.Write("\n");
            Console.WriteLine("Pesos y umbrales guardados");

            Console.ReadKey();
        }

        static double GenerarNumeroAleatorio()
        {
            Random random = new Random();

            int[] signo = new int[2];
            signo[0] = -1;
            signo[1] = 1;

            double resultado = 0;
            double[] numeroAletorio = new double[3];
            numeroAletorio[0] = -1;
            numeroAletorio[2] = 1;

            for (int i = 0; i < 5; i++)
            {
                numeroAletorio[1] = random.NextDouble() * signo[random.Next(0, 2)];
                resultado = numeroAletorio[random.Next(0, 3)];
            }
            return (Math.Truncate(resultado * 10000) / 10000);
        }

        private static void Simulacion(string patron)
        {

        }
    }
}
