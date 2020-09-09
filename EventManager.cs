using System;
using System.Collections.Generic;
using System.IO;
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

        // TODO: implement me
        //private static readonly Logger logger = Logger.getLogger(SecureNativeEventManager.class);

        public EventManager(SecureNativeOptions options)
        {
            this.Options = options;
            this.HttpClient = new SecureNativeHTTPClient(this.Options);
            this.Events = new List<RequestOptions>();
            this.Thread = new Thread(new ThreadStart(SendEvents));
            this.Thread.Start();
        }

        public void SendAsync(IEvent e, string url, bool retry)
        {
            if (this.Options.GetDisable())
            {
                return;
            }

            string body = JsonConvert.SerializeObject(e);
            this.Events.Add(new RequestOptions(url, body, retry));
        }

        public HttpResponse SendSync(IEvent e, string url)
        {
            if (this.Options.GetDisable())
            {
                // TODO: implement me
                //this.logger.warn("SDK is disabled, no operation will be performed");
                return null;
            }

            string body = JsonConvert.SerializeObject(e);
            // TODO: implement me
            //this.logger.debug("Attempting to send event", body);
            HttpResponse response = this.HttpClient.Post(url, body);
            if (!response.IsOk())
            {
                // TODO: implement me
                //this.logger.info(String.format("SecureNative http call failed to end point: %s  with event type %s. adding back to queue.", url, event.getEventType()));
                throw new IOException(response.GetStatusCode().ToString());
            }

            return response;
        }

        public void StartEventsPersist()
        {
            // TODO: implement me
            //this.logger.debug("Starting automatic event persistence");
            if (!this.Options.GetAutoSend() || this.SendEnabled)
            {
                // TODO: implement me
                //this.logger.debug("Automatic event persistence disabled, you should manually persist events");
                return;
            }

            this.SendEnabled = true;
        }

        public void StopEventsPersist()
        {
            if (this.SendEnabled)
            {
                // TODO: implement me
                //this.Logger.debug("Attempting to stop automatic event persistence")
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
                // TODO: implement me
                //this.Logger.error("Could not stop event scheduler; {}".format(e))
            }

            // TODO: implement me
            //this.Logger.debug("Stopped event persistence");
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

                            // TODO: imeplement me
                            //this.Logger.debug("Event successfully sent; {}".format(item.body))
                        }
                        catch (Exception e)
                        {
                            // TODO: imeplement me
                            //this.Logger.error("Failed to send event; {}".format(e))
                            if (item.GetRetry())
                            {
                                if (this.Coefficients.Length == this.Attempt + 1)
                                {
                                    this.Attempt = 0;
                                }

                                var backOff = this.Coefficients[this.Attempt] * this.Options.GetInterval();
                                // TODO: imeplement me
                                //Logger.debug("Automatic back-off of {}".format(back_off))
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