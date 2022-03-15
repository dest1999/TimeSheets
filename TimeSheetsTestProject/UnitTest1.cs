using TimeSheets;
using DBLibrary;
using Xunit;
using Moq;

namespace TimeSheetsTestProject
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Mock<ISheetDBRepository> mock = new();

            mock.Setup(m => m.Create(new SheetDTO { User = 1, Employee = 2, Description = "someJob" }));
            //не понятно как этим дальше дальше пользоваться(
            
        }

        [Fact]
        public void TestingSheetApproveMethodSetToTrue()
        {
            var sheet = Sheet.Create(1, 2, "someJob" );
            
            sheet.Approve(300);

            Assert.True(sheet.IsApproved);
        }

        [Fact]
        public void TestingSheetReopenMethodSetToFalse()
        {
            var sheet = Sheet.Create(1, 2, "someJob");

            sheet.Approve(300);
            sheet.Reopen();

            Assert.False(sheet.IsApproved);
        }

    }
}