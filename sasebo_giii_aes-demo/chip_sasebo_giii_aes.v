/*-------------------------------------------------------------------------
 AES cryptographic module for FPGA on SASEBO-GIII
 
 File name   : chip_sasebo_giii_aes.v
 Version     : 1.0
 Created     : APR/02/2012
 Last update : APR/25/2013
 Desgined by : Toshihiro Katashita
 
 
 Copyright (C) 2012,2013 AIST
 
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
 appreciate it if you can cite our paper.
 (http://www.risec.aist.go.jp/project/sasebo/)
 -------------------------------------------------------------------------*/


//================================================ CHIP_SASEBO_GIII_AES
module CHIP_SASEBO_GIII_AES
  (// Local bus for GII
   lbus_di_a, lbus_do, lbus_wrn, lbus_rdn,
   lbus_clkn, lbus_rstn,

   // GPIO and LED
   gpio_startn, gpio_endn, gpio_exec, led,

   // Clock OSC
   osc_en_b);
   
   //------------------------------------------------
   // Local bus for GII
   input [15:0]  lbus_di_a;
   output [15:0] lbus_do;
   input         lbus_wrn, lbus_rdn;
   input         lbus_clkn, lbus_rstn;

   // GPIO and LED
   output        gpio_startn, gpio_endn, gpio_exec;
   output [9:0]  led;

   // Clock OSC
   output        osc_en_b;

   //------------------------------------------------
   // Internal clock
   wire         clk, rst;

   // Local bus
   reg [15:0]   lbus_a, lbus_di;
   
   // Block cipher
   wire [127:0] blk_kin, blk_din, blk_dout;
   wire         blk_krdy, blk_kvld, blk_drdy, blk_dvld;
   wire         blk_encdec, blk_en, blk_rstn, blk_busy;
   reg          blk_drdy_delay;
	
	// TDC clock
	wire  			clk_sample;
	wire  [7:0] 	wave_data;
	wire  [12:0] 	mem_addr;
	wire 				mem_we;
	wire 				clk_delay;
	
	wire				ref_clk;
	wire				delay_clk1;
	wire	[15:0]	dyn_delay;
  
   //------------------------------------------------
   assign led[0] = rst;
   assign led[1] = lbus_rstn;
   assign led[2] = 1'b0;
   assign led[3] = blk_rstn;
   assign led[4] = blk_encdec;
   assign led[5] = blk_krdy;
   assign led[6] = blk_kvld;
   assign led[7] = 1'b0;
   assign led[8] = blk_dvld;
   assign led[9] = blk_busy;

   assign osc_en_b = 1'b0;
   //------------------------------------------------
   always @(posedge clk) if (lbus_wrn)  lbus_a  <= lbus_di_a;
   always @(posedge clk) if (~lbus_wrn) lbus_di <= lbus_di_a;

   LBUS_IF lbus_if
     (.lbus_a(lbus_a), .lbus_di(lbus_di), .lbus_do(lbus_do),
      .lbus_wr(lbus_wrn), .lbus_rd(lbus_rdn),
      .blk_kin(blk_kin), .blk_din(blk_din), .blk_dout(blk_dout),
      .blk_krdy(blk_krdy), .blk_drdy(blk_drdy), 
      .blk_kvld(blk_kvld), .blk_dvld(blk_dvld),
      .blk_encdec(blk_encdec), .blk_en(blk_en), .blk_rstn(blk_rstn),
      .clk(clk), .rst(rst), .clk_sample(clk_sample), .mem_addr(mem_addr), .wr_en(mem_we), .wave_data(wave_data), .dyn_delay(dyn_delay));

   //------------------------------------------------
   assign gpio_startn = ~blk_drdy;
   assign gpio_endn   = 1'b0; //~blk_dvld;
   assign gpio_exec   = 1'b0; //blk_busy;

   always @(posedge clk) blk_drdy_delay <= blk_drdy;

   AES_Composite_enc AES_Composite_enc
     (.Kin(blk_kin), .Din(blk_din), .Dout(blk_dout),
      .Krdy(blk_krdy), .Drdy(blk_drdy_delay), .Kvld(blk_kvld), .Dvld(blk_dvld),
      .EncDec(blk_encdec), .EN(blk_en), .BSY(blk_busy),
      .CLK(clk), .RSTn(blk_rstn));

   //------------------------------------------------   
   MK_CLKRST mk_clkrst (.clkin(lbus_clkn), .rstnin(lbus_rstn),
                        .clk(clk), .clk_sample(clk_sample), .rst(rst), .clk_delay(clk_delay), .delay(dyn_delay));
								
								
	tracer tracer0 (.clk_sample(clk_sample), .tdc_start(blk_drdy), .lbus_rstn(blk_rstn), .mem_we(mem_we), .mem_addr(mem_addr), .wave_data(wave_data), .clk_delay(clk_delay));
	
//	dyn_delay dd0 (.clk_in(clk_delay), .dyn_delay(dyn_delay), .ref_clk(ref_clk), .delay_clk(delay_clk1));
	
//
//module tracer(
//	clk_sample,
//	tdc_start,
//	lbus_rstn,
//	mem_we,
//	wave_data
//    );
	
								
endmodule // CHIP_SASEBO_GIII_AES


   
//================================================ MK_CLKRST
module MK_CLKRST (clkin, rstnin, clk, clk_sample, clk_delay, rst, delay);
   //synthesis attribute keep_hierarchy of MK_CLKRST is no;
   
   //------------------------------------------------
   input  clkin, rstnin;
	input  [7:0]  delay;
   output clk, clk_sample, rst, clk_delay;
   
   //------------------------------------------------
   wire    refclk;
	wire    clk0;
	wire    clk1;
	wire    clkref;
	
//   wire   clk_dcm, locked;

   //------------------------------------------------ clock
   IBUFG u10 (.I(clkin), .O(refclk)); 

/*
   DCM_BASE u11 (.CLKIN(refclk), .CLKFB(clk), .RST(~rstnin),
                 .CLK0(clk_dcm),     .CLKDV(),
                 .CLK90(), .CLK180(), .CLK270(),
                 .CLK2X(), .CLK2X180(), .CLKFX(), .CLKFX180(),
                 .LOCKED(locked));
   BUFG  u12 (.I(clk_dcm),   .O(clk));
*/

   //BUFG  u12 (.I(refclk),   .O(clk));

   //------------------------------------------------ reset
   MK_RST u20 (.locked(rstnin), .clk(clk), .rst(rst));
	
  	BUFG u12 (.I(clk0), .O(clk)); 
  	BUFG u13 (.I(clk1), .O(clk_sample)); 
  	BUFG u14 (.I(clk2), .O(clk_ref)); 
   //assign clk = clk0;


	wire CLKFBOUT, CLKFBIN;
	BUFG  u15 (.I(CLKFBOUT),   .O(CLKFBIN));
 
							
								
	MMCME2_BASE #(
		.BANDWIDTH("OPTIMIZED"), // Jitter programming (OPTIMIZED, HIGH, LOW)
		.CLKFBOUT_MULT_F(30.0), // Multiply value for all CLKOUT (2.000-64.000).
		.CLKFBOUT_PHASE(0.0), // Phase offset in degrees of CLKFB (-360.000-360.000).
		.CLKIN1_PERIOD(41), // Input clock period in ns to ps resolution (i.e. 33.333 is 30 MHz).
		// CLKOUT0_DIVIDE - CLKOUT6_DIVIDE: Divide amount for each CLKOUT (1-128)
		.CLKOUT1_DIVIDE(3),
		.CLKOUT2_DIVIDE(2),
		.CLKOUT3_DIVIDE(1),
		.CLKOUT4_DIVIDE(1),
		.CLKOUT5_DIVIDE(1),
		.CLKOUT6_DIVIDE(1),
		.CLKOUT0_DIVIDE_F(30.0), // Divide amount for CLKOUT0 (1.000-128.000).
		// CLKOUT0_DUTY_CYCLE - CLKOUT6_DUTY_CYCLE: Duty cycle for each CLKOUT (0.01-0.99).
		.CLKOUT0_DUTY_CYCLE(0.5),
		.CLKOUT1_DUTY_CYCLE(0.5),
		.CLKOUT2_DUTY_CYCLE(0.5),
		.CLKOUT3_DUTY_CYCLE(0.5),
		.CLKOUT4_DUTY_CYCLE(0.5),
		.CLKOUT5_DUTY_CYCLE(0.5),
		.CLKOUT6_DUTY_CYCLE(0.5),
		// CLKOUT0_PHASE - CLKOUT6_PHASE: Phase offset for each CLKOUT (-360.000-360.000).
		.CLKOUT0_PHASE(0.0),
		.CLKOUT1_PHASE(0.0),
		.CLKOUT2_PHASE(0.0),
		.CLKOUT3_PHASE(0.0),
		.CLKOUT4_PHASE(0.0),
		.CLKOUT5_PHASE(0.0),
		.CLKOUT6_PHASE(0.0),
		.CLKOUT4_CASCADE("FALSE"), // Cascade CLKOUT4 counter with CLKOUT6 (FALSE, TRUE)
		.DIVCLK_DIVIDE(1), // Master division value (1-106)
		.REF_JITTER1(0.0), // Reference input jitter in UI (0.000-0.999).
		.STARTUP_WAIT("FALSE") // Delays DONE until MMCM is locked (FALSE, TRUE)
	)
	MMCME2_BASE_inst (
		// Clock Outputs: 1-bit (each) output: User configurable clock outputs
		.CLKOUT0(clk0), // 1-bit output: CLKOUT0
		.CLKOUT1(clk1), // 1-bit output: CLKOUT1
		.CLKOUT2(clk2), // 1-bit output: CLKOUT2
		//.CLKOUT3(CLKOUT3), // 1-bit output: CLKOUT3
		//.CLKOUT4(CLKOUT4), // 1-bit output: CLKOUT4
		//.CLKOUT5(CLKOUT5), // 1-bit output: CLKOUT5
		//.CLKOUT6(CLKOUT6), // 1-bit output: CLKOUT6
		// Feedback Clocks: 1-bit (each) output: Clock feedback ports
		.CLKFBOUT(CLKFBOUT), // 1-bit output: Feedback clock
		//.CLKFBOUTB(CLKFBOUT), // 1-bit output: Inverted CLKFBOUT
		// Status Ports: 1-bit (each) output: MMCM status ports
		.LOCKED(LOCKED), // 1-bit output: LOCK
		// Clock Inputs: 1-bit (each) input: Clock input
		.CLKIN1(refclk), // 1-bit input: Clock
		// Control Ports: 1-bit (each) input: MMCM control ports
		.PWRDWN(PWRDWN), // 1-bit input: Power-down
		.RST(1'b0), // 1-bit input: Reset
		// Feedback Clocks: 1-bit (each) input: Clock feedback ports
		.CLKFBIN(CLKFBIN) // 1-bit input: Feedback clock
	);
	// End of MMCME2_BASE_inst instantiation				
			
							



IDELAYE2 #(
.CINVCTRL_SEL("FALSE"), // Enable dynamic clock inversion (FALSE, TRUE)
.DELAY_SRC("DATAIN"), // Delay input (IDATAIN, DATAIN)
.HIGH_PERFORMANCE_MODE("TRUE"), // Reduced jitter ("TRUE"), Reduced power ("FALSE")
.IDELAY_TYPE("VAR_LOAD"), // FIXED, VARIABLE, VAR_LOAD, VAR_LOAD_PIPE
.IDELAY_VALUE(0), // Input delay tap setting (0-31)
.PIPE_SEL("TRUE"), // Select pipelined mode, FALSE, TRUE
.REFCLK_FREQUENCY(192.0), // IDELAYCTRL clock input frequency in MHz (190.0-210.0).
.SIGNAL_PATTERN("CLOCK") // DATA, CLOCK input signal
)
IDELAYE2_inst0 (
.CNTVALUEOUT(), // 5-bit output: Counter value output
.DATAOUT(clk_delay), // 1-bit output: Delayed data output
.C(clk_sample), // 1-bit input: Clock input  refclk
.CE(1'b0), // 1-bit input: Active high enable increment/decrement input
.CINVCTRL(1'b0), // 1-bit input: Dynamic clock inversion input
.CNTVALUEIN(delay), // 5-bit input: Counter value input
.DATAIN(clk_sample), // 1-bit input: Internal delay data input
.IDATAIN(), // 1-bit input: Data input from the I/O
.INC(1'b0), // 1-bit input: Increment / Decrement tap delay input
.LD(1'b1), // 1-bit input: Load IDELAY_VALUE input
.LDPIPEEN(1'b1), // 1-bit input: Enable PIPELINE register to load data input
.REGRST(1'b0) // 1-bit input: Active-high reset tap-delay input
);


//
////(* IODELAY_LOC = "X1Y0" *)
//IDELAYE2 #(
//.CINVCTRL_SEL("FALSE"), // Enable dynamic clock inversion (FALSE, TRUE)
//.DELAY_SRC("DATAIN"), // Delay input (IDATAIN, DATAIN)
//.HIGH_PERFORMANCE_MODE("TRUE"), // Reduced jitter ("TRUE"), Reduced power ("FALSE")
//.IDELAY_TYPE("VAR_LOAD"), // FIXED, VARIABLE, VAR_LOAD, VAR_LOAD_PIPE
//.IDELAY_VALUE(7), // Input delay tap setting (0-31)
//.PIPE_SEL("TRUE"), // Select pipelined mode, FALSE, TRUE
//.REFCLK_FREQUENCY(200.0), // IDELAYCTRL clock input frequency in MHz (190.0-210.0).
//.SIGNAL_PATTERN("CLOCK") // DATA, CLOCK input signal
//)
//IDELAYE2_inst1 (
//.CNTVALUEOUT(), // 5-bit output: Counter value output
//.DATAOUT(clk4t), // 1-bit output: Delayed data output
//.C(clk4), // 1-bit input: Clock input  refclk
//.CE(1'b0), // 1-bit input: Active high enable increment/decrement input
//.CINVCTRL(1'b0), // 1-bit input: Dynamic clock inversion input
//.CNTVALUEIN(dataIn[15]), // 5-bit input: Counter value input
//.DATAIN(clk4), // 1-bit input: Internal delay data input
//.IDATAIN(), // 1-bit input: Data input from the I/O
//.INC(1'b0), // 1-bit input: Increment / Decrement tap delay input
//.LD(1'b1), // 1-bit input: Load IDELAY_VALUE input
//.LDPIPEEN(1'b1), // 1-bit input: Enable PIPELINE register to load data input
//.REGRST(1'b0) // 1-bit input: Active-high reset tap-delay input
//);



//(*LOC="IDELAY_X1Y1"*)
//(* IODELAY_LOC = "X1Y0" *)
IDELAYCTRL IDELAYCTRL_inst (
.RDY(), // 1-bit output: Ready output
.REFCLK(clkref), // 1-bit input: Reference clock input  //CLKOUTREF
.RST(1'b0) // 1-bit input: Active high reset input
);

							
	
	
endmodule // MK_CLKRST



//================================================ MK_RST
module MK_RST (locked, clk, rst);
   //synthesis attribute keep_hierarchy of MK_RST is no;
   
   //------------------------------------------------
   input  locked, clk;
   output rst;

   //------------------------------------------------
   reg [15:0] cnt;
   
   //------------------------------------------------
   always @(posedge clk or negedge locked) 
     if (~locked)    cnt <= 16'h0;
     else if (~&cnt) cnt <= cnt + 16'h1;

   assign rst = ~&cnt;
endmodule // MK_RST
