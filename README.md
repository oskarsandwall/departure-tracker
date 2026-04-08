# departure-tracker

This solution will not work without a appsettings.json in the SlBusProxy folder looking something like this:

```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "SlSettings": {
    "ApiKey": "YOUR_API_KEY",
    "SiteId": "ORIGIN_BUSS_STATION_ID"
  }
}
```
