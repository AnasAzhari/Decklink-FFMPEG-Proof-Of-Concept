using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;
using SIRModel;


namespace SIR_Online
{

    public class SurveyHelper
    {
        public static SerialPort SerialPort;
        public static string cOMPort;
        public static int baudRate;
        public static Parity parity;
        public static int dataBits;
        public static StopBits stopBits;

        public static SurveyConfigModel surveyConfigModel;

        public static string[] comPorts = SerialPort.GetPortNames();
        //public static string[] comPorts;
        public static int[] baudRateList = new int[1] { 9600 };
        public static Parity[] parities = new Parity[1] { Parity.None };
        public static int[] databitList = new int[1] { 8 };
        public static StopBits[] StopBitList = new StopBits[1] { StopBits.One };
        public static SFSystemSettings cf;

        public SurveyHelper(SerialPort _sp,SFSystemSettings _cf)
        {
            try
            {
                cf = _cf;
                surveyConfigModel = new SurveyConfigModel();
                //SerialPort = _sp;
                //SerialPort = new SerialPort();
                //comPorts = new string[1] { "ComTest" };
                SerialPort = _sp;
                //SetDefaultPortConfig();
                //NavConfigModel inConfig = new NavConfigModel
                //{
                //    IsCommaSeparated = true,
                //    CommaFieldIndexes = new Dictionary<string, int>
                //{
                //     { "Date", 0},
                //    { "Time", 1},
                //    { "KP", 4},
                //    { "Easting", 2},
                //    { "Northing", 3}
                //}
                //};
                //SetSurveyConfig(inConfig);


               // SerialPort.Open();
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
          
        }

        public void SetDefaultPortConfig()
        {
            if(SerialPort !=null && SerialPort.IsOpen)
            {
                SerialPort.Close();
            }
            cOMPort = comPorts[2];
            baudRate = baudRateList[0];
            parity = parities[0];
            dataBits = databitList[0];
            stopBits = StopBitList[0];

            SerialPort.PortName = cOMPort;
            SerialPort.BaudRate = baudRate;
            SerialPort.Parity = parity;
            SerialPort.DataBits = dataBits;
            SerialPort.StopBits = stopBits;

            SerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceived);
            
            //SerialPort.Open();
           
           
        }
        public void OpenPort()
        {
            if (SerialPort != null && SerialPort.IsOpen)
            {
                SerialPort.Close();
            }
            //SerialPort.DataReceived = null;
            //cOMPort = comPorts[0];
            //baudRate = baudRateList[0];
            //parity = parities[0];
            //dataBits = databitList[0];
            //stopBits = StopBitList[0];

            SerialPort.PortName = cOMPort;
            SerialPort.BaudRate = baudRate;
            SerialPort.Parity = parity;
            SerialPort.DataBits = dataBits;
            SerialPort.StopBits = stopBits;

            SerialPort.Open();
            SerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceived);

        }
        public void SetSurveyConfig(NavConfigModel inConfig = null)
        {
            try
            {
                if (inConfig !=null)
                {
                    
                    surveyConfigModel.SurveyInConfig = inConfig;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private static void DataReceived(object sender,SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            //Console.WriteLine(" data received");
            //if (surveyConfigModel.SurveyInConfig.IsCommaSeparated)
            //{
                var data = indata.Split(',');
                SurveyData.data = data;
                //Console.WriteLine("data length :" + data.Length);
                //if (data.Length == 5)
                //{
                //    SurveyData.Time = $"{data[surveyConfigModel.SurveyInConfig.CommaFieldIndexes["Date"]]} {data[surveyConfigModel.SurveyInConfig.CommaFieldIndexes["Time"]]}";
                //    SurveyData.KilometerPipeline = float.Parse(data[surveyConfigModel.SurveyInConfig.CommaFieldIndexes["KP"]]);
                //    SurveyData.Easting = float.Parse(data[surveyConfigModel.SurveyInConfig.CommaFieldIndexes["Easting"]]);
                //    SurveyData.Northing= float.Parse(data[surveyConfigModel.SurveyInConfig.CommaFieldIndexes["Northing"]]);

                //    //Console.WriteLine("TIme : " + SurveyData.Time);
                //    //Console.WriteLine(" Kilometer pipeleine : " + SurveyData.KilometerPipeline);
                //    //Console.WriteLine(" easting :" + SurveyData.Easting);



                //    //cf.UpdateDataStringLabel(SurveyData.Time, SurveyData.KilometerPipeline, SurveyData.Easting, SurveyData.Northing);

                //}
                Console.WriteLine(" survey Time :" + SurveyData.GetStringFromDictKey("Time"));
           // }
        }
    }

    public static class SurveyData
    {
        
       
        public static string[] data;
        static Action cbNavConfigChanged;
        public static NavConfigModel _NavConfigModel;
        public static NavConfigModel NavConfigModel
        {
            get
            {
                return _NavConfigModel;
            }
            set
            {
                NavConfigModel old = _NavConfigModel;
                NavConfigModel newncm = value;
                if (newncm != old)
                {
                    _NavConfigModel = newncm;
                    if (cbNavConfigChanged != null)
                    {
                        cbNavConfigChanged();
                    }
                    
                }
            }
        }

        public static void RegisterNavConfigChanged(Action callback)
        {
            cbNavConfigChanged += callback;
        }
        public static string[] GetStringsFromDictComma(string[] splittedData)
        {
            string[] vs = new string[splittedData.Length];
            for (int i = 0; i < splittedData.Length; i++)
            {
                
            }
            return vs;

        }
        public static string GetStringFromDictKey(string dictKey)
        {
            string reseult="";
            if (NavConfigModel == null)
            {
                Console.WriteLine("naveconfig mode null");
            }
            if (data != null)
            {
                try
                {
                    if (NavConfigModel.CommaFieldIndexes.ContainsKey(dictKey))
                        reseult = $"{data[NavConfigModel.CommaFieldIndexes[dictKey]]}";
                    else
                        reseult = "Can't find survey data key ";

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                    SurveyHelper.SerialPort.Close();
                }
                
            }
            return reseult;
        }
    }
   
    public class SurveyConfigModel
    {
        public NavConfigModel SurveyInConfig { get; set; }
        
    }




}
