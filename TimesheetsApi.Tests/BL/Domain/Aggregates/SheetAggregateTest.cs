using Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheets.BL.Domain.Aggregates;
using Xunit;

namespace TimesheetsApi.Tests.BL.Domain.Aggregates
{
    public class SheetAggregateTest
    {
        [Fact]
        public void Create_2ChangedSheetReturned()
        {
            Guid invoiceId = Guid.NewGuid();
            
            var sheet1 = SheetAggregate.Create(1, invoiceId);
            var sheet2 = SheetAggregate.Create(1, invoiceId);

            Assert.NotEqual(sheet1.Id, sheet2.Id);
        }


        [Fact]
        public void Create_SheetTypeReturned()
        {
            Guid invoiceId = Guid.NewGuid();

            var sheet = SheetAggregate.Create(1, invoiceId);

            Assert.IsType<Sheet>(sheet);
        }


        [Fact]
        public void Approve_IsApprovedTrueReturned()
        {
            var sheet = SheetAggregate.Create(1, Guid.NewGuid());
            sheet.Approve();
            Assert.True(sheet.IsApproved);
        }


        [Fact]
        public void Approve_IsApprovedChangedTrueReturned()
        {
            var actual = SheetAggregate.Create(1, Guid.NewGuid());
            var expected = actual.IsApproved;
            actual.Approve();
            Assert.NotEqual(expected, actual.IsApproved);
        }
    }
}
