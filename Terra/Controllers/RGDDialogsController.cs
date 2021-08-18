using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Terra.Models.Entities;

namespace Terra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RGDDialogsController : ControllerBase
    {
        [HttpGet]
        public async Task<object> Get([FromQuery] Guid[] ids)
        {
            RGDialogsClients rgd = new RGDialogsClients();

            HashSet<Guid> _dialogIds = new HashSet<Guid>();

            foreach (var item in rgd.Init())
                _dialogIds.Add(item.IDRGDialog);

            int _count = 0;

            foreach (var item in _dialogIds)
            {
                foreach (var id in ids)
                {
                    if (rgd.Init().Where(x => x.IDRGDialog == item).Select(y => y.IDClient).Contains(id))
                    {
                        _count++;

                        if (_count == ids.Length)
                            return Ok(await Task.Run(() => item));

                        else continue;
                    }    
                    else break;
                }
            }

            return Ok(new Guid());
        }
    }
}
