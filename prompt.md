The current layout overhaul still has critical visual and rendering defects across the WinForms application:
1. Severe Arabic font rendering glitches where Arabic letters are rendered disconnected and spaced out (e.g., "د ل ي ل  ا ل م و ظ ف ي ن", "ف ا ت و ر ة", "ق ط ع ة").
2. Vertical text clipping inside DataGridView column headers (headers like "عدد الأصناف", "سعر الشراء", "الكمية الحالية" have their bottom half cut off).
3. DataGridViews do not stretch vertically or horizontally to occupy the available screen height, leaving massive empty white space below the rows.
4. Column FillWeights are improperly balanced (Barcode columns are too wide, while Product Name columns are squeezed).

You MUST first present a detailed, step-by-step Architectural & Layout Refactoring Plan before generating the updated code.

---

### Phase 1: Architectural Plan Requirements
Analyze each form and explicitly specify:
- Custom Embedded Font Pipeline: How you will load and integrate the embedded fonts from the `fonts/` directory (specifically `Cairo-Regular.ttf`, `Cairo-Bold.ttf`, `Cairo-SemiBold.ttf`, `Cairo-Medium.ttf`) via a centralized `FontManager` class utilizing `PrivateFontCollection`.
- Typography & RTL Text Fixes: How you will eliminate the disconnected Arabic letter bug (cleaning any artificial character spacing or tab formatting, setting `UseCompatibleTextRendering = false` across all controls, and dynamically applying the Cairo font family).
- DataGridView Vertical & Horizontal Stretching: Configuration steps to ensure grids fill 100% of available height and width (e.g., setting `Dock = Fill` inside parent panels/TableLayoutPanels, setting `RowTemplate.Height = 40`, `ColumnHeadersHeight = 48`, and setting `EnableHeadersVisualStyles = false`).
- Column AutoSizeMode & FillWeight Distribution: Exact weighting tables for each DataGridView so columns fit their actual data without truncation or dead space.
- Container Hierarchy: Structural blueprint using `TableLayoutPanel` and `SplitContainer` to eliminate all dead white space.

---

### Phase 2: Implementation Specifications

1. Font Integration (`FontManager.cs` & `Program.cs`):
- Implement `FontManager.cs` to load the embedded resources from `POS.fonts.Cairo-*.ttf`.
- Provide helper methods: `GetRegular(float size)`, `GetBold(float size)`, `GetSemiBold(float size)`, and a recursive `ApplyCairoFont(Control parent)` method.
- Ensure `Program.cs` initializes `FontManager` before loading any form.

2. Dashboard Form (`DashboardForm.cs` & `DashboardForm.Designer.cs`):
- KPI Cards: Ensure KPI titles and numbers use clean connected Arabic text and Cairo font (`Cairo-Bold` for numbers, `Cairo-Regular` for labels).
- Grid Column Headers: Set `ColumnHeadersHeight = 48` with cell vertical padding to prevent diacritic/text clipping.
- Grid Layout: Wrap the 3 dashboard grids in a `TableLayoutPanel` that fills the remaining vertical height (`Dock = Fill`). In `dgvLowStock`, assign 35% FillWeight to Product Name, and fixed widths to Barcode and Numeric values.

3. Purchases Form (`PurchasesForm.cs` & `PurchasesForm.Designer.cs`):
- Previous Invoices Tab: Set the `SplitContainer` to `Dock = Fill` so both `dgvPurchasesHistory` and `dgvPurchaseHistoryDetails` expand completely to the bottom of the viewport.
- Set `AutoSizeColumnsMode = Fill` on both grids with balanced FillWeights.

4. Users Form (`UsersForm.cs` & `UsersForm.Designer.cs`):
- Header Title: Fix the corrupted "دليل الموظفين والمستخدمين" title label with clean Arabic text and `Cairo-Bold` (16pt).
- Grid Stretching: Set `dgvUsers.Dock = Fill` inside the main container to occupy 100% of the vertical and horizontal space below the action bar.
- Columns: Set FillWeights: "الاسم الكامل" (30%), "اسم المستخدم" (20%), "الدور / الصلاحية" (15%), "تاريخ الإنشاء" (15%), "آخر تسجيل دخول" (15%), and fixed width (60px) for "الحالة".

5. Products Form (`ProductsForm.cs` & `ProductsForm.Designer.cs`):
- Editor Panel: Ensure the right sidebar has a fixed width (340px) and `dgvProducts` takes the remaining width with `Dock = Fill`.
- Grid Headers: Prevent header truncation by setting `ColumnHeadersHeight = 46` and giving "اسم المنتج" the largest FillWeight.

Present your complete, step-by-step diagnostic plan first, followed by the fully updated, compiling C# source code and Designer.cs files for all affected components.