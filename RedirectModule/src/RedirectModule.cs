// Author: Fabrice Marguerie
// http://weblogs.asp.net/fmarguerie/
// fabrice@madgeek.com
//
// Free for use
//
// Based on code from Fritz Onion: http://pluralsight.com/blogs/fritz/archive/2004/07/21/1651.aspx
// Adapted by: Nathan Taylor: http://blog.nathan-taylor.net/2010/06/aspnet-redirect-module.html
// ******************************************************************************************************
using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Collections.Generic;
using RedirectModule.Configuration;

namespace RedirectModule
{
    /// <summary>
    /// The purpose of this module is to redirect requests 
    /// based on web.config data in the &lt;redirections&gt; section.
    /// </summary>
    public class RedirectModule : IHttpModule
    {
        protected const string VirtualUrlContextItemKey = "VirtualUrl";

        private readonly string _applicationPath;
        private List<RedirectionElement> _redirections;

        public RedirectModule() {
            if (HttpRuntime.AppDomainAppVirtualPath != null) {
                _applicationPath = (HttpRuntime.AppDomainAppVirtualPath.Length > 1)
                                       ? HttpRuntime.AppDomainAppVirtualPath
                                       : String.Empty;
            }
        }

        public void Init(HttpApplication context) {
            _redirections = new List<RedirectionElement>();

            if (!RedirectionsSection.HasRedirects)
                return;

            foreach (RedirectionElement redirection in RedirectionsSection.Settings.Redirections) {
                string targetUrl = HandleTilde(redirection.TargetUrl);

                redirection.Regex = redirection.IgnoreCase
                                        ? new Regex(targetUrl, RegexOptions.IgnoreCase /* | RegexOptions.Compiled*/)
                                        : new Regex(targetUrl/*, RegexOptions.Compiled*/);

                redirection.IsAbsoluteUrl = !redirection.TargetUrl.StartsWith("~/") && !redirection.TargetUrl.StartsWith("^~/");

                _redirections.Add(redirection);
            }

            context.BeginRequest += OnBeginRequest;
        }

        public void Dispose() { }

        private void OnBeginRequest(object sender, EventArgs e) {
            HttpApplication app = sender as HttpApplication;
            if (app == null || _redirections == null || _redirections.Count < 1)
                return;

            foreach (RedirectionElement redirection in _redirections) {
                string requestUrl = redirection.IsAbsoluteUrl
                                        ? app.Request.Url.ToString()
                                        : app.Request.RawUrl;

                if (redirection.Regex.IsMatch(requestUrl)) {
                    string destinationUrl = redirection.Regex.Replace(requestUrl, redirection.DestinationUrl, 1);

                    if (!redirection.IsAbsoluteUrl)
                        destinationUrl = HandleTilde(destinationUrl);

                    // if the redirect is to an absolute URL it cannot be served with RewritePath(), making the Permanent option irrelevant
                    if (redirection.Permanent || redirection.IsAbsoluteUrl) {
                        app.Response.StatusCode = 301;
                        app.Response.Status = "301 Moved Permanently";
                        app.Response.RedirectLocation = destinationUrl;
                        app.Response.End();
                    }
                    else {
                        app.Context.RewritePath(destinationUrl);

                        // Keep track of the virtual URL because we'll need it to fix postbacks
                        // See http://weblogs.asp.net/jezell/archive/2004/03/15/90045.aspx
                        app.Context.Items[VirtualUrlContextItemKey] = requestUrl;
                    }

                    break;
                }
            }
        }

        private string HandleTilde(string url) {
            if (string.IsNullOrEmpty(url))
                return url;

            if (url.StartsWith("^~/"))
                return "^" + _applicationPath + url.Substring("^~/".Length - 1);
            else if (url.StartsWith("~/"))
                return _applicationPath + url.Substring("~/".Length - 1);

            return url;
        }
    }
}