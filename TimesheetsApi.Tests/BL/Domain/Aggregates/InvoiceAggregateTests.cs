using Core.Models.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timesheets.BL.Domain.Aggregates;
using Timesheets.DB.DAL.Implementation;
using Timesheets.DB.DAL.Interfaces;
using Xunit;

namespace TimesheetsApi.Tests.BL.Domain.Aggregates
{
    public class InvoiceAggregateTests
    {
        [Fact]
        public void Create_CompareDate ()
        {
            var expected = new Invoice()
            {
                Date = DateTime.Now.Date
            };

            var actual = InvoiceAggregate.Create();
            Assert.Equal(expected.Date, actual.Date);
        }


        [Fact]
        public void Create_TypeInvoiseReturned()
        {
            var actual = InvoiceAggregate.Create();
            Assert.IsType<Invoice>(actual);
        }


        [Fact]
        public void AddSheet_AdedSheetReturned ()
        {
            Mock<IInvoiceRepo> mock = new Mock<IInvoiceRepo>(); 
            InvoiceAggregate invoice = new InvoiceAggregate(mock.Object);
            var sheet = new Sheet();
            invoice.AddSheet(sheet);
            Assert.Equal(sheet.Id, invoice.Sheets.First().Id);
        }


        [Fact]
        public void AddSheet_Null_EmptyReturned()
        {
            Mock<IInvoiceRepo> mock = new Mock<IInvoiceRepo>();
            InvoiceAggregate invoice = new InvoiceAggregate(mock.Object);
            var sheet = new Sheet();
            invoice.AddSheet(null);
            Assert.Empty(invoice.Sheets);
        }
    }
}
