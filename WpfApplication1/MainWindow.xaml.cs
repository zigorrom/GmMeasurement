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
        GmMeasurementFileManager SingleGateFileManager;
        DoubleGatedGmMeasurementFileManager DoubleGateFileManager;

        private void MeasureStimulationValue(object sender, System.Windows.RoutedEventArgs e)
        {
            if (ImportantConstants.MeasurementInProgress) return;
            StanfordSR830 MeaFG = new StanfordSR830("SR830", 0);
            StanfordSR830 MeaBG = new StanfordSR830("SR830", 1);
            double result = 1;
            if (MeaFG.isAlive)
                MeaFG.ReadSignal(out result);
            model.StimulationValueFG = result;
            if (MeaBG.isAlive)
                MeaBG.ReadSignal(out result);
            model.StimulationValueBG = result;
            MeaFG.Dispose();
            MeaBG.Dispose();
        }

        
       
        private void StartMeasurement(object sender, System.Windows.RoutedEventArgs e)
        {

            switch (model.DoubleGatedMeasurement)
            {
                case true: DoubleGateMeasurement(); break;
                case false: SingleGateMeasurement(); break;
            }
        	// TODO: Add event handler implementation here.
        }
        GmMeasurement SingleGateMeasurer;
        private void SingleGateMeasurement()
        {
            ProgressBar1.Value = 0;
            GmMeasurementParameters DefinedInFormMeasurementSettings = new GmMeasurementParameters(model);
            try
            {
                SingleGateMeasurer = new GmMeasurement(DefinedInFormMeasurementSettings);
            }
            catch (Exception ebl)
            {
                MessageBox.Show(ebl.Message);
                return;
            }
            GraphicManager = new I_V_Graph(MyZedGraph);
            GraphicManager.PrepareForMeasurement(DefinedInFormMeasurementSettings.BackGateVoltageStart);
            AllCustomEvents.Instance.MeasurementProgressReported += SetProgressBar;
            AllCustomEvents.Instance.MeasurementFinished += FinishAllAfterMeasurement;
            SingleGateFileManager = new GmMeasurementFileManager(model.WorkFolder);
            while (SingleGateFileManager.FileExists(model.FileName)) model.FileName = SingleGateFileManager.suggestFileNameWithIncrement(model.FileName);
            SingleGateFileManager.PrepareForMeasurement(model.FileName);
            ProgressBar1.Value = 0;
            SingleGateMeasurer.StartGmMeasurement();
        }



        DoubleGatedGmMeasurement DoubleGateMeasurer;
        private void DoubleGateMeasurement()
        {
            ProgressBar1.Value = 0;
            DoubleGatedGmMeasurementParameters DefinedInFormMeasurementSettings = new DoubleGatedGmMeasurementParameters(model);
            try
            {
                DoubleGateMeasurer = new DoubleGatedGmMeasurement(DefinedInFormMeasurementSettings);
            }
            catch (Exception ebl)
            {
                MessageBox.Show(ebl.Message);
                return;
            }
            GraphicManager = new I_V_Graph(MyZedGraph);
            GraphicManager.PrepareForMeasurement(DefinedInFormMeasurementSettings.BackGateVoltageStart);
            AllCustomEvents.Instance.MeasurementProgressReported += SetProgressBar;
            AllCustomEvents.Instance.MeasurementFinished += FinishAllAfterMeasurement;
            DoubleGateFileManager = new DoubleGatedGmMeasurementFileManager(model.WorkFolder);
            while (DoubleGateFileManager.FileExists(model.FileName)) model.FileName = DoubleGateFileManager.suggestFileNameWithIncrement(model.FileName);
            DoubleGateFileManager.PrepareForMeasurement(model.FileName);
            ProgressBar1.Value = 0;
            DoubleGateMeasurer.StartGmMeasurement();
        }
        private void FinishAllAfterMeasurement(object sender, EventArgs e)
        {
            GraphicManager.FinishMeasurement();
            if (!model.DoubleGatedMeasurement)
            {
                SingleGateFileManager.FinishMeasurement();
                SingleGateMeasurer = null;
            }
            else
            {
                DoubleGateFileManager.FinishMeasurement();
                DoubleGateMeasurer = null;
            }
        }

        private void StopMeasurement(object sender, System.Windows.RoutedEventArgs e)
        {
        	// TODO: Add event handler implementation here.
            if (ImportantConstants.MeasurementInProgress)
            {
                if (!model.DoubleGatedMeasurement)
                    SingleGateMeasurer.StopGmMeasurement();
                else
                    DoubleGateMeasurer.StopGmMeasurement();
            }
            
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
