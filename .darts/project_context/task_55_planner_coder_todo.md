# Planner-Coder Todo — 55
**Requirement:** Update the existing Supplier Company creation and License functionality.

When a new Supplier Company is created:
1. Automatically create a corresponding License record.
2. Set the License Status to “New” using the matching record from the existing Status table. Do not hardcode the Status ID.
3. Generate the License Number from the License ID using 4-digit zero padding:
1 → 0001
25 → 0025
123 → 0123
4. Associate the License with the newly created Supplier Company using the existing database relationship.
5. The License Number must not be displayed anywhere to the user unless the Company License Status is Active.
6. While the status is New (or any status other than Active), display the License Number as hidden/empty/not available.
7. Once the License Status becomes Active, display the generated License Number.
8. Apply this visibility rule consistently wherever the company/license information is displayed, including My Supplier Companies, Company Profile, License details, and related views.
9. Use the existing License, Status, and Supplier Company tables/models. Do not create new tables.
10. Keep the company + license creation transactional so a company cannot be created without its required license.
11. First inspect the existing schema, relationships, creation flow, and UI before making changes.
12. Follow the existing tech stack, architecture, components, styling, and coding patterns.
13. Do not modify unrelated functionality.

---

## Wiring Manifest

### Existing (preserve every line when modifying these files)
- PlasticLicensingApp\Data\ApplicationDbContext.cs: Licenses, Statuses, Suppliers DbSets
- PlasticLicensingApp\Controllers\SupplierController.cs: Register, Edit, Index actions
- PlasticLicensingApp\ViewModels\SupplierDashboardViewModel.cs: ID, CompanyName, City, State, CreatedDate
- PlasticLicensingApp\Views\Supplier\Index.cshtml: List of companies
- PlasticLicensingApp\Views\Supplier\Edit.cshtml: Edit company form

### Planned (add exactly these in STEP 3 — decided now, not during coding)
- PlasticLicensingApp\ViewModels\SupplierDashboardViewModel.cs: add `LicenseNumber`, `LicenseStatus`
- PlasticLicensingApp\ViewModels\SupplierEditViewModel.cs: add `LicenseNumber`, `LicenseStatus`

---

## All Tasks

| ID | Task | Files | Status | Depends On |
|---|---|---|---|---|
| T-001 | Backend — Update Supplier registration to create License and ViewModels for display | PlasticLicensingApp\Controllers\SupplierController.cs, PlasticLicensingApp\ViewModels\SupplierDashboardViewModel.cs, PlasticLicensingApp\ViewModels\SupplierEditViewModel.cs | pending | — |
| T-002 | Frontend UI — Update Supplier Index and Edit views to show/hide License Number | PlasticLicensingApp\Views\Supplier\Index.cshtml, PlasticLicensingApp\Views\Supplier\Edit.cshtml | pending | T-001 |
