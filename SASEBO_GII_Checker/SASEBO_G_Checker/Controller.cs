/*-------------------------------------------------------------------------
 Control AES function on SASEBO-GII

 File name   : Checker.cs
 Version     : 1.3
 Created     : APR/02/2012
 Last update : MAY/24/2012
 Desgined by : Toshihiro Katashita
 
 
 Copyright (C) 2012 AIST
 
 By using this code, you agree to the following terms and conditions.
 
 This code is copyrighted by AIST ("us").
 
 Permission is hereby granted to copy, reproduce, redistribute or
 otherwise use this code as long as: there is no monetary profit gained
 specifically from the use or reproduction of this code, it is not sold,
 rented, traded or otherwise marketed, and this copyright notice is
 included prominently in any copy made.
 
 We shall not be liable for any damages, including without limitation
 direct, indirect, incidental, special or consequential damages arising
 from the use of this code.
 
 When you publish any results arising from the use of this code, we will
 appreciate it if you can cite our webpage.
 (http://www.aist.go.jp/aist_e/research_results/publications/synthesiology_e/vol3_no1/vol03_01_p86_p95.pdf)
 -------------------------------------------------------------------------*/

using System;
using System.ComponentModel;
using System.Threading;
using System.IO;
using System.Text;
using System.Collections;
using System.Linq;
using System.Globalization;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;
using System.Collections.Generic;

namespace SASEBO_G_Checker
{
	//================================================================ Tracer_SASEBO_G
  public class Controller 
  {
		//************************************************ Variable
	private const string OSCILLOSCOPE = "USB0::0x0699::0x0450::C011432::INSTR";
	
	ArrayList instrlist;
    int j;
    float ymult1, yzero1, yoff1, xincr1, timepoint;
    float ymult2, yzero2, yoff2, xincr2;
    string response;
    bool status;
	protected BackgroundWorker worker;
    protected BackgroundWorker workerCPA;

        CipherModule.IBlockCipher module;
    int num_trace;
    byte[] key;
	//int Delay;
	int samples;
    bool multipleAES;
    ushort sensor_delay;



    public static readonly byte[] inv_sbox = new byte[256]
      { 0x52, 0x09, 0x6a, 0xd5, 0x30, 0x36, 0xa5, 0x38, 0xbf, 0x40, 0xa3, 0x9e, 0x81, 0xf3, 0xd7, 0xfb,
      0x7c, 0xe3, 0x39, 0x82, 0x9b, 0x2f, 0xff, 0x87, 0x34, 0x8e, 0x43, 0x44, 0xc4, 0xde, 0xe9, 0xcb,
      0x54, 0x7b, 0x94, 0x32, 0xa6, 0xc2, 0x23, 0x3d, 0xee, 0x4c, 0x95, 0x0b, 0x42, 0xfa, 0xc3, 0x4e,
      0x08, 0x2e, 0xa1, 0x66, 0x28, 0xd9, 0x24, 0xb2, 0x76, 0x5b, 0xa2, 0x49, 0x6d, 0x8b, 0xd1, 0x25,
      0x72, 0xf8, 0xf6, 0x64, 0x86, 0x68, 0x98, 0x16, 0xd4, 0xa4, 0x5c, 0xcc, 0x5d, 0x65, 0xb6, 0x92,
      0x6c, 0x70, 0x48, 0x50, 0xfd, 0xed, 0xb9, 0xda, 0x5e, 0x15, 0x46, 0x57, 0xa7, 0x8d, 0x9d, 0x84,
      0x90, 0xd8, 0xab, 0x00, 0x8c, 0xbc, 0xd3, 0x0a, 0xf7, 0xe4, 0x58, 0x05, 0xb8, 0xb3, 0x45, 0x06,
      0xd0, 0x2c, 0x1e, 0x8f, 0xca, 0x3f, 0x0f, 0x02, 0xc1, 0xaf, 0xbd, 0x03, 0x01, 0x13, 0x8a, 0x6b,
      0x3a, 0x91, 0x11, 0x41, 0x4f, 0x67, 0xdc, 0xea, 0x97, 0xf2, 0xcf, 0xce, 0xf0, 0xb4, 0xe6, 0x73,
      0x96, 0xac, 0x74, 0x22, 0xe7, 0xad, 0x35, 0x85, 0xe2, 0xf9, 0x37, 0xe8, 0x1c, 0x75, 0xdf, 0x6e,
      0x47, 0xf1, 0x1a, 0x71, 0x1d, 0x29, 0xc5, 0x89, 0x6f, 0xb7, 0x62, 0x0e, 0xaa, 0x18, 0xbe, 0x1b,
      0xfc, 0x56, 0x3e, 0x4b, 0xc6, 0xd2, 0x79, 0x20, 0x9a, 0xdb, 0xc0, 0xfe, 0x78, 0xcd, 0x5a, 0xf4,
      0x1f, 0xdd, 0xa8, 0x33, 0x88, 0x07, 0xc7, 0x31, 0xb1, 0x12, 0x10, 0x59, 0x27, 0x80, 0xec, 0x5f,
      0x60, 0x51, 0x7f, 0xa9, 0x19, 0xb5, 0x4a, 0x0d, 0x2d, 0xe5, 0x7a, 0x9f, 0x93, 0xc9, 0x9c, 0xef,
      0xa0, 0xe0, 0x3b, 0x4d, 0xae, 0x2a, 0xf5, 0xb0, 0xc8, 0xeb, 0xbb, 0x3c, 0x83, 0x53, 0x99, 0x61,
      0x17, 0x2b, 0x04, 0x7e, 0xba, 0x77, 0xd6, 0x26, 0xe1, 0x69, 0x14, 0x63, 0x55, 0x21, 0x0c, 0x7d };

      public static readonly int[] inv_shift = new int[16] { 0, 5, 10, 15, 4, 9, 14, 3, 8, 13, 2, 7, 12, 1, 6, 11 };

      private static int[] hamming_table;

      List<int>[] wave = new List<int>[1024];  //List<int>[] a = new List<int>[100];
      List<int>[] waveTemp = new List<int>[1024];

      List<byte> cipher = new List<byte>();
      List<byte> cipherTemp = new List<byte>();


        List<int>[] cipherHD = new List<int>[256];
        List<float>[] cipherDST = new List<float>[256];

      List<float>[] waveDST = new List<float>[1024];
        double[] corrCoe = new double[256];

        int waveLowLim  = 100;
        int waveUpLim   = 200;

        //************************************************ Method
        //------------------------------------------------ Constructor
        public Controller(CipherModule.IBlockCipher module, int num_trace, string key_st, int samples, bool multipleAES/*, int Delay*/)
        {
            this.module = module;
            this.num_trace = num_trace;
            //this.Delay = Delay;
            this.samples = samples;
            // this.multipleAES =multipleAES;

            key = new byte[16];
            for (int i = 0; i < 16; i++)
                key[i] = Convert.ToByte(key_st.Substring(i * 3, 2), 16);

            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);

            workerCPA = new BackgroundWorker();
            workerCPA.WorkerReportsProgress = true;
            workerCPA.WorkerSupportsCancellation = true;
            workerCPA.DoWork += new DoWorkEventHandler(worker_DoWorkCPA);

            hamming_table = new int[256];
            for (int i = 0; i < 256; i++)
            {
                int num = 0;
                for (int tmp = i; tmp > 0; tmp = tmp >> 1)
                {
                    num += tmp & 0x01;
                }
                hamming_table[i] = num;
            }

            for (int i = 0; i < 1024; i++)
            {
                wave[i] = new List<int>();
                waveTemp[i] = new List<int>();
                waveDST[i] = new List<float>();


            }
            for (int i = 0; i < 256; i++)
            {
                cipherHD[i] = new List<int>();
                cipherDST[i] = new List<float>();

            }
        }

		//************************************************ BackgroudWorker
		//------------------------------------------------ addBGWorker_ConpletedEventHandler()
    public void addBGWorker_CompletedEventHandler (RunWorkerCompletedEventHandler handler)
    {
      worker.RunWorkerCompleted += handler;
    }

		//------------------------------------------------ addBGWorker_ProgressChangedEventHandler()
    public void addBGWorker_ProgressChangedEventHandler (ProgressChangedEventHandler handler)
    {
      worker.ProgressChanged += handler;
    }

	//------------------------------------------------ run()
	public void run()    
    { 
        worker.RunWorkerAsync(); 
    }

	//------------------------------------------------ cansel()
	public void cancel() 
    { 
        worker.CancelAsync(); 
    }

    public void set_delay(int delay)
        {
            sensor_delay = (ushort)delay;
    }


        public void set_multiAES(Boolean multiAES)
        {
            this.multipleAES = multiAES;

        }
            
            //------------------------------------------------ worker_DoWork()
        private void worker_DoWork(object sender, DoWorkEventArgs e)
    {
      e.Cancel = false;

      CipherModule.IBlockCipher cipher_hw = (CipherModule.IBlockCipher) module;
      CipherTool.IBlockCipher   cipher_sw = (CipherTool.IBlockCipher) new CipherTool.AES();

      int progress = 0;
      e.Cancel = mainloop_AES(cipher_hw, cipher_sw, ref progress);
    }


        private void worker_DoWorkCPA(object sender, DoWorkEventArgs e)
  {

            Console.WriteLine("CPA calculating");

            // clean lists;
            double[] cipherMeanArray = new double[256];
            double[] traceMeanArray  = new double[1024];
           // double[,] cipherDST     = new double[256, cipher.Count];
           // double[,] waverDST      = new double[1024, cipher.Count];
            double SumCipher2       = 0;
            double SumWave2         = 0;

            double[] DevCipher = new double[256];
            double[] DevWave = new double[1024];

            // cipher HD
            for (int key = 0; key < 256; key++)
            {
                cipherHD[key].Clear();
                cipherDST[key].Clear();

                for (int index = 0; index < cipher.Count; index++)
                {
                    byte st10 = cipher[index];
                    byte st9 = inv_sbox[cipher[index] ^ key];
                    cipherHD[key].Add(hamming_table[st9 ^ st10]);   // calculate HD here

                }
                    

                cipherMeanArray[key] = cipherHD[key].Average();

                SumCipher2           = 0;

                for (int index = 0; index < cipher.Count; index++)
                {
                    cipherDST[key].Add((float)(cipherHD[key][index] - cipherMeanArray[key]));
                    SumCipher2          += cipherHD[key][index] * cipherHD[key][index];
                }
                DevCipher[key]          = (double)Math.Sqrt((double)SumCipher2 / cipher.Count - cipherMeanArray[key] * cipherMeanArray[key]);
            }
            Console.WriteLine(cipher.Count +"   "+ cipherHD[0].Count);


            // wave 

            for (int waveindex = waveLowLim; waveindex < waveUpLim; waveindex++)
            {
                traceMeanArray[waveindex] = wave[waveindex].Average();
                SumWave2 = 0;
                waveDST[waveindex].Clear();

                for (int index = 0; index < wave[waveindex].Count; index++)
                {
                    waveDST[waveindex].Add((float) (wave[waveindex][index] - traceMeanArray[waveindex]));  //
                    SumWave2 += wave[waveindex][index] * wave[waveindex][index];

                }
                DevWave[waveindex] = (double)Math.Sqrt((double)SumWave2 / wave[waveindex].Count - traceMeanArray[waveindex] * traceMeanArray[waveindex]);

            }

            double denomi       =0.0f;
            double numerator    =0.0f;
            double corr         =0.0f;
            for (int key = 0; key < 256; key++)
            {
                corrCoe[key] = 0.0f;
                for (int waveindex = waveLowLim; waveindex < waveUpLim; waveindex++)
                {
                    corr = 0.0f;

                    numerator = calcNumerator(waveDST[waveindex], cipherDST[key]);
                    denomi = DevWave[waveindex] * DevCipher[key];
                    if (denomi != 0.0f)
                        corr = Math.Abs(numerator / denomi);

                    if (corrCoe[key] < corr)
                        corrCoe[key] = corr;


                }
                
            }

            Console.Write("**"+corrCoe.Max().ToString("0.0000")+"**"  + corrCoe[74].ToString("0.0000") + "    ");
            for (int key = 0; key < 256; key++)
                Console.Write(key + "=>" + corrCoe[key].ToString("0.0000") +"   ");


                Console.WriteLine("CPA finished");

        }

        private double calcNumerator(List<float> wave, List<float> hddst)
        {
            double num = 0.0d;
            for (int i = 0; i < wave.Count; i++)
            {
                num += wave[i] * hddst[i];
            }
            return num / wave.Count;
        }


        //------------------------------------------------ worker_DoWork()
        private bool mainloop_AES(CipherModule.IBlockCipher hw, CipherTool.IBlockCipher sw, ref int progress)
	{
      Random rand = new Random();

      int loop     = 0;
     // int delayTime = Delay;
	  int sampleLength = samples;
      bool error   = false;

      byte[] text_in  = new byte[16];
      byte[] text_out = new byte[16];
      byte[] text_ans = new byte[16];

      byte[] byte_trace = new byte[1024];


            // FILE READER
            //StreamWriter myWriter;
            // myWriter = new StreamWriter("output.csv");
            //Console.WriteLine("reading from file");

            // Initialization
            hw.setKey(key, 16); // Hardware
            sw.setKey(key);     // Software 
      
	  //string path = @"E:\VS\plaintexts\pt_9000.txt";
     // string path = @"D:\sanjana_laptop\downloads\PlainText_1000000.txt";  //@"E:\VS\plaintexts\pt_9000.txt"; //pt_117000
      // Main loop
      progress = 0;
      worker.ReportProgress(progress, (object)(new ControllerReport(loop, text_in, text_out, text_ans, byte_trace)));

            // myWriter.WriteLine(BitConverter.ToString(key).Replace("-", " "));

            

            //int loop = 0;  
            string ln;
		while ((loop < num_trace)) { 
        progress = (int)(100.0d * loop / num_trace);


                // Generate plain text
                for (int i = 0; i < 16; i++) text_in[i] = (byte)rand.Next(256);
                   loop++;
        // for (int i=0; i<16; i++)   text_in[i] =  0;
		// Software encryption
        text_ans = sw.encrypt(text_in);
                //hw.setEnc();

                if(multipleAES==true)
                    hw.setDec();
                else
                    hw.setEnc();

                // Hardware encryption
                byte[] delay_array = { 240, 255 };
        hw.writeText(text_in,  16);
                delay_array[1] = (byte)(sensor_delay & 0xFF);
                delay_array[0] = (byte)(sensor_delay >> 8 & 0xFF);
                hw.writeDelay(delay_array, 2);
		hw.execute();
        hw.readText (text_out, 16);
        hw.readTrace(byte_trace, 1024);

             //   for (int z = 0; z < 1024; z++)
           //     Debug.Write(byte_trace[z] + " ");
           //Debug.Write( "\n");

        worker.ReportProgress(progress, (object)(new ControllerReport(loop, text_in, text_out, text_ans, byte_trace)));
        System.Threading.Thread.Sleep(10);

                // update cihper
                cipherTemp.Add(text_out[0]);


                // update wave
                for (int x = 0; x < 1024; x++)
                {
                    waveTemp[x].Add(byte_trace[x]);   
                }


                if (loop % 200 == 0 && loop != 0 && workerCPA.IsBusy==false)
                {
                    

                    foreach (var temp in cipherTemp)
                    {
                        cipher.Add(temp);

                    }
                    cipherTemp.Clear();

                    for (int x = 0; x < 1024; x++)
                    {
                        foreach (var temp in waveTemp[x])
                            wave[x].Add(temp);
                        waveTemp[x].Clear();
                    }

                    workerCPA.RunWorkerAsync();
                   // workerCPA.WorkerReportsProgress();
                }
                if (worker.CancellationPending || error) 
            break;

        //  Sleep for given period of time
       // System.Threading.Thread.Sleep(100/*delayTime*/);

      } //endof loop

	
     // myWriter.Close();  
      if (worker.CancellationPending) 
          error = true;
      
      worker.ReportProgress(progress, (object)(new ControllerReport(loop, text_in, text_out, text_ans, byte_trace)));
      return error;
    }

  }

	
  //================================================================ ControllerReport
  public struct ControllerReport
  {
    public int num_trace;
    public byte[] text_in;
    public byte[] text_out;
    public byte[] text_ans;
    public byte[] trace;

   // public int multipleAES;    //public Series  s;   // series = this.chart1.Series.Add("Trace");

        //------------------------------------------------ Constructor
        public ControllerReport(int num_trace, byte[] text_in, byte[] text_out, byte[] text_ans, byte[] trace)
    {
      this.num_trace = num_trace;
      this.text_in  = (byte[])text_in.Clone();
      this.text_out = (byte[])text_out.Clone();
      this.text_ans = (byte[])text_ans.Clone();
      this.trace    = (byte[])trace.Clone();
      //this.multipleAES  = mu
            //this.s        = (Series)s; 
        }
  }
  
}