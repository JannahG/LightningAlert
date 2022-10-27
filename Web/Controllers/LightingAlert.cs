using Entities;
using Microsoft.AspNetCore.Mvc;
using Web.Service;

namespace LightningAlert.Controllers;

[ApiController]
[Route("[controller]")]
public class LightningAlertController : ControllerBase
{

    private readonly ILogger<LightningAlertController> _logger;
    private readonly ILightningService service;

    public LightningAlertController(
        ILogger<LightningAlertController> logger,
        ILightningService service
    )
    {
        this.service = service;
        _logger = logger;
    }

    [HttpPost("GetLightningAlerts")]
    public IEnumerable<Alert> GetLightningAlerts([FromBody] List<LightningStrike> lightningStrikes)
    {
        var alerts = service.PrintAlerts(lightningStrikes);
        return alerts;
    }
}
