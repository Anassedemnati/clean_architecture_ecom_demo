using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Domain.Common;

public class EntityBase
{
    public int Id { get; protected set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? LastModifiedDate { get; set; }
}
