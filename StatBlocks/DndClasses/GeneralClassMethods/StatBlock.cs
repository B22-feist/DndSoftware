using System.Collections.Immutable;
using MainFunction.MathsFunctions;


// This namespace is used to handle statblocks
namespace MainFunction.StatBlocks.DndClasses.GeneralClassMethods
{
	//here we initialize the stat blocks through either database requests, or through users making new characters.
	class StatBlockClass
	{
		// stat dictionary contains the key value pairs of dnd stats i.e 1 become -5, 20 becomes 5.
		private readonly Dictionary<int, int> _checkStatDictionary = new Dictionary<int, int>() {
			{ 1, -5 },
			{ 2, -4 },
			{ 3, -4 },
			{ 4, -3 },
			{ 5, -3 },
			{ 6, -2 },
			{ 7, -2 },
			{ 8, -1 },
			{ 9, -1 },
			{ 10, 0 },
			{ 11, 0 },
			{ 12, 1 },
			{ 13, 1 },
			{ 14, 2 },
			{ 15, 2 },
			{ 16, 3 },
			{ 17, 3 },
			{ 18, 4 },
			{ 19, 4 },
			{ 20, 5 },
			{ 21, 5 },
			{ 22, 6 },
			{ 23, 6 },
			{ 24, 7 },
			{ 25, 7 },
			{ 26, 8 },
			{ 27, 8 },
			{ 28, 9 },
			{ 29, 9 },
			{ 30, 10 }
		};

		//here we generate stat blocks (should only be called if generating stat blocks)
		public Dictionary<string, object> StatBlockGeneratorAbilityScoresModifiers(List<int> level, string race,
			List<string> dndClass)
		{
			Dictionary<string, object> StatBlockStats = new();
			ImmutableArray<int> ProfBonus = [2, 2, 2, 2, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5, 6, 6, 6, 6];
			Dictionary<String, int> RaceAbilityScoreBonuses = RaceAbilityScoreModifiers(race);
			List<int> StatBlockStatsUserInputs = AbilityScoresUserInput(level, dndClass);

			StatBlockStats.Add("str",
				_checkStatDictionary[StatBlockStatsUserInputs[0] + RaceAbilityScoreBonuses["str"]]);
			StatBlockStats.Add("con",
				_checkStatDictionary[StatBlockStatsUserInputs[1] + RaceAbilityScoreBonuses["con"]]);
			StatBlockStats.Add("dex",
				_checkStatDictionary[StatBlockStatsUserInputs[2] + RaceAbilityScoreBonuses["dex"]]);
			StatBlockStats.Add("int",
				_checkStatDictionary[StatBlockStatsUserInputs[3] + RaceAbilityScoreBonuses["int"]]);
			StatBlockStats.Add("wis",
				_checkStatDictionary[StatBlockStatsUserInputs[4] + RaceAbilityScoreBonuses["wis"]]);
			StatBlockStats.Add("chr",
				_checkStatDictionary[StatBlockStatsUserInputs[5] + RaceAbilityScoreBonuses["chr"]]);

			StatBlockStats.Add("health",
				HealthComputation(level[0], Convert.ToInt32(StatBlockStats["con"]), dndClass[0]));

			int TotalLevel = 0;

			foreach (int LevelNumbers in level)
			{
				TotalLevel += LevelNumbers;
			}

			StatBlockStats.Add("profBonus", ProfBonus[TotalLevel]);

			return StatBlockStats;
		}

		private int HealthComputation(int level, int con, String dndClass)
		{
			DiceRoller HealthDiceRoller = new();
			Dictionary<string, string> HitDieDictionary = new()
			{
				{ "barbarian", "d12" },
				{ "fighter", "d10" },
				{ "paladin", "d10" },
				{ "ranger", "d10" },
				{ "artificer", "d8" },
				{ "bard", "d8" },
				{ "cleric", "d8" },
				{ "druid", "d8" },
				{ "monk", "d8" },
				{ "rogue", "d8" },
				{ "warlock", "d8" },
				{ "sorcerer", "d6" },
				{ "wizard", "d6" }
			};

			string DiceRoll = (level - 1) + HitDieDictionary[dndClass];
			string FirstRoll = (HitDieDictionary[dndClass]).Trim('d');

			int ReturnHealth = Convert.ToInt32(HealthDiceRoller.DndDiceRoller(DiceRoll)) + int.Parse(FirstRoll) +
			                   (level * con);

			if (ReturnHealth == 0)
			{
				ReturnHealth = 1;
				return ReturnHealth;
			}

			else
			{
				return ReturnHealth;
			}
		}

		private Dictionary<string, int> RaceAbilityScoreModifiers(string race)
		{
			Dictionary<string, int> RaceAbilityScoreModifiersReturnDictionary = new()
			{
				{ "str", 0 },
				{ "con", 0 },
				{ "dex", 0 },
				{ "int", 0 },
				{ "wis", 0 },
				{ "chr", 0 }
			};
			bool ExitWhileLoop = false;
			while (true)
			{
				switch (race)
				{
					case "HillDwarf":
						RaceAbilityScoreModifiersReturnDictionary["con"] = 2;
						RaceAbilityScoreModifiersReturnDictionary["wis"] = 1;
						ExitWhileLoop = true;
						break;

					case "MountainDwarf":

						RaceAbilityScoreModifiersReturnDictionary["str"] = 2;
						RaceAbilityScoreModifiersReturnDictionary["con"] = 2;
						ExitWhileLoop = true;
						break;

					case "HighElf":
						RaceAbilityScoreModifiersReturnDictionary["dex"] = 2;
						RaceAbilityScoreModifiersReturnDictionary["int"] = 1;
						ExitWhileLoop = true;
						break;

					case "WoofElf":
						RaceAbilityScoreModifiersReturnDictionary["dex"] = 2;
						RaceAbilityScoreModifiersReturnDictionary["wis"] = 1;
						ExitWhileLoop = true;
						break;

					case "DarkElf":

						RaceAbilityScoreModifiersReturnDictionary["dex"] = 2;
						RaceAbilityScoreModifiersReturnDictionary["chr"] = 1;
						ExitWhileLoop = true;
						break;

					case "LightFootHalfing":
						RaceAbilityScoreModifiersReturnDictionary["dex"] = 2;
						RaceAbilityScoreModifiersReturnDictionary["chr"] = 1;
						ExitWhileLoop = true;
						break;

					case "StoutHalfing":
						RaceAbilityScoreModifiersReturnDictionary["con"] = 1;
						RaceAbilityScoreModifiersReturnDictionary["dex"] = 2;
						ExitWhileLoop = true;
						break;

					case "NormalHuman":
						RaceAbilityScoreModifiersReturnDictionary["str"] = 1;
						RaceAbilityScoreModifiersReturnDictionary["con"] = 1;
						RaceAbilityScoreModifiersReturnDictionary["dex"] = 1;
						RaceAbilityScoreModifiersReturnDictionary["int"] = 1;
						RaceAbilityScoreModifiersReturnDictionary["wis"] = 1;
						RaceAbilityScoreModifiersReturnDictionary["chr"] = 1;
						ExitWhileLoop = true;
						break;

					case "VariantHuman":
						int VariantHumanCounter = 0;

						while (VariantHumanCounter < 2)
						{
							Console.WriteLine("pick an ability score to improve by one \n str,con,dex,int,wis,chr");
							string VariantHumanStatIncrease = Console.ReadLine();

							if (RaceAbilityScoreModifiersReturnDictionary.ContainsKey(VariantHumanStatIncrease) )
							{
								RaceAbilityScoreModifiersReturnDictionary[VariantHumanStatIncrease]++;
								VariantHumanCounter++;
							}

							else
							{
								Console.WriteLine("no such ability score increase");
							}
						}
						
						ExitWhileLoop = true;
						return RaceAbilityScoreModifiersReturnDictionary;

					case "Dragonborn":
						RaceAbilityScoreModifiersReturnDictionary["str"] = 2;
						RaceAbilityScoreModifiersReturnDictionary["chr"] = 1;
						ExitWhileLoop = true;
						break;

					case "ForestGnome":
						RaceAbilityScoreModifiersReturnDictionary["dex"] = 1;
						RaceAbilityScoreModifiersReturnDictionary["int"] = 2;
						ExitWhileLoop = true;
						break;

					case "RockGnome":
						RaceAbilityScoreModifiersReturnDictionary["con"] = 1;
						RaceAbilityScoreModifiersReturnDictionary["int"] = 2;
						ExitWhileLoop = true;
						break;

					case "HalfElf":
						int HalfElfCounter = 0;

						RaceAbilityScoreModifiersReturnDictionary["chr"] = 2;
						while (HalfElfCounter < 2)
						{
							Console.WriteLine("pick an ability score to improve by one \n str,con,dex,int,wis");
							string HalfElfStatIncrease = Console.ReadLine();

							if (RaceAbilityScoreModifiersReturnDictionary.ContainsKey(HalfElfStatIncrease) == false ||
							    HalfElfStatIncrease.ToLower() == "chr")
							{
								Console.Write("this is a reapeat");
							}

							else
							{
								RaceAbilityScoreModifiersReturnDictionary[HalfElfStatIncrease]++;
								HalfElfCounter++;
							}
						}
						
						ExitWhileLoop = true;
						break;


					case "HalfOrc":
						RaceAbilityScoreModifiersReturnDictionary["str"] = 2;
						RaceAbilityScoreModifiersReturnDictionary["con"] = 1;
						ExitWhileLoop = true;
						break;

					case "Tiefling":
						RaceAbilityScoreModifiersReturnDictionary["int"] = 1;
						RaceAbilityScoreModifiersReturnDictionary["chr"] = 2;
						ExitWhileLoop = true;
						break;
				}

				if (ExitWhileLoop)
				{
					break;
				}
			}
			
			
			return RaceAbilityScoreModifiersReturnDictionary;
		}

		//Ability score user inputs takes in an arguement of level, which contains the level if multiclassed, and the classes, which is a list to help with multi classing.
		private List<int> AbilityScoresUserInput(List<int> level, List<string> dndclass)
		{
			DiceRoller AbilityScoreRoller = new();
			List<int> AbilityScores = new();
			for (int GenerateDiceRollRepeats = 0; GenerateDiceRollRepeats < 6; GenerateDiceRollRepeats++)
			{
				AbilityScores.Add(AbilityScoreRoller.DndDiceRoller("3d6"));
			}

			AbilityScores.Sort();

			Console.WriteLine("Here are the ability scores:");
			
			foreach (int AbilityScoresScores in AbilityScores)
			{
				Console.WriteLine(AbilityScoresScores);
			}

			List<int> ReturnAbilityScores = new();
			ImmutableArray<string> AbilityScoreNames = ["str", "con", "dex", "int", "wis", "chr"];

			for (int UserInputCounterAbilitscores = 0; UserInputCounterAbilitscores < 6; UserInputCounterAbilitscores++)
			{
				while (true)
				{
					Console.WriteLine($"enter the ability score you wish to use for {AbilityScoreNames[UserInputCounterAbilitscores]}");
					
					String StringAbilityScoreInput = Console.ReadLine() ?? string.Empty;
					
					int InputAbilityScore = int.Parse(StringAbilityScoreInput);
					
					if (AbilityScores.Contains(InputAbilityScore))
					{
						ReturnAbilityScores.Add(InputAbilityScore);
						AbilityScores.Remove(InputAbilityScore);
						break;
					}

					else
					{
						Console.WriteLine("no such ability score");
					}
				}
			}
			
			
			return ReturnAbilityScores;
		}

		private List<int> ClassAbilityScoreImprovements(List<int> level, List<string> dndclass, List<int> abilityScores)
		{
			Dictionary<string, int> AbilityScoresLocations = new()
			{
				{"str", 0},
				{"con", 1},
				{"dex", 2},
				{"int", 3},
				{"wis", 4},
				{"chr", 5}
			};
			List<int> NormalAbilityScoreImprovementLevels = [4, 8, 12, 16, 19];
			
			List<int> RougeAbilityScoreImprovementLevels = [4,8,10,12,16,19];
			
			List<int> FighterAbilityScoreImprovementLevels = [4,6,8,12,16,19];

			for (int DndClassLoop = 0; DndClassLoop < dndclass.Count; DndClassLoop++)
			{
				if (dndclass[DndClassLoop] =="fighter") 
				{
					for (int FighterAblityScoreLevel = 0; FighterAblityScoreLevel < level[DndClassLoop]; FighterAblityScoreLevel++)
					{
						if (FighterAbilityScoreImprovementLevels.Contains(FighterAblityScoreLevel))
						{
							Console.WriteLine("which stat would you like to improve?");
							String StringAbilityScoreStatIncrease = Console.ReadLine() ?? string.Empty;

							if (AbilityScoresLocations.ContainsKey(StringAbilityScoreStatIncrease.ToLower()))
							{
								if (abilityScores[AbilityScoresLocations[StringAbilityScoreStatIncrease]]++ >= 20)
								{
									Console.WriteLine($"that would increase{StringAbilityScoreStatIncrease} past 20 which isn't allowed");
									FighterAblityScoreLevel--;
								}

								else
								{
									abilityScores[AbilityScoresLocations[StringAbilityScoreStatIncrease]]++;
								}
							}
							
						}
					}
				}
			}
			return abilityScores;
		}
	}
}