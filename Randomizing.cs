using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Framework
{
	public class Randomizing
	{
		public static Random MyRandom = new Random();

		private static string Numbers = "0123456789";
		private static string AllChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
		private static string SmallChars = "abcdefghijklmnopqrstuvwxyz";
		private static string SmallCharsAndNumbers = "abcdefghijklmnopqrstuvwxyz0123456789";
		private static string LoremIpsumChars = "loremipsumdolorsitamet";

		public enum CharMode
		{ 
			All,
			Small,
			SmallAndNumbers,
			LoremIpsum,
			Number
		}

		public static bool GetRandomBool()
		{
			return MyRandom.NextDouble() > 0.5;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="probabilityRatioAsPercent">True olma olasılığı olarak: 1 ile 100 arasında bir değer</param>
		/// <returns></returns>
		public static bool GetRandomBool(int probabilityOfTrueRatioAsPercent)
		{
			if ( probabilityOfTrueRatioAsPercent <= 0 )
			{
				throw new ServiceException("probabilityRatioAsPercent 0 dan büyük olmalı");
			}
			return MyRandom.NextDouble() > ( (double)1 - ( (double)probabilityOfTrueRatioAsPercent / (double)100 ) );
		}

		public static int GetRandomInt(int start, int end)
		{
			int rnum = start + Convert.ToInt32(Math.Floor(MyRandom.NextDouble() * (end - start + 1)));
			return rnum;
		}

		public static List<int> GetRandomListAsInt(int minListLength, int maxListLength, int maxNumberToGenerate)
		{
			List<int> list = new List<int>();
			int count = GetRandomInt(minListLength, maxListLength);
			for (int i = 0; i < count; i++)
			{
				list.Add(GetRandomInt(maxNumberToGenerate));
			}
			return list;
		}

		public static int GetRandomInt(int max)
		{
			return GetRandomInt(0, max);
		}

		public static double GetRandomDouble(int start, int end)
		{
			return GetRandomDouble((double)start, (double)end);
		}

		public static double GetRandomDouble(double start, double end)
		{
			double rnum = start + ( MyRandom.NextDouble() * (end - start) );
			return rnum;
		}

		public static decimal GetRandomDecimal(double start, double end)
		{
			decimal rnum = Convert.ToDecimal( start + (MyRandom.NextDouble() * (end - start)));
			return rnum;
		}

		public static DateTime GetRandomDate(DateTime startDate, DateTime endDate)
		{
			double minutes = endDate.Subtract(startDate).TotalMinutes;
			double increase = GetRandomDouble(0, minutes);
			return startDate.AddMinutes(increase);
		}

		public static DateTime GetRandomDate(int upToNHoursAgo)
		{
			DateTime startDate = DateTime.Now.AddHours(-upToNHoursAgo);
			return GetRandomDate(startDate, DateTime.Now);
		}

		public static string GetRandomString(int min, int max, CharMode charMode)
		{
			return GetRandomString(GetRandomInt(min, max), charMode);
		}

		/// <summary>
		/// dakikaya kadar date string + len kadar random string
		/// </summary>
		/// <param name="lenAfterDatePrefix"></param>
		/// <param name="charMode"></param>
		/// <returns></returns>
		public static string GetRandomStringWithDatePrefix(int lenAfterDatePrefix, CharMode charMode)
		{
			return GetDateString() + GetRandomString(lenAfterDatePrefix, charMode);
		}

		/// <summary>
		/// saniyeye kadar date string + len kadar random string
		/// </summary>
		/// <param name="lenAfterDatePrefix"></param>
		/// <param name="charMode"></param>
		/// <returns></returns>
		public static string GetRandomStringWithDatePrefixWithSeconds(int lenAfterDatePrefix, CharMode charMode)
		{
			return GetDateStringWithSeconds() + GetRandomString(lenAfterDatePrefix, charMode);
		}

		public static string GetDateStringOnlyHour()
		{
			return String.Format("{0:HHmm}", DateHelper.Now);
		}

		public static string GetDateStringTillDay()
		{
			return String.Format("{0:yyyyMMdd}", DateHelper.Now);
		}

		/// <summary>
		/// dakikaya kadar datestring
		/// </summary>
		/// <returns></returns>
		public static string GetDateString()
		{ 
			return String.Format("{0:yyyyMMddHHmm}", DateHelper.Now);
		}

		/// <summary>
		/// saniyeye kadar datestring
		/// </summary>
		/// <returns></returns>
		public static string GetDateStringWithSeconds()
		{
			return String.Format("{0:yyyyMMddHHmmss}", DateHelper.Now);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="minWords">Paragrafın en az kaç kelime olacağı</param>
		/// <param name="maxWords">Paragrafın en fazla kaç kelime olacağı</param>
		/// <param name="minWordLen">Paragraftaki kelimelerin en az kaç harf olacağı</param>
		/// <param name="maxWordLen">Paragraftaki kelimelerin en fazla kaç harf olacağı</param>
		/// <param name="charMode"></param>
		/// <returns></returns>
		public static string GetRandomPharagraph(int minWords, int maxWords, int minWordLen, int maxWordLen, CharMode charMode)
		{
			string text = "";
			int wordCount = GetRandomInt(minWords, maxWords);
			for (int i = 0; i < wordCount; i++)
			{
				text += GetRandomString(GetRandomInt(minWordLen, maxWordLen), charMode);
				if (i < wordCount-1)
					text += " ";
			}
			return text;
		}

		public static string GetRandomPharagraph(int minWords, int maxWords, CharMode charMode)
		{
			return GetRandomPharagraph(minWords, maxWords, 3, 15, charMode);
		}

		public static List<string> GetRandomListString(int minListLength, int maxListLength, int maxStringLength, CharMode charMode)
		{
			List<string> list = new List<string>();
			int wordCount = GetRandomInt(minListLength, maxListLength);
			for (int i = 0; i < wordCount; i++)
			{
				list.Add(GetRandomString(maxStringLength, charMode));
			}
			return list;
		}

		public static string GetRandomString(int length, CharMode charMode)
		{
			string result = String.Empty;
			string chars = "";
			switch (charMode)
			{
				case CharMode.Small:
					chars = SmallChars;
					break;
				case CharMode.LoremIpsum:
					chars = LoremIpsumChars;
					break;
				case CharMode.Number:
					chars = Numbers;
					break;
				case CharMode.SmallAndNumbers:
					chars = SmallCharsAndNumbers;
					break;
				default:
					chars = AllChars;
					break;
			}
			return GetRandomString(length, chars);
		}

		public static string GetRandomString(int length, string sourceChars)
		{
			string result = String.Empty;
			for (int i = 0; i < length; i++)
			{
				int rnum = Convert.ToInt32(Math.Floor(MyRandom.NextDouble() * sourceChars.Length));
				result += sourceChars.Substring(rnum, 1);
			}
			return result;
		}

		public static T GetRandomValueFromList<T>(List<T> list) where T : class
		{
			if(list == null || list.Count == 0)
				return null;
			int index = GetRandomInt(list.Count - 1);
			return list[index];
		}

		public static T2 GetRandomValueFromDictionary<T1, T2>(Dictionary<T1, T2> list) where T2 : class
		{
			if(list == null || list.Count == 0)
				return null;
			int index = GetRandomInt(list.Count - 1);
			return list.ElementAt(index).Value;
		}

		public static string GetRandomStringFromList(params string[] elements)
		{
			if ( elements == null || elements.Length == 0 )
				return "";
			int index = GetRandomInt(elements.Length - 1);
			return elements[index];
		}

		public class TreeListGenerator<T> where T : class
		{
			public int MaxFirstLevelItems = 10;
			public int MaxChildrenCount = 5;

			public Func<T> CreatorFunction = null;
			public Expression<Func<T, List<T>>> ChildrenSelectorExpression = null;
			public Expression<Func<T, T>> ParentSelectorExpression = null;
			public Action<T> OnAfterItemCreate = null;

			private Func<T, List<T>> _getChildren = null;
			private Func<T, List<T>> GetChildren
			{
				get
				{
					if (_getChildren == null)
						_getChildren = ChildrenSelectorExpression.Compile();
					return _getChildren;
				}
			}

			private Func<T, T> _getParent = null;
			private Func<T, T> GetParent
			{
				get
				{
					if (_getParent == null)
						_getParent = ParentSelectorExpression.Compile();
					return _getParent;
				}
			}

			public List<T> GetTreeList()
			{

				if (CreatorFunction == null)
				{
					throw new Exception("CreatorFunction can not be null");
				}

				if (ChildrenSelectorExpression == null)
				{
					throw new Exception("ChildrenSelectorExpression can not be null");
				}
				
				List<T> list = new List<T>();

				int count = Framework.Randomizing.GetRandomInt(1, MaxFirstLevelItems);

				for (int i = 0; i < count; i++)
				{
					T firstLevelItem = CreatorFunction();
					list.Add(firstLevelItem);
					AddChildren(firstLevelItem, 0);
				}
				return list;
			}

			void AddChildren(T item, int currentLevel)
			{
				// currentLevent arttıkça çocuk olasılığı azalıyor
				bool willHaveChildren = Framework.Randomizing.GetRandomInt(currentLevel + 1) == currentLevel;
				if (willHaveChildren)
				{
					int childrenCount = Framework.Randomizing.GetRandomInt(MaxChildrenCount);
					for (int i = 0; i < childrenCount; i++)
					{
						T children = CreatorFunction();
						GetChildren.Invoke(item).Add(children);
						if (ParentSelectorExpression != null)
						{
							T parent = GetParent.Invoke(children);
							parent = item;
						}
						AddChildren(children, currentLevel + 1);
					}
				}
				if (OnAfterItemCreate != null)
					OnAfterItemCreate(item);
			}
		}
	}
}
