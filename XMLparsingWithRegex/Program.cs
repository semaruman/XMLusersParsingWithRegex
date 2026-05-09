

using System.Xml.Linq;
using System.Text.RegularExpressions;
using XMLparsingWithRegex.Models;
using System.Resources;

public class Program
{
    public static void Main(string[] args)
    {
        string xmlPath = @"XMLFile.xml";
        string excelPath = @"ExcelFile.xlsx";

        XDocument xmlDocument = XDocument.Load(xmlPath);
        Console.WriteLine("XML файл загружен!");
        
        //получение моделей пользователей из XML документа
        var users = xmlDocument.Root.Elements()
            .Select(element =>
            {
                var elements = element.Elements().ToArray();
                string fio = GetFio(elements[0].Value);
                DateTime birthDate = GetBirthDate(elements[1].Value);
                string number = GetUserNumber(elements[2].Value);
                int rating = GetUserRating(elements[3].Value);

                //Console.WriteLine(fio);

                return new User
                {
                    UserFIO = fio,
                    UserBirthDate = birthDate,
                    UserNumber = number,
                    UserRating = rating
                };
            });
        Console.WriteLine($"Загружено пользователей: {users.Count()}");
    }

    

    static public string GetFio(string xmlFio)
    {
        //Console.WriteLine($"XML фио: {xmlFio}");

        string pattern = @"(?<Surname>\w+) (?<Name>\w+)[\. ]+(?<Ot>\w+\.?)";
        var str = Regex.Match(xmlFio, pattern);
        
        if (str.Success)
        {
            string surname = str.Groups["Surname"].Value;
            string name = str.Groups["Name"].Value;
            string ot = str.Groups["Ot"].Value;

            //формат: Фамилия И. О.
            string res = $"{surname[0].ToString().ToUpper()}{string.Join("", surname.Skip(1))} {name[0].ToString().ToUpper()}. {ot[0].ToString().ToUpper()}.";

            //Console.WriteLine($"\tИтоговое фио: {res}");
            return res;
        }

        //Console.WriteLine("\tНеверный формат");
        return "NULL";
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