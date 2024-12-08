

//this namespace is used to handle maths operations
namespace MainFunction.MathsFunctions
{
	class DiceRoller
	{
		// this is a dice roller method
		public int DndDiceRoller(string diceRoll)
		{

			// adding plus to help with interpolation
			diceRoll = '+' + diceRoll + '+';
			int DiceResult = 0;

			List<int> LocationOfD = new();
			List<int> LocationOfPlus = new();

			string NumConcatenator = "";

			//removes spaces
			diceRoll.Trim(' ');

			for (int LinqDLocationIndex = 0; LinqDLocationIndex < diceRoll.Length; LinqDLocationIndex++)
			{
				if (diceRoll[LinqDLocationIndex] == 'd')
				{
					LocationOfD.Add(LinqDLocationIndex);
				}
			}

			for (int LinqPlusLocationIndex =0; LinqPlusLocationIndex < diceRoll.Length; LinqPlusLocationIndex++)
			{
				if (diceRoll[LinqPlusLocationIndex] == '+')
				{
					LocationOfPlus.Add(LinqPlusLocationIndex);
				}
			}

			Random RandNumGen = new();
			
			for (int IndexLocationOfD = 0; IndexLocationOfD < LocationOfD.Count; IndexLocationOfD++) 
			{
				for (int NumDiceNum = LocationOfPlus[IndexLocationOfD]; NumDiceNum < LocationOfD[IndexLocationOfD]-1; NumDiceNum++)
				{
					NumConcatenator += diceRoll[NumDiceNum+1];
				}

				int NumDice = int.Parse(NumConcatenator);

				int LocationOfPlusSubstring = LocationOfPlus[IndexLocationOfD + 1] - (LocationOfD[IndexLocationOfD] + 1);

				string DiceSizeParseValue = diceRoll.Substring(LocationOfD[IndexLocationOfD] + 1, LocationOfPlusSubstring);

				int DiceSize = int.Parse(DiceSizeParseValue);

				for (int RandRepeats = 0; RandRepeats < NumDice; RandRepeats++)
				{
					DiceResult = DiceResult + RandNumGen.Next(1, DiceSize);
				}
			}

			return DiceResult;

		}
	}
}
