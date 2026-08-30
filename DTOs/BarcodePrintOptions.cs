using System;

namespace POS
{
    public enum BarcodePrintMode
    {
        ThermalRoll = 0,
        A4Sheet = 1
    }

    public class BarcodePrintOptions
    {
        public string StoreName { get; set; } = "";
        public bool ShowStoreName { get; set; } = true;
        public bool ShowProductName { get; set; } = true;
        public bool ShowPrice { get; set; } = true;
        public bool ShowBarcodeText { get; set; } = true;
        public string CurrencySymbol { get; set; } = "ج.م";
        
        // Dimensions in Millimeters
        public float LabelWidthMm { get; set; } = 38f;
        public float LabelHeightMm { get; set; } = 25f;
        
        // Printing Mode & Sheet Layout
        public BarcodePrintMode PrintMode { get; set; } = BarcodePrintMode.ThermalRoll;
        public int SheetColumns { get; set; } = 3;
        public int SheetRows { get; set; } = 8;
        
        // Number of copies
        public int Copies { get; set; } = 1;
        
        // Selected Printer
        public string PrinterName { get; set; } = "";
        
        // Calculated hundredths of an inch (1 mm = 3.937 hundredths of an inch)
        public int WidthInHundredths => (int)Math.Round(LabelWidthMm * 3.93701f);
        public int HeightInHundredths => (int)Math.Round(LabelHeightMm * 3.93701f);
    }
}
