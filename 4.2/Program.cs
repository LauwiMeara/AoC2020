using System;
using System.IO;

namespace _4._2
{
    class Program
    {
        static void Main()
        {
            string[] input = File.ReadAllText("input.txt").Split("\r\n\r\n", StringSplitOptions.RemoveEmptyEntries);

            Console.WriteLine("The number of valid passports is: {0}.", CountValidPassports(input));
        }

        static int CountValidPassports(string[] input)
        {
            int counter = 0;

            // Check if passport contains all required fields
            for (int i = 0; i < input.Length; i++)
            {
                string[] requiredFields = { "byr:", "iyr:", "eyr:", "hgt:", "hcl:", "ecl:", "pid:" };

                bool isValid = true;

                foreach (string field in requiredFields)
                {
                    isValid = input[i].Contains(field);

                    if (!isValid)
                    {
                        break;
                    }
                }

                // Validate fields
                char[] separators = { ' ', '\r', '\n' };
                string[] passport = input[i].Split(separators);

                for (int j = 0; j < passport.Length; j++)
                {
                    if (isValid)
                    {
                        if (passport[j].Contains("byr:"))
                        {
                            isValid = ValidateBirthYear(passport, j, isValid);
                        }

                        else if (passport[j].Contains("iyr:"))
                        {
                            isValid = ValidateIssueYear(passport, j, isValid);
                        }

                        else if (passport[j].Contains("eyr:"))
                        {
                            isValid = ValidateExpirationYear(passport, j, isValid);
                        }

                        else if (passport[j].Contains("hgt:"))
                        {
                            isValid = ValidateHeight(passport, j, isValid);
                        }

                        else if (passport[j].Contains("hcl:"))
                        {
                            isValid = ValidateHairColor(passport, j, isValid);
                        }

                        else if (passport[j].Contains("ecl:"))
                        {
                            isValid = ValidateEyeColor(passport, j, isValid);
                        }

                        else if (passport[j].Contains("pid:"))
                        {
                            isValid = ValidatePassportId(passport, j, isValid);
                        }
                    }
                }

                if (isValid)
                {
                    counter++;
                }
            }

            return counter;
        }

        static bool ValidateBirthYear(string[] passport, int j, bool isValid)
        {
            // byr(Birth Year) - four digits; at least 1920 and at most 2002.
            int birthYear = int.Parse(passport[j].Substring(4));

            if (birthYear < 1920 || birthYear > 2002)
            {
                isValid = false;
            }

            return isValid;
        }

        static bool ValidateIssueYear(string[] passport, int j, bool isValid)
        {
            // iyr(Issue Year) - four digits; at least 2010 and at most 2020.
            int issueYear = int.Parse(passport[j].Substring(4));

            if (issueYear < 2010 || issueYear > 2020)
            {
                isValid = false;
            }

            return isValid;
        }

        static bool ValidateExpirationYear(string[] passport, int j, bool isValid)
        {
            // eyr (Expiration Year) - four digits; at least 2020 and at most 2030.
            int expirationYear = int.Parse(passport[j].Substring(4));

            if (expirationYear < 2020 || expirationYear > 2030)
            {
                isValid = false;
            }

            return isValid;
        }

        static bool ValidateHeight(string[] passport, int j, bool isValid)
        {
            // hgt(Height) - a number followed by either cm or in:
            string height = passport[j].Substring(4);

            if (height.IndexOf('c') == -1 && height.IndexOf('i') == -1)
            {
                isValid = false;
            }

            // If cm, the number must be at least 150 and at most 193.
            else if (passport[j].Contains("cm"))
            {
                int heightInCm = int.Parse(passport[j].Substring(4, passport[j].IndexOf('c') - 4));

                if (heightInCm < 150 || heightInCm > 193)
                {
                    isValid = false;
                }
            }

            // If in, the number must be at least 59 and at most 76.
            else if (passport[j].Contains("in"))
            {
                int heightInInch = int.Parse(passport[j].Substring(4, passport[j].IndexOf('i') - 4));

                if (heightInInch < 59 || heightInInch > 76)
                {
                    isValid = false;
                }
            }

            return isValid;
        }

        static bool ValidateHairColor(string[] passport, int j, bool isValid)
        {
            // hcl(Hair Color) - a # followed by exactly six characters 0-9 or a-f.
            char[] validCharacters = { '0', '1', '2', '3' , '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };

            string hairColor = passport[j].Substring(5);

            foreach  (char color in hairColor)
            {
                if (Array.IndexOf(validCharacters,  color) == -1)
                {
                    isValid = false;
                    break;
                }
            }

            if (passport[j][4] != '#' || hairColor.Length != 6)
            {
                isValid = false;
            }

            return isValid;
        }

        static bool ValidateEyeColor(string[] passport, int j, bool isValid)
        {
            // ecl(Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
            string eyeColor = passport[j].Substring(4);

            string[] validEyeColors = { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

            if (Array.IndexOf(validEyeColors, eyeColor) == -1)
            {
                isValid = false;
            }

            return isValid;
        }

        static bool ValidatePassportId(string[] passport, int j, bool isValid)
        {
            // pid(Passport ID) - a nine - digit number, including leading zeroes
            string passportId = passport[j].Substring(4);

            bool isNumber = int.TryParse(passportId, out int digits);

            if (!isNumber || passportId.Length != 9)
            {
                isValid = false;
            }

            return isValid;
        }
    }
}
