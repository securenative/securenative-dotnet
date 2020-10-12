using System.Collections.Generic;

namespace SecureNative.SDK.Models
{
    public class RequestEvent
    {
        private readonly string _rid;
        private readonly string _eventType;
        private readonly string _userId;
        private readonly UserTraits _userTraits;
        private readonly RequestContext _request;
        private readonly string _timestamp;
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