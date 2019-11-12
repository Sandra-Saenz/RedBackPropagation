using CapaDominio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessageBox = System.Windows.Forms.MessageBox;

namespace CapaUI
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BackPropagation backPropagation = new BackPropagation();

        public MainWindow()
        {
            InitializeComponent();
        }

        private int ObtenerNIteraciones()
        {
            return Convert.ToInt32(TxtIteracion.Text);
        }


        private void CargarSolucionEnDGV()
        {
            lbTablaSolucion.Items.Clear();
            string dirPeso = "C:/Users/55YV/Downloads/redes/ArchivosBackPropagation/pesosEntrenamientoUno.txt";
            string dirUmbral = "C:/Users/55YV/Downloads/redes/ArchivosBackPropagation/umbralesEntrenamientoUno.txt";
            string dirPeso2 = "C:/Users/55YV/Downloads/redes/ArchivosBackPropagation/pesosEntrenamientoDos.txt";
            string dirUmbral2 = "C:/Users/55YV/Downloads/redes/ArchivosBackPropagation/umbralesEntrenamientoDos.txt";
            string dirPeso3 = "C:/Users/55YV/Downloads/redes/ArchivosBackPropagation/pesosEntrenamientoSalida.txt";
            string dirUmbral3 = "C:/Users/55YV/Downloads/redes/ArchivosBackPropagation/umbralesEntrenamientoSalida.txt";

            try
            {
                //capa 1
                StreamReader import = new StreamReader(dirPeso);
                lbTablaSolucion.Items.Add("TABLA SOLUCION DEL ENTRENAMIENTO");
                lbTablaSolucion.Items.Add("Matriz de peso 1");
                while (import.Peek() >= 0)
                {
                    string linea = import.ReadLine();
                    string numeros = linea.Replace(";", "   ");
                    lbTablaSolucion.Items.Add(Convert.ToString(numeros));
                }
                StreamReader importU = new StreamReader(dirUmbral);
                lbTablaSolucion.Items.Add("Vector de umbral capa 1");
                while (importU.Peek() >= 0)
                {
                    string lineas = importU.ReadLine();
                    string numero = lineas.Replace(";", "   ");
                    lbTablaSolucion.Items.Add(Convert.ToString(numero));
                }

                //capa 2
                StreamReader importq = new StreamReader(dirPeso2);
                lbTablaSolucion.Items.Add(" ");
                lbTablaSolucion.Items.Add("Matriz de peso 2");
                while (importq.Peek() >= 0)
                {
                    string linea = importq.ReadLine();
                    string numeros = linea.Replace(";", "   ");
                    lbTablaSolucion.Items.Add(Convert.ToString(numeros));
                }
                StreamReader importe = new StreamReader(dirUmbral2);
                lbTablaSolucion.Items.Add("Vector de umbral capa 2");
                while (importe.Peek() >= 0)
                {
                    string lineas = importe.ReadLine();
                    string numero = lineas.Replace(";", "   ");
                    lbTablaSolucion.Items.Add(Convert.ToString(numero));
                }

                //capa 3
                StreamReader importr = new StreamReader(dirPeso3);
                lbTablaSolucion.Items.Add(" ");
                lbTablaSolucion.Items.Add("Matriz de peso salida");
                while (importr.Peek() >= 0)
                {
                    string linea = importr.ReadLine();
                    string numeros = linea.Replace(";", "   ");
                    lbTablaSolucion.Items.Add(Convert.ToString(numeros));
                }
                StreamReader importt = new StreamReader(dirUmbral3);
                lbTablaSolucion.Items.Add("Vector de umbral salida");
                while (importt.Peek() >= 0)
                {
                    string lineas = importt.ReadLine();
                    string numero = lineas.Replace(";", "   ");
                    lbTablaSolucion.Items.Add(Convert.ToString(numero));
                }

                import.Close(); importt.Close(); importr.Close();
                importU.Close(); importe.Close(); importq.Close();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(Convert.ToString(ex.Message));
                return;
            }

        }

        private void RealizarEntrenamiento()
        {
            int entrada = 0, salida = 0; string direccion = TxtArchivo.Text;

            StreamReader sr = new StreamReader(direccion);
            int numeroFilas = File.ReadAllLines(direccion).Length;

            for (int f = 0; f < 1; f++)
            {
                string linea = sr.ReadLine();

                for (int c = 0; c < linea.Length; c++)
                {
                    if (Convert.ToString(linea[c]) == "x" || Convert.ToString(linea[c]) == "X")
                    {
                        entrada++;
                    }
                    else if (Convert.ToString(linea[c]) == "y" || Convert.ToString(linea[c]) == "Y")
                    {
                        salida++;
                    }
                }
            }
            sr.Close();

            int numeroIteraciones = ObtenerNIteraciones();
            int numeroEntradas = entrada;
            int numeroSalidas = salida;
            int numeroPatrones = numeroFilas - 1;
            double rata = Convert.ToDouble(TxtRata.Text);
            double errorMax = Convert.ToDouble(TxtErrorMax.Text);

            //llamar al metodo normalizar y enviarle la direccion y colocar el return en la sgte funcion
            backPropagation.Entrenamiento(numeroIteraciones, numeroEntradas, numeroSalidas, numeroPatrones, direccion, rata, errorMax);
        }

        private void Grafica()
        {

        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            TxtIteracion.Text = "";
            TxtArchivo.Text = "";
            TxtRata.Text = "";
            LbTablaSimulacion.Items.Clear();
            lbTablaSolucion.Items.Clear();
            lbTablaProblema.Items.Clear();
        }

        private void BtnExaminar_Click(object sender, RoutedEventArgs e)
        {
            this.lbTablaProblema.Items.Clear();
            OpenFileDialog open = new OpenFileDialog();
            //open.Filter = "Archivo txt (*.txt)|*.txt|All(*,*)|*,*";
            try
            {
                open.ShowDialog();
                TxtArchivo.Text = open.FileName;
                //llamar funcion par normalizar datos aqui
                StreamReader import = new StreamReader(Convert.ToString(TxtArchivo.Text));
                lbTablaProblema.Items.Add("TABLA DEL PROBLEMA");
                while (import.Peek() >= 0)
                {
                    string linea = import.ReadLine();
                    string numeros = linea.Replace(",", "   ");
                    lbTablaProblema.Items.Add(Convert.ToString(numeros));
                }
                import.Close();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(Convert.ToString(ex.Message));
                return;
            }
        }

        private void BtnEntrenar_Click(object sender, RoutedEventArgs e)
        {
            RealizarEntrenamiento();
            Grafica();
            MessageBox.Show("Entrenamiento realizado.");
            CargarSolucionEnDGV();
        }

        private void BtnSimular_Click(object sender, RoutedEventArgs e)
        {
            LbTablaSimulacion.Items.Clear();
            string patron = TxtPatron.Text;
            string resultado = backPropagation.Simulacion(patron);

            try
            {
                LbTablaSimulacion.Items.Add("TABLA RESULTADO DE LA SIMULACION");
                LbTablaSimulacion.Items.Add(resultado);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex.Message));
                return;
            }
        }
    }
}
