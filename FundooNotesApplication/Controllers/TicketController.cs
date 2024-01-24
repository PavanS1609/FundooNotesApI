using Business_Layer.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooNotesApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IBus _bus;
        private readonly IUser_BL iuser_BL;
        public TicketController(IBus bus, IUser_BL iuser_BL)
        {
            this._bus = bus;
            this.iuser_BL = iuser_BL;
        }
    }
}
