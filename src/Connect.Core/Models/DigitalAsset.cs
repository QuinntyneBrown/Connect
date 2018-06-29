using Connect.Core.Common;
using System;

namespace Connect.Core.Models
{
    public class DigitalAsset: Entity
    {
        public Guid DigitalAssetId { get; set; }           
        public string Name { get; set; }        
        public byte[] Bytes { get; set; }
        public string ContentType { get; set; }
    }
}
