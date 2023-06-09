////////////////////////////////////////////////////////////////////////////////
// Copyright (c) 1995-2013 Xilinx, Inc.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////
//   ____  ____ 
//  /   /\/   / 
// /___/  \  /    Vendor: Xilinx 
// \   \   \/     Version : 14.7
//  \   \         Application : xaw2verilog
//  /   /         Filename : test.v
// /___/   /\     Timestamp : 07/14/2018 14:57:59
// \   \  /  \ 
//  \___\/\___\ 
//
//Command: xaw2verilog -st E:\Microblaze\CLK-scrmb\GII\randclkswitch\giii\v3.1 - recong\sasebo_giii_aes\sasebo_giii_aes\ipcore_dir\.\test.xaw E:\Microblaze\CLK-scrmb\GII\randclkswitch\giii\v3.1 - recong\sasebo_giii_aes\sasebo_giii_aes\ipcore_dir\.\test
//Design Name: test
//Device: xc5vlx50-2ff324
//
// Module test
// Generated by Xilinx Architecture Wizard
// Written for synthesis tool: XST
// For block PLL_ADV_INST, Estimated PLL Jitter for CLKOUT0 = 0.171 ns
`timescale 1ns / 1ps

module test(CLKIN_IN, 
            RST_IN, 
            CLKIN_IBUFG_OUT, 
            CLKOUT0_OUT, 
            LOCKED_OUT);

    input CLKIN_IN;
    input RST_IN;
   output CLKIN_IBUFG_OUT;
   output CLKOUT0_OUT;
   output LOCKED_OUT;
   
   wire CLKFBOUT_CLKFBIN;
   wire CLKFB_IN;
   wire CLKIN_IBUFG;
   wire CLKOUT0_BUF;
   wire CLK0_BUF;
   wire DCM_LOCKED_INV_IN;
   wire GND_BIT;
   wire [4:0] GND_BUS_5;
   wire [6:0] GND_BUS_7;
   wire [15:0] GND_BUS_16;
   wire PLL_LOCKED_INV_RST;
   wire VCC_BIT;
   
   assign GND_BIT = 0;
   assign GND_BUS_5 = 5'b00000;
   assign GND_BUS_7 = 7'b0000000;
   assign GND_BUS_16 = 16'b0000000000000000;
   assign VCC_BIT = 1;
   assign CLKIN_IBUFG_OUT = CLKIN_IBUFG;
   IBUFG  CLKIN_IBUFG_INST (.I(CLKIN_IN), 
                           .O(CLKIN_IBUFG));
   BUFG  CLKOUT0_BUFG_INST (.I(CLKOUT0_BUF), 
                           .O(CLKOUT0_OUT));
   BUFG  CLK0_BUFG_INST (.I(CLK0_BUF), 
                        .O(CLKFB_IN));
   DCM_ADV #( .CLK_FEEDBACK("1X"), .CLKDV_DIVIDE(2.0), .CLKFX_DIVIDE(1), 
         .CLKFX_MULTIPLY(4), .CLKIN_DIVIDE_BY_2("FALSE"), 
         .CLKIN_PERIOD(25.000), .CLKOUT_PHASE_SHIFT("NONE"), 
         .DCM_AUTOCALIBRATION("TRUE"), .DCM_PERFORMANCE_MODE("MAX_SPEED"), 
         .DESKEW_ADJUST("SYSTEM_SYNCHRONOUS"), .DFS_FREQUENCY_MODE("LOW"), 
         .DLL_FREQUENCY_MODE("LOW"), .DUTY_CYCLE_CORRECTION("TRUE"), 
         .FACTORY_JF(16'hF0F0), .PHASE_SHIFT(0), .STARTUP_WAIT("FALSE"), 
         .SIM_DEVICE("VIRTEX5") ) DCM_ADV_INST (.CLKFB(CLKFB_IN), 
                         .CLKIN(CLKIN_IBUFG), 
                         .DADDR(GND_BUS_7[6:0]), 
                         .DCLK(GND_BIT), 
                         .DEN(GND_BIT), 
                         .DI(GND_BUS_16[15:0]), 
                         .DWE(GND_BIT), 
                         .PSCLK(GND_BIT), 
                         .PSEN(GND_BIT), 
                         .PSINCDEC(GND_BIT), 
                         .RST(RST_IN), 
                         .CLKDV(), 
                         .CLKFX(), 
                         .CLKFX180(), 
                         .CLK0(CLK0_BUF), 
                         .CLK2X(), 
                         .CLK2X180(), 
                         .CLK90(), 
                         .CLK180(), 
                         .CLK270(), 
                         .DO(), 
                         .DRDY(), 
                         .LOCKED(DCM_LOCKED_INV_IN), 
                         .PSDONE());
   INV  INV_INST (.I(DCM_LOCKED_INV_IN), 
                 .O(PLL_LOCKED_INV_RST));
   PLL_ADV #( .BANDWIDTH("OPTIMIZED"), .CLKIN1_PERIOD(10.000), 
         .CLKIN2_PERIOD(10.000), .CLKOUT0_DIVIDE(8), .CLKOUT0_PHASE(0.000), 
         .CLKOUT0_DUTY_CYCLE(0.500), .COMPENSATION("DCM2PLL"), 
         .DIVCLK_DIVIDE(1), .CLKFBOUT_MULT(6), .CLKFBOUT_PHASE(0.0), 
         .REF_JITTER(0.005000) ) PLL_ADV_INST (.CLKFBIN(CLKFBOUT_CLKFBIN), 
                         .CLKINSEL(VCC_BIT), 
                         .CLKIN1(CLK0_BUF), 
                         .CLKIN2(GND_BIT), 
                         .DADDR(GND_BUS_5[4:0]), 
                         .DCLK(GND_BIT), 
                         .DEN(GND_BIT), 
                         .DI(GND_BUS_16[15:0]), 
                         .DWE(GND_BIT), 
                         .REL(GND_BIT), 
                         .RST(PLL_LOCKED_INV_RST), 
                         .CLKFBDCM(), 
                         .CLKFBOUT(CLKFBOUT_CLKFBIN), 
                         .CLKOUTDCM0(), 
                         .CLKOUTDCM1(), 
                         .CLKOUTDCM2(), 
                         .CLKOUTDCM3(), 
                         .CLKOUTDCM4(), 
                         .CLKOUTDCM5(), 
                         .CLKOUT0(CLKOUT0_BUF), 
                         .CLKOUT1(), 
                         .CLKOUT2(), 
                         .CLKOUT3(), 
                         .CLKOUT4(), 
                         .CLKOUT5(), 
                         .DO(), 
                         .DRDY(), 
                         .LOCKED(LOCKED_OUT));
endmodule
