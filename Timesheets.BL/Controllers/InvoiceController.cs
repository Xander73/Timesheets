using Core.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Timesheets.DB.DAL.Implementation;
using Timesheets.DB.DAL.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Timesheets.BL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        IInvoiceRepo _invoiceRepo;


        public InvoiceController(IInvoiceRepo invoiceRepo)
        {
            _invoiceRepo = invoiceRepo;
        }


        [HttpGet]
        public IEnumerable<Invoice> Get(CancellationToken token)
        {
            return  _invoiceRepo.GetAll(token).Result;
        }


        [HttpGet("{id}")]
        public Invoice Get(Guid id, CancellationToken token)
        {
            return _invoiceRepo.Get(id, token).Result;
        }


        [HttpPost]
        public void Post([FromBody] Invoice item, CancellationToken token)
        {
            try
            {
                _invoiceRepo.AddItem(item, token);
            }
            catch (Exception ex)
            {

                string s = ex.Message;
            }
            
        }

        [HttpPost("add/sheet/{invoiceId}")]
        public void PostAddSheet([FromRoute] Guid invoiceId, [FromBody] Invoice item, CancellationToken token)
        {
            _invoiceRepo.AddItem(item, token);
        }


        [HttpDelete("{id}")]
        public void Delete(Guid id, CancellationToken token)
        {
            _invoiceRepo.DeleteItem(id, token);
        }
    }
}
