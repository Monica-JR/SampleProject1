# Planner-Coder Todo — 53
**Requirement:** Create an “Edit Profile” page for Supplier Companies from the existing “My Supplier Companies” page.

Before coding, inspect the existing codebase, database schema/models, API/services, UI components, routing, and validation patterns.

Requirements:
1. Add an Edit Profile action for each supplier company.
2. Load and pre-fill the company’s existing data.
3. Include all relevant editable fields from the actual database schema — do not invent or hardcode fields.
4. Use the existing tech stack, architecture, components, styling, and patterns.
5. Reuse existing APIs/services if available; otherwise implement the update using the existing project conventions.
6. Apply appropriate validation, null/optional handling, loading/error/success states, and authorization.
7. Persist all changes to the database.
8. After saving, update/refresh My Supplier Companies so the changes are reflected immediately.
9. Do not modify unrelated functionality.

---

## Wiring Manifest

### Existing (preserve every line when modifying these files)
- PlasticLicensingApp/Controllers/SupplierController.cs: Index, Register, GetProfileAddress actions.
- PlasticLicensingApp/Views/Supplier/Index.cshtml: List of companies.

### Planned (add exactly these in STEP 3 — decided now, not during coding)
- PlasticLicensingApp/Controllers/SupplierController.cs: Add Edit(int id) [HttpGet] and Edit(int id, SupplierEditViewModel model) [HttpPost] actions.
- PlasticLicensingApp/Views/Supplier/Index.cshtml: Update "Edit Profile" link to `asp-action="Edit" asp-route-id="@item.ID"`.

---

## All Tasks

| ID | Task | Files | Status | Depends On |
|---|---|---|---|---|
| T-001 | Backend & ViewModel — Create SupplierEditViewModel and implement Edit actions in SupplierController | PlasticLicensingApp/ViewModels/SupplierEditViewModel.cs, PlasticLicensingApp/Controllers/SupplierController.cs | pending | — |
| T-002 | Frontend UI — Create Edit view and update Index view | PlasticLicensingApp/Views/Supplier/Edit.cshtml, PlasticLicensingApp/Views/Supplier/Index.cshtml | pending | T-001 |
