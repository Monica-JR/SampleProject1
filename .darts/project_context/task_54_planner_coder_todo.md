# Planner-Coder Todo — 54
**Requirement:** Implement the “View Products” functionality for each Supplier Company in My Supplier Companies.

1. Clicking View Products should display all products associated with that Supplier Company.
2. Use the existing product/database tables and relationships. Do not create duplicate tables or invent a new data model.
3. Add a “Create Product” button on the products page.
4. The create-product form should use the existing product schema, fields, validation, API/services, and UI patterns.
5. Ensure the new product is correctly associated with the selected Supplier Company.
6. After creating a product, refresh the list so it appears immediately.
7. Follow the existing tech stack, architecture, styling, and components.
8. Include appropriate loading, error, success, and authorization handling.
9. Do not modify unrelated functionality.

---

## Wiring Manifest

### Existing (preserve every line when modifying these files)
- PlasticLicensingApp/Data/ApplicationDbContext.cs: DbSet<Product>, DbSet<Supplier>, DbSet<Parameter>, DbSet<ProductParameter>
- PlasticLicensingApp/Controllers/ProductController.cs: [Authorize], List(int supplierId), Create(int supplierId), Create([FromBody] ProductSubmissionViewModel model)
- PlasticLicensingApp/Views/Supplier/Index.cshtml: Link to Product/List
- PlasticLicensingApp/Program.cs: Default routes, DB migrations, Seeding

### Planned (add exactly these in STEP 3 — decided now, not during coding)
- (None - functionality is already implemented)

---

## All Tasks

| ID | Task | Files | Status | Depends On |
|---|---|---|---|---|
| T-001 | Verify and Complete Requirements | PlasticLicensingApp/Controllers/ProductController.cs, PlasticLicensingApp/Views/Product/List.cshtml, PlasticLicensingApp/Views/Product/Create.cshtml, PlasticLicensingApp/Views/Supplier/Index.cshtml | pending | — |
