//using dbStuff;
using System.Data;
using MainFunction.Database;
using MainFunction.MathsFunctions;
using MainFunction.StatBlocks.DndClasses.GeneralClassMethods;

//this namespace is used to display information
namespace MainFunction {
	class Program
	{
		static void Main()
		{
			StatBlockClass Testing = new StatBlockClass();
			
			List<int> inputList = [5];
			List<string> Classinput = ["fighter"];

			Dictionary<String, object> TestingDic = Testing.StatBlockGeneratorAbilityScoresModifiers(inputList, "HalfElf", Classinput);

			foreach (var VARIABLE in TestingDic)
			{
				Console.WriteLine(VARIABLE);
			}
		}
	}
}