using System.Globalization;

namespace SchoolProject.Data.Commons
{
	public class GeneralLocalizableEntity
	{
		public string Localize(string txtAr, string txtEn)
		{
			CultureInfo culture = Thread.CurrentThread.CurrentCulture;
			if (culture.TwoLetterISOLanguageName.ToLower() == "ar")
			{
				return txtAr;
			}
			return txtEn;
		}
	}
}
