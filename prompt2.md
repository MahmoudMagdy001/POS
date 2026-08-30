Act as a senior .NET software architect. Review my C# WinForms project (using SQL Server as the database) and reorganize its file/folder structure following clean architecture and separation of concerns best practices.

Requirements:
1. Analyze the current project structure and list all files with a short description of what each one does.
2. Propose a clean, scalable folder structure using this pattern (adapt as needed):
   - /Models        → Data models / entities
   - /DTOs          → Data transfer objects (if used)
   - /Data          → DbContext, connection strings, SQL Server access layer (ADO.NET/EF/Dapper)
   - /Repositories  → Data access logic (CRUD operations per entity)
   - /Services      → Business logic layer
   - /Forms         → WinForms UI (.cs/.Designer.cs/.resx grouped by feature, e.g. /Forms/Sales, /Forms/Inventory, /Forms/Admin)
   - /Helpers       → Utility/helper classes (e.g. ReceiptPrinter, validators)
   - /Resources     → Fonts, images, icons
   - /Reports       → Printing/reporting logic
   - /Constants     → App-wide constants and enums
3. For each existing file, tell me exactly which folder it should move to, and flag any file that mixes responsibilities (e.g. UI code containing direct SQL queries) so I can refactor it.
4. Suggest a consistent naming convention for classes, files, and namespaces matching the new structure.
5. Point out any SQL Server access code that should be moved into a repository/data layer instead of being scattered across forms.
6. Give me the final structure as a clean tree diagram.

Do not rewrite business logic — only reorganize structure and suggest what needs refactoring, with brief reasons.