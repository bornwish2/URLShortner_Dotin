using Framework.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.ShortUrl.ValueObjects
{
    public class URL : BaseValueObject<URL>
    {
        /// <summary>
        /// RegEx for except Only url value
        /// </summary>
        [RegularExpression(pattern:"/^(https?:\\/\\/)?([\\da-z\\.-]+)\\.([a-z\\.]{2,6})([\\/\\w \\.-]*)*\\/?$/"
            , ErrorMessage = "لطفا آدرس دامنه را به درستی وارد کنید")]
        public string Value { get; private set; }

        public static URL FromString(string value) => new URL(value);
        private URL()
        {
        }
        public URL(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("برای دامنه مقدار لازم است", nameof(value));
            }
            Value = value;
        }
        public override int ObjectGetHashCode() => Value.GetHashCode();
        public override bool ObjectIsEqual(URL otherObject) => Value == otherObject.Value;

        public static implicit operator string(URL urlString) => urlString.Value;
    }
}
