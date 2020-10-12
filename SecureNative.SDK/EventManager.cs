using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using Newtonsoft.Json;
using NLog;
using SecureNative.SDK.Config;
using SecureNative.SDK.Http;
using SecureNative.SDK.Models;

namespace SecureNative.SDK
{
    public class EventManager : IEventManager
    {
        private readonly SecureNativeOptions _options;
        private readonly int[] _coefficients = {1, 1, 2, 3, 5, 8, 13};
        private int _attempt;
        private bool _sendEnabled;
        private readonly SecureNativeHttpClient _httpClient;
        private readonly Queue<RequestOptions> _events;
        private readonly Thread _thread;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();


        public EventManager(SecureNativeOptions options, HttpMessageHandler handler = null)
        {
            _options = options;
            _httpClient = new SecureNativeHttpClient(_options, handler);
            _events = new Queue<RequestOptions>();
            _thread = new Thread(SendEvents);
            _thread.Start();
        }
        
        private static string SerializeSdkEvent(IEvent e)
        {
            var sdk = (SdkEvent) e;
            return JsonConvert.SerializeObject(new RequestEvent(sdk.GetRid(), e.GetEventType(), sdk.GetUserId(),
                sdk.GetUserTraits(),
                sdk.GetRequest(), sdk.GetTimestamp(), sdk.GetProperties()));
        }

        public void SendAsync(IEvent e, string url, bool retry)
        {
            if (_options.IsDisabled())
            {
                return;
            }

            var body = SerializeSdkEvent(e);
            _events.Enqueue(new RequestOptions(url, body, retry));
        }

        public HttpResponse SendSync(IEvent e, string url)
        {
            if (_options.IsDisabled())
            {
                Logger.Warn("SDK is disabled, no operation will be performed");
                return null;
            }

            var body = SerializeSdkEvent(e);
            Logger.Debug("Attempting to send event", body);
            var response = _httpClient.Post(url, body);
            if (response.IsOk()) return response;
            Logger.Info(
                $"SecureNative http call failed to end point: {url}  with event type {e.GetEventType()}. adding back to queue.");
            throw new IOException(response.GetStatusCode().ToString());
        }

        public void StartEventsPersist()
        {
            Logger.Debug("Starting automatic event persistence");
            if (!_options.IsAutoSend() || _sendEnabled)
            {
                Logger.Debug("Automatic event persistence disabled, you should manually persist events");
                return;
            }

            _sendEnabled = true;
        }

        public void StopEventsPersist()
        {
            if (_sendEnabled)
            {
                Logger.Debug("Attempting to stop automatic event persistence");
            }

            try
            {
                Flush();
                _thread?.Abort();
            }
            catch (Exception e)
            {
                Logger.Error($"Could not stop event scheduler; {e}");
            }

            Logger.Debug("Stopped event persistence");
        }

        private void Flush()
        {
            for (var i = 0; i < _events.Count; i++)
            {
                var item = _events.Dequeue();
                _httpClient.Post(item.GetUrl(), item.GetBody());   
            }
        }

        private void SendEvents()
        {
            while (true)
            {
                if (_events.Count > 0 && _sendEnabled)
                {
                    var item = _events.Dequeue();
                    {
                        try
                        {
                            var res = _httpClient.Post(item.GetUrl(), item.GetBody());
                            if (res.GetStatusCode() == 401)
                            {
                                item.SetRetry(false);
                            }
                            else if (res.GetStatusCode() != 200)
                            {
                                item.SetRetry(true);
                                _events.Enqueue(item);
                            }
                            Logger.Debug($"Event successfully sent; {item.GetBody()}");
                        }
                        catch (Exception e)
                        {
                            Logger.Error($"Failed to send event; {e}");
                            if (!item.GetRetry()) continue;
                            if (_coefficients.Length == _attempt + 1)
                            {
                                _attempt = 0;
                            }

                            var backOff = _coefficients[_attempt] * _options.GetInterval();
                            Logger.Debug($"Automatic back-off of {backOff}");
                            _sendEnabled = false;
                            Thread.Sleep(backOff);
                            _sendEnabled = true;
                        }
                    }
                }
                Thread.Sleep(_options.GetInterval() / 1000);
            }
        }
    }
}