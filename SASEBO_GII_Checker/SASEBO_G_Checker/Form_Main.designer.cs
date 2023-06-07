//IITG code
namespace SASEBO_G_Checker
{
  partial class Form_Main
  {
    /// <summary>
    /// 必要なデザイナ変数です。
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// 使用中のリソースをすべてクリーンアップします。
    /// </summary>
    /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows フォーム デザイナで生成されたコード

    /// <summary>
    /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
    /// コード エディタで変更しないでください。
    /// </summary>
    private void InitializeComponent()
    {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tbox_numtrace = new System.Windows.Forms.TextBox();
            this.lb_fixed_numtrace = new System.Windows.Forms.Label();
            this.button_start = new System.Windows.Forms.Button();
            this.button_key = new System.Windows.Forms.Button();
            this.label_text_ans = new System.Windows.Forms.Label();
            this.label_text_out = new System.Windows.Forms.Label();
            this.label_text_in = new System.Windows.Forms.Label();
            this.lb_fixed_plaintext = new System.Windows.Forms.Label();
            this.lb_fixed_answer = new System.Windows.Forms.Label();
            this.lb_fixed_ciphertext = new System.Windows.Forms.Label();
            this.lb_fixed_numtraces = new System.Windows.Forms.Label();
            this.label_numtrace = new System.Windows.Forms.Label();
            this.lb_fixed_key = new System.Windows.Forms.Label();
            this.tb_key = new System.Windows.Forms.TextBox();
            this.samples = new System.Windows.Forms.Label();
            this.tb_samples = new System.Windows.Forms.TextBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.chartCPA = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartCPA)).BeginInit();
            this.SuspendLayout();
            // 
            // tbox_numtrace
            // 
            this.tbox_numtrace.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.tbox_numtrace.Location = new System.Drawing.Point(113, 9);
            this.tbox_numtrace.Margin = new System.Windows.Forms.Padding(4);
            this.tbox_numtrace.Name = "tbox_numtrace";
            this.tbox_numtrace.Size = new System.Drawing.Size(92, 26);
            this.tbox_numtrace.TabIndex = 83;
            this.tbox_numtrace.Text = "50000";
            // 
            // lb_fixed_numtrace
            // 
            this.lb_fixed_numtrace.AutoSize = true;
            this.lb_fixed_numtrace.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lb_fixed_numtrace.Location = new System.Drawing.Point(16, 12);
            this.lb_fixed_numtrace.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_fixed_numtrace.Name = "lb_fixed_numtrace";
            this.lb_fixed_numtrace.Size = new System.Drawing.Size(87, 17);
            this.lb_fixed_numtrace.TabIndex = 82;
            this.lb_fixed_numtrace.Text = "#TRACES";
            // 
            // button_start
            // 
            this.button_start.BackColor = System.Drawing.SystemColors.Control;
            this.button_start.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_start.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_start.Location = new System.Drawing.Point(379, 95);
            this.button_start.Margin = new System.Windows.Forms.Padding(4);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(89, 43);
            this.button_start.TabIndex = 81;
            this.button_start.Text = "Start";
            this.button_start.UseVisualStyleBackColor = false;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // button_key
            // 
            this.button_key.BackColor = System.Drawing.SystemColors.Control;
            this.button_key.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_key.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_key.Location = new System.Drawing.Point(500, 101);
            this.button_key.Margin = new System.Windows.Forms.Padding(4);
            this.button_key.Name = "button_key";
            this.button_key.Size = new System.Drawing.Size(124, 32);
            this.button_key.TabIndex = 90;
            this.button_key.Text = "Change key";
            this.button_key.UseVisualStyleBackColor = false;
            this.button_key.Click += new System.EventHandler(this.button_key_Click);
            // 
            // label_text_ans
            // 
            this.label_text_ans.AutoSize = true;
            this.label_text_ans.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.label_text_ans.Location = new System.Drawing.Point(128, 276);
            this.label_text_ans.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_text_ans.Name = "label_text_ans";
            this.label_text_ans.Size = new System.Drawing.Size(479, 20);
            this.label_text_ans.TabIndex = 99;
            this.label_text_ans.Text = "00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00";
            // 
            // label_text_out
            // 
            this.label_text_out.AutoSize = true;
            this.label_text_out.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_text_out.Location = new System.Drawing.Point(128, 246);
            this.label_text_out.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_text_out.Name = "label_text_out";
            this.label_text_out.Size = new System.Drawing.Size(479, 20);
            this.label_text_out.TabIndex = 98;
            this.label_text_out.Text = "00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00";
            // 
            // label_text_in
            // 
            this.label_text_in.AutoSize = true;
            this.label_text_in.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_text_in.Location = new System.Drawing.Point(128, 218);
            this.label_text_in.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_text_in.Name = "label_text_in";
            this.label_text_in.Size = new System.Drawing.Size(479, 20);
            this.label_text_in.TabIndex = 97;
            this.label_text_in.Text = "00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00";
            // 
            // lb_fixed_plaintext
            // 
            this.lb_fixed_plaintext.AutoSize = true;
            this.lb_fixed_plaintext.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lb_fixed_plaintext.Location = new System.Drawing.Point(16, 215);
            this.lb_fixed_plaintext.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_fixed_plaintext.Name = "lb_fixed_plaintext";
            this.lb_fixed_plaintext.Size = new System.Drawing.Size(81, 17);
            this.lb_fixed_plaintext.TabIndex = 92;
            this.lb_fixed_plaintext.Text = "Plaintext";
            // 
            // lb_fixed_answer
            // 
            this.lb_fixed_answer.AutoSize = true;
            this.lb_fixed_answer.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lb_fixed_answer.Location = new System.Drawing.Point(16, 276);
            this.lb_fixed_answer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_fixed_answer.Name = "lb_fixed_answer";
            this.lb_fixed_answer.Size = new System.Drawing.Size(68, 17);
            this.lb_fixed_answer.TabIndex = 93;
            this.lb_fixed_answer.Text = "Answer";
            // 
            // lb_fixed_ciphertext
            // 
            this.lb_fixed_ciphertext.AutoSize = true;
            this.lb_fixed_ciphertext.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lb_fixed_ciphertext.Location = new System.Drawing.Point(16, 245);
            this.lb_fixed_ciphertext.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_fixed_ciphertext.Name = "lb_fixed_ciphertext";
            this.lb_fixed_ciphertext.Size = new System.Drawing.Size(94, 17);
            this.lb_fixed_ciphertext.TabIndex = 94;
            this.lb_fixed_ciphertext.Text = "Ciphertext";
            // 
            // lb_fixed_numtraces
            // 
            this.lb_fixed_numtraces.AutoSize = true;
            this.lb_fixed_numtraces.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lb_fixed_numtraces.Location = new System.Drawing.Point(16, 185);
            this.lb_fixed_numtraces.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_fixed_numtraces.Name = "lb_fixed_numtraces";
            this.lb_fixed_numtraces.Size = new System.Drawing.Size(74, 17);
            this.lb_fixed_numtraces.TabIndex = 100;
            this.lb_fixed_numtraces.Text = "#Traces";
            // 
            // label_numtrace
            // 
            this.label_numtrace.AutoSize = true;
            this.label_numtrace.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_numtrace.Location = new System.Drawing.Point(128, 185);
            this.label_numtrace.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_numtrace.Name = "label_numtrace";
            this.label_numtrace.Size = new System.Drawing.Size(19, 20);
            this.label_numtrace.TabIndex = 101;
            this.label_numtrace.Text = "0";
            // 
            // lb_fixed_key
            // 
            this.lb_fixed_key.AutoSize = true;
            this.lb_fixed_key.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lb_fixed_key.Location = new System.Drawing.Point(52, 50);
            this.lb_fixed_key.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_fixed_key.Name = "lb_fixed_key";
            this.lb_fixed_key.Size = new System.Drawing.Size(41, 17);
            this.lb_fixed_key.TabIndex = 106;
            this.lb_fixed_key.Text = "KEY";
            // 
            // tb_key
            // 
            this.tb_key.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_key.Location = new System.Drawing.Point(113, 47);
            this.tb_key.Margin = new System.Windows.Forms.Padding(4);
            this.tb_key.Name = "tb_key";
            this.tb_key.Size = new System.Drawing.Size(523, 26);
            this.tb_key.TabIndex = 107;
            this.tb_key.Text = "00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E F0";
            // 
            // samples
            // 
            this.samples.AutoSize = true;
            this.samples.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.samples.Location = new System.Drawing.Point(25, 101);
            this.samples.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.samples.Name = "samples";
            this.samples.Size = new System.Drawing.Size(88, 17);
            this.samples.TabIndex = 108;
            this.samples.Text = "SAMPLES";
            // 
            // tb_samples
            // 
            this.tb_samples.Location = new System.Drawing.Point(117, 98);
            this.tb_samples.Margin = new System.Windows.Forms.Padding(4);
            this.tb_samples.Name = "tb_samples";
            this.tb_samples.Size = new System.Drawing.Size(73, 22);
            this.tb_samples.TabIndex = 109;
            this.tb_samples.Text = "5000";
            // 
            // chart1
            // 
            this.chart1.Location = new System.Drawing.Point(723, 9);
            this.chart1.Margin = new System.Windows.Forms.Padding(4);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(550, 369);
            this.chart1.TabIndex = 110;
            this.chart1.Text = "chart1";
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(113, 146);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(207, 21);
            this.checkBox1.TabIndex = 111;
            this.checkBox1.Text = "Enable Multiple AES Circuits";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(552, 159);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 22);
            this.numericUpDown1.TabIndex = 112;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(455, 161);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 17);
            this.label1.TabIndex = 113;
            this.label1.Text = "Offset Adjust";
            // 
            // chartCPA
            // 
            chartArea1.Name = "ChartArea1";
            this.chartCPA.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartCPA.Legends.Add(legend1);
            this.chartCPA.Location = new System.Drawing.Point(1327, 9);
            this.chartCPA.Name = "chartCPA";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartCPA.Series.Add(series1);
            this.chartCPA.Size = new System.Drawing.Size(626, 369);
            this.chartCPA.TabIndex = 114;
            this.chartCPA.Text = "chart2";
            this.chartCPA.Click += new System.EventHandler(this.chartCPA_Click);
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2000, 422);
            this.Controls.Add(this.chartCPA);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.tb_samples);
            this.Controls.Add(this.samples);
            this.Controls.Add(this.label_numtrace);
            this.Controls.Add(this.lb_fixed_answer);
            this.Controls.Add(this.lb_fixed_ciphertext);
            this.Controls.Add(this.label_text_in);
            this.Controls.Add(this.label_text_out);
            this.Controls.Add(this.lb_fixed_plaintext);
            this.Controls.Add(this.label_text_ans);
            this.Controls.Add(this.tb_key);
            this.Controls.Add(this.lb_fixed_key);
            this.Controls.Add(this.lb_fixed_numtraces);
            this.Controls.Add(this.button_key);
            this.Controls.Add(this.tbox_numtrace);
            this.Controls.Add(this.lb_fixed_numtrace);
            this.Controls.Add(this.button_start);
            this.Location = new System.Drawing.Point(10, 10);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form_Main";
            this.Text = "SASEBO_G Checker (AES)";
            this.Load += new System.EventHandler(this.Form_Controller_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartCPA)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox tbox_numtrace;
    private System.Windows.Forms.Label lb_fixed_numtrace;
    private System.Windows.Forms.Button button_start;
    private System.Windows.Forms.Button button_key;
    private System.Windows.Forms.Label label_text_ans;
    private System.Windows.Forms.Label label_text_out;
    private System.Windows.Forms.Label label_text_in;
    private System.Windows.Forms.Label lb_fixed_plaintext;
    private System.Windows.Forms.Label lb_fixed_answer;
    private System.Windows.Forms.Label lb_fixed_ciphertext;
    private System.Windows.Forms.Label lb_fixed_numtraces;
		private System.Windows.Forms.Label label_numtrace;
    private System.Windows.Forms.Label lb_fixed_key;
		private System.Windows.Forms.TextBox tb_key;
        private System.Windows.Forms.Label samples;
        private System.Windows.Forms.TextBox tb_samples;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartCPA;
        //     private System.Windows.Forms.Label Delay;
        //     private System.Windows.Forms.TextBox tb_Delay;
    }
}

