using Microsoft.Azure;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Management.WebSites;
using Microsoft.WindowsAzure.Management.WebSites.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace API_APP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static string azureActiveDirectoryInstance = System.Configuration.ConfigurationManager.AppSettings["AzureActiveDirectoryInstance"];
        private static string subscriptionID = System.Configuration.ConfigurationManager.AppSettings["SubscriptionID"];
        private static string resourceURL = System.Configuration.ConfigurationManager.AppSettings["Resource"];
        private static string tenantID = System.Configuration.ConfigurationManager.AppSettings["TenantID"];
        private static string clientID = System.Configuration.ConfigurationManager.AppSettings["ClientID"];
        private static string appKey = System.Configuration.ConfigurationManager.AppSettings["AppKey"];
        static string authority = azureActiveDirectoryInstance + tenantID;

        private static X509Certificate2 GetCertificate()
        {
            var encodedData = "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";
            var certificateAsBytes = Convert.FromBase64String(encodedData);
            var certificate = new X509Certificate2(certificateAsBytes);
            return certificate;
        }

        private void btnApiApps_Click(object sender, EventArgs e)
        {
            // Without Proxy
            //AzureWebSiteClient newClient = new AzureWebSiteClient(subscriptionID, GetCertificate(), null);

            // With Proxy
            AzureWebSiteClient newClient = new AzureWebSiteClient(subscriptionID, GetCertificate(), GetWebProxy());

            var client = new WebSiteManagementClient(new Microsoft.WindowsAzure.CertificateCloudCredentials(subscriptionID, GetCertificate()));
            //client.HttpClient = httpClient;
            WebSpacesListResponse webspaces = newClient.GetWebSpaces();

            foreach (var p in webspaces)
            {
                WebSpacesListWebSitesResponse websitesInWebspace = newClient.GetWebSites(p.Name);

                foreach (var o in websitesInWebspace)
                {
                    ApiAppslistBox.Items.Add(o.Name);

                    string url = string.Format("{0}{1}{2}", "https://", o.Name, ".azurewebsites.net/swagger/docs/v1");
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.Timeout = 15000;
                    string responseText = string.Empty;

                    HttpWebResponse response;
                    try
                    {
                        response = (HttpWebResponse)request.GetResponse();
                        using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
                        {
                            responseText = reader.ReadToEnd();
                            JObject parsed = JObject.Parse(responseText);
                            JToken paths = parsed["paths"];

                            foreach (JProperty path in paths)
                            {
                                var apiName = path.Name;
                                urlListCollections.Items.Add(apiName);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //Ignore the exception - some other ex / incase of Api app is not available in azure.
                        Console.WriteLine(ex.Message);
                    }

                    //OperationResponse operation = client.WebSites.Restart(p.Name, o.Name);
                    //Console.WriteLine(operation.StatusCode.ToString());
                }
            }

            webspaces.Select(p =>
            {
                Console.WriteLine("Processing webspace {0}", p.Name);

                WebSpacesListWebSitesResponse websitesInWebspace = client.WebSpaces.ListWebSites(p.Name,
                                new WebSiteListParameters()
                                {
                                });

                websitesInWebspace.Select(o =>
                {
                    Console.Write(" - Restarting {0} ... ", o.Name);

                    OperationResponse operation = client.WebSites.Restart(p.Name, o.Name);

                    Console.WriteLine(operation.StatusCode.ToString());

                    return o;
                }).ToArray();

                return p;
            }).ToArray();

            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("Press anykey to exit");
                Console.Read();
            }
        }

        public static WebProxy GetWebProxy()
        {
            WebProxy webproxy = new WebProxy("192.168.1.106", 808);

            NetworkCredential credential = new NetworkCredential("Sivanraj", "Sivan@123", "BT360DEV3");
            webproxy.Credentials = credential;

            return webproxy;
        }

    }
}
