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
    CipherModule.IBlockCipher module;
    int num_trace;
    byte[] key;
	//int Delay;
	int samples;
    bool multipleAES;
    ushort sensor_delay;


    //************************************************ Method
    //------------------------------------------------ Constructor
        public Controller (CipherModule.IBlockCipher module, int num_trace, string key_st, int samples, bool multipleAES/*, int Delay*/)
    {
      this.module    = module;
	  this.num_trace = num_trace;
      //this.Delay = Delay;
	  this.samples = samples;
     // this.multipleAES =multipleAES;

       key = new byte[16];
      for (int i=0; i<16; i++) 
          key[i] = Convert.ToByte(key_st.Substring(i * 3, 2), 16);

      worker = new BackgroundWorker();
      worker.WorkerReportsProgress      = true;
      worker.WorkerSupportsCancellation = true;
      worker.DoWork += new DoWorkEventHandler(worker_DoWork);
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
        System.Threading.Thread.Sleep(100);


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