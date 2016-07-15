using Hyak.Common.Internals;
using Microsoft.WindowsAzure.Management.WebSites.Models;
using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Linq;

namespace API_APP
{
    public class AzureWebSiteClient
    {
        #region Constants

        private const string BASE_URI = "https://management.core.windows.net/";
        private const string VERSION_HEADER = "x-ms-version";
        private const string VERSION = "2014-04-01";
        private const string WEB_SPACES = "/services/WebSpaces";
        private const string WEB_SITES = "/sites";
        private const string SLASH = "/";

        #endregion

        #region Required Properties

        private string _subscriptionID;
        private X509Certificate2 _certificate;
        private WebProxy _webProxy;

        public string SubscriptionID
        {
            get { return _subscriptionID; }
        }

        public X509Certificate2 Certificate
        {
            get { return _certificate; }
        }

        public WebProxy WebProxy
        {
            get { return _webProxy; }
        }

        #endregion Required Properties

        #region Constructor Initializing Properties

        public AzureWebSiteClient(string subscriptionID, X509Certificate2 certificate, WebProxy webProxy)
        {
            _subscriptionID = subscriptionID;
            _certificate = certificate;
            _webProxy = webProxy;
        }

        #endregion Constructor Initializing Properties

        #region Public Methods

        public WebSpacesListResponse GetWebSpaces()
        {
            return WebSpaces();
        }

        public WebSpacesListWebSitesResponse GetWebSites(string webSpaceName)
        {
            if (webSpaceName == null)
                throw new ArgumentNullException("webSpaceName");

            return WebSites(webSpaceName);

        }

        #endregion

        #region Private Methods

        private WebSpacesListResponse WebSpaces()
        {
            WebSpacesListResponse result = null;

            var response = GetResponseString(WEB_SPACES, HttpMethod.Get.Method);
            if (response.Item1 && !string.IsNullOrEmpty(response.Item2))
            {
                result = new WebSpacesListResponse();
                XDocument responseDoc = XDocument.Parse(response.Item2);
                XElement webSpacesSequenceElement = responseDoc.Element(XName.Get("WebSpaces", "http://schemas.microsoft.com/windowsazure"));
                if (webSpacesSequenceElement != null)
                {
                    foreach (XElement webSpacesElement in webSpacesSequenceElement.Elements(XName.Get("WebSpace", "http://schemas.microsoft.com/windowsazure")))
                    {
                        WebSpacesListResponse.WebSpace webSpaceInstance = new WebSpacesListResponse.WebSpace();
                        result.WebSpaces.Add(webSpaceInstance);

                        XElement availabilityStateElement = webSpacesElement.Element(XName.Get("AvailabilityState", "http://schemas.microsoft.com/windowsazure"));
                        if (availabilityStateElement != null)
                        {
                            WebSpaceAvailabilityState availabilityStateInstance = ((WebSpaceAvailabilityState)Enum.Parse(typeof(WebSpaceAvailabilityState), availabilityStateElement.Value, true));
                            webSpaceInstance.AvailabilityState = availabilityStateInstance;
                        }

                        XElement currentNumberOfWorkersElement = webSpacesElement.Element(XName.Get("CurrentNumberOfWorkers", "http://schemas.microsoft.com/windowsazure"));
                        if (currentNumberOfWorkersElement != null && !string.IsNullOrEmpty(currentNumberOfWorkersElement.Value))
                        {
                            bool isNil = false;
                            XAttribute nilAttribute = currentNumberOfWorkersElement.Attribute(XName.Get("nil", "http://www.w3.org/2001/XMLSchema-instance"));
                            if (nilAttribute != null)
                            {
                                isNil = nilAttribute.Value == "true";
                            }
                            if (isNil == false)
                            {
                                int currentNumberOfWorkersInstance = int.Parse(currentNumberOfWorkersElement.Value, CultureInfo.InvariantCulture);
                                webSpaceInstance.CurrentNumberOfWorkers = currentNumberOfWorkersInstance;
                            }
                        }

                        XElement currentWorkerSizeElement = webSpacesElement.Element(XName.Get("CurrentWorkerSize", "http://schemas.microsoft.com/windowsazure"));
                        if (currentWorkerSizeElement != null && !string.IsNullOrEmpty(currentWorkerSizeElement.Value))
                        {
                            bool isNil2 = false;
                            XAttribute nilAttribute2 = currentWorkerSizeElement.Attribute(XName.Get("nil", "http://www.w3.org/2001/XMLSchema-instance"));
                            if (nilAttribute2 != null)
                            {
                                isNil2 = nilAttribute2.Value == "true";
                            }
                            if (isNil2 == false)
                            {
                                WebSpaceWorkerSize currentWorkerSizeInstance = ((WebSpaceWorkerSize)Enum.Parse(typeof(WebSpaceWorkerSize), currentWorkerSizeElement.Value, true));
                                webSpaceInstance.CurrentWorkerSize = currentWorkerSizeInstance;
                            }
                        }

                        XElement geoLocationElement = webSpacesElement.Element(XName.Get("GeoLocation", "http://schemas.microsoft.com/windowsazure"));
                        if (geoLocationElement != null)
                        {
                            string geoLocationInstance = geoLocationElement.Value;
                            webSpaceInstance.GeoLocation = geoLocationInstance;
                        }

                        XElement geoRegionElement = webSpacesElement.Element(XName.Get("GeoRegion", "http://schemas.microsoft.com/windowsazure"));
                        if (geoRegionElement != null)
                        {
                            string geoRegionInstance = geoRegionElement.Value;
                            webSpaceInstance.GeoRegion = geoRegionInstance;
                        }

                        XElement nameElement = webSpacesElement.Element(XName.Get("Name", "http://schemas.microsoft.com/windowsazure"));
                        if (nameElement != null)
                        {
                            string nameInstance = nameElement.Value;
                            webSpaceInstance.Name = nameInstance;
                        }

                        XElement planElement = webSpacesElement.Element(XName.Get("Plan", "http://schemas.microsoft.com/windowsazure"));
                        if (planElement != null)
                        {
                            string planInstance = planElement.Value;
                            webSpaceInstance.Plan = planInstance;
                        }

                        XElement statusElement = webSpacesElement.Element(XName.Get("Status", "http://schemas.microsoft.com/windowsazure"));
                        if (statusElement != null)
                        {
                            WebSpaceStatus statusInstance = ((WebSpaceStatus)Enum.Parse(typeof(WebSpaceStatus), statusElement.Value, true));
                            webSpaceInstance.Status = statusInstance;
                        }

                        XElement subscriptionElement = webSpacesElement.Element(XName.Get("Subscription", "http://schemas.microsoft.com/windowsazure"));
                        if (subscriptionElement != null)
                        {
                            string subscriptionInstance = subscriptionElement.Value;
                            webSpaceInstance.Subscription = subscriptionInstance;
                        }

                        XElement workerSizeElement = webSpacesElement.Element(XName.Get("WorkerSize", "http://schemas.microsoft.com/windowsazure"));
                        if (workerSizeElement != null && !string.IsNullOrEmpty(workerSizeElement.Value))
                        {
                            WebSpaceWorkerSize workerSizeInstance = ((WebSpaceWorkerSize)Enum.Parse(typeof(WebSpaceWorkerSize), workerSizeElement.Value, true));
                            webSpaceInstance.WorkerSize = workerSizeInstance;
                        }
                    }
                }
            }
            else
                throw new Exception(string.IsNullOrEmpty(response.Item2) ? "Error Occurred while processing your command" : response.Item2);

            return result;
        }

        private WebSpacesListWebSitesResponse WebSites(string webSpaceName)
        {
            WebSpacesListWebSitesResponse result = null;
            string uri = WEB_SPACES + SLASH + Uri.EscapeDataString(webSpaceName) + WEB_SITES;
            var response = GetResponseString(uri, HttpMethod.Get.Method);
            if (response.Item1 && !string.IsNullOrEmpty(response.Item2))
            {
                result = new WebSpacesListWebSitesResponse();
                XDocument responseDoc = XDocument.Parse(response.Item2);
                XElement sitesSequenceElement = responseDoc.Element(XName.Get("Sites", "http://schemas.microsoft.com/windowsazure"));
                if (sitesSequenceElement != null)
                {
                    foreach (XElement sitesElement in sitesSequenceElement.Elements(XName.Get("Site", "http://schemas.microsoft.com/windowsazure")))
                    {
                        WebSite siteInstance = new WebSite();
                        result.WebSites.Add(siteInstance);

                        XElement adminEnabledElement = sitesElement.Element(XName.Get("AdminEnabled", "http://schemas.microsoft.com/windowsazure"));
                        if (adminEnabledElement != null && !string.IsNullOrEmpty(adminEnabledElement.Value))
                        {
                            bool adminEnabledInstance = bool.Parse(adminEnabledElement.Value);
                            siteInstance.AdminEnabled = adminEnabledInstance;
                        }

                        XElement availabilityStateElement = sitesElement.Element(XName.Get("AvailabilityState", "http://schemas.microsoft.com/windowsazure"));
                        if (availabilityStateElement != null && !string.IsNullOrEmpty(availabilityStateElement.Value))
                        {
                            WebSpaceAvailabilityState availabilityStateInstance = ((WebSpaceAvailabilityState)Enum.Parse(typeof(WebSpaceAvailabilityState), availabilityStateElement.Value, true));
                            siteInstance.AvailabilityState = availabilityStateInstance;
                        }

                        XElement sKUElement = sitesElement.Element(XName.Get("SKU", "http://schemas.microsoft.com/windowsazure"));
                        if (sKUElement != null)
                        {
                            SkuOptions sKUInstance = ((SkuOptions)Enum.Parse(typeof(SkuOptions), sKUElement.Value, true));
                            siteInstance.Sku = sKUInstance;
                        }

                        XElement enabledElement = sitesElement.Element(XName.Get("Enabled", "http://schemas.microsoft.com/windowsazure"));
                        if (enabledElement != null && !string.IsNullOrEmpty(enabledElement.Value))
                        {
                            bool enabledInstance = bool.Parse(enabledElement.Value);
                            siteInstance.Enabled = enabledInstance;
                        }

                        XElement enabledHostNamesSequenceElement = sitesElement.Element(XName.Get("EnabledHostNames", "http://schemas.microsoft.com/windowsazure"));
                        if (enabledHostNamesSequenceElement != null)
                        {
                            foreach (XElement enabledHostNamesElement in enabledHostNamesSequenceElement.Elements(XName.Get("string", "http://schemas.microsoft.com/2003/10/Serialization/Arrays")))
                            {
                                siteInstance.EnabledHostNames.Add(enabledHostNamesElement.Value);
                            }
                        }

                        XElement hostNameSslStatesSequenceElement = sitesElement.Element(XName.Get("HostNameSslStates", "http://schemas.microsoft.com/windowsazure"));
                        if (hostNameSslStatesSequenceElement != null)
                        {
                            foreach (XElement hostNameSslStatesElement in hostNameSslStatesSequenceElement.Elements(XName.Get("HostNameSslState", "http://schemas.microsoft.com/windowsazure")))
                            {
                                WebSite.WebSiteHostNameSslState hostNameSslStateInstance = new WebSite.WebSiteHostNameSslState();
                                siteInstance.HostNameSslStates.Add(hostNameSslStateInstance);

                                XElement nameElement = hostNameSslStatesElement.Element(XName.Get("Name", "http://schemas.microsoft.com/windowsazure"));
                                if (nameElement != null)
                                {
                                    string nameInstance = nameElement.Value;
                                    hostNameSslStateInstance.Name = nameInstance;
                                }

                                XElement sslStateElement = hostNameSslStatesElement.Element(XName.Get("SslState", "http://schemas.microsoft.com/windowsazure"));
                                if (sslStateElement != null && !string.IsNullOrEmpty(sslStateElement.Value))
                                {
                                    WebSiteSslState sslStateInstance = ((WebSiteSslState)Enum.Parse(typeof(WebSiteSslState), sslStateElement.Value, true));
                                    hostNameSslStateInstance.SslState = sslStateInstance;
                                }

                                XElement thumbprintElement = hostNameSslStatesElement.Element(XName.Get("Thumbprint", "http://schemas.microsoft.com/windowsazure"));
                                if (thumbprintElement != null)
                                {
                                    bool isNil = false;
                                    XAttribute nilAttribute = thumbprintElement.Attribute(XName.Get("nil", "http://www.w3.org/2001/XMLSchema-instance"));
                                    if (nilAttribute != null)
                                    {
                                        isNil = nilAttribute.Value == "true";
                                    }
                                    if (isNil == false)
                                    {
                                        string thumbprintInstance = thumbprintElement.Value;
                                        hostNameSslStateInstance.Thumbprint = thumbprintInstance;
                                    }
                                }

                                XElement virtualIPElement = hostNameSslStatesElement.Element(XName.Get("VirtualIP", "http://schemas.microsoft.com/windowsazure"));
                                if (virtualIPElement != null)
                                {
                                    bool isNil2 = false;
                                    XAttribute nilAttribute2 = virtualIPElement.Attribute(XName.Get("nil", "http://www.w3.org/2001/XMLSchema-instance"));
                                    if (nilAttribute2 != null)
                                    {
                                        isNil2 = nilAttribute2.Value == "true";
                                    }
                                    if (isNil2 == false)
                                    {
                                        string virtualIPInstance = virtualIPElement.Value;
                                        hostNameSslStateInstance.VirtualIP = virtualIPInstance;
                                    }
                                }
                            }
                        }

                        XElement hostNamesSequenceElement = sitesElement.Element(XName.Get("HostNames", "http://schemas.microsoft.com/windowsazure"));
                        if (hostNamesSequenceElement != null)
                        {
                            foreach (XElement hostNamesElement in hostNamesSequenceElement.Elements(XName.Get("string", "http://schemas.microsoft.com/2003/10/Serialization/Arrays")))
                            {
                                siteInstance.HostNames.Add(hostNamesElement.Value);
                            }
                        }

                        XElement lastModifiedTimeUtcElement = sitesElement.Element(XName.Get("LastModifiedTimeUtc", "http://schemas.microsoft.com/windowsazure"));
                        if (lastModifiedTimeUtcElement != null && !string.IsNullOrEmpty(lastModifiedTimeUtcElement.Value))
                        {
                            DateTime lastModifiedTimeUtcInstance = DateTime.Parse(lastModifiedTimeUtcElement.Value, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal).ToLocalTime();
                            siteInstance.LastModifiedTimeUtc = lastModifiedTimeUtcInstance;
                        }

                        XElement nameElement2 = sitesElement.Element(XName.Get("Name", "http://schemas.microsoft.com/windowsazure"));
                        if (nameElement2 != null)
                        {
                            string nameInstance2 = nameElement2.Value;
                            siteInstance.Name = nameInstance2;
                        }

                        XElement repositorySiteNameElement = sitesElement.Element(XName.Get("RepositorySiteName", "http://schemas.microsoft.com/windowsazure"));
                        if (repositorySiteNameElement != null)
                        {
                            string repositorySiteNameInstance = repositorySiteNameElement.Value;
                            siteInstance.RepositorySiteName = repositorySiteNameInstance;
                        }

                        XElement runtimeAvailabilityStateElement = sitesElement.Element(XName.Get("RuntimeAvailabilityState", "http://schemas.microsoft.com/windowsazure"));
                        if (runtimeAvailabilityStateElement != null && !string.IsNullOrEmpty(runtimeAvailabilityStateElement.Value))
                        {
                            WebSiteRuntimeAvailabilityState runtimeAvailabilityStateInstance = ((WebSiteRuntimeAvailabilityState)Enum.Parse(typeof(WebSiteRuntimeAvailabilityState), runtimeAvailabilityStateElement.Value, true));
                            siteInstance.RuntimeAvailabilityState = runtimeAvailabilityStateInstance;
                        }

                        XElement selfLinkElement = sitesElement.Element(XName.Get("SelfLink", "http://schemas.microsoft.com/windowsazure"));
                        if (selfLinkElement != null)
                        {
                            Uri selfLinkInstance = TypeConversion.TryParseUri(selfLinkElement.Value);
                            siteInstance.Uri = selfLinkInstance;
                        }

                        XElement serverFarmElement = sitesElement.Element(XName.Get("ServerFarm", "http://schemas.microsoft.com/windowsazure"));
                        if (serverFarmElement != null)
                        {
                            string serverFarmInstance = serverFarmElement.Value;
                            siteInstance.ServerFarm = serverFarmInstance;
                        }

                        XElement sitePropertiesElement = sitesElement.Element(XName.Get("SiteProperties", "http://schemas.microsoft.com/windowsazure"));
                        if (sitePropertiesElement != null)
                        {
                            WebSite.WebSiteProperties sitePropertiesInstance = new WebSite.WebSiteProperties();
                            siteInstance.SiteProperties = sitePropertiesInstance;

                            XElement appSettingsSequenceElement = sitePropertiesElement.Element(XName.Get("AppSettings", "http://schemas.microsoft.com/windowsazure"));
                            if (appSettingsSequenceElement != null)
                            {
                                foreach (XElement appSettingsElement in appSettingsSequenceElement.Elements(XName.Get("NameValuePair", "http://schemas.microsoft.com/windowsazure")))
                                {
                                    string appSettingsKey = appSettingsElement.Element(XName.Get("Name", "http://schemas.microsoft.com/windowsazure")).Value;
                                    string appSettingsValue = appSettingsElement.Element(XName.Get("Value", "http://schemas.microsoft.com/windowsazure")).Value;
                                    sitePropertiesInstance.AppSettings.Add(appSettingsKey, appSettingsValue);
                                }
                            }

                            XElement metadataSequenceElement = sitePropertiesElement.Element(XName.Get("Metadata", "http://schemas.microsoft.com/windowsazure"));
                            if (metadataSequenceElement != null)
                            {
                                foreach (XElement metadataElement in metadataSequenceElement.Elements(XName.Get("NameValuePair", "http://schemas.microsoft.com/windowsazure")))
                                {
                                    string metadataKey = metadataElement.Element(XName.Get("Name", "http://schemas.microsoft.com/windowsazure")).Value;
                                    string metadataValue = metadataElement.Element(XName.Get("Value", "http://schemas.microsoft.com/windowsazure")).Value;
                                    sitePropertiesInstance.Metadata.Add(metadataKey, metadataValue);
                                }
                            }

                            XElement propertiesSequenceElement = sitePropertiesElement.Element(XName.Get("Properties", "http://schemas.microsoft.com/windowsazure"));
                            if (propertiesSequenceElement != null)
                            {
                                foreach (XElement propertiesElement in propertiesSequenceElement.Elements(XName.Get("NameValuePair", "http://schemas.microsoft.com/windowsazure")))
                                {
                                    string propertiesKey = propertiesElement.Element(XName.Get("Name", "http://schemas.microsoft.com/windowsazure")).Value;
                                    string propertiesValue = propertiesElement.Element(XName.Get("Value", "http://schemas.microsoft.com/windowsazure")).Value;
                                    sitePropertiesInstance.Properties.Add(propertiesKey, propertiesValue);
                                }
                            }
                        }

                        XElement stateElement = sitesElement.Element(XName.Get("State", "http://schemas.microsoft.com/windowsazure"));
                        if (stateElement != null)
                        {
                            string stateInstance = stateElement.Value;
                            siteInstance.State = stateInstance;
                        }

                        XElement usageStateElement = sitesElement.Element(XName.Get("UsageState", "http://schemas.microsoft.com/windowsazure"));
                        if (usageStateElement != null && !string.IsNullOrEmpty(usageStateElement.Value))
                        {
                            WebSiteUsageState usageStateInstance = ((WebSiteUsageState)Enum.Parse(typeof(WebSiteUsageState), usageStateElement.Value, true));
                            siteInstance.UsageState = usageStateInstance;
                        }

                        XElement webSpaceElement = sitesElement.Element(XName.Get("WebSpace", "http://schemas.microsoft.com/windowsazure"));
                        if (webSpaceElement != null)
                        {
                            string webSpaceInstance = webSpaceElement.Value;
                            siteInstance.WebSpace = webSpaceInstance;
                        }
                    }
                }
            }
            else
                throw new Exception(string.IsNullOrEmpty(response.Item2) ? "Error Occurred while processing your command" : response.Item2);
            return result;
        }

        private Tuple<bool, string> GetResponseString(string uri, string method)
        {
            string responseText = string.Empty;
            bool result = false;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(BASE_URI + SubscriptionID + uri);
            request.ClientCertificates.Add(Certificate);
            if (WebProxy != null && !string.IsNullOrEmpty(WebProxy.Address.AbsoluteUri))
                request.Proxy = WebProxy;
            request.Method = method;
            request.Headers.Add(VERSION_HEADER, VERSION);
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (var reader = new StreamReader(response.GetResponseStream(), Encoding.ASCII))
                {
                    responseText = reader.ReadToEnd();
                }
                result = response.StatusCode == HttpStatusCode.OK;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return Tuple.Create(result, responseText);
        }

        #endregion
    }
}