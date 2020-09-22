using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using Newtonsoft.Json;
using SecureNative.SDK.Config;
using SecureNative.SDK.Http;
using SecureNative.SDK.Models;

namespace SecureNative.SDK
{
    public class EventManager : IEventManager
    {
        private readonly SecureNativeOptions Options;
        private readonly int[] Coefficients = new int[] { 1, 1, 2, 3, 5, 8, 13 };
        private int Attempt = 0;
        private Boolean SendEnabled = false;
        private readonly SecureNativeHTTPClient HttpClient;
        private List<RequestOptions> Events;
        private readonly Thread Thread;
        private readonly static NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();


        public EventManager(SecureNativeOptions options, HttpMessageHandler handler = null)
        {
            this.Options = options;
            this.HttpClient = new SecureNativeHTTPClient(this.Options, handler);
            this.Events = new List<RequestOptions>();
            this.Thread = new Thread(new ThreadStart(SendEvents));
            this.Thread.Start();
        }

        public void SendAsync(IEvent e, string url, bool retry)
        {
            if (this.Options.IsDisabled())
            {
                return;
            }

            string body = JsonConvert.SerializeObject(e);
            this.Events.Add(new RequestOptions(url, body, retry));
        }

        public HttpResponse SendSync(IEvent e, string url)
        {
            if (this.Options.IsDisabled())
            {
                Logger.Warn("SDK is disabled, no operation will be performed");
                return null;
            }

            string body = JsonConvert.SerializeObject(e);
            Logger.Debug("Attempting to send event", body);
            HttpResponse response = this.HttpClient.Post(url, body);
            if (!response.IsOk())
            {
                Logger.Info(String.Format("SecureNative http call failed to end point: %s  with event type %s. adding back to queue.", url, e.GetEventType()));
                throw new IOException(response.GetStatusCode().ToString());
            }

            return response;
        }

        public void StartEventsPersist()
        {
            Logger.Debug("Starting automatic event persistence");
            if (!this.Options.IsAutoSend() || this.SendEnabled)
            {
                Logger.Debug("Automatic event persistence disabled, you should manually persist events");
                return;
            }

            this.SendEnabled = true;
        }

        public void StopEventsPersist()
        {
            if (this.SendEnabled)
            {
                Logger.Debug("Attempting to stop automatic event persistence");
            }

            try
            {
                this.Flush();
                if (this.Thread != null)
                {
                    this.Thread.Abort();
                }

            }
            catch (Exception e)
            {
                Logger.Error(String.Format("Could not stop event scheduler; %s", e));
            }
            Logger.Debug("Stopped event persistence");
        }

        private void Flush()
        {
            foreach (RequestOptions item in this.Events)
            {
                this.HttpClient.Post(item.GetUrl(), item.GetBody());
            }
        }

        private void SendEvents()
        {
            while (true)
            {
                if (this.Events.Count > 0 && this.SendEnabled)
                {
                    foreach (RequestOptions item in this.Events)
                    {
                        try
                        {
                            HttpResponse res = this.HttpClient.Post(item.GetUrl(), item.GetBody());
                            if (res.GetStatusCode() == 401)
                            {
                                item.SetRetry(false);
                            }
                            else if (res.GetStatusCode() != 200)
                            {
                                item.SetRetry(true);
                            }

                            this.Events.Remove(item);
                            Logger.Debug(String.Format("Event successfully sent; %s", item.GetBody()));
                        }
                        catch (Exception e)
                        {
                            Logger.Error(String.Format("Failed to send event; %s", e));
                            if (item.GetRetry())
                            {
                                if (this.Coefficients.Length == this.Attempt + 1)
                                {
                                    this.Attempt = 0;
                                }

                                var backOff = this.Coefficients[this.Attempt] * this.Options.GetInterval();
                                Logger.Debug(String.Format("Automatic back-off of %s", backOff));
                                this.SendEnabled = false;
                                Thread.Sleep(backOff);
                                this.SendEnabled = true;
                            }
                        }

                    }
                }
                Thread.Sleep(this.Options.GetInterval() / 1000);
            }
        }
    }
}