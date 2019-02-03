using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Provausio.PhillipsHue.Bridge
{
    public class HueBridgeConfiguration
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("zigbeechannel")]
        public string ZigbeeChannel { get; set; }

        [JsonProperty("mac")]
        public string MacAddress { get; set; }

        [JsonProperty("dhcp")]
        public bool Dhcp { get; set; }

        [JsonProperty("ipaddress")]
        public string IpAddress { get; set; }

        [JsonProperty("netmask")]
        public string NetMask { get; set; }

        [JsonProperty("gateway")]
        public string Gateway { get; set; }

        [JsonProperty("proxyaddress")]
        public string ProxyAddress { get; set; }

        [JsonProperty("proxyport")]
        public int ProxyPort { get; set; }

        [JsonProperty("utc")]
        public DateTime UtcTime { get; set; }

        [JsonProperty("localtime")]
        public DateTime LocalTime { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("modelid")]
        public string ModelId { get; set; }

        [JsonProperty("datastoreversion")]
        public string DataStoreVersion { get; set; }

        [JsonProperty("swversion")]
        public string SoftwareVersion { get; set; }

        [JsonProperty("apiversion")]
        public string ApiVersion { get; set; }

        [JsonProperty("swupdate")]
        public SoftwareUpdateInfo SoftwareUpdateInfo { get; set; }

        [JsonProperty("linkbutton")]
        public bool LinkButton { get; set; }

        [JsonProperty("portalservices")]
        public bool PortalServices { get; set; }

        [JsonProperty("portalconnection")]
        public string PortalConnection { get; set; }

        [JsonProperty("portalstate")]
        public PortalState PortalState { get; set; }

        [JsonProperty("internetservices")]
        public InternetServices InternetServices { get; set; }

        [JsonProperty("factorynew")]
        public bool IsFactoryNew { get; set; }

        [JsonProperty("replacesbridgeid")]
        public string ReplacesbridgeId { get; set; }

        [JsonProperty("backup")]
        public Backup Backup { get; set; }

        [JsonProperty("starterkitid")]
        public string StarterKitId { get; set; }

        [JsonProperty("whitelist")]
        public Dictionary<string, WhiteListItem> WhiteList { get; set; }
    }
}