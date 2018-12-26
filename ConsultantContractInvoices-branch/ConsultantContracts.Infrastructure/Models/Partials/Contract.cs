using System.Text;

namespace ConsultantContracts.Infrastructure.Models
{
    public partial class Contract
    {
        public string TypeWorkList()
        {
            var sb = new StringBuilder();
            foreach (var type in this.WorkTypes)
            {
                sb.Append(type.WorkTypeName);
                sb.Append(" ");
            }

            return sb.ToString();
        }
    }
}