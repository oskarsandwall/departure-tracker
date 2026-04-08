# departure-tracker

I only made this solution for fun to check which buses I can take between work and home. The original solution consists of a index.html which contains the presentation of the information and a .NET backend for calling the actual APIs that have the bus departure/arrival information. In the same directory as the index.html file there is also a html file that is not dependent on a backend. But in order to use that you need to hardcode in your own API key which is not best practice if you intend to expose this file to other ppl since API keys are considered secrets and should be treated as such.

In order to get this solution to work you need to add a appsettings.json file in the SlBusProxy folder looking something like this and add the required IDs and API key. More on the keys and APIs needed further down on this page.

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
    "OriginSiteId": "ORIGIN_STATION_ID",
    "DestinationSiteId": "DESTINATION_STATION_ID"
  }
}
```

# Regarding the APIs used for this solution
The APIs are provided by https://trafiklab.se and according to their website they are provided with the following licence:
> [!NOTE]
> These APIs are licensed as CC-BY
> This allows you to do whatever you want as long as you include a little “data from Trafiklab.se” attribution on screens and website widgets.
This licence text was copied from https://www.trafiklab.se/api/our-apis/trafiklab-realtime-apis/timetables

# How to get an API key
1. Create an account at https://trafiklab.se
2. Create a project
3. In this project generate a key

# How to get a station id
Use the following query and substitute the *searchValue* for the station name and the *key* for the api key you created in your project
```
https://realtime-api.trafiklab.se/v1/stops/name/{searchValue}?key={YOUR_API_KEY}
```
This was copied from https://www.trafiklab.se/api/our-apis/trafiklab-realtime-apis/stop-lookup so check there in case the above api call doesn't work for you

# How to find departures from a given station
Check the documentation at https://www.trafiklab.se/api/our-apis/trafiklab-realtime-apis/timetables

# How to find out which bus/metro/tram pass a specific station
```
https://api.resrobot.se/v2.1/trip?format=json&originId={ORIGIN_STATION_ID}&destId={DESTINATION_STATION_ID}&passlist=true&showPassingPoints=true&accessId=API_KEY
```
