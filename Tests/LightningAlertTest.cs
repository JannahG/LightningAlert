using LightningAlert.Controllers;
using Xunit;

namespace Tests;

public class LightningAlertTest 
{
    LightningFixture fixture;
    LightningAlertController controller;

    public LightningAlertTest(
        LightningFixture fixture,
        LightningAlertController controller
    )
    {
        this.fixture = fixture;
        this.controller = controller;
    }

    [Fact]
    public void Create_LightningAlert_Success()
    {
        //Arrange
        var payload = fixture.CreateLightningStrikePayload();

        //Act 
        var alert = controller.GetLightningAlerts(payload);

        //Assert
        Assert.NotNull(alert);
    }
}