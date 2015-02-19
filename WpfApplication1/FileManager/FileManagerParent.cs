using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GmMeasurement
{
    class FileManagerParent
    {
        

        private string _workfolder;

        public string workfolder
        {
            get { return this._workfolder; }
            set { this._workfolder = value; this.readFolder(); }
        }
        public FileManagerParent(string Folder="")
        {
            workfolder = Folder;
            
            
        }
        protected bool readFolder()
        {
            if (!Directory.Exists(this._workfolder))
            {
                _workfolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            }
            else
            {
                Directory.SetCurrentDirectory(_workfolder);
                return true;
            }
            return false;
        }
        public bool FileExists(string filename)
        {
            return File.Exists(filename);
        }
        public string suggestFileNameWithIncrement(string filename)
        {
            int searchResult = filename.LastIndexOf('.');
            if (searchResult == -1) filename += ".dat";
            searchResult = filename.IndexOf('.');
            string fileNameWithoutExtension = filename.Substring(0, searchResult);
            string extension = filename.Substring(searchResult, filename.Length - searchResult);
            int filenumber=0;
            int digitsInNumber = 2;
            searchResult = fileNameWithoutExtension.LastIndexOf('-');
            if (searchResult != -1)
            {
                string LastSymbolsAfterDashInFilename = fileNameWithoutExtension.Substring(searchResult+1, fileNameWithoutExtension.Length-searchResult-1);
                bool isLastPartANumber = Int32.TryParse(LastSymbolsAfterDashInFilename, out filenumber);
                if (isLastPartANumber)
                {
                    filenumber++;
                    digitsInNumber = fileNameWithoutExtension.Length - searchResult-1;
                    fileNameWithoutExtension = fileNameWithoutExtension.Substring(0, searchResult);
                    
                }
                
            }


            filename = fileNameWithoutExtension +"-"+filenumber.ToString("D"+digitsInNumber)+extension;
            
            return filename;
        }
            
    }
}
