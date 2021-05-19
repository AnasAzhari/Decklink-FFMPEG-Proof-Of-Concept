using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIRModel.Online;
using SIRModel.Eventing;
using System.Configuration;
using System.IO;
using DeckLinkAPI;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Serialization.Formatters.Binary;

namespace SIR_Online
{
    public class SIROnlineSession
    {
        
        Dive currentDive;
        
        static SIROnlineSession _instance;
        public Bitmap logo;

        //public DeckLinkDeviceDiscovery m_deckLinkDiscovery;
       // public DeckLinkInputDevice m_SelectedCaptureDevice;
       // public Bgra32FrameConverter m_frameConverter;
       // public DeckLinkOutputDevice m_selectedPlaybackDevice;

        public static SIROnlineSession Instance { get
            {
                if (_instance == null)
                {
                    _instance = new SIROnlineSession();
                    return _instance;
                }
                else
                {
                    return _instance;
                }

            } }

        public SIROnlineSession()
        {
            EventLogs = new List<EventLog>();
            //m_deckLinkDiscovery = new DeckLinkDeviceDiscovery();
            //m_frameConverter = new Bgra32FrameConverter();
            //logo = new Bitmap(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "logo.jpg"));
            //InstantiateDirectories();
            RegisterDiveStarted(MakeFoldersByStructure);
        }
        public List<EventLog> EventLogs;
        Action cbSelectedCurrentProject;
        Action<Structure> cbDiveStart;
        OnlineProject _onlineProject;
        public SFSystemSettings SFSystemSettings;
        public OnlineProject CurrentProject
        {
            get { return _onlineProject; }
             set
            {
                OnlineProject Oldproject = _onlineProject;
                OnlineProject NewProject = value;
                if (Oldproject != NewProject)
                {
                    _onlineProject = NewProject;
                    if(cbSelectedCurrentProject != null){
                        cbSelectedCurrentProject();
                    }
                }
            }
        }
        Structure _currStructure;
        public Structure CurrentStructure
        {
            get { return _currStructure; }
            set {
            
                    if (value == null)
                    {
                        throw new Exception("null  value");
                        return;
                    }
                    if (!CurrentProject.structures.Contains(value))
                    {
                        throw new Exception("Project doesn't contain the given structure");
                        return;
                    }
                    Structure oldStructure = _currStructure;
                    Structure newStructure = value;

                if (oldStructure != newStructure && oldStructure != null)
                {
                    _currStructure = newStructure;
                    cbDiveStart(_currStructure);
                }
                else if (oldStructure == null)
                {
                    _currStructure = newStructure;
                    if (cbDiveStart != null)
                    {
                        cbDiveStart(_currStructure);
                    }

                }
                          
            }
        }
        DivingType _currDiveType;
        public DivingType CurrentDiveType
        {
            get
            {
                return _currDiveType;
            }
            set
            {
                _currDiveType = value;
            }
        }


        public void RegisterDiveStarted(Action<Structure> _callback)
        {
            cbDiveStart += _callback;

        }
        public void RegisterCurrentProjectSelected(Action _callback)
        {
            cbSelectedCurrentProject += _callback;
        }


        public string GetEventingSavePathLocation()
        {
            // navigate to sir system dir
            string currentdir = Directory.GetCurrentDirectory();
            string sirsystempath=currentdir.Trim();
            DirectoryInfo binDir = Directory.GetParent(currentdir);
            DirectoryInfo sironlinedir = Directory.GetParent(binDir.FullName);
            DirectoryInfo sironlinemain = Directory.GetParent(sironlinedir.FullName);
            DirectoryInfo sirsystemdir = Directory.GetParent(sironlinemain.FullName);
            string sirSystemDir = sirsystemdir.FullName;

            //navigate to sir eventing event log
            string tempEventlogfile = sirSystemDir + "\\SIR Eventing\\SIR Eventing\\bin\\Debug\\Event Logs\\TempEventLog.json";


            //C:\\Users\\AnasRig\\Documents\\SIR Eventing\\SIR System\\SIR Online\\SIR Online\\bin\\Debug
            return tempEventlogfile;
        }

        public string FileBaseSavePath()
        {
            string currentdir = Directory.GetCurrentDirectory();
            return currentdir;
        }

        public void SyncWithEventing()
        {
            SaveAndLoadJson.SyncEventLogFromEventing();

        }

        void InstantiateDirectories()
        {
            //string savepath;

            //savepath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Dive");
            //if (!Directory.Exists(savepath))
            //{
            //    Directory.CreateDirectory(savepath);
            //}
          
            //// snapShot dir
            //string SavePathSnapshot = System.IO.Path.Combine(savepath, "SnapShot");
            //if (!Directory.Exists(SavePathSnapshot))
            //{
            //    Directory.CreateDirectory(SavePathSnapshot);
            //}
            
            //// video dir
            //string SavePathVideo = System.IO.Path.Combine(savepath, "Video");
            //if(!Directory.Exists(SavePathVideo))
            //    Directory.CreateDirectory(SavePathVideo);
            //// Backup dir
            //string SavePathBackup = System.IO.Path.Combine(savepath, "Backup");
            //if(!Directory.Exists(SavePathBackup))
            //    Directory.CreateDirectory(SavePathBackup);
            //// Backup video dir
            //string SavePathVideoBackup = System.IO.Path.Combine(SavePathBackup, "Video");
            //if(!Directory.Exists(SavePathVideoBackup))
            //    Directory.CreateDirectory(SavePathVideoBackup);
            //string SavePathShortClip= System.IO.Path.Combine(savepath, "ShortClip");
            //if (!Directory.Exists(SavePathShortClip))
            //    Directory.CreateDirectory(SavePathShortClip);
            //// logs data 
            //    string SavePathLogs = System.IO.Path.Combine(savepath, "Logs");
            //if(!Directory.Exists(SavePathLogs))
            //    Directory.CreateDirectory(SavePathLogs);

        }

        public void MakeFoldersByStructure(Structure _structure)
        {
            string basepath = DefaultProjectSavePath();
            foreach (DivingType _dt in _structure.dives)
            {
                
                string projectstructurepath = DefaultProjectStructureSavePath(_structure, _dt);
                string video = GetCurrentDiveVideoFolder(_structure,_dt);
                string snapshot = GetCurrentDiveSnapshotFoler(_structure,_dt);
                string shortclip = GetCurrentShortClipfolder(_structure,_dt);
                string log = GetCurrentDiveLogsFolder(_structure,_dt);

                string backuppath = DefaultBackupProjectSavePath();
                string backupstructurepath = DefaultBackupProjectStructureSavePath(_structure,_dt);

            }
            


        }
        public string DefaultProjectSavePath()
        {
            string savepath;

            // Directory.GetCurrentDirectory() to be replace by persistent path
            
            savepath = System.IO.Path.Combine(CurrentProject.FileLocation,CurrentProject.ProjectName);
            if (!Directory.Exists(savepath))
            {
                Directory.CreateDirectory(savepath);
                //// snapShot dir
                //string SavePathSnapshot = System.IO.Path.Combine(savepath, "SnapShot");
                //Directory.CreateDirectory(SavePathSnapshot);
                //// video dir
                //string SavePathVideo = System.IO.Path.Combine(savepath, "Video");
                //Directory.CreateDirectory(SavePathVideo);
                //// Backup dir
                //string SavePathBackup = System.IO.Path.Combine(savepath, "Backup");
                //Directory.CreateDirectory(SavePathBackup);
                //// Backup video dir
                //string SavePathVideoBackup = System.IO.Path.Combine(SavePathBackup, "Video");
                //Directory.CreateDirectory(SavePathVideoBackup);
                //// logs data 
                //string SavePathLogs = System.IO.Path.Combine(savepath, "Logs");
                //Directory.CreateDirectory(SavePathLogs);
            }
 
            return savepath;
        }

        public string GetProjectFilenameFullPath()
        {
            string path = System.IO.Path.Combine(DefaultProjectSavePath(), CurrentProject.ProjectName+ ".SIROproject");
            return path;
        }
        public string DefaultProjectStructureSavePath(Structure _selectedStructure,DivingType _dt)
        {
            string savepath;
            // Directory.GetCurrentDirectory() to be replace by persistent path

            savepath = System.IO.Path.Combine(DefaultProjectSavePath(), _dt.ToString()+ _selectedStructure.StructureName);
            Console.WriteLine(" dt :" + _dt.ToString());
            if (!Directory.Exists(savepath))
                Directory.CreateDirectory(savepath);

            return savepath;
        }
        public string GetCurrentDiveVideoFolder(Structure _selectedStructure,DivingType _dt)
        {
            string folder = System.IO.Path.Combine(DefaultProjectStructureSavePath(_selectedStructure,_dt), "Video");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            return folder;
        }
        public string GetCurrentDiveSnapshotFoler(Structure _selectedStructure,DivingType _dt)
        {
            string folder = System.IO.Path.Combine(DefaultProjectStructureSavePath(_selectedStructure,_dt), "SnapShot");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            return folder;
        }
        public string GetCurrentDiveLogsFolder(Structure _selectedStructure,DivingType _dt)
        {
            string folder = System.IO.Path.Combine(DefaultProjectStructureSavePath(_selectedStructure,_dt), "Logs");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            return folder;
        }
        public string GetCurrentShortClipfolder(Structure _selectedStructure,DivingType _dt)
        {
            string folder = System.IO.Path.Combine(DefaultProjectStructureSavePath(_selectedStructure,_dt), "ShortClip");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            return folder;
        }
        public string DefaultBackupProjectSavePath()
        {
            string savepath;
            // Directory.GetCurrentDirectory() to be replace by persistent path

            savepath = System.IO.Path.Combine(CurrentProject.BackupFileLocation, "Backup"+CurrentProject.ProjectName);
            if (!Directory.Exists(savepath))
            {
                Directory.CreateDirectory(savepath);
            }

            return savepath;
        }
        public string DefaultBackupProjectStructureSavePath(Structure _selectedStructure,DivingType _dt)
        {
            string savepath;
            // Directory.GetCurrentDirectory() to be replace by persistent path

            savepath = System.IO.Path.Combine(_dt.ToString(),DefaultBackupProjectSavePath(), _dt.ToString() + _selectedStructure.StructureName);
            if (!Directory.Exists(savepath))
                Directory.CreateDirectory(savepath);

            return savepath;
        }
        public string GetCurrentBackupDiveVideoFolder(Structure _selectedStructure,DivingType _dt)
        {
            string folder = System.IO.Path.Combine(DefaultBackupProjectStructureSavePath(_selectedStructure,_dt), "Video");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            return folder;
        }
        public string GetCurrentBackupDiveSnapshotFoler(Structure _selectedStructure,DivingType _dt)
        {
            string folder = System.IO.Path.Combine(DefaultBackupProjectStructureSavePath(_selectedStructure,_dt), "SnapShot");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            return folder;
        }
        public string GetCurrentBackupDiveLogsFolder(Structure _selectedStructure,DivingType _dt)
        {
            string folder = System.IO.Path.Combine(DefaultBackupProjectStructureSavePath(_selectedStructure,_dt), "Logs");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            return folder;
        }
        public string GetCurrentBackupShortClipfolder(Structure _selectedStructure,DivingType _dt)
        {
            string folder = System.IO.Path.Combine(DefaultProjectStructureSavePath(_selectedStructure,_dt), "ShortClip");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            return folder;
        }

        public void SaveProject()
        {
            OnlineProject currProj = CurrentProject;
            //FileStream file =new FileStream() 
            System.Runtime.Serialization.IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(GetProjectFilenameFullPath(), FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, currProj);
            stream.Close();

        }


    }
}
