using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiDEWPF.Resources
{
	public class EValue : IComparable<EValue>
	{
		public string StrategyName { get; set; }
		public string EName { get; set; }
		public int EScore { get; set; }
		public int CostMoney { get; set; }
		public int CostEffort { get; set; }

		public int CompareTo(EValue evalue)
		{
			if (evalue == null)
			{
				return 1;
			}
			return this.CostMoney.CompareTo(evalue.CostMoney);
		}

	}
}
