using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace POS
{
    /// <summary>
    /// Centralized Font Manager that embeds and registers the Cairo font family
    /// into both GDI+ (PrivateFontCollection) and GDI (AddFontMemResourceEx)
    /// to ensure 100% connected, non-glitched Arabic text rendering across WinForms controls.
    /// </summary>
    public static class FontManager
    {
        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);

        private static readonly PrivateFontCollection _pfc = new PrivateFontCollection();
        private static FontFamily _cairoFamily;
        private static bool _initialized = false;

        public static FontFamily CairoFamily => _cairoFamily ?? (_cairoFamily = GetLoadedCairoFamily());

        public static void Initialize()
        {
            if (_initialized) return;

            string[] fontFiles = new string[]
            {
                "Cairo-Regular.ttf",
                "Cairo-Bold.ttf",
                "Cairo-SemiBold.ttf",
                "Cairo-Medium.ttf",
                "Cairo-Black.ttf",
                "Cairo-ExtraBold.ttf",
                "Cairo-Light.ttf",
                "Cairo-ExtraLight.ttf"
            };

            var assembly = Assembly.GetExecutingAssembly();

            foreach (var fontFile in fontFiles)
            {
                byte[] fontData = null;

                // 1. Try loading from Embedded Resource: POS.fonts.<filename>
                string resourceName = $"POS.fonts.{fontFile}";
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream != null)
                    {
                        fontData = new byte[stream.Length];
                        stream.Read(fontData, 0, fontData.Length);
                    }
                }

                // 2. Fallback: try loading from disk (AppDomain BaseDirectory or relative fonts folder)
                if (fontData == null)
                {
                    string localPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "fonts", fontFile);
                    if (File.Exists(localPath))
                    {
                        fontData = File.ReadAllBytes(localPath);
                    }
                    else
                    {
                        string devPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\fonts", fontFile);
                        if (File.Exists(devPath))
                        {
                            fontData = File.ReadAllBytes(devPath);
                        }
                    }
                }

                if (fontData != null && fontData.Length > 0)
                {
                    RegisterFontMemory(fontData);
                }
            }

            _cairoFamily = GetLoadedCairoFamily();
            _initialized = true;
        }

        private static void RegisterFontMemory(byte[] fontData)
        {
            try
            {
                IntPtr fontPtr = Marshal.AllocCoTaskMem(fontData.Length);
                Marshal.Copy(fontData, 0, fontPtr, fontData.Length);

                // Register with GDI+ (PrivateFontCollection)
                _pfc.AddMemoryFont(fontPtr, fontData.Length);

                // Register with Native GDI (GDI32) so TextRenderer with UseCompatibleTextRendering=false can resolve it
                uint pcFonts = 0;
                AddFontMemResourceEx(fontPtr, (uint)fontData.Length, IntPtr.Zero, ref pcFonts);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error registering font: {ex.Message}");
            }
        }

        private static FontFamily GetLoadedCairoFamily()
        {
            if (_pfc.Families.Length > 0)
            {
                foreach (var family in _pfc.Families)
                {
                    if (family.Name.IndexOf("Cairo", StringComparison.OrdinalIgnoreCase) >= 0)
                        return family;
                }
                return _pfc.Families[0];
            }

            // Fallback to system font if Cairo was not found
            try
            {
                return new FontFamily("Segoe UI");
            }
            catch
            {
                return FontFamily.GenericSansSerif;
            }
        }

        public static Font GetRegular(float size, FontStyle style = FontStyle.Regular)
        {
            return new Font(CairoFamily, size, style, GraphicsUnit.Point);
        }

        public static Font GetBold(float size)
        {
            return new Font(CairoFamily, size, FontStyle.Bold, GraphicsUnit.Point);
        }

        public static Font GetSemiBold(float size)
        {
            return new Font(CairoFamily, size, FontStyle.Bold, GraphicsUnit.Point);
        }

        public static Font GetMedium(float size)
        {
            return new Font(CairoFamily, size, FontStyle.Regular, GraphicsUnit.Point);
        }

        /// <summary>
        /// Recursively applies the Cairo font family across a control hierarchy,
        /// ensuring clean connected Arabic shaping and proper DataGridView metrics.
        /// </summary>
        public static void ApplyCairoFont(Control root)
        {
            if (root == null) return;

            ApplyControlFont(root);

            foreach (Control child in root.Controls)
            {
                ApplyCairoFont(child);
            }
        }

        private static void ApplyControlFont(Control ctrl)
        {
            try
            {
                // Preserve current style and size, switch font family to Cairo
                FontStyle style = ctrl.Font != null ? ctrl.Font.Style : FontStyle.Regular;
                float size = ctrl.Font != null ? ctrl.Font.Size : 9.5f;
                ctrl.Font = new Font(CairoFamily, size, style, GraphicsUnit.Point);

                // For Label, Button, CheckBox, RadioButton: ensure UseCompatibleTextRendering is false for GDI shaping
                if (ctrl is Button btn)
                {
                    btn.UseCompatibleTextRendering = false;
                }
                else if (ctrl is Label lbl)
                {
                    lbl.UseCompatibleTextRendering = false;
                }
                else if (ctrl is CheckBox chk)
                {
                    chk.UseCompatibleTextRendering = false;
                }
                else if (ctrl is RadioButton rdo)
                {
                    rdo.UseCompatibleTextRendering = false;
                }
                else if (ctrl is DataGridView dgv)
                {
                    dgv.EnableHeadersVisualStyles = false;
                    dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                    if (dgv.ColumnHeadersHeight < 46)
                    {
                        dgv.ColumnHeadersHeight = 48;
                    }
                    if (dgv.RowTemplate.Height < 38)
                    {
                        dgv.RowTemplate.Height = 40;
                    }

                    dgv.ColumnHeadersDefaultCellStyle.Font = GetBold(9.5f);
                    dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(6, 6, 6, 6);
                    dgv.DefaultCellStyle.Font = GetRegular(9.5f);
                    dgv.DefaultCellStyle.Padding = new Padding(6, 4, 6, 4);
                }
            }
            catch
            {
                // Fallback gracefully on any individual control failure
            }
        }
    }
}
