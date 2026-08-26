The text and icons inside almost all buttons across the application are misaligned and pushed against the edges instead of being properly centered (e.g., Save Changes, Restore, + User, Edit, Delete, + Add to Invoice, Save Invoice, and the Exit button).

Please refactor the button styling/code across all screens to ensure perfect horizontal and vertical centering by applying the following:

1. Button Alignment Properties:
   - Set `TextAlign = ContentAlignment.MiddleCenter`
   - Set `ImageAlign = ContentAlignment.MiddleCenter`
   - Set `TextImageRelation = TextImageRelation.ImageBeforeText` (ensure the icon and text group together in the exact center of the button).

2. Padding & Layout:
   - Reset button internal padding to zero: `Padding = new Padding(0)`.
   - Ensure the `RightToLeft` layout does not cause unexpected margin offsets or clipping.

3. Custom Paint / Custom Controls (if applicable):
   - If buttons are custom-rendered via `OnPaint`, calculate the total combined width of (Icon + Spacing + Text) and center the entire block: `int startX = (Width - totalContentWidth) / 2;` along with vertical centering.

Please review and apply these fixes to all buttons across: General Settings, Users & Permissions, Sales Invoices, Purchase Invoices, and the Top Navigation Bar.