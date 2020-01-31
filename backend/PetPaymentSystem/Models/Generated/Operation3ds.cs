using System;
using System.Collections.Generic;

namespace PetPaymentSystem.Models.Generated
{
    public partial class Operation3ds
    {
        public int Id { get; set; }
        public int OperationId { get; set; }
        public string LocalMd { get; set; }
        public string RemoteMd { get; set; }
        public bool SaveCredentials { get; set; }

        public virtual Operation Operation { get; set; }
    }
}
