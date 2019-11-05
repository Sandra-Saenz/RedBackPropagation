﻿using System;
using System.IO;

namespace CapaDominio
{
    public class BackPropagation
    {
        public double GenerarNumeroAleatorio()
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

        private static string Normalizar(string direccionArchivo)
        {
            // normalizar el archivo de datos categoricos a numericos, desde el UI se llamara esta funcion primero
            // y se le dara una direccion con el archivo se hace todo y envia otra direccion con un nuevo archivo normalizado

            return direccionArchivo;
        }

        public void Entrenamiento(int iteraciones, int numEntradas, int numSalidas,
            int Patrones, string direccion)
        {
            int num = 1; double emp = 0.0001; double erms;
            double rataAprendizaje = 0.1;
            int neuronasCapaOculta1 = 6, neuronasCapaOculta2 = 5;

            double[] vectorUmbralUno = new double[neuronasCapaOculta1];
            double[] vectorUmbralDos = new double[neuronasCapaOculta2];
            double[] vectorUmbralTres = new double[numSalidas];

            double[,] matrizPesoUno = new double[numEntradas, neuronasCapaOculta1];
            double[,] matrizPesoDos = new double[neuronasCapaOculta1, neuronasCapaOculta2];
            double[,] matrizPesoTres = new double[neuronasCapaOculta2, numSalidas];

            double[,] matrizProblema = new double[Patrones, numEntradas + numSalidas];
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

            bool detener = false;
            while (num <= iteraciones && detener == false)
            {
                //una iteracion es pasar por todos los patrones
                for (int i = 0; i < Patrones; i++)
                {
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

                        double calcularSalida = (Math.Truncate((funcionSoma - vectorUmbralUno[q]) * 10000) / 10000);

                        double h = Math.Tanh(calcularSalida);
                        SalidaRed1[q] = h;
                    }

                    //capa 2
                    double funcionSoma2 = 0;
                    for (int q = 0; q < neuronasCapaOculta2; q++)
                    {
                        for (int j = 0; j < neuronasCapaOculta1; j++)
                        {
                            funcionSoma2 = (SalidaRed1[j] * matrizPesoDos[j, q]) + funcionSoma2;
                        }

                        double calcularSalida = (Math.Truncate((funcionSoma2 - vectorUmbralDos[q]) * 10000) / 10000);

                        double h = Math.Tanh(calcularSalida);
                        SalidaRed2[q] = h;
                    }

                    //salida 
                    double funcionSoma3 = 0;
                    for (int q = 0; q < numSalidas; q++)
                    {
                        for (int j = 0; j < neuronasCapaOculta2; j++)
                        {
                            funcionSoma3 = (SalidaRed2[j] * matrizPesoTres[j, q]) + funcionSoma3;
                        }

                        double calcularSalida = (Math.Truncate((funcionSoma3 - vectorUmbralTres[q]) * 10000) / 10000);

                        double h = Math.Tanh(calcularSalida);
                        SalidaRed3[q] = h;
                    }

                    //calcular los errores lineales producidos a la salida
                    for (int ii = 0; ii < numSalidas; ii++)
                    {
                        erroresLineales[ii] = (Math.Truncate((vectorSalida[ii] - SalidaRed3[ii]) * 10000) / 10000);
                    }

                    //calcular los errores no lineales de cada capa
                    //capa 2
                    double nolineal = 0;
                    for (int l = 0; l < neuronasCapaOculta2; l++)
                    {
                        for (int w = 0; w < numSalidas; w++)
                        {
                            nolineal = (Math.Truncate(erroresLineales[w] * matrizPesoTres[l, w] + nolineal) / 10000);
                        }
                        erroresNoLinealesDos[l] = nolineal;
                    }

                    //capa 1
                    for (int l = 0; l < neuronasCapaOculta1; l++)
                    {
                        for (int w = 0; w < neuronasCapaOculta2; w++)
                        {
                            nolineal = (Math.Truncate(erroresNoLinealesDos[w] * matrizPesoDos[l, w] + nolineal) / 10000);
                        }
                        erroresNoLinealesUno[l] = nolineal;
                    }

                    //calcular el error del patron
                    double sumaErrores = 0;
                    for (int a = 0; a < numSalidas; a++)
                    {
                        sumaErrores = erroresLineales[a] + sumaErrores;
                    }

                    errorPatron[i] = (Math.Truncate((sumaErrores / numSalidas) * 10000) / 10000);

                    //modificar pesos    
                    for (int z = 0; z < numEntradas; z++)
                    {
                        for (int x = 0; x < neuronasCapaOculta1; x++)
                        {
                            matrizPesoUno[z, x] = (Math.Truncate((matrizPesoUno[z, x] + 2 * rataAprendizaje * erroresNoLinealesUno[x] * (Math.Tanh(SalidaRed1[x])) * vectorEntrada[z]) * 10000) / 10000);
                            
                        }
                    }

                    for (int x = 0; x < neuronasCapaOculta1; x++)
                    {
                        vectorUmbralUno[x] = (Math.Truncate((vectorUmbralUno[x] + 2 * rataAprendizaje * erroresNoLinealesUno[x] * (Math.Tanh(SalidaRed1[x])) * 1) * 10000) / 10000);
                        
                    }

                    for (int z = 0; z < neuronasCapaOculta1; z++)
                    {
                        for (int x = 0; x < neuronasCapaOculta2; x++)
                        {
                            matrizPesoDos[z, x] = (Math.Truncate((matrizPesoDos[z, x] + 2 * rataAprendizaje * erroresNoLinealesDos[x] * (Math.Tanh(SalidaRed2[x])) * SalidaRed1[x]) * 10000) / 10000);
                            
                        }
                    }

                    for (int x = 0; x < neuronasCapaOculta2; x++)
                    {
                        vectorUmbralDos[x] = (Math.Truncate((vectorUmbralDos[x] + 2 * rataAprendizaje * erroresNoLinealesDos[x] * (Math.Tanh(SalidaRed2[x])) * 1) * 10000) / 10000);
                        
                    }

                    for (int z = 0; z < neuronasCapaOculta2; z++)
                    {
                        for (int x = 0; x < numSalidas; x++)
                        {
                            matrizPesoTres[z, x] = (Math.Truncate((matrizPesoTres[z, x] + 2 * rataAprendizaje * erroresLineales[x] * SalidaRed2[z]) * 10000) / 10000);
                            
                        }
                    }

                    for (int x = 0; x < numSalidas; x++)
                    {
                        vectorUmbralTres[x] = (Math.Truncate((vectorUmbralTres[x] + 2 * rataAprendizaje * erroresLineales[x] * 1) * 10000) / 10000);
                        
                    }
                }

                //calcular el error del entrenamiento (con este se hace la grafica)
                double sumaErrorPatron = 0;
                for (int l = 0; l < Patrones; l++)
                {
                    sumaErrorPatron = Math.Abs(errorPatron[l] + sumaErrorPatron);
                }

                erms = (Math.Truncate((sumaErrorPatron / Patrones) * 10000) / 10000);

                if (erms <= emp)
                {
                    detener = true;
                }
                num++;
            }

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
        }

        private static void Simulacion(string patron)
        {

        }
    }
}
