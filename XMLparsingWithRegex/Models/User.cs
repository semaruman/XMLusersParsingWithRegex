using System;
using System.Collections.Generic;
using System.Text;

namespace XMLparsingWithRegex.Models
{
    public class User
    {
        public string UserFIO {  get; set; } //Фамилия И.О.
        public string UserBirthDate { get; set; } //дд.мм.гггг
        public string UserNumber { get; set; } //+7(nnn)nnn-nn-nn

        public int UserRating { get; set; } //n
    }
}
