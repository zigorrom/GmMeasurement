using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using StanfordLockInSR830;


using System.ComponentModel;
namespace GmMeasurement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region MVVM interactions

        MainWindow_DataModel model = new MainWindow_DataModel();

        #endregion

        public MainWindow()
        {
            InitializeComponent();
            ProgressBar1.Maximum = 100;
            ProgressBar1.Minimum = 0;
            this.DataContext=model;
        }
        #region boring events (Simple Main Form relations)
        private void FrontGateNoSweepClick(object sender, System.Windows.RoutedEventArgs e)
        {
			model.FrontGateVoltageNumberOfPoints=1;
        }

        private void BackGateNoSweepClick(object sender, System.Windows.RoutedEventArgs e)
        {
        	model.BackGateVoltageNumberOfPoints=1;
        }

        private void OpenFolderClick(object sender, System.Windows.RoutedEventArgs e)
        {
        	var dialog = new System.Windows.Forms.FolderBrowserDialog();
System.Windows.Forms.DialogResult result = dialog.ShowDialog();
			if (result==System.Windows.Forms.DialogResult.OK)
            {
                model.WorkFolder = dialog.SelectedPath;
            }
            else
            {
                if (model.WorkFolder == "")
                    model.WorkFolder = System.IO.Path.GetTempPath();
            }
        }

       		
		#endregion
        #region FormMeasurementActivity

        I_V_Graph GraphicManager;
        GmMeasurementFileManager FileManager;
        private void MeasureStimulationValue(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ImportantConstants.MeasurementInProgress) return;
                StanfordSR830 Mea = new StanfordSR830("SR830");
                double result = 1;
                if (Mea.isAlive)
                    Mea.ReadSignal(out result);
                model.StimulationValue = result;
                Mea.Dispose();
            }

        GmMeasurement MainMeasurer;
        private void StartMeasurement(object sender, System.Windows.RoutedEventArgs e)
        {
            ProgressBar1.Value = 0; 
            GmMeasurementParameters DefinedInFormMeasurementSettings = new GmMeasurementParameters(model);
            try
            {
                MainMeasurer = new GmMeasurement(DefinedInFormMeasurementSettings);
            }
            catch(Exception ebl )
            {
                MessageBox.Show(ebl.Message);
                return;
            }
            GraphicManager = new I_V_Graph(MyZedGraph);
            GraphicManager.PrepareForMeasurement(DefinedInFormMeasurementSettings.BackGateVoltageStart);
            AllCustomEvents.Instance.MeasurementProgressReported += SetProgressBar;
            AllCustomEvents.Instance.MeasurementFinished += FinishAllAfterMeasurement;
            FileManager = new GmMeasurementFileManager(model.WorkFolder);
            while (FileManager.FileExists(model.FileName)) model.FileName = FileManager.suggestFileNameWithIncrement(model.FileName);
            FileManager.PrepareForMeasurement(model.FileName);
            ProgressBar1.Value = 0;
            MainMeasurer.StartGmMeasurement();
            
        	// TODO: Add event handler implementation here.
        }
        private void FinishAllAfterMeasurement(object sender, EventArgs e)
        {
            
            GraphicManager.FinishMeasurement();
            FileManager.FinishMeasurement();
            MainMeasurer = null;
        }

        private void StopMeasurement(object sender, System.Windows.RoutedEventArgs e)
        {
        	// TODO: Add event handler implementation here.
            if(ImportantConstants.MeasurementInProgress)
            MainMeasurer.StopGmMeasurement();
            
        }
        private void SetProgressBar(object sender, MeasurementProgressReportedEventArgs eventArgs)
        
        {
            
            ProgressBar1.Value = eventArgs.percentProgress;
            

        }
        private void Test(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            
        }
        #endregion
    }
}
