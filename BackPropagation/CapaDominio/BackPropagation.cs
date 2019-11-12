using System;
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
            return Math.Round(resultado, 4);
        }

        public string Normalizar(string direccionArchivo)
        {
            // normalizar el archivo de datos categoricos a numericos, desde el UI se llamara esta funcion primero
            // y se le dara una direccion con el archivo se hace todo y envia otra direccion con un nuevo archivo normalizado

            return direccionArchivo;
        }

        public void Entrenamiento(int iteraciones, int numEntradas, int numSalidas,
            int Patrones, string direccion, double rata, double errorMax)
        {
            int num = 1; double emp = errorMax; double erms;
            double rataAprendizaje = rata;
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

                        double calcularSalida = (funcionSoma - vectorUmbralUno[q]);

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

                        double calcularSalida = (funcionSoma2 - vectorUmbralDos[q]);

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

                        double calcularSalida = (funcionSoma3 - vectorUmbralTres[q]);

                        double h = Math.Tanh(calcularSalida);
                        SalidaRed3[q] = h;
                    }

                    //calcular los errores lineales producidos a la salida
                    for (int ii = 0; ii < numSalidas; ii++)
                    {
                        erroresLineales[ii] = vectorSalida[ii] - SalidaRed3[ii];
                    }

                    //calcular los errores no lineales de cada capa
                    //capa 2
                    double nolineal = 0;
                    for (int l = 0; l < neuronasCapaOculta2; l++)
                    {
                        for (int w = 0; w < numSalidas; w++)
                        {
                            nolineal = erroresLineales[w] * matrizPesoTres[l, w] + nolineal;
                        }
                        erroresNoLinealesDos[l] = nolineal;
                    }

                    //capa 1
                    for (int l = 0; l < neuronasCapaOculta1; l++)
                    {
                        for (int w = 0; w < neuronasCapaOculta2; w++)
                        {
                            nolineal = erroresNoLinealesDos[w] * matrizPesoDos[l, w] + nolineal;
                        }
                        erroresNoLinealesUno[l] =  nolineal;
                    }

                    //calcular el error del patron
                    double sumaErrores = 0;
                    for (int a = 0; a < numSalidas; a++)
                    {
                        sumaErrores = erroresLineales[a] + sumaErrores;
                    }

                    errorPatron[i] = sumaErrores / numSalidas;

                    //modificar pesos    algoritmo de entrenamiento
                    for (int z = 0; z < numEntradas; z++)
                    {
                        for (int x = 0; x < neuronasCapaOculta1; x++)
                        {
                            matrizPesoUno[z, x] = (matrizPesoUno[z, x] + 2 * rataAprendizaje * erroresNoLinealesUno[x] * (1 / (Math.Cosh(SalidaRed1[x]) * Math.Cosh(SalidaRed1[x]))) * vectorEntrada[z]);
                            
                        }
                    }

                    for (int x = 0; x < neuronasCapaOculta1; x++)
                    {
                        vectorUmbralUno[x] = (vectorUmbralUno[x] + 2 * rataAprendizaje * erroresNoLinealesUno[x] * (1 / (Math.Cosh(SalidaRed1[x]) * Math.Cosh(SalidaRed1[x]))) * 1);
                        
                    }

                    for (int z = 0; z < neuronasCapaOculta1; z++)
                    {
                        for (int x = 0; x < neuronasCapaOculta2; x++)
                        {
                            matrizPesoDos[z, x] = (matrizPesoDos[z, x] + 2 * rataAprendizaje * erroresNoLinealesDos[x] * (1 / (Math.Cosh(SalidaRed2[x]) * Math.Cosh(SalidaRed2[x]))) * SalidaRed1[z]);
                            
                        }
                    }

                    for (int x = 0; x < neuronasCapaOculta2; x++)
                    {
                        vectorUmbralDos[x] = (vectorUmbralDos[x] + 2 * rataAprendizaje * erroresNoLinealesDos[x] * (1 / (Math.Cosh(SalidaRed2[x]) * Math.Cosh(SalidaRed2[x]))) * 1);
                        
                    }

                    for (int z = 0; z < neuronasCapaOculta2; z++)
                    {
                        for (int x = 0; x < numSalidas; x++)
                        {
                            matrizPesoTres[z, x] = (matrizPesoTres[z, x] + 2 * rataAprendizaje * erroresLineales[x] * SalidaRed2[z]);
                            
                        }
                    }

                    for (int x = 0; x < numSalidas; x++)
                    {
                        vectorUmbralTres[x] = (vectorUmbralTres[x] + 2 * rataAprendizaje * erroresLineales[x] * 1);
                        
                    }
                }

                //calcular el error del entrenamiento (con este se hace la grafica)
                double sumaErrorPatron = 0;
                for (int l = 0; l < Patrones; l++)
                {
                    sumaErrorPatron = Math.Abs(errorPatron[l] + sumaErrorPatron);
                }

                erms = (sumaErrorPatron / Patrones);

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
                        writer.Write(Math.Round(matrizPesoUno[i, j], 4).ToString() + ";");
                    }
                    writer.Write("\n");
                }
            }

            using (StreamWriter writer = new StreamWriter("C:/Users/55YV/Downloads/redes/ArchivosBackPropagation/umbralesEntrenamientoUno.txt", false))
            {
                for (int i = 0; i < vectorUmbralUno.Length; i++)
                {
                    writer.Write(Math.Round(vectorUmbralUno[i], 4).ToString() + ";");
                }
            }

            //capa 2
            using (StreamWriter writer = new StreamWriter("C:/Users/55YV/Downloads/redes/ArchivosBackPropagation/pesosEntrenamientoDos.txt", false))
            {
                for (int i = 0; i < neuronasCapaOculta1; i++)
                {
                    for (int j = 0; j < neuronasCapaOculta2; j++)
                    {
                        writer.Write(Math.Round(matrizPesoDos[i, j], 4).ToString() + ";");
                    }
                    writer.Write("\n");
                }
            }

            using (StreamWriter writer = new StreamWriter("C:/Users/55YV/Downloads/redes/ArchivosBackPropagation/umbralesEntrenamientoDos.txt", false))
            {
                for (int i = 0; i < vectorUmbralDos.Length; i++)
                {
                    writer.Write(Math.Round(vectorUmbralDos[i], 4).ToString() + ";");
                }
            }

            //capa salida
            using (StreamWriter writer = new StreamWriter("C:/Users/55YV/Downloads/redes/ArchivosBackPropagation/pesosEntrenamientoSalida.txt", false))
            {
                for (int i = 0; i < neuronasCapaOculta2; i++)
                {
                    for (int j = 0; j < numSalidas; j++)
                    {
                        writer.Write(Math.Round(matrizPesoTres[i, j], 4).ToString() + ";");
                    }
                    writer.Write("\n");
                }
            }
            
            using (StreamWriter writer = new StreamWriter("C:/Users/55YV/Downloads/redes/ArchivosBackPropagation/umbralesEntrenamientoSalida.txt", false))
            {
                for (int i = 0; i < vectorUmbralTres.Length; i++)
                {
                    writer.Write(Math.Round(vectorUmbralTres[i], 4).ToString() + ";");
                }
            }
        }

        public string Simulacion(string patron)
        {
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

                calcularSalida = (funcionSoma2 - vectorUmbralDos[q]);

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

                calcularSalida = (funcionSoma3 - vectorUmbralTres[q]);

                double h = Math.Tanh(calcularSalida);
                SalidaRed3[q] = Math.Round(h,2);
            }

            string resultado = "";
            for (int i = 0; i < numSalidas; i++)
            {
                resultado = resultado + " " + Convert.ToString(SalidaRed3[i]);
            }

            return resultado;
        }

    }
}
