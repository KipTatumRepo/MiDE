using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MiDEWPF.Resources;

namespace MiDEWPF.Resources
{
	public class Data
	{

		public List<string> MasterBuildingList()
		{
			List<string> mbl = new List<string>();
			for (int i = 1; i <= 31; i++)
			{
				mbl.Add("Building " + i);
			}
			return mbl;
		}
		public List<SValue> SValue()
		{
			List<SValue> sv = new List<SValue>();
			sv.Add(new SValue { Value = 1, Name = "Moving Office in Same Building", Description = "Change office within same building" });
			sv.Add(new SValue { Value = 3, Name = "Moving Office in Next Building Over", Description = "Change office to a building in close proximity" });
			sv.Add(new SValue { Value = 2, Name = "Sharing An Office", Description = "Adding multiple people to an office" });
			sv.Add(new SValue { Value = 4, Name = "Moving Across Campus", Description = "Move to a building on the other side of campus" });
			sv.Add(new SValue { Value = 9, Name = "Move Off Campus", Description = "Moved off campus temporarily" });
			sv.Add(new SValue { Value = 2, Name = "Lose Of A Window", Description = "Move to interior part of building" });
			sv.Add(new SValue { Value = 4, Name = "Move From Common Area", Description = "Move to less desirable area" });
			sv.Add(new SValue { Value = 5, Name = "Lose Of Prime Parking", Description = "Move results in lose of good parking spot" });
			sv.Add(new SValue { Value = 5, Name = "Team Shuffle", Description = "Altering team makeup" });
			sv.Add(new SValue { Value = 7, Name = "Building Over Capacity", Description = "Adding of more personel moves the building population too high" });
			return sv;
		}

		public List<KeyValuePair<int, string>> StrategyGroups()
		{
			List<KeyValuePair<int, string>> dictList = new List<KeyValuePair<int, string>>();
			dictList.Add(new KeyValuePair<int, string>(1, "Party"));
			dictList.Add(new KeyValuePair<int, string>(2, "Cafe"));
			dictList.Add(new KeyValuePair<int, string>(3, "Ops"));
			return dictList;
		}

		public List<EValue> EValue()
		{
			List<EValue> ev = new List<EValue>();
			ev.Add(new EValue { StrategyName = "Party", EScore = 6, EName = "Weekly Ice Cream Parties", CostEffort = 3, CostMoney = 3 });
			ev.Add(new EValue { StrategyName = "Cafe", EScore = 10, EName = "Weekly Free Lunch", CostEffort = 10, CostMoney = 10 });
			ev.Add(new EValue { StrategyName = "Cafe", EScore = 3, EName = "Expand Lunch Hours", CostEffort = 5, CostMoney = 5 });
			ev.Add(new EValue { StrategyName = "Party", EScore = 3, EName = "Weekly Movie Time Parties", CostEffort = 5, CostMoney = 3 });
			ev.Add(new EValue { StrategyName = "Ops", EScore = 4, EName = "Open Space", CostEffort = 1, CostMoney = 1 });
			ev.Add(new EValue { StrategyName = "Ops", EScore = 2, EName = "Live Music Breaks", CostEffort = 3, CostMoney = 6 });
			ev.Add(new EValue { StrategyName = "Ops", EScore = 5, EName = "Mobile Snack Cart", CostEffort = 3, CostMoney = 4 });
			ev.Add(new EValue { StrategyName = "Cafe", EScore = 1, EName = "Expand Variety", CostEffort = 3, CostMoney = 3 });
			ev.Add(new EValue { StrategyName = "Cafe", EScore = 10, EName = "Add Staff", CostEffort = 10, CostMoney = 10 });
			ev.Add(new EValue { StrategyName = "Cafe", EScore = 3, EName = "Expand Coffee Station", CostEffort = 5, CostMoney = 5 });
			ev.Add(new EValue { StrategyName = "Party", EScore = 1, EName = "90's Dance Party", CostEffort = 5, CostMoney = 3 });
			ev.Add(new EValue { StrategyName = "Party", EScore = 1, EName = "Weekly Prizes", CostEffort = 1, CostMoney = 1 });
			ev.Add(new EValue { StrategyName = "Ops", EScore = 1, EName = "Add Couches", CostEffort = 3, CostMoney = 6 });
			ev.Add(new EValue { StrategyName = "Ops", EScore = 3, EName = "Food Trucks", CostEffort = 3, CostMoney = 4 });

			return ev;
		}

		public Dictionary<string, int> EValueToDict()
		{
			Dictionary<string, int> dict = new Dictionary<string, int>();
			List<EValue> ev = new List<EValue>();
			foreach (var item in ev)
			{
				dict.Add(item.StrategyName, item.EScore);
			}
			return dict;
		}

		public List<PopulationType> PopulationTypes()
		{
			List<PopulationType> pt = new List<PopulationType>();
			pt.Add(new PopulationType { PopId = 1, PopTypeName = "Executive" });
			pt.Add(new PopulationType { PopId = 2, PopTypeName = "Engineers" });
			pt.Add(new PopulationType { PopId = 3, PopTypeName = "Sales" });
			pt.Add(new PopulationType { PopId = 4, PopTypeName = "Support" });
			pt.Add(new PopulationType { PopId = 5, PopTypeName = "Mixed" });

			return pt;

		}

		public List<string> Population()
		{
			List<string> pop = new List<string>();
			pop.Add("< 500");
			pop.Add("500 - 999");
			pop.Add(">1000");

			return pop;
		}
	}
}
