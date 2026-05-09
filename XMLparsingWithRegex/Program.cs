

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
                string birthDate = GetBirthDate(elements[1].Value);
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

    static public string GetBirthDate(string xmlBirthDate)
    {
        //Формат возвращающей даты: дд.мм.гггг

        //Console.WriteLine($"XML дата: {xmlBirthDate}");
        string pattern1 = @"(?<day>\d{2}).(?<month>\d{2}).(?<year>\d{4})";
        var str1 = Regex.Match(xmlBirthDate, pattern1);
        if (str1.Success)
        {
            if (!DateTime.TryParse(str1.Groups[0].Value, out DateTime _))
            {
                //Console.WriteLine("\tНеверная дата");
                return "NULL";
            }
            Console.WriteLine($"\tДата: {str1.Groups[0].Value}");
            return str1.Groups[0].Value;
        }

        var months = new Dictionary<string, string>();
        months["января"] = "01";
        months["февраля"] = "02";
        months["марта"] = "03";
        months["апреля"] = "04";
        months["мая"] = "05";
        months["июня"] = "06";
        months["июля"] = "07";
        months["августа"] = "08";
        months["сентября"] = "09";
        months["октября"] = "10";
        months["ноября"] = "11";
        months["декабря"] = "12";

        string pattern2 = @"(?<day>\d{2}) (?<month>\w+) (?<year>\d{4})";
        var str2 = Regex.Match(xmlBirthDate, pattern2);

        if (str2.Success)
        {
            string day = str2.Groups["day"].Value;
            string month = months[str2.Groups["month"].Value.ToLower()];
            string year = str2.Groups["year"].Value;

            string resDate = $"{day}.{month}.{year}";

            //Console.WriteLine($"\tДата: {resDate}");
            return resDate;
        }
        //Console.WriteLine("\tДата не распознана");
        return "NULL";
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