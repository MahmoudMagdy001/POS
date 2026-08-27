# POS Design System & UI Component Standards

This directory contains the unified **Design System** and reusable components for the POS desktop application.

---

## 📁 Architecture Overview

```
DesignSystem/
├── Tokens/
│   ├── UIColors.cs         # Color palette tokens (Primary, Semantic, Grays, Surface, Backgrounds)
│   ├── UITypography.cs     # Standard Cairo typography scales & font definitions
│   └── UISpacing.cs        # Spacing scales, corner radii, and control dimensions
├── Helpers/
│   └── GraphicsHelper.cs   # GDI+ rendering engine (anti-aliasing, rounded rectangles, borders)
└── Components/
    ├── AppButton.cs        # Standard button with variants (Primary, Secondary, Success, Danger, etc.)
    ├── AppCard.cs          # Rounded card container with custom borders and accent support
    ├── AppKpiCard.cs       # KPI/Metric dashboard cards with values, trends, and icons
    ├── AppTextBox.cs       # Rounded text input with placeholder & focus highlight
    ├── AppSearchBox.cs     # Search input with embedded icon & instant clear action
    ├── AppBadge.cs         # Status pill badge (Success, Danger, Warning, Info, Neutral)
    ├── AppGrid.cs          # Pre-styled DataGridView with Cairo font & double-buffering
    ├── AppHeader.cs        # Standardized page title header with actions slot
    └── AppModal.cs         # Base form dialog for modal popups with header & footer
```

---

## 🎨 Design Tokens

### Colors (`UIColors.cs`)
- **Primary**: `#0EA5E9` (Sky Blue) - Main action buttons, active navigation, focused outlines.
- **Success**: `#16A34A` (Emerald) - Paid invoices, active status, completion actions.
- **Danger**: `#DC2626` (Crimson) - Delete actions, overdue alerts, out-of-stock items.
- **Warning**: `#D97706` (Amber) - Pending payments, low stock warnings.
- **Info**: `#2563EB` (Royal Blue) - Informational chips and details.
- **AppBackground**: `#F8FAFC` - Default background across all forms.
- **Surface**: `#FFFFFF` - Cards, data tables, and modal backgrounds.

### Typography (`UITypography.cs`)
All typography is powered by the embedded **Cairo** Arabic font family:
- **PageTitle**: Cairo Bold 18pt
- **SectionHeader**: Cairo Bold 14pt
- **CardTitle**: Cairo Bold 11pt
- **Body**: Cairo Regular 9.5pt / **BodyBold**: Cairo Bold 9.5pt
- **Caption**: Cairo Regular 8.5pt
- **Button**: Cairo Bold 10pt
- **KpiNumberLarge**: Cairo Bold 22pt

### Spacing & Metrics (`UISpacing.cs`)
- **RadiusSmall**: 4px | **RadiusMedium**: 8px | **RadiusLarge**: 12px
- **ButtonHeightDefault**: 36px | **InputHeightDefault**: 36px
- **GridHeaderHeight**: 38px | **GridRowHeight**: 36px

---

## 🧩 Component Usage Examples

### 1. `AppButton`
```csharp
var btnSave = new AppButton
{
    Text = "حفظ البيانات",
    Variant = ButtonVariant.Success,
    BorderRadius = 8
};
```

### 2. `AppCard`
```csharp
var card = new AppCard
{
    BorderRadius = 8,
    BorderColor = UIColors.Border,
    AccentColor = UIColors.Primary
};
```

### 3. `AppKpiCard`
```csharp
var kpiSales = new AppKpiCard
{
    Title = "إجمالي المبيعات",
    Value = "12,450.00 ج.م",
    Subtitle = "+15% عن الأسبوع الماضي",
    AccentColor = UIColors.Success
};
```

### 4. `AppBadge`
```csharp
var badge = new AppBadge
{
    Text = "مدفوع بالكامل",
    Variant = BadgeVariant.Success,
    ShowDot = true
};
```

### 5. `AppSearchBox`
```csharp
var searchBox = new AppSearchBox
{
    Placeholder = "ابحث بالاسم أو الباركود..."
};
searchBox.SearchTextChanged += (s, e) => FilterData(searchBox.SearchText);
```

### 6. `AppGrid`
```csharp
var grid = new AppGrid();
// Pre-configured with Cairo fonts, alternating rows, clean headers, and RTL support!
```

### 7. `AppModal`
```csharp
public class CustomDialog : AppModal
{
    public CustomDialog()
    {
        ModalTitle = "إضافة عميل جديد";
        // Place inputs in Body, buttons in Footer
    }
}
```
