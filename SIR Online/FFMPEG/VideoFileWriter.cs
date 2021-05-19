using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Drawing.Imaging;
using System.IO.Pipes;

namespace SIR_Online.FFMPEG
{
    public class VideoFileWriter : IVideoFileWriter
    {
        public bool IsOpen { get; set; }

        public void Close()
        {
            StopRecording();
        }
        Process process;
        NamedPipeServerStream pipeV;
        NamedPipeServerStream pipeA;
        public void Open(string _deviceName,string _ffmpegLoc,int _width,int _height,int _bitrate, int _fps, string _OutputFileName)
        {
          Task.Factory.StartNew(() =>
           {

                if (process == null)
                {
                    process = new Process();

                }
                if (pipeV == null)
                {
                  //  pipeV = new NamedPipeServerStream("pipeV"+_deviceName, PipeDirection.Out, 1, PipeTransmissionMode.Byte, PipeOptions.None);

                }
                if (pipeA == null)
                {
                   // pipeA= new NamedPipeServerStream("pipeA" + _deviceName, PipeDirection.Out, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
                }
               
                //process.StartInfo.Arguments = String.Format("-f image2pipe -i pipe:.bmp -maxrate {0}k -r {1} -an -y {2}",
                //    _bitrate, _fps, _OutputFileName);
                string outputFile = string.Format(@"""{0}""", _OutputFileName);
                    Arguments arguments = new Arguments(
                    InputArgumentOptions.GetHwAccelsOptionStr(HardwareAccelarations.cuda),
                    InputArgumentOptions.GetFormatOptionStr(Formats.image2pipe),
                    "-i pipe:.bmp",
                    //InputArgumentOptions.GetInputOptionStr("pipeV" + _deviceName),
                    InputArgumentOptions.GetVideoSizeOptionStr(_width,_height),                 
                    InputArgumentOptions.GetMaxBitRateStr(_bitrate), InputArgumentOptions.GetAudioEnableOptionStr(true),
                    InputArgumentOptions.GetVariableFPSOptionStr(_fps),
                    InputArgumentOptions.GetVideoCodecOptionStr(VideoCodec.h264),                 
                    outputFile);

                ProcessStartInfo startInfo = new ProcessStartInfo();
                
                startInfo.FileName = _ffmpegLoc;
                startInfo.Arguments = arguments.ToString();
                Console.WriteLine(startInfo.Arguments);
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;
                startInfo.RedirectStandardInput = true;
                startInfo.RedirectStandardOutput = true;
                        
                process.StartInfo=startInfo;
                process.Start();
                process.PriorityClass = ProcessPriorityClass.RealTime;
                IsOpen = true;
               // pipeV.WaitForConnection();
               // pipeA.WaitForConnectionAsync();
              

         });
                
        }
        public void Open(string _ffmpegLoc,int NumberofChannel, int _width, int _height, int _bitrate, int _fps, string _OutputFileName)
        {
            Task.Factory.StartNew(() =>
            {

                if (process == null)
                {
                    process = new Process();

                }

                //process.StartInfo.Arguments = String.Format("-f image2pipe -i pipe:.bmp -maxrate {0}k -r {1} -an -y {2}",
                //    _bitrate, _fps, _OutputFileName);
                string outputFile = string.Format(@"""{0}""", _OutputFileName);
                Arguments arguments = new Arguments(InputArgumentOptions.GetFormatOptionStr(Formats.image2pipe),
                    "-i pipe:.bmp",
                    InputArgumentOptions.GetVideoSizeOptionStr(_width, _height),
                    InputArgumentOptions.GetMaxBitRateStr(_bitrate),
                    InputArgumentOptions.GetVideoCodecOptionStr(VideoCodec.h264),
                    InputArgumentOptions.GetAudioEnableOptionStr(true),
                    InputArgumentOptions.GetVariableFPSOptionStr(_fps),
                outputFile);

                ProcessStartInfo startInfo = new ProcessStartInfo();

                startInfo.FileName = _ffmpegLoc;
                startInfo.Arguments = arguments.ToString();
                Console.WriteLine(startInfo.Arguments);
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;
                startInfo.RedirectStandardInput = true;
                startInfo.RedirectStandardOutput = true;

                //process.StartInfo.Arguments = arguments.ToString();

                //process.StartInfo.UseShellExecute = false;
                //process.StartInfo.CreateNoWindow = true;
                //process.PriorityClass = ProcessPriorityClass.RealTime;

                //process.StartInfo.RedirectStandardInput = true;
                //process.StartInfo.RedirectStandardOutput = true;

                process.StartInfo = startInfo;
                process.Start();
                process.PriorityClass = ProcessPriorityClass.RealTime;

                IsOpen = true;
            });
        }

        public void WriteAudioFrame(byte[] _audioBuffer)
        {
            //using (var ms = new MemoryStream(_audioBuffer))
            //{
            //    ms.WriteTo(process.StandardInput.BaseStream);
            //}
        }

        public  void WriteVideoFrame(Bitmap _bmp)
        {

            Bitmap b = (Bitmap)_bmp.Clone();

            Task.Factory.StartNew(() =>
            {
                using (var ms = new MemoryStream())
                {
                    b.Save(ms, ImageFormat.Bmp);

                    ms.WriteTo(process.StandardInput.BaseStream);
                    //pipeV.Write(ms.ToArray(), 0, ms.ToArray().Length);
                    //pipeV.RunAsClient(pipes)
                }
                b.Dispose();
            });
            //Task task = new Task(() =>
            //{
            //        Bitmap b = (Bitmap)_bmp.Clone();

            //        using (var ms = new MemoryStream())
            //        {
            //            b.Save(ms, ImageFormat.Bmp);

            //            ms.WriteTo(process.StandardInput.BaseStream);
            //            //pipeV.Write(ms.ToArray(), 0, ms.ToArray().Length);
            //            //pipeV.RunAsClient(pipes)
            //        }
            //        b.Dispose();
            //    });


        }
        public void StopRecording()
        {
            //pipeV.Flush();
           // pipeV.Dispose();
           // pipeA.Flush();
           // pipeA.Dispose();
            process.Close();

            IsOpen = false;
        }
    }
}
