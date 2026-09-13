using System;
using System.Collections.Generic;
using System.Text;

namespace BadrHospital.Domain.Models.Common
{
    public interface IHasRowVersion
    {
        public byte[] RowVersion { get; set; }
    }
}
