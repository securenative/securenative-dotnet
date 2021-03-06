<p align="center">
  <a href="https://www.securenative.com"><img src="https://user-images.githubusercontent.com/45174009/77826512-f023ed80-7120-11ea-80e0-58aacde0a84e.png" alt="SecureNative Logo"/></a>
</p>

<p align="center">
  <b>A Cloud-Native Security Monitoring and Protection for Modern Applications</b>
</p>
<p align="center">
  <a href="https://github.com/securenative/securenative-node">
    <img alt="Github Actions" src="https://github.com/securenative/securenative-java/workflows/CI/badge.svg">
  </a>
  <a href="https://codecov.io/gh/securenative/securenative-dotnet">
    <img src="https://codecov.io/gh/securenative/securenative-dotnet/branch/master/graph/badge.svg" />
  </a>
  <a href="https://www.nuget.org/packages/SecureNative.SDK/">
    <img src="https://img.shields.io/nuget/v/securenative.sdk.svg" alt="npm version" height="20">
  </a>
</p>
<p align="center">
  <a href="https://docs.securenative.com">Documentation</a> |
  <a href="https://docs.securenative.com/quick-start">Quick Start</a> |
  <a href="https://blog.securenative.com">Blog</a> |
  <a href="">Chat with us on Slack!</a>
</p>
<hr/>


[SecureNative](https://www.securenative.com/) performs user monitoring by analyzing user interactions with your application and various factors such as network, devices, locations and access patterns to stop and prevent account takeover attacks.

## Install the SDK

When using command line run the following:
```shell script
$ nuget install SecureNative.Sdk
```

When using packet manager console run the following:
```shell script
$ install-package SecureNative.Sdk
```

When using Visual Studio do the following:
* Go to Tools -> Package Manager -> Manage NuGet Packages for Solution...
* Click the Browse tab and search for `SecureNative.Sdk`
* Click `SecureNative.Sdk` package in the search results, select desired version and desired projects to apply to and click Install

## Initialize the SDK

To get your *API KEY*, login to your SecureNative account and go to project settings page:

### Option 1: Initialize via Config file
SecureNative can automatically load your config from *securenative.json* file or from the file that is specified in your *SECURENATIVE_CONFIG_FILE* env variable or in your project's root folder:

```dotenv
using SecureNative.SDK;


var securenative = Client.Init("path/to/securenative.json");
```
### Option 2: Initialize via API Key

```dotenv
using SecureNative.SDK;


var securenative = Client.Init("YOUR_API_KEY");
```

### Option 3: Initialize via ConfigurationBuilder
```dotenv
using SecureNative.SDK;


SecureNativeOptions Options = ConfigurationManager.ConfigBuilder()
                                .WithApiKey("API_KEY"))
                                .WithMaxEvents(10)
                                .WithLogLevel("error")
                                .Build());

var securenative = Client.Init(Options);
```

## Getting SecureNative instance
Once initialized, sdk will create a singleton instance which you can get: 
```dotenv
using SecureNative.SDK;


var securenative = Client.GetInstance();
```

## Tracking events

Once the SDK has been initialized, tracking requests sent through the SDK
instance. Make sure you build event with the EventBuilder:

 ```dotenv
using SecureNative.SDK;


//
// GET: /events/track
public void Track()
{
    var securenative = Client.GetInstance();

    var context = Client.ContextBuilder()
         .WithIp("127.0.0.1")
         .WithClientToken("SECURENATIVE_CLIENT_TOKEN")
         .WithHeaders(new Dictionary<string, string> { { "user-agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.121 Safari/537.36" } })
         .Build();

    var eventOptions = EventOptionsBuilder.Builder(EventTypes.LOG_IN)
        .WithUserId("1234")
        .WithUserTraits("Your Name", "name@gmail.com", "+1234567890")
        .WithContext(context)
        .WithProperties(new Dictionary<object, object> { { "prop1", "CUSTOM_PARAM_VALUE" }, { "prop2", true }, { "prop3", 3 } })
        .WithTimestamp(new DateTime())
        .Build();

    securenative.Track(eventOptions);
}
 ```

You can also create request context from HttpServletRequest:

```dotenv
using SecureNative.SDK;


//
// GET: /events/track
public void Track()
{
    var securenative = Client.GetInstance();
    var context = Client.FromHttpRequest(Request).Build();

    var eventOptions = EventOptionsBuilder.Builder(EventTypes.LOG_IN)
        .WithUserId("1234")
        .WithUserTraits("Your Name", "name@gmail.com", "+1234567890")
        .WithContext(context)
        .WithProperties(new Dictionary<object, object> { { "prop1", "CUSTOM_PARAM_VALUE" }, { "prop2", true }, { "prop3", 3 } })
        .WithTimestamp(new DateTime())
        .Build();

    securenative.Track(eventOptions);
}
```

## Verify events

**Example**

```dotenv
using SecureNative.SDK;


//
// GET: /events/verify
public void Verify()
{
    var securenative = Client.GetInstance();
    var context = Client.FromHttpRequest(Request).Build();

    var eventOptions = EventOptionsBuilder.Builder(EventTypes.LOG_IN)
        .WithUserId("1234")
        .WithUserTraits("Your Name", "name@gmail.com", "+1234567890")
        .WithContext(context)
        .WithProperties(new Dictionary<object, object> { { "prop1", "CUSTOM_PARAM_VALUE" }, { "prop2", true }, { "prop3", 3 } })
        .WithTimestamp(new DateTime())
        .Build();

    var verifyResult = securenative.Verify(eventOptions);
    verifyResult.GetRiskLevel(); // Low, Medium, High
    verifyResult.GetScore(); // Risk score: 0 -1 (0 - Very Low, 1 - Very High)
    verifyResult.GetTriggers(); // ["TOR", "New IP", "New City"]
}
```

## Webhook signature verification

Apply our filter to verify the request is from us, example in spring:

```dotenv
using SecureNative.SDK;


//
// GET: /webhook
public void WebhookEndpoint()
{
    var securenative = Client.GetInstance();
    
    // Checks if request is verified
    var isVerified = securenative.VerifyRequestPayload(Request);
}
 ```

## Extract proxy headers from cloud providers

You can specify custom header keys to allow extraction of client ip from different providers.
This example demonstrates the usage of proxy headers for ip extraction from Cloudflare.

### Option 1: Using config file
```json
{
    "SECURENATIVE_API_KEY": "YOUR_API_KEY",
    "SECURENATIVE_PROXY_HEADERS": ["CF-Connecting-IP"]
}
```

Initialize sdk as shown above.

### Options 2: Using ConfigurationBuilder

```dotenv
using SecureNative.SDK;


SecureNativeOptions Options = ConfigurationManager.ConfigBuilder()
                                .WithApiKey("API_KEY"))
                                .WithProxyHeaders(new ["CF-Connecting-IP"])
                                .Build());

var securenative = Client.Init(Options);
```

## Remove PII Data From Headers

By default, SecureNative SDK remove any known pii headers from the received request.
We also support using custom pii headers and regex matching via configuration, for example:

### Option 1: Using config file
```json
{
    "SECURENATIVE_API_KEY": "YOUR_API_KEY",
    "SECURENATIVE_PII_HEADERS": ["apiKey"]
}
```

Initialize sdk as shown above.

### Options 2: Using ConfigurationBuilder

```dotenv
using SecureNative.SDK;


SecureNativeOptions Options = ConfigurationManager.ConfigBuilder()
                                .WithApiKey("API_KEY"))
                                .WithPiiRegexPattern(@"((?i)(http_auth_)(\w+)?)")
                                .Build());

var securenative = Client.Init(Options);
```