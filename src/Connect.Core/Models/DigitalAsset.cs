using Connect.Core.Common;
using System;

namespace Connect.Core.Models
{
    public class DigitalAsset: AggregateRoot
    {
        public System.Guid DigitalAssetId { get; set; }           
        public string Name { get; set; }        
        public byte[] Bytes { get; set; }
        public string ContentType { get; set; }
    }
}
