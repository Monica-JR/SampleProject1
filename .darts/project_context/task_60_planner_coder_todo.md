# Planner-Coder Todo — 60
**Requirement:** Make the required changes without affecting any existing functionality or product-status behavior.

Requirement
1. Product table stores product details.
2. Parameters table stores the master list of plastic parameters.
3. ProductParameters stores parameter values for each product, with at minimum:
  - ProductId (FK)
  - ParameterId (FK to Parameters)
  - Value

Current Flow
1. Clicking Create Product opens the first page, which captures only the Product Name.
2. The second page should load the complete parameter list from Parameters as labels.
3. The user enters a value for each parameter.
4. Save these values into ProductParameters against the newly created product using the corresponding ParameterId.

Issue:
The second-page parameter loading and/or saving to ProductParameters is currently not working correctly.

Task:
Trace the existing Create Product flow end-to-end and fix the issue with the minimum necessary changes. 
Ensure that:
1. All active/relevant parameters are loaded from Parameters.
2. Each parameter value is correctly associated with the newly created ProductId.
3. ParameterId is correctly stored as the FK.
4. Values are persisted correctly in ProductParameters.
5. Existing create/edit functionality and validation continue to work.
6. Do not modify or reset any existing Product Status values or status-related logic.
7. Follow the project's existing architecture, patterns, naming conventions, and database practices.
8. Do not introduce unnecessary schema or architectural changes.

Before making changes, identify the root cause and verify the complete data flow from UI → API/service → database.

---

## Wiring Manifest

### Existing (preserve every line when modifying these files)
- PlasticLicensingApp/Data/ApplicationDbContext.cs: Roles, Users, Addresses, Suppliers, Statuses, Licenses, Products, Parameters, ProductParameters, Files, ProductResponses, StatusHistoryChanges, NotificationTemplates, Notifications, Logs registered.
- PlasticLicensingApp/Controllers/ProductController.cs: List, Create, Edit, SaveParameters, GetParameters actions present.
- PlasticLicensingApp/Views/Product/Create.cshtml: KnockoutJS ProductWizardViewModel bound.

### Planned (add exactly these in STEP 3 — decided now, not during coding)
- No new registrations required. Fixes will be applied to existing logic.

---

## All Tasks

| ID | Task | Files | Status | Depends On |
|---|---|---|---|---|
| T-001 | Backend Fixes | PlasticLicensingApp/Controllers/ProductController.cs | pending | — |
| T-002 | Frontend Fixes | PlasticLicensingApp/Views/Product/Create.cshtml | pending | T-001 |
