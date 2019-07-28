
# C# SDK for SecureNative


**[SecureNative](https://www.securenative.com/) is rethinking-security-as-a-service, disrupting the cyber security space and the way enterprises consume and implement security solutions.**


#SDK

This C# sdk is very light

# Quickstart

Install the `SecureNative.Sdk` nuget.
 
//Command line:
nuget install SecureNative.Sdk

//Packet Manager Console

 install-package SecureNative.Sdk

//Visual Studio

1. Go to Tools -> Package Manager -> Manage NuGet Packages for Solution...
2. Click the Browse tab and search for `SecureNative.Sdk`
3. Click the `SecureNative.Sdk` package in the search results, select version and what projects to apply it to on the right side, and click Install

## Initialize the SDK

Go to the settings page of your SecureNative account and find your **API KEY**

**Initialize using API KEY**

```csharp
var apiKey = "API_KEY";
var sn = new SecureNative.SDK.SecureNative(apiKey, new SecureNative.SDK.Models.SecureNativeOptions());
```

You can pass empty SecureNativeOptions object or you can set the following:

   api url - target url the events will be sent (https://api.securenative.com/collector/api/v1).
   interval - minimum interval between sending events (1000ms).
   max events - maximum events that will be sent (1000).
   timeout - (1500 ms).

 ```csharp
var apiKey = "API_KEY";
var sn = new SecureNative.SDK.SecureNative(apiKey, new SecureNative.SDK.Models.SecureNativeOptions(){
            ApiUrl = "https://other.domain.com",
           Interval = 1000,
           MaxEvents = 1000,
           Timeout = 1500,
           AutoSend = true
        });
```

You can build event merely from HttpContext:

```csharp
var ev = SecureNative.SDK.VerifyWebhook.BuildEventFromContxt(HttpContext.Current, null);
// you can either send null as ievent or half completed ievent object  
```


## Tracking events

Once the SDK has been initialized, tracking requests are sent through the SDK instance.
```csharp
var apiKey = "API_KEY";
var sn = new SecureNative.SDK.SecureNative(apiKey, new SecureNative.SDK.Models.SecureNativeOptions()
{
    ApiUrl = "https://api.securenative.com/collector/api/v1",
    Interval = 1000,
    MaxEvents = 1000,
    Timeout = 1500,
    AutoSend = true
});
sn.Track(new SecureNative.SDK.Models.EventOptions()
{
    EventType = EventTypes.LOG_IN.ToDescriptionString(),
    IP = "162.247.74.201",
    User = new SecureNative.SDK.Models.User()
    {
        Id = "1",
        Email = "1@example.com",
        Name = "example"
    }
});
```




## Verification events

**Example**

```csharp
var apiKey = "API_KEY";
var sn = new SecureNative.SDK.SecureNative(apiKey, new SecureNative.SDK.Models.SecureNativeOptions()
{
    ApiUrl = "https://api.securenative.com/collector/api/v1",
    Interval = 1000,
    MaxEvents = 1000,
    Timeout = 1500,
    AutoSend = true
});

var verified = sn.Verify(new SecureNative.SDK.Models.EventOptions()
{
    EventType = EventTypes.LOG_IN.ToDescriptionString(),
    IP = "162.247.74.201",
    User = new SecureNative.SDK.Models.User()
    {
        Id = "1",
        Email = "1@example.com",
        Name = "example"
    }
});
```

## Webhook entry filter

Apply our filter to verify the request is from us:
```csharp

var apiKey = "1234";
var sn = new SecureNative.SDK.SecureNative(apiKey, new SecureNative.SDK.Models.SecureNativeOptions());
bool isOk = SecureNative.SDK.VerifyWebhook.IsRequestFromSecureNative(HttpContext.Current, apiKey);
//if isOK is true, continue. Otherwise, consider ruturning http code 401. 





