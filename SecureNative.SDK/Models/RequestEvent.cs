using System.Collections.Generic;
using Newtonsoft.Json;

namespace SecureNative.SDK.Models
{
    public class RequestEvent
    {
        [JsonProperty("rid", NullValueHandling=NullValueHandling.Ignore)]
        private readonly string _rid;
        [JsonProperty("eventType", NullValueHandling=NullValueHandling.Ignore)]
        private readonly string _eventType;
        [JsonProperty("userId", NullValueHandling=NullValueHandling.Ignore)]
        private readonly string _userId;
        [JsonProperty("userTraits", NullValueHandling=NullValueHandling.Ignore)]
        private readonly UserTraits _userTraits;
        [JsonProperty("request", NullValueHandling=NullValueHandling.Ignore)]
        private readonly RequestContext _request;
        [JsonProperty("timestamp", NullValueHandling=NullValueHandling.Ignore)]
        private readonly string _timestamp;
        [JsonProperty("properties", NullValueHandling=NullValueHandling.Ignore)]
        private readonly Dictionary<object, object> _properties;

        public RequestEvent(string rid, string eventType, string userId, UserTraits userTraits, RequestContext request, string timestamp, Dictionary<object, object> properties)
        {
            _rid = rid;
            _eventType = eventType;
            _userId = userId;
            _userTraits = userTraits;
            _request = request;
            _timestamp = timestamp;
            _properties = properties;
        }
    }
}