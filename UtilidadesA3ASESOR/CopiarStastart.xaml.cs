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
using System.IO;



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

        private void BtnSeleccionSTASTART_Click(object sender, RoutedEventArgs e)
        {

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                try
                {
                                        
                    string patron = "(^[a-zA-Z]:)(.*)(STASTART.INI)";
                    
                    

                    openFileDialog.InitialDirectory = CarpetaAplicacionesDestino.Text + "\\";
                    openFileDialog.Filter = "Ficheros (STASTART.INI)|STASTART.INI|Todos los ficheros (*.*)|*.*";
                    openFileDialog.FilterIndex = 1;
                    openFileDialog.Multiselect = false;
                    openFileDialog.RestoreDirectory = true;


                    if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {

                        //Get the path of specified file
                        stastartOrigen.Text = openFileDialog.FileName;

                        if (!Regex.IsMatch(stastartOrigen.Text, patron))
                        {
                            System.Windows.MessageBox.Show("El fichero seleccionado no es STASTART.INI o no lo has seleccionado desde una unidad de disco local o una unidad mapeada (por ejemplo: C: D: E: F: G: H: I: ...)", "Fichero o ruta no válidos", MessageBoxButton.OK, MessageBoxImage.Warning);
                            stastartOrigen.Text = "";                         
                        }
                        else
                        {
                            //string stringBeforeChar = authors.Substring(0, authors.IndexOf(","));
                            
                            //string stringBeforeChar = stastartOrigen.Text.Substring(0, stastartOrigen.Text.IndexOf("\\"));
                            //CarpetaAplicacionesDestino.Text =  stringBeforeChar;
                            //LlenarListadoCarpetas(CarpetaAplicacionesDestino.Text + "\\");



                        }

                        
                    }
                }
                catch(Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message, "Error en la selección del fichero STASTART.INI");
                }
                
            }

        }


        

        private void BtnSeleccionDestino_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog seleccionadorDeCarpetas = new FolderBrowserDialog();
            string patron = "^[a-zA-Z]:";

            if (seleccionadorDeCarpetas.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                CarpetaAplicacionesDestino.Text = seleccionadorDeCarpetas.SelectedPath;
                if (!Regex.IsMatch(CarpetaAplicacionesDestino.Text, patron))
                {
                    System.Windows.MessageBox.Show("La ruta seleccionada no es una unidad de disco local o una unidad mapeada (por ejemplo: C: D: E: F: G: H: I: ...)", "Ruta no válidos", MessageBoxButton.OK, MessageBoxImage.Warning);
                    CarpetaAplicacionesDestino.Text = "";
                }
                else
                {
                    ListadoCarpetas.Items.Clear();
                    LlenarListadoCarpetas(CarpetaAplicacionesDestino.Text);
                }

            }
        }



        public void LlenarListadoCarpetas(string ruta)
        {
            /*string separador = new string('_', nivel);
            DirectoryInfo listaDeDirectorios = new DirectoryInfo(ruta);
            if (listaDeDirectorios.Exists)
            {
                nivel += 2;
            }
            foreach (var directorio in listaDeDirectorios.EnumerateDirectories())
            {
                Console.WriteLine(separador + directorio.Name);
                string nuevaRuta = ruta + "\\" + directorio.Name;
                ArbolDeDirectorios(nuevaRuta, nivel);
            }*/




            //DriveInfo[] drives = DriveInfo.GetDrives();
            //foreach (DriveInfo driveInfo in drives)
            //    ListadoCarpetas.Items.Add(CreateTreeItem(driveInfo));

            ListadoCarpetas.Items.Clear();
            DirectoryInfo listaDeDirectorios = new DirectoryInfo(ruta);
            foreach (DirectoryInfo directorio in listaDeDirectorios.GetDirectories())
                ListadoCarpetas.Items.Add(CreateTreeItem(directorio));




        }



        public void TreeViewItem_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = e.Source as TreeViewItem;
            if ((item.Items.Count == 1) && (item.Items[0] is string))
            {
                item.Items.Clear();

                DirectoryInfo expandedDir = null;
                if (item.Tag is DriveInfo)
                    expandedDir = (item.Tag as DriveInfo).RootDirectory;
                if (item.Tag is DirectoryInfo)
                    expandedDir = (item.Tag as DirectoryInfo);
                try
                {
                    foreach (DirectoryInfo subDir in expandedDir.GetDirectories())
                        item.Items.Add(CreateTreeItem(subDir));
                }
                catch { }
            }
        }




        private TreeViewItem CreateTreeItem(object o)
        {
            TreeViewItem item = new TreeViewItem();
            item.Header = o.ToString();
            item.Tag = o;
            item.Items.Add("Loading...");
            return item;
        }




    }
}
