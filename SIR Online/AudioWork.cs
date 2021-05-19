using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeckLinkAPI;
using Microsoft.DirectX.DirectSound;
using System.Runtime.InteropServices;

namespace SIR_Online
{
    public static class AudioWork
    {
       
        public static Byte[] ReadAudioInputmemory(IntPtr audioBuffer, int samplesToWrite, int channels, _BMDAudioSampleType sampleDepth)
        {
            byte[] buffer = null;
            if ((uint)sampleDepth == 16)
            {
                Int16[] buffer16 = new Int16[channels * samplesToWrite];
                //Marshal.Copy(audioBuffer, buffer, 0, (int)(channels * samplesToWrite*2));
                //Marshal.Copy(audioBuffer,0,)
                //Marshal.Copy(audioBuffer, buffer16, 0, channels * samplesToWrite);
                Marshal.Copy(audioBuffer, buffer16, 0, channels * samplesToWrite);
                buffer = new byte[(int)(channels * samplesToWrite * sizeof(Int16))];
                //Marshal.Copy(audioBuffer, buffer, 0, (int)(channels * samplesToWrite * sizeof(int)));
                System.Buffer.BlockCopy(buffer16, 0, buffer, 0, buffer.Length);
                return buffer;

                //return buffer;
            }
            else if ((uint)sampleDepth == 32)
            {

                return buffer;
            }
            return buffer;
        }


        public static Byte[] CopyAudioInputMemoryToByteArray(IntPtr audioBuffer, int samplesToWrite, int channels, _BMDAudioSampleType sampleDepth)
        {
            byte[] buffer = null;
            if ((uint)sampleDepth == 16)
            {
                //Int16[] buffer16 = new Int16[channels * samplesToWrite];
                //Marshal.Copy(audioBuffer, buffer, 0, (int)(channels * samplesToWrite*2));
                //Marshal.Copy(audioBuffer,0,)
                //Marshal.Copy(audioBuffer, buffer16, 0, channels * samplesToWrite);
                //Marshal.Copy(audioBuffer, buffer16, 0, channels * samplesToWrite);
                //buffer = new byte[(int)(channels * samplesToWrite * sizeof(Int16))];
                ////Marshal.Copy(audioBuffer, buffer, 0, (int)(channels * samplesToWrite * sizeof(int)));
                //System.Buffer.BlockCopy(buffer16, 0, buffer, 0, buffer.Length);

                buffer = new byte[(int)(channels * samplesToWrite * sizeof(Int16))];
                Marshal.Copy(audioBuffer, buffer, 0, (int)(channels * samplesToWrite * sizeof(Int16)));

                return buffer;

                //return buffer;
            }
            else if ((uint)sampleDepth == 32)
            {
                // Console.WriteLine("32 bit depth audio");
                //Console.WriteLine(sizeof(int).ToString());
                //audioBuffer.ToInt32();
                //Int32[] buffer32 = new Int32[channels*samplesToWrite];



                //Console.WriteLine(b32data.Length);           
                //Marshal.Copy(audioBuffer, buffer, 0, (int)(channels * samplesToWrite*4));
                // ##Int32

                // Marshal.Copy(audioBuffer, buffer32, 0, channels* samplesToWrite);

                //Marshal.Copy(audioBuffer, float32, 0, channels * samplesToWrite);
                //for (int i = 0; i < 10; i++)
                //{
                //    Console.WriteLine(" buffer value at  " + i + " :" + buffer32[i]);
                //}

                //for (int i = 0; i < buffer32.Length; i++)
                //{
                //    if (i % 4 == 0 && i!=0)
                //    {
                //        buffer32[i-3] = buffer32[i];
                //    }
                //}

                // ##
                //for (uint i = 0; i < samplesToWrite/200; i++)
                //{
                //    Int32 sample = (Int32)(1610612736.0 * Math.Sin((i * 2.0 * Math.PI) / 48.0));
                //    for (uint ch = 0; ch < channels; ch++)
                //    {
                //        buffer32[i * channels + ch] = sample;
                //    }
                //}

                buffer = new byte[(int)(channels * samplesToWrite * sizeof(int))];
                //Marshal.Copy(audioBuffer, buffer, 0, (int)(samplesToWrite * sizeof(int)));
                Marshal.Copy(audioBuffer, buffer, 0, (int)(channels * samplesToWrite * sizeof(int)));

                // ##### copy integer array or float to byte array
                // System.Buffer.BlockCopy(buffer32, 0, buffer, 0, buffer.Length);
                //######################################

                // Marshal.Copy(audioBuffer, buffer, 0, (int)(channels * samplesToWrite * sizeof(int)));

                //for (int i = 0; i < 32; i++)
                //{
                //    Console.WriteLine(" buffer value at " + i + "  :" + buffer[i]);
                //}

                //Console.WriteLine(" buffer size : " + buffer.Length);
                //Console.WriteLine(" buffer value at 0 : " + buffer[0].ToString());
                //Console.WriteLine(" buffer value at 1 : " + buffer[1].ToString());
                //Console.WriteLine(" buffer value at 2 : " + buffer[2].ToString());
                //Console.WriteLine(" buffer value at 3 : " + buffer[3].ToString());
                //Console.WriteLine(" buffer value at 4 : " + buffer[4].ToString());
                //Console.WriteLine(" buffer value at 5 : " + buffer[5].ToString());
                //Console.WriteLine(" buffer value at 10 : " + buffer[10].ToString());
                //Console.WriteLine(" buffer value at 150 : " + buffer[150].ToString());
                //Console.WriteLine(" buffer value at 624: " + buffer[624].ToString());
                //Console.WriteLine(" buffer value at : " + buffer[1244].ToString());

                //Console.WriteLine(" buffer value at 0 : " + buffer32[0].ToString());
                //Console.WriteLine(" buffer value at 1 : " + buffer32[1].ToString());
                //Console.WriteLine(" buffer value at 2 : " + buffer32[2].ToString());
                //Console.WriteLine(" buffer value at 3 : " + buffer32[3].ToString());
                //Console.WriteLine(" buffer value at 4 : " + buffer32[4].ToString());
                //Console.WriteLine(" buffer value at 5 : " + buffer32[5].ToString());
                //Console.WriteLine(" buffer value at 6 : " + buffer32[6].ToString());
                //Console.WriteLine(" buffer value at 7 : " + buffer32[7].ToString());
                //Console.WriteLine(" buffer value at 8 : " + buffer32[8].ToString());
                //Console.WriteLine(" buffer value at 9 : " + buffer32[9].ToString());
                //Console.WriteLine(" buffer value at 10 : " + buffer32[10].ToString());
                //Console.WriteLine(" buffer value at 150 : " + buffer32[150].ToString());
                //Console.WriteLine(" buffer value at 624: " + buffer32[624].ToString());
                //Console.WriteLine(" buffer value at : " + buffer32[1244].ToString());
                //Console.WriteLine(" buffer value at 2001 : " + buffer32[2001].ToString());
                //Console.WriteLine(" buffer value at 2002 : " + buffer32[2002].ToString());
                //Console.WriteLine(" buffer value at 2003 : " + buffer32[2003].ToString());
                //Console.WriteLine(" buffer value at 2004 : " + buffer32[2004].ToString());
                //Console.WriteLine(" buffer value at 2005 : " + buffer32[2005].ToString());
                //Console.WriteLine(" buffer value at 2006 : " + buffer32[2006].ToString());
                //Console.WriteLine(" buffer value at 3500 : " + buffer32[3500].ToString());
                //Console.WriteLine(" buffer value at 3800 : " + buffer32[3800].ToString());
                //Console.WriteLine(" buffer value at 3801 : " + buffer32[3801].ToString());
                //Console.WriteLine(" buffer value at 3802 : " + buffer32[3802].ToString());



                return buffer;
                //return buffer;
            }

            return buffer;
        }


    }
}
