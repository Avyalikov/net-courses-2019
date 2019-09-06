using Microsoft.AspNetCore.Mvc;
using stockSimulator.Core.DTO;
using stockSimulator.Core.Models;
using stockSimulator.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace stockSimulator.WevServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SharesController : ControllerBase
    {
        private readonly EditCleintStockService editCleintStockService;

        public SharesController(EditCleintStockService editCleintStockService)
        {
            this.editCleintStockService = editCleintStockService;
        }

        [HttpGet]
        [Route("")]
        // shares
        public ActionResult<IEnumerable<StockOfClientsEntity>> Get(int clientId)
        {
            try
            {
                var stocks = this.editCleintStockService.GetStocksOfClient(clientId);
                return Ok(stocks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [Route("add")]
        public ActionResult<string> AddNewStockToClient([FromBody]EditStockOfClientInfo addInfo)
        {
            try
            {
                string result = this.editCleintStockService.addStock(addInfo);
                return Ok(result);
            }
            catch (Exeption ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [Route("update")]
        public ActionResult<string> UpdateStockInfoOfClient([FromBody]EditStockOfClientInfo updateInfo)
        {
            try
            {
                string result = this.editCleintStockService.Edit(updateInfo);
                return Ok(result);
            }
            catch (Exeption ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [Route("remove")]
        public ActionResult<string> RemoveStockInfoOfClient([FromBody]EditStockOfClientInfo removeInfo)
        {
            try
            {
                string result = this.editCleintStockService.Remove(removeInfo);
                return Ok(result);
            }
            catch (Exeption ex)
            {
                return StatusCode(500, ex);
            }
        }

        [Serializable]
        private class Exeption : Exception
        {
            public Exeption()
            {
            }

            public Exeption(string message) : base(message)
            {
            }

            public Exeption(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected Exeption(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
    }
}
