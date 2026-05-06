

using System.Xml.Linq;
using System.Text.RegularExpressions;
using XMLparsingWithRegex.Models;

public class Program
{
    public static void Main(string[] args)
    {
        string xmlPath = @"XMLFile.xml";
        string excelPath = @"ExcelFile.xlsx";

        XDocument xmlDocument = XDocument.Load(xmlPath);

        //получение моделей пользователей из XML документа
        var users = xmlDocument.Root.Elements()
            .Select(element =>
            {
                var elements = element.Elements().ToArray();
                string fio = GetFio(elements[0].Value);
                DateTime birthDate = GetBirthDate(elements[1].Value);
                string number = GetUserNumber(elements[2].Value);
                int rating = GetUserRating(elements[3].Value);

                return new User
                {
                    UserFIO = fio,
                    UserBirthDate = birthDate,
                    UserNumber = number,
                    UserRating = rating
                };
            });


    }

    

    static public string GetFio(string xmlFio)
    {
        return "";
    }

    static public DateTime GetBirthDate(string xmlBirthDate)
    {
        return DateTime.Now;
    }

    static string GetUserNumber(string xmlNumber)
    {
        return "";
    }

    static int GetUserRating(string xmlRating)
    {
        return 0;
    }
}