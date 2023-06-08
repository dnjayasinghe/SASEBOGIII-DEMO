//IITG code
/*-------------------------------------------------------------------------
 SASEBO_G_Checker (ATMega card, AES)

 File name   : Form_Main.cs
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SASEBO_G_Checker
{
  //================================================================ Form_Controller
  public partial class Form_Main : Form
  {
    //************************************************ Variable
    Manager manager;
    Random rand;
    Series series1;
        Series[] seriesCPA;
    ChartArea chartArea, chatCPAarea;
    byte[] Trace_data;
    bool check;// = false;
    bool CPAPlot;
    List<float>[] cpa;

        //************************************************ Method
        //------------------------------------------------ Constructor
        public Form_Main()
    {
      this.StartPosition = FormStartPosition.Manual;
      InitializeComponent();

      rand = new Random();
      Trace_data = new byte[1024];
      check = false;
                                 
        }

    //------------------------------------------------ Initialization
    private void Form_Controller_Load(object sender, EventArgs e)
    {
      manager = new Manager();

            chartArea = new ChartArea();
            chartArea.Name = "First Area";
            chart1.ChartAreas.Add(chartArea);
            chartArea.BackColor = Color.Azure;
            chartArea.BackGradientStyle = GradientStyle.HorizontalCenter;
            chartArea.BackHatchStyle = ChartHatchStyle.LargeGrid;
            chartArea.BorderDashStyle = ChartDashStyle.Solid;
            chartArea.BorderWidth = 1;
            chartArea.BorderColor = Color.Red;
            //chartArea.ShadowColor = Color.Purple;
            chartArea.ShadowOffset = 0;
            chart1.ChartAreas[0].Axes[0].MajorGrid.Enabled = false;//x axis
            chart1.ChartAreas[0].Axes[1].MajorGrid.Enabled = false;//y axis

            //Cursor：only apply the top area
            chartArea.CursorX.IsUserEnabled = true;
            chartArea.CursorX.AxisType = AxisType.Primary;//act on primary x axis
            chartArea.CursorX.Interval = 1;
            chartArea.CursorX.LineWidth = 1;
            chartArea.CursorX.LineDashStyle = ChartDashStyle.Dash;
            chartArea.CursorX.IsUserSelectionEnabled = true;
            chartArea.CursorX.SelectionColor = Color.Yellow;
            chartArea.CursorX.AutoScroll = true;

            chartArea.CursorY.IsUserEnabled = true;
            chartArea.CursorY.AxisType = AxisType.Primary;//act on primary y axis
            chartArea.CursorY.Interval = 1;
            chartArea.CursorY.LineWidth = 1;
            chartArea.CursorY.LineDashStyle = ChartDashStyle.Dash;
            chartArea.CursorY.IsUserSelectionEnabled = true;
            chartArea.CursorY.SelectionColor = Color.Yellow;
            chartArea.CursorY.AutoScroll = true;

            // Axis
            chartArea.AxisY.Minimum = 0;//Y axis Minimum value
           chartArea.AxisY.Maximum = 160;
            chartArea.AxisY.Title = @"Sensor Value";
            //chartArea.AxisY.Maximum = 100d;//Y axis Maximum value
            chartArea.AxisX.Minimum = 0d; //X axis Minimum value
            chartArea.AxisX.Maximum = 256;
            //chartArea.AxisX.Maximum = 1024;
            chartArea.AxisX.IsLabelAutoFit = true;
            //chartArea.AxisX.La InterlacedColor = BackColor;

            //chartArea.AxisX.LabelAutoFitMaxFontSize = 12;
            chartArea.AxisX.LabelAutoFitMinFontSize = 5;
            chartArea.AxisX.LabelStyle.Angle = -20;
            chartArea.AxisX.LabelStyle.IsEndLabelVisible = true;//show the last label
            chartArea.AxisX.Interval = 100;
            //chartArea.AxisX.IntervalAutoMode = IntervalAutoMode.FixedCount;
            //chartArea.AxisX.IntervalType = DateTimeIntervalType.NotSet;
            chartArea.AxisX.Title = @"Sample";
            chartArea.AxisX.TextOrientation = TextOrientation.Auto;
            chartArea.AxisX.LineWidth = 0;
            chartArea.AxisX.LineColor = Color.DarkOrchid;
            chartArea.AxisX.Enabled = AxisEnabled.True;
            //chartArea.AxisX.ScaleView.MinSizeType = DateTimeIntervalType.Months;
            chartArea.AxisX.ScrollBar = new AxisScrollBar();

            series1 = new Series();
            series1.ChartArea = "First Area";
            chart1.Series.Add(series1);



            


            chatCPAarea = new ChartArea();
            chatCPAarea.Name = "CPA Plots";
            chartCPA.ChartAreas.Add(chatCPAarea);
            chatCPAarea.AxisX.MajorGrid.Enabled = false;
            chatCPAarea.AxisX.Title = @"Number of Samples";
            chatCPAarea.AxisY.Title = @"Pearson Correlation Coefficient";

            //chatCPAarea.AxisY.Maximum = 1.0;
            //chatCPAarea.AxisY.Minimum = 0.0;
            seriesCPA = new Series[256];
            for (int i = 0; i < 256; i++)
            {
                seriesCPA[i] = new Series();

                seriesCPA[i].Name = @"series：Test One" +i;
                chartCPA.Series.Add(seriesCPA[i]);


                seriesCPA[i].ChartType = SeriesChartType.Line;  // type
                seriesCPA[i].BorderWidth = 1;
                seriesCPA[i].Color = Color.FromArgb(40, Color.Green);
                seriesCPA[i].XValueType = ChartValueType.Int32;//x axis type
                seriesCPA[i].YValueType = ChartValueType.Double;//y axis type
                seriesCPA[i].IsVisibleInLegend = false;

                seriesCPA[i].IsVisibleInLegend = false;
            }

            chartCPA.ChartAreas[0].AxisX.Minimum = double.NaN;
            chartCPA.ChartAreas[0].AxisX.Maximum = double.NaN;
            chartCPA.ChartAreas[0].AxisX.Interval = double.NaN;
            chartCPA.ChartAreas[0].AxisY.Minimum = double.NaN;
            chartCPA.ChartAreas[0].AxisY.Maximum = double.NaN;


            //seriesCPA.
            // numUD_Graph_Xmin.Enabled = false;
            // numUD_Graph_Xmax.Enabled = false;
            // numUD_Graph_XInterval.Enabled = false;
            // numUD_Graph_Ymin.Enabled = false;
            // numUD_Graph_Ymax.Enabled = false;


        }

    //************************************************ Control
    //------------------------------------------------ Button, Change key
    private void button_key_Click(object sender, EventArgs e)
    {
      String key_st = "";
      for (int i=0; i<16; i++) 
          key_st += (rand.Next(256)).ToString("X2") + " ";
      tb_key.Text = key_st;
    }

    //------------------------------------------------ Button, Start
    private void button_start_Click(object sender, EventArgs e)
    {



            if (button_start.Text == "Start")
            {
           button_start.Text = "Stop";

        manager.init_module();
	    manager.init_controller(tbox_numtrace.Text, tb_key.Text, tb_samples.Text, checkBox1.Checked /*, tb_Delay.Text*/);
        manager.tracer_addCompletedEventHandler(new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted));
        manager.tracer_addProgressChangedEventHandler(new ProgressChangedEventHandler(worker_ProgressChanged));
        manager.tracer_run();

        


      }
      else
      {
        manager.tracer_cancel();
               // System.Threading.Thread.Sleep(200);
                //chartArea.Dispose();
                //chart1.ChartAreas.Remove(new"First Area");
      }
    }


    //************************************************ BackGroundWorker_EventHandler
    //------------------------------------------------ 
    private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e) {
      label_numtrace.Text = ((ControllerReport)e.UserState).num_trace.ToString();

      label_text_in.Text  = format(((ControllerReport)e.UserState).text_in);
      label_text_out.Text = format(((ControllerReport)e.UserState).text_out);
      label_text_ans.Text = format(((ControllerReport)e.UserState).text_ans);
 
      Trace_data = ((ControllerReport)e.UserState).trace;
      CPAPlot = ((ControllerReport)e.UserState).CPAPlot;
      cpa = (List<float>[])((ControllerReport)e.UserState).cpa;


            // chart1.Series.Add("1");
            // string[] seriesArray = { "Cat", "Dog", "Bird", "Monkey" };
            //int[] pointsArray = { 2, 1, 7, 5 };
            // series1.Points.Invalidate();
            series1.Points.Clear();
            //float[] values = { 0, 70, 90, 20, 70, 220, 30, 60, 30, 81, 10, 39, 0, 70, 90, 20, 70, 220, 30, 60, 30, 81, 10, 39, 0, 70, 90, 20, 70, 220, 30, 60, 30, 81, 10, 39, 0, 70, 90, 20, 70, 220, 30, 60, 30, 81, 10, 39 };

            for (int i=5;i<1024;i++)
            {
                series1.Points.AddXY(i, Trace_data[i]);
               
            }


            if (CPAPlot == true && cpa[0].Count > 0)
            {
                for (int key = 0; key < 256; key++)
                {
                    //seriesCPA[key].Points.Clear();
                    //for (int i = 0; i < cpa[0].Count; i++)
                    int index = cpa[0].Count - 1;
                    {
                        // series1.Points.AddXY((i + 1) * 200, 0.01);
                        seriesCPA[key].Points.AddXY((index+1) * 200, cpa[key][index]);

                    }
                    seriesCPA[74].Color = Color.FromArgb(255, Color.Red);
                    //  chartCPA.ChartAreas[0].RecalculateAxesScale();
                    //  chartCPA.Update();
                }
            }
            


        }

    //------------------------------------------------ 
    private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
      if (!e.Cancelled)
      { // finished
        //MessageBox.Show("Finished.",  "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
      else
      {
        //MessageBox.Show("Cancelled.", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Stop);
      }
      button_start.Text = "Start";
    }

 
    //************************************************ 
 		//------------------------------------------------ format()
    private string format(byte[] data)
    {
      string text = "";
      for (int i = 0; i < data.Length; i++) text += String.Format("{0:X2} ", data[i]);
      return text;
    }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            manager.set_multiAES(checkBox1.Checked);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            manager.set_delay((int)numericUpDown1.Value);
        }

        private void chartCPA_Click(object sender, EventArgs e)
        {

        }
    }
}
