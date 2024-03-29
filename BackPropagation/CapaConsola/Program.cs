﻿using System;
using System.IO;

namespace CapaConsola
{
    class Program
    {
        static void Main(string[] args)
        {
            Entrenamiento(10, "C:/Users/55YV/Downloads/redes/ArchivosBackPropagation/problema.csv",0.1,0.01);
            Simulacion("0;0");
            Console.ReadKey();
        }

        private static void Normalizar(string direccionArchivo)
        {

        }

        private static void Entrenamiento(int iteraciones, string direccionArchivo, double rata, double errorMax)
        {
            int numEntradas = 0, numSalidas = 0;
            string direccion = direccionArchivo;

            //normalizar el archivo de datos categoricos a numericos, llamando a una funcion


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


            int num = 1, neuronasCapaOculta1 = 6, neuronasCapaOculta2 = 5; double emp = errorMax, erms;
            double rataAprendizaje = rata;

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

            double[] erroresNoLinealesUno = new double[neuronasCapaOculta1];
            double[] erroresNoLinealesDos = new double[neuronasCapaOculta2];
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

                    //calcular funcion de activacion  por capas 
                    //capa 1
                    double funcionSoma = 0;
                    for (int q = 0; q < neuronasCapaOculta1; q++)
                    {
                        for (int j = 0; j < numEntradas; j++)
                        {
                            funcionSoma = (vectorEntrada[j] * matrizPesoUno[j, q]) + funcionSoma;
                        }

                        double calcularSalida = (funcionSoma - vectorUmbralUno[q]);
                        Console.WriteLine("salida de la red 1: " + calcularSalida);

                        double h = Math.Tanh(calcularSalida);
                        SalidaRed1[q] = Math.Round(h, 4);
                        Console.WriteLine("salida de la red con la funcion activacion: " + SalidaRed1[q]);
                    }

                    //capa 2
                    double funcionSoma2 = 0;
                    for (int q = 0; q < neuronasCapaOculta2; q++)
                    {
                        for (int j = 0; j < neuronasCapaOculta1; j++)
                        {
                            funcionSoma2 = (SalidaRed1[j] * matrizPesoDos[j, q]) + funcionSoma2;
                        }

                        double calcularSalida = (funcionSoma2 - vectorUmbralDos[q]);
                        Console.WriteLine("salida de la red 2: " + calcularSalida);

                        double h = Math.Tanh(calcularSalida);
                        SalidaRed2[q] = Math.Round(h, 4);
                        Console.WriteLine("salida de la red con la funcion activacion: " + SalidaRed2[q]);
                    }

                    //salida 
                    double funcionSoma3 = 0;
                    for (int q = 0; q < numSalidas; q++)
                    {
                        for (int j = 0; j < neuronasCapaOculta2; j++)
                        {
                            funcionSoma3 = (SalidaRed2[j] * matrizPesoTres[j,q]) + funcionSoma3;
                        }

                        double calcularSalida = (funcionSoma3 - vectorUmbralTres[q]);
                        Console.WriteLine("salida de la red 3: " + calcularSalida);

                        double h = Math.Tanh(calcularSalida);
                        SalidaRed3[q] = Math.Round(h, 4);
                        Console.WriteLine("salida de la red con la funcion activacion: " + SalidaRed3[q]);
                    }

                    //calcular los errores lineales producidos a la salida
                    for (int ii = 0; ii < numSalidas; ii++)
                    {
                        erroresLineales[ii] = Math.Round(vectorSalida[ii] - SalidaRed3[ii], 4);
                        Console.WriteLine("error lineal " + ii + ": " + erroresLineales[ii]);
                    }

                    //calcular los errores no lineales de cada capa
                    //capa 2
                    double nolineal = 0;
                    Console.WriteLine("error no lineal capa 2");
                    for (int l = 0; l < neuronasCapaOculta2; l++)
                    {
                        for (int w = 0; w < numSalidas; w++)
                        {
                            nolineal = (erroresLineales[w] * matrizPesoTres[l,w] + nolineal);
                        }
                        erroresNoLinealesDos[l] = Math.Round(nolineal, 4);
                        Console.WriteLine("error no lineal " + l + ": " + erroresNoLinealesDos[l]);
                    }

                    //capa 1
                    Console.WriteLine("error no lineal capa 1");
                    for (int l = 0; l < neuronasCapaOculta1; l++)
                    {
                        for (int w = 0; w < neuronasCapaOculta2; w++)
                        {
                            nolineal = erroresNoLinealesDos[w] * matrizPesoDos[l,w] + nolineal;
                        }
                        erroresNoLinealesUno[l] = Math.Round(nolineal, 4);
                        Console.WriteLine("error no lineal " + l + ": " + erroresNoLinealesUno[l]);
                    }

                    //calcular el error del patron
                    double sumaErrores = 0;
                    for (int a = 0; a < numSalidas; a++)
                    {
                        sumaErrores = erroresLineales[a] + sumaErrores;
                    }

                    errorPatron[i] = Math.Round(sumaErrores / numSalidas, 4);
                    Console.WriteLine("error del patron: " + errorPatron[i]);

                    //modificar pesos  
                    Console.Write("\n");
                    Console.WriteLine("nueva matriz peso 1");

                    for (int z = 0; z < numEntradas; z++)
                    {
                        for (int x = 0; x < neuronasCapaOculta1; x++)
                        {
                            matrizPesoUno[z, x] = (matrizPesoUno[z, x] + 2 * rataAprendizaje * erroresNoLinealesUno[x] * (1 / (Math.Cosh(SalidaRed1[x]) * Math.Cosh(SalidaRed1[x]))) * vectorEntrada[z]);
                            Console.Write(matrizPesoUno[z, x] + " ");
                        }
                        Console.Write("\n");
                    }

                    Console.Write("\n");
                    Console.WriteLine("nuevo umbral capa 1");
                    for (int x = 0; x < neuronasCapaOculta1; x++)
                    {
                        vectorUmbralUno[x] = (vectorUmbralUno[x] + 2* rataAprendizaje * erroresNoLinealesUno[x] * (1 / (Math.Cosh(SalidaRed1[x]) * Math.Cosh(SalidaRed1[x]))) * 1);
                        Console.Write(vectorUmbralUno[x] + " ");
                    }

                    Console.Write("\n");
                    Console.WriteLine("nueva matriz peso 2");

                    for (int z = 0; z < neuronasCapaOculta1; z++)
                    {
                        for (int x = 0; x < neuronasCapaOculta2; x++)
                        {
                            matrizPesoDos[z, x] = (matrizPesoDos[z, x] + 2 * rataAprendizaje * erroresNoLinealesDos[x] * (1 / (Math.Cosh(SalidaRed2[x]) * Math.Cosh(SalidaRed2[x]))) * SalidaRed1[z]);
                            Console.Write(matrizPesoDos[z, x] + " ");
                        }
                        Console.Write("\n");
                    }

                    Console.Write("\n");
                    Console.WriteLine("nuevo umbral capa 2");
                    for (int x = 0; x < neuronasCapaOculta2; x++)
                    {
                        vectorUmbralDos[x] = (vectorUmbralDos[x] + 2 * rataAprendizaje * erroresNoLinealesDos[x] * (1 / (Math.Cosh(SalidaRed2[x]) * Math.Cosh(SalidaRed2[x]))) * 1);
                        Console.Write(vectorUmbralDos[x] + " ");
                    }

                    Console.Write("\n");
                    Console.WriteLine("nueva matriz peso salida");

                    for (int z = 0; z < neuronasCapaOculta2; z++)
                    {
                        for (int x = 0; x < numSalidas; x++)
                        {
                            matrizPesoTres[z, x] = (matrizPesoTres[z, x] + 2 * rataAprendizaje * erroresLineales[x] * SalidaRed2[z]);
                            Console.Write(matrizPesoTres[z, x] + " ");
                        }
                        Console.Write("\n");
                    }

                    Console.Write("\n");
                    Console.WriteLine("nuevo umbral salida");
                    for (int x = 0; x < numSalidas; x++)
                    {
                        vectorUmbralTres[x] = (vectorUmbralTres[x] + 2 * rataAprendizaje * erroresLineales[x] * 1);
                        Console.Write(vectorUmbralTres[x] + " ");
                    }
                }
                Console.Write("\n");

                //calcular el error del entrenamiento (con este se hace la grafica)
                double sumaErrorPatron = 0;
                for (int l = 0; l < Patrones; l++)
                {
                    sumaErrorPatron = Math.Abs(errorPatron[l] + sumaErrorPatron);
                }

                erms = sumaErrorPatron / Patrones;
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

            //capa 1
            using (StreamWriter writer = new StreamWriter("C:/Users/55YV/Downloads/redes/ArchivosBackPropagation/pesosEntrenamientoUno.txt", false))
            {
                for (int i = 0; i < numEntradas; i++)
                {
                    for (int j = 0; j < neuronasCapaOculta1; j++)
                    {
                        writer.Write(matrizPesoUno[i, j].ToString() + ";");
                    }
                    writer.Write("\n");
                }
            }
            
            using (StreamWriter writer = new StreamWriter("C:/Users/55YV/Downloads/redes/ArchivosBackPropagation/umbralesEntrenamientoUno.txt", false))
            {
                for (int i = 0; i < vectorUmbralUno.Length; i++)
                {
                    writer.Write(vectorUmbralUno[i].ToString() + ";");
                }
            }

            //capa 2
            using (StreamWriter writer = new StreamWriter("C:/Users/55YV/Downloads/redes/ArchivosBackPropagation/pesosEntrenamientoDos.txt", false))
            {
                for (int i = 0; i < neuronasCapaOculta1; i++)
                {
                    for (int j = 0; j < neuronasCapaOculta2; j++)
                    {
                        writer.Write(matrizPesoDos[i, j].ToString() + ";");
                    }
                    writer.Write("\n");
                }
            }

            using (StreamWriter writer = new StreamWriter("C:/Users/55YV/Downloads/redes/ArchivosBackPropagation/umbralesEntrenamientoDos.txt", false))
            {
                for (int i = 0; i < vectorUmbralDos.Length; i++)
                {
                    writer.Write(vectorUmbralDos[i].ToString() + ";");
                }
            }

            //capa salida
            using (StreamWriter writer = new StreamWriter("C:/Users/55YV/Downloads/redes/ArchivosBackPropagation/pesosEntrenamientoSalida.txt", false))
            {
                for (int i = 0; i < neuronasCapaOculta2; i++)
                {
                    for (int j = 0; j < numSalidas; j++)
                    {
                        writer.Write(matrizPesoTres[i, j].ToString() + ";");
                    }
                    writer.Write("\n");
                }
            }

            using (StreamWriter writer = new StreamWriter("C:/Users/55YV/Downloads/redes/ArchivosBackPropagation/umbralesEntrenamientoSalida.txt", false))
            {
                for (int i = 0; i < vectorUmbralTres.Length; i++)
                {
                    writer.Write(vectorUmbralTres[i].ToString() + ";");
                }
            }

            Console.Write("\n");
            Console.WriteLine("Pesos y umbrales guardados");

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
            return Math.Round(resultado, 4);
        }

        private static void Simulacion(string patron)
        {
            Console.WriteLine("\nSimulacion \n");
            Console.WriteLine("entrada: " + patron);

            string dirPesos1 = "C:/Users/55YV/Downloads/redes/ArchivosBackPropagation/pesosEntrenamientoUno.txt";
            string dirUmbrales1 = "C:/Users/55YV/Downloads/redes/ArchivosBackPropagation/umbralesEntrenamientoUno.txt";
            string dirPesos2 = "C:/Users/55YV/Downloads/redes/ArchivosBackPropagation/pesosEntrenamientoDos.txt";
            string dirUmbrales2 = "C:/Users/55YV/Downloads/redes/ArchivosBackPropagation/umbralesEntrenamientoDos.txt";
            string dirPesos3 = "C:/Users/55YV/Downloads/redes/ArchivosBackPropagation/pesosEntrenamientoSalida.txt";
            string dirUmbrales3 = "C:/Users/55YV/Downloads/redes/ArchivosBackPropagation/umbralesEntrenamientoSalida.txt";
            string direccion = "C:/Users/55YV/Downloads/redes/ArchivosBackPropagation/problema.csv";

            int numEntradas = 0, numSalidas = 0;

            StreamReader sr = new StreamReader(direccion);
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

            int neuronasCapaOculta1 = 6, neuronasCapaOculta2 = 5;

            double[] vectorUmbralUno = new double[neuronasCapaOculta1];
            double[] vectorUmbralDos = new double[neuronasCapaOculta2];
            double[] vectorUmbralTres = new double[numSalidas];

            double[,] matrizPesoUno = new double[numEntradas, neuronasCapaOculta1];
            double[,] matrizPesoDos = new double[neuronasCapaOculta1, neuronasCapaOculta2];
            double[,] matrizPesoTres = new double[neuronasCapaOculta2, numSalidas];

            double[] SalidaRed1 = new double[neuronasCapaOculta1];
            double[] SalidaRed2 = new double[neuronasCapaOculta2];
            double[] SalidaRed3 = new double[numSalidas];


            //guardar pesos y umbrales 
            // 1
            int fila1 = File.ReadAllLines(dirPesos1).Length;
            StreamReader sreader = new StreamReader(dirPesos1);
            for (int f = 0; f < fila1; f++)
            {
                string linea = sreader.ReadLine();
                string[] numero = linea.Split(';');
                for (int c = 0; c < numero.Length - 1; c++)
                {
                    matrizPesoUno[f, c] = Convert.ToDouble(numero[c]);
                }
            }
            sreader.Close();

            StreamReader reader = new StreamReader(dirUmbrales1);
            string lineas = reader.ReadLine();
            string[] numeros = lineas.Split(';');
            for (int f = 0; f < numeros.Length - 1; f++)
            {
                vectorUmbralUno[f] = Convert.ToDouble(numeros[f]);
            }
            sreader.Close();
            // 2
            int fila2 = File.ReadAllLines(dirPesos2).Length;
            sreader = new StreamReader(dirPesos2);
            for (int f = 0; f < fila2; f++)
            {
                string linea = sreader.ReadLine();
                string[] numero = linea.Split(';');
                for (int c = 0; c < numero.Length - 1; c++)
                {
                    matrizPesoDos[f, c] = Convert.ToDouble(numero[c]);
                }
            }
            sreader.Close();

            reader = new StreamReader(dirUmbrales2);
            lineas = reader.ReadLine();
            string[] numeros2 = lineas.Split(';');
            for (int f = 0; f < numeros2.Length - 1; f++)
            {
                vectorUmbralDos[f] = Convert.ToDouble(numeros2[f]);
            }
            sreader.Close();
            // 3
            int fila3 = File.ReadAllLines(dirPesos3).Length;
            sreader = new StreamReader(dirPesos3);
            for (int f = 0; f < fila3; f++)
            {
                string linea = sreader.ReadLine();
                string[] numero = linea.Split(';');
                for (int c = 0; c < numero.Length - 1; c++)
                {
                    matrizPesoTres[f, c] = Convert.ToDouble(numero[c]);
                }
            }
            sreader.Close();

            reader = new StreamReader(dirUmbrales3);
            lineas = reader.ReadLine();
            string[] numeros3 = lineas.Split(';');
            for (int f = 0; f < numeros3.Length - 1; f++)
            {
                vectorUmbralTres[f] = Convert.ToDouble(numeros3[f]);
            }
            sreader.Close();

            //presentar el patron de entrada 
            double calcularSalida;
            string[] numeroPatron = patron.Split(';');
            double[] patronSimulado = new double[numEntradas];

            for (int w = 0; w < numEntradas; w++)
            {
                patronSimulado[w] = Convert.ToDouble(numeroPatron[w]);
                Console.WriteLine(patronSimulado[w]);
            }

            //calcular las salidas de la red 
            //capa 1
            double funcionSoma = 0;
            for (int q = 0; q < neuronasCapaOculta1; q++)
            {
                for (int j = 0; j < numEntradas; j++)
                {
                    funcionSoma = (patronSimulado[j] * matrizPesoUno[j, q]) + funcionSoma;
                }

                calcularSalida = (funcionSoma - vectorUmbralUno[q]);

                double h = Math.Tanh(calcularSalida);
                SalidaRed1[q] = Math.Round(h, 4);
            }

            //capa 2
            double funcionSoma2 = 0;
            for (int q = 0; q < neuronasCapaOculta2; q++)
            {
                for (int j = 0; j < neuronasCapaOculta1; j++)
                {
                    funcionSoma2 = (SalidaRed1[j] * matrizPesoDos[j, q]) + funcionSoma2;
                }

                calcularSalida = (funcionSoma2 - vectorUmbralDos[q]);

                double h = Math.Tanh(calcularSalida);
                SalidaRed2[q] = Math.Round(h, 4); ;
            }

            //salida 
            double funcionSoma3 = 0;
            for (int q = 0; q < numSalidas; q++)
            {
                for (int j = 0; j < neuronasCapaOculta2; j++)
                {
                    funcionSoma3 = (SalidaRed2[j] * matrizPesoTres[j, q]) + funcionSoma3;
                }

                calcularSalida = (funcionSoma3 - vectorUmbralTres[q]);

                double h = Math.Tanh(calcularSalida);
                SalidaRed3[q] = Math.Round(h);
            }

            Console.Write("salida: \n");
            string resultado = "";
            for (int i = 0; i < numSalidas; i++)
            {
                resultado = resultado + " " + Convert.ToString(SalidaRed3[i]);
            }
            Console.WriteLine(resultado);
        }
    }
}
