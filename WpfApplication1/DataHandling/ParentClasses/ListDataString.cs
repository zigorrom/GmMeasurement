using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GmMeasurement
{
    class ListDataString : IEnumerable<MeasurDataInterface>
    {
        private List<MeasurDataInterface> _ListOfData;
        private MeasurDataInterface _DataString;
        private string _FileHeader = "";
        private string _FileSubheader = "";
        private string _FileName = "";

        public List<MeasurDataInterface> ListOfData{ set {_ListOfData=value;} get {return _ListOfData;}}
        protected MeasurDataInterface DataString { set { _DataString = value; } get { return _DataString; } }
        public string FileHeader { set { _FileHeader = value; } get { return _FileHeader; } }
        public string FileSubheader {set {_FileSubheader=value; } get {return _FileSubheader;}}
        public string FileName {set {_FileName=value;  } get {return _FileName;}}

        private FileStream _MainFile;
        private StreamWriter _writeMainFile;
        
        public IEnumerator<MeasurDataInterface> GetEnumerator()
        {
            return _ListOfData.GetEnumerator();
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _ListOfData.GetEnumerator();
        }
        public string returnType() { return _DataString.GetType().ToString(); }
        public void readFromFile()
        {
            if (_ListOfData!=null)_ListOfData.Clear();
            _CheckFileForExistanceAndCreateFileWithHeaders();
            _MainFile = File.Open(this.FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);

            StreamReader readMainFile = new StreamReader(_MainFile);

            string ReadString;

            ReadString = readMainFile.ReadLine();//считывание первой строчки
            ReadString = readMainFile.ReadLine();//считывание второй строчки

            while ((ReadString = readMainFile.ReadLine()) != null)
            {
                if (ReadString != "")
                {

                    _DataString.parseFromString(ReadString);
                    _ListOfData.Add(_DataString.clone());
                }
            }
            
            readMainFile.Close();
            _MainFile.Close();
        }
        private void _CheckFileForExistanceAndCreateFileWithHeaders()
        {
            bool FileExists = File.Exists(this.FileName);
            _MainFile = File.Open(this.FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            _writeMainFile = new StreamWriter(_MainFile);
            if (!FileExists)
            {
                _writeMainFile.WriteLine(FileHeader);
                _writeMainFile.WriteLine(FileSubheader);
            }
            _writeMainFile.Close();
            _MainFile.Close();
        }
        public void OpenFileForWriting(bool Overwrite=true)
        {
            if(Overwrite)if(File.Exists(this._FileName))File.Delete(this._FileName);

            _CheckFileForExistanceAndCreateFileWithHeaders();

            _MainFile = File.Open(this.FileName, FileMode.Append, FileAccess.Write);
            _writeMainFile = new StreamWriter(_MainFile);
            
        }
        public void writeToFile()
        {
            bool BaseStreamWasNull = false;

            if ((_writeMainFile.BaseStream == null)) { this.OpenFileForWriting(); BaseStreamWasNull = true; }
            foreach (MeasurDataInterface Data in _ListOfData)
                _writeMainFile.WriteLine(Data.toString());
            if (BaseStreamWasNull) this.CloseFileForWriting();

        }
        public void CloseFileForWriting()
        {if(_writeMainFile.BaseStream!=null)
            _writeMainFile.Close();
            if(_MainFile!=null)
            _MainFile.Close();
        }
        public void Open_Write_CloseFile(bool Overwrite=true)
        {
            this.OpenFileForWriting();
            writeToFile();
            CloseFileForWriting();
            
        }


    }
}
