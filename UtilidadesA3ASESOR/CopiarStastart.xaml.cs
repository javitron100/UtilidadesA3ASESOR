using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


using System.Windows.Forms;
using System.Text.RegularExpressions;



namespace UtilidadesA3ASESOR
{
    /// <summary>
    /// Lógica de interacción para CopiarStastart.xaml
    /// </summary>
    public partial class CopiarStastart : Window
    {
        public CopiarStastart()
        {
            InitializeComponent();
        }

        private void BtnSeleccion_Click(object sender, RoutedEventArgs e)
        {

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                try
                {
                                        
                    string patron = "(^[a-zA-Z]:)(.*)(STASTART.INI)";
                    

                    openFileDialog.InitialDirectory = "C:\\";
                    openFileDialog.Filter = "Ficheros (STASTART.INI)|STASTART.INI|Todos los ficheros (*.*)|*.*";
                    openFileDialog.FilterIndex = 1;
                    openFileDialog.Multiselect = false;
                    openFileDialog.RestoreDirectory = true;


                    if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        //Get the path of specified file
                        txtPath.Text = openFileDialog.FileName;
                        


                        if (!Regex.IsMatch(txtPath.Text, patron))
                        {
                            System.Windows.MessageBox.Show("El fichero seleccionado no es STASTART.INI o no lo has seleccionado desde una unidad de disco local o una unidad mapeada (por ejemplo: C: D: E: F: G: H: I: ...)", "Fichero o ruta no válidos", MessageBoxButton.OK, MessageBoxImage.Warning);
                            txtPath.Text = "";
                        }

                        
                    }
                }
                catch(Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message, "Error en la selección del fichero STASTART.INI");
                }
                
            }

        }
                
    }
}
