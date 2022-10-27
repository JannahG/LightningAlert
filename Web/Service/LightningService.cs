using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Entities;
using Microsoft.MapPoint;

namespace Web.Service
{
    public interface ILightningService
    {
        List<Alert> PrintAlerts(List<LightningStrike> lightningStrikes);
    }

    public class LightningService : ILightningService
    {
        public List<Alert> PrintAlerts(List<LightningStrike> lightningStrikes)
        {
            var assets = GetAssetPerLightningStrike(lightningStrikes);
            var alerts = new List<Alert>();

            foreach(var asset in assets)
            {
                alerts.Add(new Alert{
                    AlertMessage = "Lightning alert for " + asset.AssetOwner + ": " + asset.AssetName
                });
            }

            return alerts;
        }

        public List<Asset> GetAssetPerLightningStrike(List<LightningStrike> lightningStrikes)
        {
            var assets = GetAssetData<Asset>();

            var assetList = new List<Asset>();
            //compute the long, lat and validate
            foreach (var strike in lightningStrikes)
            {
                if (strike.FlashType != FlashType.Heartbeat)
                {
                    TileSystem.LatLongToPixelXY(strike.Latitude, strike.Longitude, 1, out int pixelX, out int pixelY);
                    TileSystem.PixelXYToTileXY(pixelX, pixelY, out int tileX, out int tileY);
                    string computedQuadKey = TileSystem.TileXYToQuadKey(tileX, tileY, 1);

                    //search for the quadkey in assets
                    var matchedAsset = assets
                        .Where(a => a.QuadKey == computedQuadKey)
                        .FirstOrDefault();
                    
                    if (matchedAsset != null)
                    {
                        assetList.Add(new Asset{
                            AssetName = matchedAsset.AssetName,
                            QuadKey = matchedAsset.QuadKey,
                            AssetOwner = matchedAsset.AssetOwner
                        });
                    }
                }
            }

            return assetList
                .DistinctBy(a => a.QuadKey)
                .ToList();
        }

        public static List<Asset> GetAssetData<Asset>()
        {
            string fileName = Path.Combine(
                AppContext.BaseDirectory,
                @"assets.json");

            var result = new List<Asset>();
            string json = File.ReadAllText(fileName);
            result = JsonSerializer.Deserialize<List<Asset>>(json);

            if (result == null)
            {
                throw new Exception("File cannot be read");
            }

            return result;
        }
    }
}