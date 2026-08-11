using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlasticLicensingApp.Data.Migrations
{
    public partial class FixExistingSupplierLicenses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create Licenses for existing suppliers who don't have one
            migrationBuilder.Sql(@"
                INSERT INTO Licenses (SupplierID, StatusID, CreatedBy, CreatedDate, LastChangedDate)
                SELECT s.ID, 
                       (SELECT TOP 1 ID FROM Statuses WHERE StatusName = 'New' AND StatusType = 'License'), 
                       s.CreatedBy, 
                       GETDATE(), 
                       GETDATE()
                FROM Suppliers s
                WHERE NOT EXISTS (SELECT 1 FROM Licenses l WHERE l.SupplierID = s.ID)
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Cleanup of auto-generated licenses if needed
        }
    }
}
