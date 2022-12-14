using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Domain.Exceptions
{
    public class InvalidEntityStateException : Exception
    {
        public InvalidEntityStateException(object entity, string message)
        : base(string.Format("امکان تغییر وضعیت {0} وجود ندارد. {1}", entity.GetType().Name, message))
        {
        }
    }
}
