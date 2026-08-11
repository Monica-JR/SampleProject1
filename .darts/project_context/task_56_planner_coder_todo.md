# Planner-Coder Todo — 56
**Requirement:** Update the existing License functionality with the following rules:
1. License Number and License Status must not be editable by users.
2. Do not show License Number or License Status as editable fields in any Supplier Company create/edit forms.
3. License Number should be system-generated from the License ID using 4-digit zero padding:
ID 1 → 0001
ID 25 → 0025
4. License Status should be system-managed and initially set to “New” using the existing Status table.
5. Users must not be able to change License Number or License Status through the UI or API.
6. The License Number should only be displayed when the License Status is Active. Otherwise, keep it hidden/not available.
7. Existing Supplier Companies without Licenses should be handled through the migration:
8. Create the License.
9. Set Status to New.
10. Generate the License Number from the generated License ID.
11. Avoid duplicate License records.
12. New Supplier Companies should automatically receive a License with Status New.
13. Use the existing SupplierCompany, License, and Status tables and relationships.
14. Enforce these rules at the backend/service layer as well as the frontend.
15. Follow the existing tech stack, architecture, validation, authorization, and migration patterns.

---

## Wiring Manifest

### Existing (preserve every line when modifying these files)
- PlasticLicensingApp/Data/ApplicationDbContext.cs: Roles, Users, Addresses, Suppliers, Statuses, Licenses, Products, Parameters registered
- PlasticLicensingApp/Program.cs: DbContext, Auth, Session, Seeding logic

### Planned (add exactly these in STEP 3 — decided now, not during coding)
- PlasticLicensingApp/Data/Migrations/20260722000000_FixExistingSupplierLicenses.cs: new migration to fix existing suppliers without licenses

---

## All Tasks

| ID | Task | Files | Status | Depends On |
|---|---|---|---|---|
| T-001 | Backend Logic & Models — Implement license generation, system management of status, and migration | PlasticLicensingApp/Data/ApplicationDbContext.cs, PlasticLicensingApp/Controllers/SupplierController.cs, PlasticLicensingApp/Data/Migrations/20260722000000_FixExistingSupplierLicenses.cs | pending | — |
| T-002 | Frontend UI — Ensure license fields are non-editable and conditionally displayed | PlasticLicensingApp/Views/Supplier/Edit.cshtml, PlasticLicensingApp/Views/Supplier/Register.cshtml | pending | T-001 |
