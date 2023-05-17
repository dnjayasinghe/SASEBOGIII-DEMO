# SASEBOGIII-DEMO

This repo contains Xilinx ISE Design files and Visual C# project files to demonstrate TDC sensor Operations. 

![image](https://github.com/dnjayasinghe/SASEBOGIII-DEMO/assets/29743044/7b75e999-cc63-4ca4-a642-5edd652eaaf2)


The hardware project is created demonstrate on-chip voltage sensor power measurements on a SASEBO GIII FPGA. SASEBO GIII is manufactured using different Xilinx Kintex 7 FPGAs and we used 325T. You can change FPGA part number to suit your SASEBO GIII FPGA board.

The SASEBO checker displays TDC sensor readings. "Enable Multiple AES Circuits" check box can be used to increase FPGA power consumption (enables 2 additional AES circuits) which will reflect in TDC sensor readings.  
1 AES circuit is enabled
![image](https://github.com/dnjayasinghe/SASEBOGIII-DEMO/assets/29743044/9ca946f0-4469-4a38-ab5d-d29d9de0f5d0)

3 AES circuits are enabled
![image](https://github.com/dnjayasinghe/SASEBOGIII-DEMO/assets/29743044/bdfce848-1709-441c-8293-9518834c9603)


"Offset Adjust" value can be used to adjust the vertical offset of the TDC sensor outputs (similar to an oscilloscope).  
