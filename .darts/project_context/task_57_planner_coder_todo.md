# Planner-Coder Todo — 57
**Requirement:** Feature: Create Product

Implement a Create Product workflow in the existing application.

## Relevant existing files/components/services discovered:
- `ProductController.cs`: Existing controller with basic `List` and `Create` (single-step).
- `Product.cs`, `ProductParameter.cs`, `Parameter.cs`, `Status.cs`: Existing models.
- `ApplicationDbContext.cs`: Existing DB context.
- `ProductSubmissionViewModel.cs`: Existing VM for product creation.
- `Views/Product/Create.cshtml`: Existing creation page (single-step).

## Implementation plan:
1. **T-001 Backend**: Update `ProductController` to handle the two-step process:
   - `Create` (POST): Create product with status "New" (retrieved from DB).
   - `SaveParameters` (POST): Update/Create product parameters and update product status to "Submitted".
   - `Edit` (GET): To resume a "New" product and go to parameter screen.
2. **T-002 Frontend UI**: Update `Views/Product/Create.cshtml` to use a two-screen wizard approach with KnockoutJS:
   - Screen 1: Product Details (Name). Save creates the product and moves to Screen 2.
   - Screen 2: Product Parameters. Load parameters from lookup, display inputs, save updates status to "Submitted".
   - Allow navigation and resuming (handled by `List` and `Edit`).

## Ambiguities:
- None. The requirement specifies "New" and "Submitted" statuses and two screens.

---

## Wiring Manifest

### Existing (preserve every line when modifying these files)
- PlasticLicensingApp/Data/ApplicationDbContext.cs: DbSet<Product>, DbSet<ProductParameter>, DbSet<Status>, DbSet<Parameter>
- PlasticLicensingApp/Controllers/ProductController.cs: [Authorize], [HttpGet] List, [HttpGet] Create, [HttpPost] Create

### Planned (add exactly these in STEP 3 — decided now, not during coding)
- PlasticLicensingApp/Controllers/ProductController.cs: add `[HttpPost] SaveParameters`, `[HttpGet] Edit`
- PlasticLicensingApp/Views/Product/Create.cshtml: update to multi-screen KnockoutJS implementation.

---

## All Tasks

| ID | Task | Files | Status | Depends On |
|---|---|---|---|---|
| T-001 | Backend — Update ProductController with two-step logic | PlasticLicensingApp/Controllers/ProductController.cs, PlasticLicensingApp/ViewModels/ProductSubmissionViewModel.cs | pending | — |
| T-002 | Frontend UI — Two-screen product creation wizard | PlasticLicensingApp/Views/Product/Create.cshtml, PlasticLicensingApp/Views/Product/List.cshtml | pending | T-001 |
