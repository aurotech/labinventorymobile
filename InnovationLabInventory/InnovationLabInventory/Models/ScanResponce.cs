using System;
namespace InnovationLabInventory
{
    public class ScanResponce
    {
        public string assetId { get; set; }
        public string assetType { get; set; }
        public string description { get; set; }
        public string assignee { get; set; }
        public string status { get; set; }
        public int quantity { get; set; }
        public string timeLastUpdated { get; set; }
        public string timeLastScanned { get; set; }
        public string comment { get; set; }
    }
}
